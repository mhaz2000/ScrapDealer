using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.SaleOrders;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Categories;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOrdersController : BaseController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public SaleOrdersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSaleOrderCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateSaleOrderCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { Id = id });
            return BaseOk();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commandDispatcher.DispatchAsync(new DeleteSaleOrderCommand(id));
            return BaseOk();
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<SaleOrderDto>>> Get([FromQuery] GetSaleOrdersQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpGet("MySaleOrder")]
        public async Task<ActionResult<PaginatedResult<SaleOrderDto>>> GetMySaleOrder()
        {
            var result = await _queryDispatcher.QueryAsync(new GetMySaleOrdersQuery(UserId));
            return OkOrNotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleOrderDto>> GetSaleOrder(Guid id)
        {
            var result = await _queryDispatcher.QueryAsync(new GetSaleOrderQuery(id));
            return OkOrNotFound(result);
        }
    }
}
