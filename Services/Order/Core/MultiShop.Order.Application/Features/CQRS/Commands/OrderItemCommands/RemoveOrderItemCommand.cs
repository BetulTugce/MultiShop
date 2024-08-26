using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderItemCommands
{
	public class RemoveOrderItemCommand
	{
		public int Id { get; set; }

		public RemoveOrderItemCommand(int id)
		{
			Id = id;
		}
	}
}
