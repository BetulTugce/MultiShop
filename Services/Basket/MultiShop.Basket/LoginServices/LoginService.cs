using System.Security.Claims;

namespace MultiShop.Basket.LoginServices
{
	public class LoginService : ILoginService
	{
		// HttpContext'e erişim sağlamak için..
		private readonly IHttpContextAccessor _contextAccessor;

		public LoginService(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		//public string GetUserId
		//{
		//	get
		//	{
		//		var httpContext = _contextAccessor.HttpContext;

		//		if (httpContext?.User == null)
		//		{
		//			throw new InvalidOperationException("User is not authenticated.");
		//		}

		//		var userId = httpContext.User.FindFirst("sub")?.Value;

		//		if (string.IsNullOrEmpty(userId))
		//		{
		//			throw new InvalidOperationException("User ID claim is not available.");
		//		}

		//		return userId;
		//	}
		//}


		//public string GetUserId => _contextAccessor.HttpContext.User.FindFirst("sub").Value;

		// userid bilgisini HttpContext üzerinden alıyor..
		public string GetUserId => _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}
