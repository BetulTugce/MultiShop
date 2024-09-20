using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.AboutCollectionName)
        {
        }
    }
}
