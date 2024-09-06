using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT bazlý kimlik doðrulama yapýlandýrmasý kullanýlýyor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Tokenýn geçerli olduðu kaynak adý
	opt.Audience = "ResourceCargo";

	/* Geliþtirme ortamýnda HTTPS zorunluluðunu devre dýþý býrakmak için false olarak ayarlanabilir yani, IdentityServerý geliþtirme ortamýnda ayaða kaldýrýrken https://localhost:5001 þeklinde https zorunlu olmasýn isteniyorsa bu özellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
builder.Services.AddScoped<ICargoDal, EfCargoDal>();
builder.Services.AddScoped<ICargoMovementDal, EfCargoMovementDal>();
builder.Services.AddScoped<ICargoCompanyService, CargoCompanyManager>();
builder.Services.AddScoped<ICargoService, CargoManager>();
builder.Services.AddScoped<ICargoMovementService, CargoMovementManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CargoContext sinifi DI containera ekleniyor..
builder.Services.AddDbContext<CargoContext>(options =>
{
	// ConnectionString, appsettings.json dosyasindan aliniyor..
	options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServerConnection"), opt =>
	{
		// EfCoreun migration dosyalarini saklayacagi assembly yani proje belirtiliyor..
		opt.MigrationsAssembly(Assembly.GetAssembly(typeof(CargoContext)).GetName().Name);
	});
});

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
