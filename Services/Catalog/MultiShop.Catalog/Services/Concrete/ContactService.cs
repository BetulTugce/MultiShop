using AutoMapper;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            var contactMessage = _mapper.Map<Contact>(createContactDto);
            await _contactRepository.CreateAsync(contactMessage);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _contactRepository.DeleteAsync(id);
        }

        public async Task<List<ResultContactDto>> GetAllContactsAsync()
        {
            var contactMessages = await _contactRepository.GetAllAsync();
            return _mapper.Map<List<ResultContactDto>>(contactMessages);
        }

        public async Task<GetByIdContactDto> GetContactByIdAsync(string id)
        {
            var contactMessage = await _contactRepository.GetByIdAsync(id);
            return _mapper.Map<GetByIdContactDto>(contactMessage);
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var contactMessage = _mapper.Map<Contact>(updateContactDto);
            await _contactRepository.UpdateAsync(contactMessage);
        }
    }
}
