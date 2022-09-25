using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MontyHall.Middleware
{
    public class UnhandledExceptionCatchingMiddleware
    {
        private readonly ILogger<UnhandledExceptionCatchingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public UnhandledExceptionCatchingMiddleware(RequestDelegate next, ILogger<UnhandledExceptionCatchingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, $"An unhandled exception occurred. Trace Identifier: {context.TraceIdentifier}");

            try
            {
                context.Response.StatusCode = GetstatusCode(ex);
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var errorPayload = ex.InnerException?.GetType() == typeof(ValidationException)
                                    ? GetValidationProblemDetails(context, ex)
                                    : GetProblemDetails(context, ex);

                return context.Response.WriteAsync(JsonConvert.SerializeObject(errorPayload), Encoding.UTF8);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "There was an exception creating the error payload response.");
            }

            return Task.CompletedTask;
        }

        private int GetstatusCode(Exception ex)
        {
            if (ex.InnerException?.GetType() == typeof(ValidationException))
            {
                return StatusCodes.Status400BadRequest;
            }

            return StatusCodes.Status500InternalServerError;
        }

        private ProblemDetails GetProblemDetails(HttpContext context, Exception ex)
        {
            return new ProblemDetails
            {
                Type = ex.GetType().Name,
                Detail = $"The following unhandled exception was occurred whilst processing a request: {ex.Message}",
                Status = StatusCodes.Status500InternalServerError,
                Title = "An exception occurred whilst processing a request."
            };
        }

         private ProblemDetails GetValidationProblemDetails(HttpContext context, Exception ex)
        {
            var validationException = ex.InnerException as ValidationException;
            ModelStateDictionary errors = new ModelStateDictionary();
            if (validationException != null)
            {
                foreach (var error in validationException.Errors.GroupBy(x => x.PropertyName).Select(x => x.First()))
                {
                    errors.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            var validationProblemDetails = new ValidationProblemDetails(errors)
            {
                Type = ex.GetType().Name,
                Detail = "The following validation errors occurred whilst processing a request",
                Status = StatusCodes.Status400BadRequest,
                Title = "An exception occurred whilst processing a request."
            };
            return validationProblemDetails;
        }
    }
}
