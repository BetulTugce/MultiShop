using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class FeatureSliderRepository : GenericRepository<FeatureSlider>, IFeatureSliderRepository
    {
        public FeatureSliderRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.FeatureSliderCollectionName)
        {
        }

        public Task FeatureSliderChangeStatus(string id)
        {
            throw new NotImplementedException();
        }
    }
}
