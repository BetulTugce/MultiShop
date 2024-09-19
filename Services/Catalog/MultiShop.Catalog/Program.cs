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

// JWT bazlý kimlik doðrulama yapýlandýrmasý kullanýlýyor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Tokenýn geçerli olduðu kaynak adý
	opt.Audience = "ResourceCatalog";

	/* Geliþtirme ortamýnda HTTPS zorunluluðunu devre dýþý býrakmak için false olarak ayarlanabilir yani, IdentityServerý geliþtirme ortamýnda ayaða kaldýrýrken https://localhost:5001 þeklinde https zorunlu olmasýn isteniyorsa bu özellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

// Yetkilendirme yapýlandýrmasý
builder.Services.AddAuthorization(options =>
{
	// "CatalogReadOrFullPermission" adýnda bir yetkilendirme politikasý tanýmlanýyor..
	options.AddPolicy("CatalogReadOrFullPermission", policy =>
	{
		// Tokenda "scope" claiminde "CatalogReadPermission" veya "CatalogFullPermission" deðerlerine sahip olma gerekliliði..
		policy.RequireClaim("scope", "CatalogReadPermission", "CatalogFullPermission");
	});

	// "CatalogFullPermission" adlý bir policy tanýmlanýyor..
	options.AddPolicy("CatalogFullPermission", policy =>
	{
		// Tokenda "scope" claiminde sadece "CatalogFullPermission" deðerine sahip olma gerekliliði
		policy.RequireClaim("scope", "CatalogFullPermission");
	});
});
// AutoMapper ekleniyor ve mevcut assemblydeki profiller yani mapping konfigürasyonlarý otomatik olarak yüklenir..
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

// appsettings.json dosyasýndaki "DatabaseSettings" adlý kýsým, veritabaný ile ilgili yapýlandýrma ayarlarýný içeriyor. appsettings.json dosyasýnda bu alanýn keyini kullanarak DatabaseSettings adlý sýnýfa baðlýyor..
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

//DatabaseSettings sýnýfýndaki deðerlere ulaþmayý saðlar..
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
	// IDatabaseSettings talep edildiðinde DI sisteminden DatabaseSettings nesnesi alýnýr..
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
