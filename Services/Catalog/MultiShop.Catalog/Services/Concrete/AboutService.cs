using AutoMapper;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IMapper _mapper;

        public AboutService(IAboutRepository aboutRepository, IMapper mapper)
        {
            _aboutRepository = aboutRepository;
            _mapper = mapper;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var About = _mapper.Map<About>(createAboutDto);
            await _aboutRepository.CreateAsync(About);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _aboutRepository.DeleteAsync(id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutsAsync()
        {
            var Abouts = await _aboutRepository.GetAllAsync();
            return _mapper.Map<List<ResultAboutDto>>(Abouts);
        }

        public async Task<GetByIdAboutDto> GetAboutByIdAsync(string id)
        {
            var About = await _aboutRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdAboutDto>(About);
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var About = _mapper.Map<About>(updateAboutDto);
            await _aboutRepository.UpdateAsync(About);
        }
    }
}
