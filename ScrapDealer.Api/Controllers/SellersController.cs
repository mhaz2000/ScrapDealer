using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public SellersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("State")]
        public async Task<ActionResult<bool>> State()
        {
            var result = await _queryDispatcher.QueryAsync(new GetSellerStateQuery(UserId));
            return OkOrNotFound(result);
        }
    }
}