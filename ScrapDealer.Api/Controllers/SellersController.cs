﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrapDealer.Application.Commands.Buyers;
using ScrapDealer.Application.Commands.Sellers;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Buyers;
using ScrapDealer.Application.Queries.Sellers;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

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

        [HttpGet("Profile")]
        public async Task<ActionResult<SellerProfileDto>> GetProfile()
        {
            var result = await _queryDispatcher.QueryAsync(new GetSellerProfileQuery(UserId));
            return OkOrNotFound(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/Get")]
        public async Task<ActionResult<PaginatedResult<SellerProfileDto>>> Get([FromQuery] GetSellersQuery query)
        {
            var result = await _queryDispatcher.QueryAsync(query);
            return OkOrNotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSellerCommand command)
        {
            await _commandDispatcher.DispatchAsync(command with { UserId = UserId });
            return BaseOk();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Admin/Verify/{id}")]
        public async Task<IActionResult> Verfiy(Guid id)
        {
            await _commandDispatcher.DispatchAsync(new VerifySellerCommand(id));
            return BaseOk();
        }
    }
}