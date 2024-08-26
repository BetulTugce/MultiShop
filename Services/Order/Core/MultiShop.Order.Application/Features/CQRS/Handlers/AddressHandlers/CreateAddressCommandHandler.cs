using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
	public class CreateAddressCommandHandler
	{
		private readonly IRepository<Address> _repository;

		public CreateAddressCommandHandler(IRepository<Address> repository)
		{
			_repository = repository;
		}

		public async Task Handle(CreateAddressCommand createAddressCommand)
		{
			await _repository.CreateAsync(new Address
			{
				City = createAddressCommand.City,
				Detail1 = createAddressCommand.Detail1,
				Detail2 = createAddressCommand.Detail2,
				Country = createAddressCommand.Country,
				Description = createAddressCommand.Description,
				District = createAddressCommand.District,
				Email = createAddressCommand.Email,
				Phone = createAddressCommand.Phone,
				Name = createAddressCommand.Name,
				Surname = createAddressCommand.Surname,
				UserId = createAddressCommand.UserId,
				ZipCode = createAddressCommand.ZipCode,
			});
		}
	}
}
