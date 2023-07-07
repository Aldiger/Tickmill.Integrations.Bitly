using Tickmill.Integrations.Bitly.Core.Dto;
using Tickmill.Integrations.Bitly.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Tickmill.Common.Types;

namespace Tickmill.Integrations.Bitly.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortenUrlController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public ShortenUrlController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("generate")]
        public async Task<ActionResult<ShortenUrlDto>> Generate([FromQuery] GetShortenUrl query, CancellationToken token)
        {
            return Ok(await _dispatcher.QueryAsync(query, token));
        }
    }
}
