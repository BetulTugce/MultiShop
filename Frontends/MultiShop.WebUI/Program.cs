using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// JWTyi temel alan bir kimlik doðrulama mekanizmasý tanýmlanýyor.. Cookie bazlý kimlik doðrulama da ekleniyor ancak þema JWT.
// JWT cookie ile beraber çalýþacak. JWT kullanýlarak kimlik doðrulamasý yapýlacak ve bir cookie oluþturulacak..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
	opt.LoginPath = "/Login/Index/"; // Kullanýcý login olmadan Auth gerektiren bir sayfaya eriþmeye çalýþtýðýnda yönlendirileceði url..
	opt.LogoutPath = "/Login/LogOut/"; // Kullanýcý çýkýþ yapmak istediðinde..
	opt.AccessDeniedPath = "/Pages/AccessDenied/"; // Kullanýcý yetkisi olmadýðý bir sayfaya eriþmeye çalýþtýðýnda..
	opt.Cookie.HttpOnly = true; // Cookie yalnýzca http istekleriyle gönderilecek..
	opt.Cookie.SameSite = SameSiteMode.Strict; // 3.parti sitelerden gelen isteklerle cookienin gönderilmesini engeller. Bu, CSRFe karþý koruma..

    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Cookienin yalnýzca güvenli HTTPS baðlantýlarý üzerinden gönderilmesini saðlar
	opt.Cookie.Name = "MultiShopJwt"; // Cookienin ismi
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
