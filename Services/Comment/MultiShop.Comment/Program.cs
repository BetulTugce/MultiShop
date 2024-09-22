using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CommentContext sinifi DI containera ekleniyor..
builder.Services.AddDbContext<CommentContext>(options =>
{
    // ConnectionString, appsettings.json dosyasindan aliniyor..
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLServerConnection"), opt =>
    {
        // EfCoreun migration dosyalarini saklayacagi assembly yani proje belirtiliyor..
        opt.MigrationsAssembly(Assembly.GetAssembly(typeof(CommentContext)).GetName().Name);
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
