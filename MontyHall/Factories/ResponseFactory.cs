using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MontyHall.Core.Common.Commands;
using MontyHall.Models.Responses;
using System;
using System.Collections.Generic;

namespace MontyHall.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public IActionResult FromResult<T>(HttpContext httpContext, CommandResult<T> commandResult)
        {
            switch (commandResult.ResultType)
            {
                case ResultType.Ok:
                case ResultType.Partial:
                    return new OkObjectResult(commandResult.Data);
                case ResultType.NotFound:
                    return GetErrorObjectResult(httpContext, StatusCodes.Status404NotFound, commandResult.Errors);
                case ResultType.Unexpected:
                    return GetErrorObjectResult(httpContext, StatusCodes.Status500InternalServerError, commandResult.Errors);
                case ResultType.Invalid:
                    return GetErrorObjectResult(httpContext, StatusCodes.Status400BadRequest, commandResult.Errors);
                case ResultType.Conflict:
                    return GetErrorObjectResult(httpContext, StatusCodes.Status409Conflict, commandResult.Errors);
                case ResultType.Cancelled:
                    return GetErrorObjectResult(httpContext, 499, commandResult.Errors);
                default:
                    throw new ArgumentOutOfRangeException(nameof(commandResult.ResultType));
            }
        }

        private static ObjectResult GetErrorObjectResult(
            HttpContext httpContext,
            int httpStatusCode,
            IEnumerable<string> errors)
        {
            var errorString = string.Join(",", errors);
            var errorResponse =
                new ErrorResponse(
                    errorString,
                    httpStatusCode,
                    ReasonPhrases.GetReasonPhrase(httpStatusCode));

            return new BadRequestObjectResult(errorResponse);
        }
    }
}
