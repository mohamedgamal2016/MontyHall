using Microsoft.AspNetCore.Mvc;

namespace MontyHall.Models.Responses
{
    public class ErrorResponse : ProblemDetails
    {
        public ErrorResponse(string errorString, int httpStatusCode, string reason)
        {
            Status = httpStatusCode;
            Detail = errorString;
            Title = reason;
        }
    }
}
