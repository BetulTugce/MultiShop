// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
	public static class Config
	{
		// ApiResources özelliği, mikroservisler için kaynakları tanımlar. Bu kaynaklar, API'lere erişim için gerekli olan izinleri belirler.
		public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
		{
            // ResourceCatalog adında bir API kaynağı oluşturulur..
            new ApiResource("ResourceCatalog")
			{
                // ResourceCatalog API kaynağına sahip olan kullanıcılar, bu kapsamları yani scopeları kullanarak katalog operasyonlarını gerçekleştirebilir.
                // Özetle, ResourceCatalog ismindeki keye sahip olan bir mikroservis kullanıcısı CatalogFullPermission işlemini gerçekleştirebilecek..
                Scopes={"CatalogFullPermission", "CatalogReadPermission" }
			},

			new ApiResource("ResourceDiscount")
			{
				Scopes={"DiscountFullPermission"}
			},

			new ApiResource("ResourceOrder")
			{
				Scopes={"OrderFullPermission"}
			},
			
			new ApiResource("ResourceCargo")
			{
				Scopes={"CargoFullPermission"}
			},

			new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

		};

		// IdentityResources özelliği, kimlik doğrulama işlemlerinde token alan kullanıcıların hangi bilgilere erişim sağlayacağını tanımlar. Örneğin, kullanıcı bilgilerini içeren profiller ve e-postalar gibi.
		public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Email(),
			new IdentityResources.Profile()
		};

		// ApiScopes özelliği, her bir API kaynağı için belirlenen kapsamları tanımlar.. Bu scopelar, belirli işlemleri gerçekleştirmek için gerekli yetkilere sahip olur.
		public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
		{
			new ApiScope("CatalogFullPermission", "Full authority for catalog operations"),
			new ApiScope("CatalogReadPermission", "Reading authority for catalog operations"),
			new ApiScope("DiscountFullPermission", "Full authority for discount operations"),
			new ApiScope("OrderFullPermission", "Full authority for order operations"),
			new ApiScope("CargoFullPermission", "Full authority for cargo operations"),
			new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
		};

		/*IdentityServer'da çeşitli roller (Visitor, Manager, Admin) için istemcileri (clients) tanımlıyor. Her istemci, farklı yetkilere (scopes) sahip ve kimlik doğrulama işlemleri için farklı erişim izinleri veriliyor. Her bir istemciye özel izinler ve yaşam süresi ayarları yapılmıştır. */
		public static IEnumerable<Client> Clients => new Client[]
		{
            // Visitor rolü için istemci tanımlanıyor..

            new Client()
			{
				// İstemcinin kimliği, kimlik doğrulama işlemlerinde kullanılır.
				ClientId = "MultiShopVisitorId",
				ClientName = "Multi Shop Visitor User",
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				// Parola niteliğinde olduğu için hashi saklanır.. Doğrularken de aynı şekilde hashi alınarak veriler karşılaştırılır..
				ClientSecrets = {new Secret("multishopsecret".Sha256())},
				// Bu istemcinin erişebileceği yani yetkili olduğu izinleri (scopes) belirtir..
				AllowedScopes= {"CatalogReadPermission"}
			},

			// Manager
			new Client()
			{
				ClientId = "MultiShopManagerId",
				ClientName = "Multi Shop Manager User",
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				ClientSecrets = {new Secret("multishopsecret".Sha256())},
				AllowedScopes= { "CatalogFullPermission", "DiscountFullPermission" }
			},

			// Admin
			new Client()
			{
				ClientId = "MultiShopAdminId",
				ClientName = "Multi Shop Admin User",
				AllowedGrantTypes = GrantTypes.ClientCredentials,
				ClientSecrets = {new Secret("multishopsecret".Sha256())},
				AllowedScopes= {"CatalogReadPermission", "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermission", "CargoFullPermission",
				IdentityServerConstants.LocalApi.ScopeName, // IdentityServer projesindeki yerel API controllerlarına erişim yetkisi..
				IdentityServerConstants.StandardScopes.Email,
				IdentityServerConstants.StandardScopes.OpenId,
				IdentityServerConstants.StandardScopes.Profile
				},
				// Access token'ın ömrü..
				AccessTokenLifetime = 600 //10dk.
			},

		};
	}
}