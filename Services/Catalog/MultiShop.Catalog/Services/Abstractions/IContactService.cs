using MultiShop.Catalog.Dtos.ContactDtos;

namespace MultiShop.Catalog.Services.Abstractions
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactsAsync();
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task UpdateContactAsync(UpdateContactDto updateContactDto);
        Task DeleteContactAsync(string id);
        Task<GetByIdContactDto> GetContactByIdAsync(string id);
    }
}
