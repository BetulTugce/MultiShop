
using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Services.Abstractions
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDto>> GetAllFeaturesAsync();
        Task CreateFeatureAsync(CreateFeatureDto createFeatureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto);
        Task DeleteFeatureAsync(string id);
        Task<GetByIdFeatureDto> GetFeatureByIdAsync(string id);
    }
}
