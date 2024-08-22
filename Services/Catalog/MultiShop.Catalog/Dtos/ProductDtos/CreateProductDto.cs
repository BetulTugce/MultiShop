﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Dtos.ProductDtos
{
	public class CreateProductDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }

		public string CategoryId { get; set; }
	}
}
