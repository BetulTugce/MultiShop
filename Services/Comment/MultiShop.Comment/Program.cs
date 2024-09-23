using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Abstract;
using MultiShop.Comment.Concrete;
using MultiShop.Comment.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT bazlý kimlik doðrulama yapýlandýrmasý kullanýlýyor..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];

    // Tokenýn geçerli olduðu kaynak adý
    opt.Audience = "ResourceComment";

    /* Geliþtirme ortamýnda HTTPS zorunluluðunu devre dýþý býrakmak için false olarak ayarlanabilir yani, IdentityServerý geliþtirme ortamýnda ayaða kaldýrýrken https://localhost:5001 þeklinde https zorunlu olmasýn isteniyorsa bu özellik false verilebilir.. */
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserCommentService, UserCommentService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
