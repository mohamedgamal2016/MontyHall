using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace MontyHall.UnitTests.HelperClasses
{
    public class FakeProblemDetailsFactory : ProblemDetailsFactory
    {
        // ReSharper disable once OptionalParameterHierarchyMismatch - R# flags statusCode as having the wrong default value
        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null)
        {
            var problemDetails = new ProblemDetails
            {
                Detail = detail,
                Instance = instance,
                Status = statusCode,
                Title = title,
                Type = type
            };

            var traceId = httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            return problemDetails;
        }

        // ReSharper disable once OptionalParameterHierarchyMismatch - R# flags statusCode as having the wrong default value
        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null)
        {
            return new ValidationProblemDetails
            {
                Detail = detail,
                Instance = instance,
                Status = statusCode,
                Title = title,
                Type = type
            };
        }
    }
}
