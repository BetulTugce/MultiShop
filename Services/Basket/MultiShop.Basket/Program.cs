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

// Bir yetkilendirme politikasý oluþturur. Burada, sadece kimliði doðrulanmýþ kullanýcýlarýn eriþimine izin verilir (Proje seviyesinde authentication)..
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// sub claim'ini, varsayýlan claim türleri eþleþme listesinden kaldýrýr. Bu, JWT token'ýndaki sub claim'inin, varsayýlan claim eþlemesi tarafýndan iþlenmesini engeller..
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

// Add services to the container.

// JWT bazlý kimlik doðrulama yapýlandýrmasý kullanýlýyor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Tokenýn geçerli olduðu kaynak adý
	opt.Audience = "ResourceBasket";

	/* Geliþtirme ortamýnda HTTPS zorunluluðunu devre dýþý býrakmak için false olarak ayarlanabilir yani, IdentityServerý geliþtirme ortamýnda ayaða kaldýrýrken https://localhost:5001 þeklinde https zorunlu olmasýn isteniyorsa bu özellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor(); // HttpContexte eriþim için..
builder.Services.AddScoped<ILoginService, LoginService>(); // Scoped ile her HTTP isteði baþýna bir örnek oluþturur..
builder.Services.AddScoped<IBasketService, BasketService>();
// RedisSettings konfigürasyon ayarlarýný appsettings.json dosyasýndan okur.
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
// Singleton ile tüm uygulama boyunca ayný nesnenin kullanýlmasý saðlanýyor yani tekrar tekrar redis baðlantýsý oluþturmak yerine mevcut baðlantý üzerinden iþlemleri gerçekleþtiriyor..
builder.Services.AddSingleton<RedisService>(sp =>
{
	// appsettings.jsondaki RedisSettings alanýndaki konfigürasyon ayarlarýný alýr..
	var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
	var redis = new RedisService(redisSettings.Host, redisSettings.Port);
	redis.Connect(); // Redis sunucusuna baðlantýyý baþlatýr (Baðlantý kurma iþlemi RedisService sýnýfýnda tanýmlandý).
	return redis; // RedisService örneðini döndürerek DI konteynýra kaydedilerek uygulama boyunca kullanýlýr..
});

builder.Services.AddControllers(opt =>
{
	// Yetkilendirme filtresi ekleniyor.. Tüm eylemler için, kimliði doðrulanmýþ kullanýcýlarýn eriþimini zorunlu hale getirir..
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

// Kimlik doðrulama ve yetkilendirme middlewarelarý..
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
