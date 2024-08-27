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
	public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, List<GetOrdersQueryResult>>
	{
		private readonly IRepository<Domain.Entities.Order> _repository;

		public GetOrderQueryHandler(IRepository<Domain.Entities.Order> repository)
		{
			_repository = repository;
		}

		public async Task<List<GetOrdersQueryResult>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
		{
			var values = await _repository.GetAllAsync();
			return values.Select(x => new GetOrdersQueryResult
			{
				Id = x.Id,
				OrderDate = x.OrderDate,
				TotalPrice = x.TotalPrice,
				UserId = x.UserId
			}).ToList();
		}
	}
}
