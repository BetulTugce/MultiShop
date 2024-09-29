using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous] // SignUp üzerindeki yetkilendirmeyi kaldırır. Yani kimlik doğrulama kontrolünü engeller..
        [HttpPost("[action]")]
        public async Task<IActionResult> SignUp(UserRegisterDto userRegisterDto)
        {
            var newUser = new ApplicationUser()
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Username,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname
            };
            var results = await _userManager.CreateAsync(newUser, userRegisterDto.Password);
            if (results.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            var errors = results.Errors.Select(e => e.Description).ToList();
            return BadRequest(errors);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            // isPersistent parametresi, kullanıcı tarayıcıyı kapatıp açtığında oturumunun açık kalıp kalmayacağını belirler. true olursa, oturum tarayıcı kapatıldıktan sonra da açık kalır. false olursa, oturum yalnızca tarayıcı açıkken geçerli olur. Tarayıcı kapatıldığında oturum sona erer.
            // lockoutOnFailure parametresi, kullanıcının belirli sayıda başarısız giriş denemesinden sonra kilitlenip kilitlenmeyeceğini belirler., true olursa, yanlış şifre ya da kullanıcı adı girilirse bu girişim bir deneme olarak kaydedilir ve belli bir deneme sayısından sonra kullanıcı hesabı kilitlenir. false olursa, başarısız denemeler bir kilitlenmeye yol açmaz
            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, true);
            if (result.Succeeded)
            {
                return Ok();
            }

            // Kullanıcı kilitlenmiş mi?
            if (result.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status423Locked, new { Message = "Hesabınız kilitlendi, lütfen daha sonra tekrar deneyin." });
            }

            return Unauthorized(new { Message = "Geçersiz kullanıcı adı veya şifre" });
        }
    }
}
