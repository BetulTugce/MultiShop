using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Contexts;
using MultiShop.Discount.Services.Abstractions;
using MultiShop.Discount.Services.Concrete;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT bazlý kimlik doðrulama yapýlandýrmasý kullanýlýyor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Tokenýn geçerli olduðu kaynak adý
	opt.Audience = "ResourceDiscount";

	/* Geliþtirme ortamýnda HTTPS zorunluluðunu devre dýþý býrakmak için false olarak ayarlanabilir yani, IdentityServerý geliþtirme ortamýnda ayaða kaldýrýrken https://localhost:5001 þeklinde https zorunlu olmasýn isteniyorsa bu özellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

//builder.Services.AddTransient<DapperContext>();

//Service DI containera ekleniyor..
builder.Services.AddTransient<IDiscountService, DiscountService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
