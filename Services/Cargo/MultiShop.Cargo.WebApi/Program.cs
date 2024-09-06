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

// JWT bazl� kimlik do�rulama yap�land�rmas� kullan�l�yor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Token�n ge�erli oldu�u kaynak ad�
	opt.Audience = "ResourceCargo";

	/* Geli�tirme ortam�nda HTTPS zorunlulu�unu devre d��� b�rakmak i�in false olarak ayarlanabilir yani, IdentityServer� geli�tirme ortam�nda aya�a kald�r�rken https://localhost:5001 �eklinde https zorunlu olmas�n isteniyorsa bu �zellik false verilebilir.. */
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
