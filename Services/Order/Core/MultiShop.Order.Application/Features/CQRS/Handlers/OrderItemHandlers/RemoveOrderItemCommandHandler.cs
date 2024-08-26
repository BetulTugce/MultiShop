using MultiShop.Order.Application.Features.CQRS.Commands.OrderItemCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderItemHandlers
{
	public class RemoveOrderItemCommandHandler
	{
		private readonly IRepository<OrderItem> _repository;

		public RemoveOrderItemCommandHandler(IRepository<OrderItem> repository)
		{
			_repository = repository;
		}

		public async Task Handle(RemoveOrderItemCommand command)
		{
			var value = await _repository.GetByIdAsync(command.Id);
			await _repository.DeleteAsync(value);
		}
	}
}
