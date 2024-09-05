using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.Concrete
{
	public class CargoContext : DbContext
	{
		public CargoContext(DbContextOptions<CargoContext> options) : base(options)
		{
		}

        public DbSet<MultiShop.Cargo.EntityLayer.Concrete.Cargo> Cargos { get; set; }
        public DbSet<CargoCompany> CargoCompanies { get; set; }
        public DbSet<CargoMovement> CargoMovements { get; set; }
    }
}
