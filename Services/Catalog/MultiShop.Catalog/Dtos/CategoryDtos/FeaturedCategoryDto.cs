using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Dtos.CategoryDtos
{
    public class FeaturedCategoryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public long ProductCount { get; set; }
    }
}
