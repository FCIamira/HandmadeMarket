
using Microsoft.EntityFrameworkCore;

namespace HandmadeMarket.Repository
{
    public class CategoryRepo: GenericRepo<Category>, ICategoryRepo
    {
        private readonly HandmadeContext handmadeContext;

        public CategoryRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }



        public IQueryable<Category> GetAllCategoriesWithProducts()
        {

            return handmadeContext.Categories
                .Include(c => c.Products);
        }

        public Category GetCategoryByName(string name)
        {
            Category? categoryDTO = handmadeContext.Categories
                .Where(c => c.name == name)
                .Include(c => c.Products)
                .FirstOrDefault();
            return categoryDTO;
        }

        public Category GetCategoryDTOById(int id)
        {
            Category? category = handmadeContext.Categories
                .Where(category => category.categoryId == id)
                .Include(c => c.Products)
                .FirstOrDefault();
                
            return category;
        }
    }
}
