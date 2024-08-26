using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderItemCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderItemHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderItemQueries;

namespace MultiShop.Order.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderItemsController : ControllerBase
	{
		private readonly GetOrderItemsQueryHandler _getOrderItemsQueryHandler;
		private readonly GetOrderItemByIdQueryHandler _getOrderItemByIdQueryHandler;
		private readonly CreateOrderItemCommandHandler _createOrderItemCommandHandler;
		private readonly UpdateOrderItemCommandHandler _updateOrderItemCommandHandler;
		private readonly RemoveOrderItemCommandHandler _removeOrderItemCommandHandler;

		public OrderItemsController(GetOrderItemsQueryHandler getOrderItemsQueryHandler, GetOrderItemByIdQueryHandler getOrderItemByIdQueryHandler, CreateOrderItemCommandHandler createOrderItemCommandHandler, UpdateOrderItemCommandHandler updateOrderItemCommandHandler, RemoveOrderItemCommandHandler removeOrderItemCommandHandler)
		{
			_getOrderItemsQueryHandler = getOrderItemsQueryHandler;
			_getOrderItemByIdQueryHandler = getOrderItemByIdQueryHandler;
			_createOrderItemCommandHandler = createOrderItemCommandHandler;
			_updateOrderItemCommandHandler = updateOrderItemCommandHandler;
			_removeOrderItemCommandHandler = removeOrderItemCommandHandler;
		}

		[HttpGet]
		public async Task<IActionResult> GetOrderItems()
		{
			var values = _getOrderItemsQueryHandler.Handle();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderItemById(int id)
		{
			var value = await _getOrderItemByIdQueryHandler.Handle(new GetOrderItemByIdQuery(id));
			return Ok(value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrderItem(CreateOrderItemCommand command)
		{
			await _createOrderItemCommandHandler.Handle(command);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemCommand command)
		{
			await _updateOrderItemCommandHandler.Handle(command);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveOrderItem(int id)
		{
			await _removeOrderItemCommandHandler.Handle(new RemoveOrderItemCommand(id));
			return NoContent();
		}
	}
}
