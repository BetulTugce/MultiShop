namespace MultiShop.Catalog.Dtos.CategoryDtos
{
	public class CreateCategoryDto
	{
		// İleride yeni bir prop ekleme ihtimalimiz bulunuyor. Bu yüzden tek bir prop içermesine rağmen sınıf oluşturmak doğru bir yaklaşım.
		public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
