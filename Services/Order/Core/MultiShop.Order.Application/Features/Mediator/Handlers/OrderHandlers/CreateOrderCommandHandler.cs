using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderCommands;
using MultiShop.Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderHandlers
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
	{
		private readonly IRepository<Domain.Entities.Order> _repository;

		public CreateOrderCommandHandler(IRepository<Domain.Entities.Order> repository)
		{
			_repository = repository;
		}

		public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			await _repository.CreateAsync(new Domain.Entities.Order
			{
				OrderDate = request.OrderDate,
				TotalPrice = request.TotalPrice,
				UserId = request.UserId,
			});
		}
	}
}
