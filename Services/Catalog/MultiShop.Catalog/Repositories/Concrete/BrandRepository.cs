using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.BrandCollectionName)
        {
        }
    }
}
