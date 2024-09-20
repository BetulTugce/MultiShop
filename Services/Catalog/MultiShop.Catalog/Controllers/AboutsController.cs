using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Services.Abstractions;
using MultiShop.Catalog.Services.Concrete;

namespace MultiShop.Catalog.Controllers
{
    [Authorize(Policy = "CatalogFullPermission")]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutsController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAbouts()
        {
            var values = await _aboutService.GetAllAboutsAsync();
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            var value = await _aboutService.GetAboutByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAllAbouts()
        {
            await _aboutService.DeleteAllAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return Ok();
        }
    }
}
