using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderItemHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Persistence.Contexts;
using MultiShop.Order.Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<GetAddressesQueryHandler>();
builder.Services.AddScoped<GetAddressByIdQueryHandler>();
builder.Services.AddScoped<CreateAddressCommandHandler>();
builder.Services.AddScoped<UpdateAddressCommandHandler>();
builder.Services.AddScoped<RemoveAddressCommandHandler>();

builder.Services.AddScoped<GetOrderItemsQueryHandler>();
builder.Services.AddScoped<GetOrderItemByIdQueryHandler>();
builder.Services.AddScoped<CreateOrderItemCommandHandler>();
builder.Services.AddScoped<UpdateOrderItemCommandHandler>();
builder.Services.AddScoped<RemoveOrderItemCommandHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Veritabani Baglantisi için Konfigürasyon Ayarlari
// OrderContext sinifi DI containera ekleniyor..
builder.Services.AddDbContext<OrderContext>(options =>
{
	// ConnectionString, appsettings.json dosyasindan aliniyor..
	options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServerConnection"), opt =>
	{
		// EfCoreun migration dosyalarini saklayacagi assembly yani proje belirtiliyor..
		opt.MigrationsAssembly(Assembly.GetAssembly(typeof(OrderContext)).GetName().Name);
	});
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
