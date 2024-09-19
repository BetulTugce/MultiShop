using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class SpecialOfferRepository : GenericRepository<SpecialOffer>, ISpecialOfferRepository
    {
        public SpecialOfferRepository(IDatabaseSettings databaseSettings)
			: base(databaseSettings, databaseSettings.SpecialOfferCollectionName)
		{
		}
}
}
