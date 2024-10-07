namespace MultiShop.WebUI.Models.ViewModels.Identity.Login
{
    public class JwtResponseModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
