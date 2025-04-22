namespace HandmadeMarket.Interfaces
{
    public interface ICategoryRepo: IGenericRepo<Category>
    {
        public Category GetCategoryByName(string name);
        IEnumerable<Category> GetAllCategoriesWithProducts();
        public Category GetCategoryDTOById(int id);
    }
}
