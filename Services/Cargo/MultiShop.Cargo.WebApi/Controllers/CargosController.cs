using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CargosController : ControllerBase
	{
		private readonly ICargoService _cargoService;

		public CargosController(ICargoService cargoService)
		{
			_cargoService = cargoService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCargos()
		{
			var entities = await _cargoService.GetAllAsync();
			return Ok(entities.Select(x => new CargoDto
			{
				Id = x.Id,
				CargoCompanyId = x.CargoCompanyId,
				Receiver = x.Receiver,
				Sender = x.Sender,
				ShippingDate = x.ShippingDate,
				TrackingNumber = x.TrackingNumber
			}).ToList());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCargoById(Guid id)
		{
			var entity = await _cargoService.GetByIdAsync(id);
			if (entity is null)
			{
				return NotFound();
			}
			return Ok(new CargoDto { Id = entity.Id, Receiver = entity.Receiver, Sender = entity.Sender, ShippingDate = entity.ShippingDate, TrackingNumber = entity.TrackingNumber, CargoCompanyId = entity.CargoCompanyId });
		}

		[HttpPost]
		public async Task<IActionResult> CreateCargo(CreateCargoDto dto)
		{
			await _cargoService.InsertAsync(new MultiShop.Cargo.EntityLayer.Concrete.Cargo { Receiver = dto.Receiver, Sender = dto.Sender, ShippingDate = dto.ShippingDate, TrackingNumber = dto.TrackingNumber, CargoCompanyId = dto.CargoCompanyId });
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPut]
		public IActionResult UpdateCargo(UpdateCargoDto dto)
		{
			_cargoService.Update(new MultiShop.Cargo.EntityLayer.Concrete.Cargo { Id = dto.Id, CargoCompanyId = dto.CargoCompanyId, Receiver = dto.Receiver, Sender = dto.Sender, ShippingDate = dto.ShippingDate, TrackingNumber = dto.TrackingNumber });
			return Ok();
		}

		[HttpDelete]
		public IActionResult RemoveCargo(Guid id)
		{
			try
			{
				_cargoService.Remove(id);
				return NoContent();
			}
			catch (ArgumentNullException)
			{
				return NotFound();
			}
		}
	}
}
