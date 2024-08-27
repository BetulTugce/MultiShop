using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderResults;
using MultiShop.Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderHandlers
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResult>
	{
		private readonly IRepository<Domain.Entities.Order> _repository;

		public GetOrderByIdQueryHandler(IRepository<Domain.Entities.Order> repository)
		{
			_repository = repository;
		}

		public async Task<GetOrderByIdQueryResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var value = await _repository.GetByIdAsync(request.Id);
			return new GetOrderByIdQueryResult
			{
				Id = value.Id,
				OrderDate = value.OrderDate,
				TotalPrice = value.TotalPrice,
				UserId = value.UserId
			};
		}
	}
}
