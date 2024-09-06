using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoMovementDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CargoMovementsController : ControllerBase
	{
		private readonly ICargoMovementService _cargoMovementService;

		public CargoMovementsController(ICargoMovementService cargoMovementService)
		{
			_cargoMovementService = cargoMovementService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCargoMovements()
		{
			var entities = await _cargoMovementService.GetAllAsync();
			return Ok(entities.Select(x => new CargoMovementDto
			{
				Id = x.Id,
				CargoId = x.CargoId,
				Description = x.Description,
				Location = x.Location,
				MovementDate = x.MovementDate,
				Status = x.Status
			}).ToList());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCargoMovementById(Guid id)
		{
			var entity = await _cargoMovementService.GetByIdAsync(id);
			if (entity is null)
			{
				return NotFound();
			}
			return Ok(new CargoMovementDto { Id = entity.Id, Status = entity.Status, MovementDate = entity.MovementDate, Location = entity.Location, Description = entity.Description, CargoId = entity.CargoId });
		}

		[HttpPost]
		public async Task<IActionResult> CreateCargoMovement(CreateCargoMovementDto dto)
		{
			await _cargoMovementService.InsertAsync(new CargoMovement { CargoId = dto.CargoId, Location = dto.Location, MovementDate = dto.MovementDate, Status = dto.Status, Description = dto.Description});
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPut]
		public IActionResult UpdateCargoMovement(UpdateCargoMovementDto dto)
		{
			_cargoMovementService.Update(new CargoMovement { Id = dto.Id, CargoId = dto.CargoId, Description = dto.Description, Location = dto.Location, MovementDate = dto.MovementDate, Status = dto.Status });
			return Ok();
		}

		[HttpDelete]
		public IActionResult RemoveCargoMovement(Guid id)
		{
			_cargoMovementService.Remove(id);
			return NoContent();
		}
	}
}
