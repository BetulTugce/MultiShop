namespace MultiShop.Catalog.Services
{
    public class FileService
    {
        // IWebHostEnvironment, web uygulamasının barındırıldığı ortama ilişkin bilgileri sağlayan bir arabirim. Dosya sistemine erişimde gerekli yol belirlemeleri vb işlemler için kullanılıyor..
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GetFilePath(string fileName, string directoryName)
        {
            // Dosyanın kaydedileceği yol belirleniyor
            string directoryPath = Path.Combine(_environment.WebRootPath, directoryName); // Dosyanın kaydedileceği dizin, web kök dizini (wwwroot) altındaki parametrede verilen dizine ayarlanıyor..

            // Dizinin var olup olmadığını kontrol eder..
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Dizin yoksa oluşturur..
            }

            return Path.Combine(directoryPath, fileName); // Dosya yolu, dosya adı ve parametredeki dizinle birleştirilerek döndürülüyor..
        }

        public string GenerateRandomFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);// Orjinal dosya adının uzantısı alınıyor..
            var randomName = Guid.NewGuid().ToString();
            // Yeni dosya adı, orjinal dosya adının uzantısız hali, rastgele sayı ve uzantıyla birleştirilir ve döndürülüyor..
            return $"{Path.GetFileNameWithoutExtension(originalFileName)}_{randomName}{extension}";
        }

        public void DeleteImage(string fileName, string directoryName)
        {
            // Silinecek dosyanın yolu, web kök dizini altındaki parametrede verilen dizin ve dosya adıyla birleştiriliyor..
            var filePath = Path.Combine(_environment.WebRootPath, directoryName, fileName);
            if (System.IO.File.Exists(filePath))
            {// Dosya varsa, dosyayı siliyor..
                System.IO.File.Delete(filePath);
            }
        }

        public async Task<byte[]> GetImageAsync(string fileName, string directoryName)
        {
            // Dosya yolu, web kök dizini altındaki parametredeki dizin ve dosya adıyla birleştiriliyor..
            var filePath = Path.Combine(_environment.WebRootPath, directoryName, fileName);
            // Dosya okunarak bir byte dizisi olarak geriye döndürülüyor..
            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return imageBytes;
        }
    }
}
