using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Controllers
{
    [Authorize(Policy = "CatalogFullPermission")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _FeatureService;

        public FeaturesController(IFeatureService FeatureService)
        {
            _FeatureService = FeatureService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetFeatures()
        {
            var values = await _FeatureService.GetAllFeaturesAsync();
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(string id)
        {
            var value = await _FeatureService.GetFeatureByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _FeatureService.CreateFeatureAsync(createFeatureDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _FeatureService.DeleteFeatureAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _FeatureService.UpdateFeatureAsync(updateFeatureDto);
            return Ok();
        }
    }
}
