using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoDtos
{
	public class UpdateCargoDto
	{
		public Guid Id { get; set; }

		public string Sender { get; set; }
		public string Receiver { get; set; }

		public int TrackingNumber { get; set; } // Takip Numarası

		public DateTime ShippingDate { get; set; }

		public Guid CargoCompanyId { get; set; } // Foreign Key
		//public CargoCompany CargoCompany { get; set; } // Kargo Şirketi
	}
}
