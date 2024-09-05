﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
	public class CargoCompany
	{
        public Guid Id { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Cargo> Cargos { get; set; } // Navigation Property
	}
}
