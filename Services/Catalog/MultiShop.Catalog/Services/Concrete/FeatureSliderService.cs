using AutoMapper;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IFeatureSliderRepository _featureSliderRepository;
        private readonly IMapper _mapper;

        public FeatureSliderService(IFeatureSliderRepository featureSliderRepository, IMapper mapper)
        {
            _featureSliderRepository = featureSliderRepository;
            _mapper = mapper;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            var slider = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
            await _featureSliderRepository.CreateAsync(slider);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _featureSliderRepository.DeleteAsync(id);
        }

        public Task FeatureSliderChangeStatus(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSlidersAsync()
        {
            var sliders = await _featureSliderRepository.GetAllAsync();
            return _mapper.Map<List<ResultFeatureSliderDto>>(sliders);
        }

        public async Task<GetByIdFeatureSliderDto> GetFeatureSliderByIdAsync(string id)
        {
            var slider = await _featureSliderRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdFeatureSliderDto>(slider);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            var slider = _mapper.Map<FeatureSlider>(updateFeatureSliderDto);
            await _featureSliderRepository.UpdateAsync(slider);
        }
    }
}
