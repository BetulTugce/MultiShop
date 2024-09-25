using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstractions
{
	public interface IProductDetailRepository : IGenericRepository<ProductDetail>
	{
        Task<ProductDetail> GetProductDetailByProductIdAsync(string productId);
    }
}
