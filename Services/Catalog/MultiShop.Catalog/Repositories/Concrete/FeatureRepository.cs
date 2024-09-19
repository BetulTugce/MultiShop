using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.FeatureCollectionName)
        {
        }
    }
}
