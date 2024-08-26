using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
	public class GetAddressesQueryHandler
	{
		private readonly IRepository<Address> _repository;

		public GetAddressesQueryHandler(IRepository<Address> repository)
		{
			_repository = repository;
		}

		public async Task<List<GetAddressesQueryResult>> Handle()
		{
			var values = await _repository.GetAllAsync();
			return values.Select(x => new GetAddressesQueryResult
			{
				City = x.City,
				Detail1 = x.Detail1,
				District = x.District,
				Id = x.Id,
				UserId = x.UserId,

			}).ToList();
		}
	}
}
