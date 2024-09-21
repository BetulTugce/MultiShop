using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstractions
{
	public interface IProductImageRepository : IGenericRepository<ProductImage>
	{
        Task<ProductImage> GetProductImageByProductIdAsync(string productId);
    }
}
