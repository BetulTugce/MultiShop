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
	public class UpdateAddressCommandHandler
	{
		private readonly IRepository<Address> _repository;

		public UpdateAddressCommandHandler(IRepository<Address> repository)
		{
			_repository = repository;
		}

		public async Task Handle(UpdateAddressCommand command)
		{
			var value = await _repository.GetByIdAsync(command.Id);
			value.Name = command.Name;
			value.Surname = command.Surname;
			value.Phone = command.Phone;
			value.Email = command.Email;
			value.UserId = command.UserId;
			value.City = command.City;
			value.Country = command.Country;
			value.ZipCode = command.ZipCode;
			value.Detail1 = command.Detail1;
			value.Detail2 = command.Detail2;
			value.District = command.District;
			value.Description = command.Description;
			await _repository.UpdateAsync(value);
		}
	}
}
