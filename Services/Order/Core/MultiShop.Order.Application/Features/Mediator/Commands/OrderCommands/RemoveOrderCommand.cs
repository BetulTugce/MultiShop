using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderCommands
{
	public class RemoveOrderCommand : IRequest
	{
        public int Id { get; set; }

		public RemoveOrderCommand(int id)
		{
			Id = id;
		}
    }
}
