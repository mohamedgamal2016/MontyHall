using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MontyHall.Core.Common.Commands;

namespace MontyHall.Factories
{
    public interface IResponseFactory
    {
        IActionResult FromResult<T>(HttpContext httpContext, CommandResult<T> commandResult);
    }
}
