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
			_connectionString = _configuration.GetConnectionString("DefaultConnection");
		}

		#region Veritabanı Bağlantısı Yapılandırılması
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server=your_server_name;Database=MultiShopDiscountDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True;");
		//}
		#endregion

		public DbSet<Coupon> Coupons { get; set; }
		public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			// ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan ef core propertysidir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.
			foreach (var entry in ChangeTracker.Entries<Coupon>())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreatedDate = DateTime.UtcNow;
				}

				if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdatedDate = DateTime.UtcNow;
				}
			}

			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}
