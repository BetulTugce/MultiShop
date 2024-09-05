using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;

namespace MultiShop.Order.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AddressesController : ControllerBase
	{
		private readonly GetAddressesQueryHandler _getAddressesQueryHandler;
		private readonly GetAddressByIdQueryHandler _getAddressByIdQueryHandler;
		private readonly CreateAddressCommandHandler _createAddressCommandHandler;
		private readonly UpdateAddressCommandHandler _updateAddressCommandHandler;
		private readonly RemoveAddressCommandHandler _removeAddressCommandHandler;

		public AddressesController(GetAddressesQueryHandler getAddressesQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler, CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler, RemoveAddressCommandHandler removeAddressCommandHandler)
		{
			_getAddressesQueryHandler = getAddressesQueryHandler;
			_getAddressByIdQueryHandler = getAddressByIdQueryHandler;
			_createAddressCommandHandler = createAddressCommandHandler;
			_updateAddressCommandHandler = updateAddressCommandHandler;
			_removeAddressCommandHandler = removeAddressCommandHandler;
		}

		[HttpGet]
		public async Task<IActionResult> GetAddresses()
		{
			var values = await _getAddressesQueryHandler.Handle();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAddressById(int id)
		{
			var value = await _getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
			return Ok(value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
		{
			await _createAddressCommandHandler.Handle(command);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
		{
			await _updateAddressCommandHandler.Handle(command);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveAddress(int id)
		{
			await _removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
			return NoContent();
		}
	}
}
