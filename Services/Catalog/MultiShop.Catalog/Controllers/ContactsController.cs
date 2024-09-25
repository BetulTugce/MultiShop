using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Controllers
{
    [Authorize(Policy = "CatalogFullPermission")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var values = await _contactService.GetAllContactsAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(string id)
        {
            var value = await _contactService.GetContactByIdAsync(id);
            return Ok(value);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _contactService.CreateContactAsync(createContactDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _contactService.UpdateContactAsync(updateContactDto);
            return Ok();
        }
    }
}
