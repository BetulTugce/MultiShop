using AutoMapper;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;
using Feature = MultiShop.Catalog.Entities.Feature;

namespace MultiShop.Catalog.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;

        public FeatureService(IFeatureRepository featureRepository, IMapper mapper)
        {
            _featureRepository = featureRepository;
            _mapper = mapper;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            var feature = _mapper.Map<Feature>(createFeatureDto);
            await _featureRepository.CreateAsync(feature);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _featureRepository.DeleteAsync(id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeaturesAsync()
        {
            var features = await _featureRepository.GetAllAsync();
            return _mapper.Map<List<ResultFeatureDto>>(features);
        }

        public async Task<GetByIdFeatureDto> GetFeatureByIdAsync(string id)
        {
            var feature = await _featureRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdFeatureDto>(feature);
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            var feature = _mapper.Map<Feature>(updateFeatureDto);
            await _featureRepository.UpdateAsync(feature);
        }
    }
}
