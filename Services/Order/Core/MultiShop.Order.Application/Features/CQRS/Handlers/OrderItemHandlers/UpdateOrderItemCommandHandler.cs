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
	public class UpdateOrderItemCommandHandler
	{
		private readonly IRepository<OrderItem> _repository;

		public UpdateOrderItemCommandHandler(IRepository<OrderItem> repository)
		{
			_repository = repository;
		}

		public async Task Handle(UpdateOrderItemCommand command)
		{
			var value = await _repository.GetByIdAsync(command.Id);
			value.OrderId = command.OrderId;
			value.ProductId = command.ProductId;
			value.ProductPrice = command.ProductPrice;
			value.ProductName = command.ProductName;
			value.ProductTotalPrice = (command.ProductAmount * command.ProductPrice);
			value.ProductAmount = command.ProductAmount;
			await _repository.UpdateAsync(value);
		}
	}
}
