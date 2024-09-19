using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Mapping;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;
using MultiShop.Catalog.Services.Concrete;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT bazl� kimlik do�rulama yap�land�rmas� kullan�l�yor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Token�n ge�erli oldu�u kaynak ad�
	opt.Audience = "ResourceCatalog";

	/* Geli�tirme ortam�nda HTTPS zorunlulu�unu devre d��� b�rakmak i�in false olarak ayarlanabilir yani, IdentityServer� geli�tirme ortam�nda aya�a kald�r�rken https://localhost:5001 �eklinde https zorunlu olmas�n isteniyorsa bu �zellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

// Yetkilendirme yap�land�rmas�
builder.Services.AddAuthorization(options =>
{
	// "CatalogReadOrFullPermission" ad�nda bir yetkilendirme politikas� tan�mlan�yor..
	options.AddPolicy("CatalogReadOrFullPermission", policy =>
	{
		// Tokenda "scope" claiminde "CatalogReadPermission" veya "CatalogFullPermission" de�erlerine sahip olma gereklili�i..
		policy.RequireClaim("scope", "CatalogReadPermission", "CatalogFullPermission");
	});

	// "CatalogFullPermission" adl� bir policy tan�mlan�yor..
	options.AddPolicy("CatalogFullPermission", policy =>
	{
		// Tokenda "scope" claiminde sadece "CatalogFullPermission" de�erine sahip olma gereklili�i
		policy.RequireClaim("scope", "CatalogFullPermission");
	});
});
// AutoMapper ekleniyor ve mevcut assemblydeki profiller yani mapping konfig�rasyonlar� otomatik olarak y�klenir..
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Repository ve serviceler DI containera ekleniyor..
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IFeatureSliderRepository, FeatureSliderRepository>();
builder.Services.AddScoped<ISpecialOfferRepository, SpecialOfferRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();

// appsettings.json dosyas�ndaki "DatabaseSettings" adl� k�s�m, veritaban� ile ilgili yap�land�rma ayarlar�n� i�eriyor. appsettings.json dosyas�nda bu alan�n keyini kullanarak DatabaseSettings adl� s�n�fa ba�l�yor..
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

//DatabaseSettings s�n�f�ndaki de�erlere ula�may� sa�lar..
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
	// IDatabaseSettings talep edildi�inde DI sisteminden DatabaseSettings nesnesi al�n�r..
	return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddControllers();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
