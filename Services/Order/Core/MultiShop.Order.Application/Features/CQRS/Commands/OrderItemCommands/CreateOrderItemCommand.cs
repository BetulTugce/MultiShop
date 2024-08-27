﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderItemCommands
{
	public class CreateOrderItemCommand
	{
		public string ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		//public decimal ProductTotalPrice { get; set; }
		public int ProductAmount { get; set; }

		public int OrderId { get; set; }
	}
}