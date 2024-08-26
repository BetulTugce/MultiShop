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
	public class CreateOrderItemCommandHandler
	{
		private readonly IRepository<OrderItem> _repository;

		public CreateOrderItemCommandHandler(IRepository<OrderItem> repository)
		{
			_repository = repository;
		}

		public async Task Handle(CreateOrderItemCommand command)
		{
			await _repository.CreateAsync(new OrderItem
			{
				ProductAmount = command.ProductAmount,
				OrderId = command.OrderId,
				ProductId = command.ProductId,
				ProductName = command.ProductName,
				ProductPrice = command.ProductPrice,
				ProductTotalPrice = (command.ProductAmount * command.ProductPrice)
			});
		}
	}
}
