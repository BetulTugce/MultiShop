using MultiShop.Catalog.Dtos.AboutDtos;

namespace MultiShop.Catalog.Services.Abstractions
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAboutsAsync();
        Task CreateAboutAsync(CreateAboutDto createAboutDto);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task DeleteAboutAsync(string id);
        Task<GetByIdAboutDto> GetAboutByIdAsync(string id);
        Task DeleteAllAsync();
    }
}
