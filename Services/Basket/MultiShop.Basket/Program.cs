using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services.Abstract;
using MultiShop.Basket.Services.Concrete;
using MultiShop.Basket.Settings;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Bir yetkilendirme politikas� olu�turur. Burada, sadece kimli�i do�rulanm�� kullan�c�lar�n eri�imine izin verilir (Proje seviyesinde authentication)..
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// sub claim'ini, varsay�lan claim t�rleri e�le�me listesinden kald�r�r. Bu, JWT token'�ndaki sub claim'inin, varsay�lan claim e�lemesi taraf�ndan i�lenmesini engeller..
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

// Add services to the container.

// JWT bazl� kimlik do�rulama yap�land�rmas� kullan�l�yor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Token�n ge�erli oldu�u kaynak ad�
	opt.Audience = "ResourceBasket";

	/* Geli�tirme ortam�nda HTTPS zorunlulu�unu devre d��� b�rakmak i�in false olarak ayarlanabilir yani, IdentityServer� geli�tirme ortam�nda aya�a kald�r�rken https://localhost:5001 �eklinde https zorunlu olmas�n isteniyorsa bu �zellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor(); // HttpContexte eri�im i�in..
builder.Services.AddScoped<ILoginService, LoginService>(); // Scoped ile her HTTP iste�i ba��na bir �rnek olu�turur..
builder.Services.AddScoped<IBasketService, BasketService>();
// RedisSettings konfig�rasyon ayarlar�n� appsettings.json dosyas�ndan okur.
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
// Singleton ile t�m uygulama boyunca ayn� nesnenin kullan�lmas� sa�lan�yor yani tekrar tekrar redis ba�lant�s� olu�turmak yerine mevcut ba�lant� �zerinden i�lemleri ger�ekle�tiriyor..
builder.Services.AddSingleton<RedisService>(sp =>
{
	// appsettings.jsondaki RedisSettings alan�ndaki konfig�rasyon ayarlar�n� al�r..
	var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
	var redis = new RedisService(redisSettings.Host, redisSettings.Port);
	redis.Connect(); // Redis sunucusuna ba�lant�y� ba�lat�r (Ba�lant� kurma i�lemi RedisService s�n�f�nda tan�mland�).
	return redis; // RedisService �rne�ini d�nd�rerek DI konteyn�ra kaydedilerek uygulama boyunca kullan�l�r..
});

builder.Services.AddControllers(opt =>
{
	// Yetkilendirme filtresi ekleniyor.. T�m eylemler i�in, kimli�i do�rulanm�� kullan�c�lar�n eri�imini zorunlu hale getirir..
	opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Kimlik do�rulama ve yetkilendirme middlewarelar�..
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
