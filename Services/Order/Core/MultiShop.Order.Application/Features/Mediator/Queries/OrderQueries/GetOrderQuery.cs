using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderQueries
{
	public class GetOrderQuery : IRequest<List<GetOrdersQueryResult>>
	{
		//IRequest : MediatR kütüphanesi içerisinde yer alan bir interface.
		//Mediatorda bir merkezi sistem var ve merkezi sistem oluştuğunda kaotik bağımlılık ortadan kalkar.
		// Normalde her bir handlerı controller tarafında tek tek readonly olarak field oluşturup onu ctorda DI containerdan talep etmekle ve program.csde registration olarak eklemek yerine mediatR bunu otomatik olarak yapacak.
		//Ancak burada her bir entity için neyin istek olduğunu ve neyin bu isteğin yanıtı olduğunu belirtmemiz gerek.
	}
}
