using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

		public AuthController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
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
	}
}
