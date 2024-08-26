using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderItemQueries
{
	public class GetOrderItemByIdQuery
	{
		public int Id { get; set; }
		public GetOrderItemByIdQuery(int id)
		{
			Id = id;
		}
	}
}
