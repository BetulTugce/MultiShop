using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Identity.Login;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginCreateVM createVM)
        {
            var client = _httpClientFactory.CreateClient();
            // Gelen createVM modeli Json formatına dönüştürülüyor..
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(createVM), Encoding.UTF8, "application/json");
            // İlgili endpointe post isteği gönderiliyor..
            var response = await client.PostAsync("https://localhost:5001/api/Auth/SignIn", stringContent);
            if (response.IsSuccessStatusCode)
            {// İstek başarılıysa..
                // APIdan dönen responseun içeriği okunuyor..
                var jsonData = await response.Content.ReadAsStringAsync();
                // json stringi JwtResponseModel deserialize ediliyor..
                var tokenModel = System.Text.Json.JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                // tokenModel null değilse..
                if (tokenModel != null)
                {
                    // JWT tokenını çözmek için JwtSecurityTokenHandler kullanılıyor..
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    // Token okunarak içindeki claimler alınıyor..
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    // tokenModeldeki Token boş değilse..
                    if(tokenModel.Token is not null)
                    {
                        // Token, claimler arasına multishoptoken adıyla ekleniyor..
                        claims.Add(new Claim("multishoptoken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate, // Tokenın bitiş tarihi
                            IsPersistent = true, // Oturum kalıcı olsun mu? (kalıcı => tarayıcı kapatıldığında oturum kapanmaz)
                        };

                        // HttpContext üzerinde kullanıcıya oturum açtırılıyor..
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        return RedirectToAction("Index", "Default");
                    }
                }
            }
            return View();
        }
    }
}
