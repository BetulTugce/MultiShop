using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstractions
{
    public interface IFeatureSliderRepository : IGenericRepository<FeatureSlider>
    {
        Task FeatureSliderChangeStatus(string id);
    }
}
