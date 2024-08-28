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
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
	{
		private readonly IRepository<Domain.Entities.Order> _repository;

		public UpdateOrderCommandHandler(IRepository<Domain.Entities.Order> repository)
		{
			_repository = repository;
		}

		public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var value = await _repository.GetByIdAsync(request.Id);
			value.OrderDate = request.OrderDate;
			value.TotalPrice = request.TotalPrice;
			value.UserId = request.UserId;
			await _repository.UpdateAsync(value);
		}
	}
}
