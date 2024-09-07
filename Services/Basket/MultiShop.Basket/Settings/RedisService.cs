using StackExchange.Redis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MultiShop.Basket.Settings
{
	public class RedisService
	{
		// Bağlantı işlemleri sırasında ihtiyaç duyulan parametreler (Redis sunucusunun host ve port bilgilerini tutuyorlar)..
		private readonly string _host;
		private readonly int _port;

		// Redis bağlantısını yönetecek nesne
		// ConnectionMultiplexer sınıfı, Redis ile yapılan tüm işlemleri yöneten ve sunucuyla iletişimi sürdüren ana bileşendir.
		private ConnectionMultiplexer _multiplexer;

		public RedisService(string host, int port)
		{
			_host = host;
			_port = port;
		}

		// Redis sunucusuna bağlanma işlemi gerçekleştirir..
		public void Connect()=> _multiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

		// Redis veritabanına erişim sağlar (Rediste birden fazla veritabanı olabilir (0'dan 15'e). Varsayılan olarak db 0 kullanılır)..
		public IDatabase GetDb(int db = 1) => _multiplexer.GetDatabase(0);
		//public IDatabase GetDb(int db = 1) => _multiplexer.GetDatabase(db);
	}
}
