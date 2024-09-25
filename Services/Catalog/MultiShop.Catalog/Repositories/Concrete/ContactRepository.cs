using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.ContactCollectionName)
        {
        }
    }
}
