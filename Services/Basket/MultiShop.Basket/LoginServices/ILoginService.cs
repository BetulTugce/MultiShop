namespace MultiShop.Basket.LoginServices
{
	public interface ILoginService
	{
		// Kullanıcının idsini (userId) alacak..
		public string GetUserId { get; } // Sadece userIdye erişilecek atama yapılmayacağı için sete ihtiyaç yok.
    }
}
