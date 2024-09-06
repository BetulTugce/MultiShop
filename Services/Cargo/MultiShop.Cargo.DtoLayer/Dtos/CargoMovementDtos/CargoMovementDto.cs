using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoMovementDtos
{
	public class CargoMovementDto
	{
		public Guid Id { get; set; }

		public DateTime MovementDate { get; set; }
		public string Location { get; set; }
		public string Status { get; set; }
		public string Description { get; set; }

		public Guid CargoId { get; set; } // Foreign Key
		//public Cargo Cargo { get; set; } // Kargo
	}
}
