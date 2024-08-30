using MultiShop.Order.Application.Features.CQRS.Queries.OrderItemQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderItemResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderItemHandlers
{
	public class GetOrderItemByIdQueryHandler
	{
		private readonly IRepository<OrderItem> _repository;

		public GetOrderItemByIdQueryHandler(IRepository<OrderItem> repository)
		{
			_repository = repository;
		}

		public async Task<GetOrderItemByIdQueryResult> Handle(GetOrderItemByIdQuery query)
		{
			var value = await _repository.GetByIdAsync(query.Id);
			if (value == null)
			{
				return null;
			}
			return new GetOrderItemByIdQueryResult
			{
				Id = value.Id,
				ProductAmount = value.ProductAmount,
				ProductId = value.ProductId,
				ProductName = value.ProductName,
				OrderId = value.OrderId,
				ProductPrice = value.ProductPrice,
				ProductTotalPrice = value.ProductTotalPrice
			};
		}
	}
}
