using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Order.Application.Services;
using MultiShop.Order.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT bazl� kimlik do�rulama yap�land�rmas� kullan�l�yor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.Authority = builder.Configuration["IdentityServerUrl"];

	// Token�n ge�erli oldu�u kaynak ad�
	opt.Audience = "ResourceOrder";

	/* Geli�tirme ortam�nda HTTPS zorunlulu�unu devre d��� b�rakmak i�in false olarak ayarlanabilir yani, IdentityServer� geli�tirme ortam�nda aya�a kald�r�rken https://localhost:5001 �eklinde https zorunlu olmas�n isteniyorsa bu �zellik false verilebilir.. */
	opt.RequireHttpsMetadata = false;
});

builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

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
