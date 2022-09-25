using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MontyHall.Core.Common.Commands;
using System;
using System.Collections.Generic;

namespace MontyHall.Common.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static ActionResult FromResult<T>(this ControllerBase controller, CommandResult<T> commandResult)
        {
            Guard.Against.Null(controller, nameof(controller));
            Guard.Against.Null(commandResult, nameof(commandResult));

            switch (commandResult.ResultType)
            {
                case ResultType.Ok:
                case ResultType.Partial:
                    return controller.Ok(commandResult.Data);
                case ResultType.NotFound:
                    return GetErrorObjectResult(controller, StatusCodes.Status404NotFound, commandResult.Errors);
                case ResultType.Unexpected:
                    return GetErrorObjectResult(controller, StatusCodes.Status500InternalServerError, commandResult.Errors);
                case ResultType.Invalid:
                    return GetErrorObjectResult(controller, StatusCodes.Status400BadRequest, commandResult.Errors);
                default:
                    throw new ArgumentOutOfRangeException(nameof(commandResult.ResultType));
            }
        }

        private static ObjectResult GetErrorObjectResult(
            this ControllerBase controller,
            int httpStatusCode,
            List<string> errors)
        {
            Guard.Against.Null(errors, nameof(errors));

            var errorString = string.Join(",", errors);
            var proplemDetails = new ProblemDetails {
                Detail = errorString,
                Status = httpStatusCode,
                Title = ReasonPhrases.GetReasonPhrase(httpStatusCode)
            };

            return new ObjectResult(proplemDetails);
        }
    }
}
