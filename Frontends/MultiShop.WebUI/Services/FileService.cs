namespace MultiShop.WebUI.Services
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
            string directoryPath = Path.Combine(_environment.WebRootPath, directoryName); // Dosyanın kaydedileceği dizin, web kök dizini (wwwroot) altındaki "ProfileImages" dizinine ayarlanıyor..

            // Dizinin var olup olmadığını kontrol eder..
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Dizin yoksa oluşturur..
            }

            return Path.Combine(directoryPath, fileName); // Dosya yolu, dosya adı ve "ProfileImages" diziniyle birleştirilerek döndürülüyor..
        }

        public string GenerateRandomFileName(string originalFileName)
        {
            // Rastgele 4 haneli sayı üretiliyor..
            string randomString = new Random().Next(1000, 9999).ToString();
            string extension = Path.GetExtension(originalFileName);// Orjinal dosya adının uzantısı alınıyor..
            // Yeni dosya adı, orjinal dosya adının uzantısız hali, rastgele sayı ve uzantıyla birleştirilir ve döndürülüyor..
            return $"{Path.GetFileNameWithoutExtension(originalFileName)}_{randomString}{extension}";
        }
    }
}
