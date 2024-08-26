using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Results.AddressResults
{
	// Address sınıfındaki propertyleri tutarak listelenmesini sağlar..
	public class GetAddressesQueryResult
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string District { get; set; }
		public string City { get; set; }
		public string Detail1 { get; set; }
	}
}
