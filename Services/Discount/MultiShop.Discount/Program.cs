using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Contexts;
using MultiShop.Discount.Services.Abstractions;
using MultiShop.Discount.Services.Concrete;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT bazl� kimlik do�rulama yap�land�rmas� kullan�l�yor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Token�n ge�erli oldu�u kaynak ad�
	opt.Audience = "ResourceDiscount";

	/* Geli�tirme ortam�nda HTTPS zorunlulu�unu devre d��� b�rakmak i�in false olarak ayarlanabilir yani, IdentityServer� geli�tirme ortam�nda aya�a kald�r�rken https://localhost:5001 �eklinde https zorunlu olmas�n isteniyorsa bu �zellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

//builder.Services.AddTransient<DapperContext>();

//Service DI containera ekleniyor..
builder.Services.AddTransient<IDiscountService, DiscountService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Veritaban� Ba�lant�s� i�in Konfig�rasyon Ayarlar�
// DapperContext s�n�f� DI containera ekleniyor..
builder.Services.AddDbContext<DapperContext>(options =>
{
	// �lgili ConnectionString, appsettings.json dosyas�ndan al�n�yor..
	options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServerConnection"), opt =>
	{
		// EfCoreun migration dosyalar�n� saklayaca�� assembly yani proje belirtiliyor..
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
