using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Services.Abstractions
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSlidersAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task DeleteFeatureSliderAsync(string id);
        Task<GetByIdFeatureSliderDto> GetFeatureSliderByIdAsync(string id);
        Task FeatureSliderChangeStatus(string id);
    }
}
