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
	public class CargoMovementManager : GenericService<CargoMovement>, ICargoMovementService
	{
		private readonly ICargoMovementDal _cargoMovementRepository;

		public CargoMovementManager(ICargoMovementDal cargoMovementRepository) : base(cargoMovementRepository)
		{
			_cargoMovementRepository = cargoMovementRepository;
		}
	}
}
