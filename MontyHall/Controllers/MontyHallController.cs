using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MontyHall.Common.Extensions;
using MontyHall.Core.Commands;
using MontyHall.Core.Common.Commands;
using MontyHall.Models.Request;

namespace MontyHall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class MontyHallController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMapper _mapper;

        public MontyHallController(IMapper mapper, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _mapper = mapper;
        }

        [HttpPost("play")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlayGameAsync([FromBody] PlayGameRequest request)
        {
            var playGameCommand = _mapper.Map<PlayGameCommand>(request);
            var result = await _commandDispatcher.Send(playGameCommand);
            return this.FromResult(result);
        }
    }
}