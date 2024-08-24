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

// AutoMapper ekleniyor ve mevcut assemblydeki profiller yani mapping konfigürasyonlarý otomatik olarak yüklenir..
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Repository ve serviceler DI containera ekleniyor..
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
