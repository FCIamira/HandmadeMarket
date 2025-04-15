namespace HandmadeMarket.Interfaces
{
    public interface ICategoryRepo: IGenericRepo<Category>
    {
        public List<CategoryWithProductDTO> GetCategoryByName(string name);
        public List<CategoryWithProductDTO> CategoryDTO();
        public CategoryWithProductDTO GetCategoryDTOById(int id);
        public void AddCategory(CategoryDTO categoryDto);
    }
}
