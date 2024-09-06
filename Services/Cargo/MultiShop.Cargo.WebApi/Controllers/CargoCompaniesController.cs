using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CargoCompaniesController : ControllerBase
	{
		private readonly ICargoCompanyService _cargoCompanyService;

		public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
		{
			_cargoCompanyService = cargoCompanyService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCargoCompanies()
		{
			var entities = await _cargoCompanyService.GetAllAsync();
			return Ok(entities.Select(x => new CargoCompanyDto
			{
				Id = x.Id,
				CompanyName = x.CompanyName
			}).ToList());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCargoCompanyById(Guid id)
		{
			var entity = await _cargoCompanyService.GetByIdAsync(id);
			if (entity is null)
			{
				return NotFound();
			}
			return Ok(new CargoCompanyDto { Id = entity.Id, CompanyName = entity.CompanyName });
		}

		[HttpPost]
		public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto dto)
		{
			await _cargoCompanyService.InsertAsync(new CargoCompany { CompanyName = dto.CompanyName });
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpPut]
		public IActionResult UpdateCargoCompany(UpdateCargoCompanyDto dto)
		{
			_cargoCompanyService.Update(new CargoCompany { Id = dto.Id, CompanyName = dto.CompanyName });
			return Ok();
		}

		[HttpDelete]
		public IActionResult RemoveCargoCompany(Guid id)
		{
			_cargoCompanyService.Remove(id);
			return NoContent();
		}
	}
}
