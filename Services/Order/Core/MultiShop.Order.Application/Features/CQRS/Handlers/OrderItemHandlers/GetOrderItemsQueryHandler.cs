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
	public class GetOrderItemsQueryHandler
	{
		private readonly IRepository<OrderItem> _repository;

		public GetOrderItemsQueryHandler(IRepository<OrderItem> repository)
		{
			_repository = repository;
		}

		public async Task<List<GetOrderItemsQueryResult>> Handle()
		{
			var values = await _repository.GetAllAsync();
			return values.Select(x => new GetOrderItemsQueryResult
			{
				Id = x.Id,
				ProductAmount = x.ProductAmount,
				OrderId = x.OrderId,
				ProductId = x.ProductId,
				ProductName = x.ProductName,
				ProductPrice = x.ProductPrice,
				ProductTotalPrice = x.ProductTotalPrice
			}).ToList();
		}
	}
}
