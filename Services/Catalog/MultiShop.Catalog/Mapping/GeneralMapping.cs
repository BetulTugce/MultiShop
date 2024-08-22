﻿using AutoMapper;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Mapping
{
	public class GeneralMapping : Profile
	{
		/* Uygulamanın iç yapısındaki entity modellerini doğrudan istemciye göndermek güvenlik açısından riskli olabilir.
		 * DTO'lar, sadece dışarıya aktarılması gereken min veri setini içerir. Bu sayede, istemciye gereksiz veya hassas bilgilerin gönderilmesi engellenir. Mapping işlemi, entity'deki verilerin seçilen alanlarının DTO'ya aktarılmasını sağlar.Bu, ağ üzerinden veri aktarımını optimize eder ve istemci tarafında veri işleme yükünü azaltır.
		 * AutoMapper'da mapleme işlemi genellikle ctor içerisinde gerçekleştirilir..
		 * Özetle; mapleme işlemi, entitylerden nesne örnekleri oluşturmak yerine entitylerdeki propertyleri dtodaki propertylerle eşleştirir. Entityler üzerindeki olası değişikliklerde api'yın bu durumdan doğrudan etkilenmesini önler. Ayrıca veri güvenliği, katmanlar arası bağımsızlık, performans optimizasyonu gibi artılar sağlar. */

		public GeneralMapping()
		{
			// Entityler ve dtolar arasında çift yönlü bir mapping yapılandırması gerçekleştiriliyor..
			CreateMap<Category, ResultCategoryDto>().ReverseMap();
			CreateMap<Category, CreateCategoryDto>().ReverseMap();
			CreateMap<Category, UpdateCategoryDto>().ReverseMap();
			CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

			CreateMap<Product, ResultProductDto>().ReverseMap();
			CreateMap<Product, CreateProductDto>().ReverseMap();
			CreateMap<Product, UpdateProductDto>().ReverseMap();
			CreateMap<Product, GetByIdProductDto>().ReverseMap();

			CreateMap<ProductDetail, ResultProductDetailDto>().ReverseMap();
			CreateMap<ProductDetail, CreateProductDetailDto>().ReverseMap();
			CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();
			CreateMap<ProductDetail, GetByIdProductDetailDto>().ReverseMap();

			CreateMap<ProductImage, ResultProductImageDto>().ReverseMap();
			CreateMap<ProductImage, CreateProductImageDto>().ReverseMap();
			CreateMap<ProductImage, UpdateProductImageDto>().ReverseMap();
			CreateMap<ProductImage, GetByIdProductImageDto>().ReverseMap();
		}
	}
}
