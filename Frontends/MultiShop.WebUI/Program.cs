using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services.Abstract;
using MultiShop.WebUI.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// JWTyi temel alan bir kimlik do�rulama mekanizmas� tan�mlan�yor.. Cookie bazl� kimlik do�rulama da ekleniyor ancak �ema JWT.
// JWT cookie ile beraber �al��acak. JWT kullan�larak kimlik do�rulamas� yap�lacak ve bir cookie olu�turulacak..
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
	opt.LoginPath = "/Login/Index/"; // Kullan�c� login olmadan Auth gerektiren bir sayfaya eri�meye �al��t���nda y�nlendirilece�i url..
	opt.LogoutPath = "/Login/LogOut/"; // Kullan�c� ��k�� yapmak istedi�inde..
	opt.AccessDeniedPath = "/Pages/AccessDenied/"; // Kullan�c� yetkisi olmad��� bir sayfaya eri�meye �al��t���nda..
	opt.Cookie.HttpOnly = true; // Cookie yaln�zca http istekleriyle g�nderilecek..
	opt.Cookie.SameSite = SameSiteMode.Strict; // 3.parti sitelerden gelen isteklerle cookienin g�nderilmesini engeller. Bu, CSRFe kar�� koruma..

    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Cookienin yaln�zca g�venli HTTPS ba�lant�lar� �zerinden g�nderilmesini sa�lar
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
