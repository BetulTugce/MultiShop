using AutoMapper;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly ISpecialOfferRepository _specialOfferRepository;
        private readonly IMapper _mapper;

        public SpecialOfferService(ISpecialOfferRepository specialOfferRepository, IMapper mapper)
        {
            _specialOfferRepository = specialOfferRepository;
            _mapper = mapper;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var offer = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _specialOfferRepository.CreateAsync(offer);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _specialOfferRepository.DeleteAsync(id);
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOffersAsync()
        {
            var offers = await _specialOfferRepository.GetAllAsync();
            return _mapper.Map<List<ResultSpecialOfferDto>>(offers);
        }

        public async Task<GetByIdSpecialOfferDto> GetSpecialOfferByIdAsync(string id)
        {
            var offer = await _specialOfferRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdSpecialOfferDto>(offer);
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var offer = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
            await _specialOfferRepository.UpdateAsync(offer);
        }
    }
}
