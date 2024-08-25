using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Veritabaný Baðlantýsý için Konfigürasyon Ayarlarý
// DapperContext sýnýfý DI containera ekleniyor..
builder.Services.AddDbContext<DapperContext>(options =>
{
	// Ýlgili ConnectionString, appsettings.json dosyasýndan alýnýyor..
	options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServerConnection"), opt =>
	{
		// EfCoreun migration dosyalarýný saklayacaðý assembly yani proje belirtiliyor..
		opt.MigrationsAssembly(Assembly.GetAssembly(typeof(DapperContext)).GetName().Name);
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
