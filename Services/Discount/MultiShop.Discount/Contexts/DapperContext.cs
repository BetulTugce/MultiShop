using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MultiShop.Discount.Entities;
using System.Data;

namespace MultiShop.Discount.Contexts
{
	public class DapperContext : DbContext
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration, DbContextOptions<DapperContext> options) : base(options)
		{
			_configuration = configuration;
			_connectionString = _configuration.GetConnectionString("MSSQLServerConnection");
		}

		#region Veritabanı Bağlantısı Yapılandırılması
		//NOT : Program.cse taşındı..
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=your_server_name;Database=MultiShopDiscountDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;");
		//}
		#endregion

		public DbSet<Coupon> Coupons { get; set; }
		public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

		#region CreatedDate ve UpdatedDate Otomatik Güncellemesi
		//public override async task<int> savechangesasync(bool acceptallchangesonsuccess, cancellationtoken cancellationtoken = default)
		//{
		//	// changetracker : entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan ef core propertysidir. update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar.
		//	foreach (var entry in changetracker.entries<coupon>())
		//	{
		//		if (entry.state == entitystate.added)
		//		{
		//			entry.entity.createddate = datetime.utcnow;
		//		}

		//		if (entry.state == entitystate.modified)
		//		{
		//			entry.entity.updateddate = datetime.utcnow;
		//		}
		//	}

		//	return await base.savechangesasync(acceptallchangesonsuccess, cancellationtoken);
		//}
		#endregion
	}
}
