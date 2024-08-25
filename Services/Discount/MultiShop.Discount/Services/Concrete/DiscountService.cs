using Dapper;
using MultiShop.Discount.Contexts;
using MultiShop.Discount.Dtos.CouponDtos;
using MultiShop.Discount.Services.Abstractions;

namespace MultiShop.Discount.Services.Concrete
{
	public class DiscountService : IDiscountService
	{
		private readonly DapperContext _context;

		public DiscountService(DapperContext context)
		{
			_context = context;
		}

		// Yeni kupon ekleme işlemi..
		public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
		{
			string query = "insert into Coupons (Code, Rate, IsActive, ValidDate, CreatedDate) values (@code, @rate, @isActive, @validDate, @createdDate)";
			// Dapper ile sql sorgusuna parametreler ekleniyor..
			var parameters = new DynamicParameters();
			parameters.Add("@code", createCouponDto.Code);
			parameters.Add("@rate", createCouponDto.Rate);
			parameters.Add("@isActive", createCouponDto.IsActive);
			parameters.Add("@validDate", createCouponDto.ValidDate);
			parameters.Add("@validDate", createCouponDto.ValidDate);
			parameters.Add("@createdDate", DateTime.UtcNow);

			// Veritabanı bağlantısı oluşturuluyor..
			using (var connection = _context.CreateConnection()) 
			{ 
				// Parametreler kullanılarak veritabanında sorgu çalıştırılıyor.. 
				await connection.ExecuteAsync(query, parameters);
			}// using bloğu bitiminde connection nesnesi otomatik olarak Dispose methodunu çağırır ve bağlantıyı kapatır..
		}

		public async Task DeleteCouponAsync(int id)
		{
			string query = "Delete From Coupons where Id=@id";
			var parameters = new DynamicParameters();
			parameters.Add("id", id);

			using (var connection = _context.CreateConnection()) 
			{
				await connection.ExecuteAsync(query,parameters);
			}
		}

		public async Task<List<ResultCouponDto>> GetAllCouponsAsync()
		{
			string query = "Select * from Coupons";

			using (var connection = _context.CreateConnection()) 
			{
				var values = await connection.QueryAsync<ResultCouponDto>(query);
				return values.ToList();
			}
		}

		public async Task<GetByIdCouponDto> GetByIdCouponAsync(int id)
		{
			string query = "Select * From Coupons Where Id=@couponId";
			var parameters= new DynamicParameters();
			parameters.Add("couponId", id);

			using (var connection = _context.CreateConnection()) 
			{
				return await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query, parameters);
			}
		}

		public async Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
		{
			string query = "Update Coupons Set Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate, UpdatedDate=@updatedDate where Id=@couponId";
			// Dapper ile sql sorgusuna parametreler ekleniyor..
			var parameters = new DynamicParameters();
			parameters.Add("@couponId", updateCouponDto.Id);
			parameters.Add("@code", updateCouponDto.Code);
			parameters.Add("@rate", updateCouponDto.Rate);
			parameters.Add("@isActive", updateCouponDto.IsActive);
			parameters.Add("@validDate", updateCouponDto.ValidDate);
			parameters.Add("@updatedDate", DateTime.UtcNow);

			// Veritabanı bağlantısı oluşturuluyor..
			using (var connection = _context.CreateConnection())
			{
				// Parametreler kullanılarak veritabanında sorgu çalıştırılıyor.. 
				await connection.ExecuteAsync(query, parameters);
			}// using bloğu bitiminde connection nesnesi otomatik olarak Dispose methodunu çağırır ve bağlantıyı kapatır..
		}
	}
}
