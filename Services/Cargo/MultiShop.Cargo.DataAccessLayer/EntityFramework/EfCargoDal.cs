using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
	public class EfCargoDal : GenericRepository<MultiShop.Cargo.EntityLayer.Concrete.Cargo>, ICargoDal
	{
		public EfCargoDal(CargoContext context) : base(context)
		{
		}
	}
}
