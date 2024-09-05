using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
	public class CargoManager : GenericService<MultiShop.Cargo.EntityLayer.Concrete.Cargo>, ICargoService
	{
		private readonly ICargoDal _cargoRepository;

		public CargoManager(ICargoDal cargoRepository) : base(cargoRepository)
		{
			_cargoRepository = cargoRepository;
		}
	}
}
