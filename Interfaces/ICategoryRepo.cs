using HandmadeMarket.Data;

namespace HandmadeMarket.Interfaces
{
    public interface ICategoryRepo: IGenericRepo<Category>
    {
        public Category GetCategoryByName(string name);
        IQueryable<Category> GetAllCategoriesWithProducts();
        public Category GetCategoryDTOById(int id);
    }
}
