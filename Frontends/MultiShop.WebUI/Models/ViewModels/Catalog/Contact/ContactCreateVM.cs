namespace MultiShop.WebUI.Models.ViewModels.Catalog.Contact
{
    public class ContactCreateVM
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
