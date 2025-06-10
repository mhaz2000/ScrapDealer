using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Buyers;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public BuyersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }


        [HttpGet("State")]
        public async Task<ActionResult<bool>> State()
        {
            var result = await _queryDispatcher.QueryAsync(new GetBuyerStateQuery(UserId));
            return OkOrNotFound(result);
        }
        
        [HttpGet("Profile")]
        public async Task<ActionResult<BuyerProfileDto>> GetProfile()
        {
            var result = await _queryDispatcher.QueryAsync(new GetBuyerProfileQuery(UserId));
            return OkOrNotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBuyerCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }
    }
}