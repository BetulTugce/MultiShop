using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderItemHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Persistence.Contexts;
using MultiShop.Order.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) 
        {
			#region Veritabani Baglantisi için Konfigürasyon Ayarlari
			// OrderContext sinifi DI containera ekleniyor..
			services.AddDbContext<OrderContext>(options =>
			{
				// ConnectionString, appsettings.json dosyasindan aliniyor..
				options.UseSqlServer(configuration.GetConnectionString("MSSQLServerConnection"), opt =>
				{
					// EfCoreun migration dosyalarini saklayacagi assembly yani proje belirtiliyor..
					opt.MigrationsAssembly(Assembly.GetAssembly(typeof(OrderContext)).GetName().Name);
				});
			});
			#endregion

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			#region Command ve Query Handlers
			//CQRS handlerları dependency injection containera kaydediliyor..

			services.AddScoped<GetAddressesQueryHandler>();
			services.AddScoped<GetAddressByIdQueryHandler>();
			services.AddScoped<CreateAddressCommandHandler>();
			services.AddScoped<UpdateAddressCommandHandler>();
			services.AddScoped<RemoveAddressCommandHandler>();

			services.AddScoped<GetOrderItemsQueryHandler>();
			services.AddScoped<GetOrderItemByIdQueryHandler>();
			services.AddScoped<CreateOrderItemCommandHandler>();
			services.AddScoped<UpdateOrderItemCommandHandler>();
			services.AddScoped<RemoveOrderItemCommandHandler>();
			#endregion
		}
	}
}
