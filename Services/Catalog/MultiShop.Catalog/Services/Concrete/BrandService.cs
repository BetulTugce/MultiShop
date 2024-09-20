using AutoMapper;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var brand = _mapper.Map<Brand>(createBrandDto);
            await _brandRepository.CreateAsync(brand);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _brandRepository.DeleteAsync(id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return _mapper.Map<List<ResultBrandDto>>(brands);
        }

        public async Task<GetByIdBrandDto> GetBrandByIdAsync(string id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdBrandDto>(brand);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);
            await _brandRepository.UpdateAsync(brand);
        }
    }
}
