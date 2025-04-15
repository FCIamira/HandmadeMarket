
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

        public void AddCategory(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                name = categoryDto.name,
            };
            handmadeContext.Categories.Add(category);
        }


        public List<CategoryWithProductDTO> CategoryDTO()
        {
            List<CategoryWithProductDTO> categoryDTO = handmadeContext.Categories
                .Select(c => new CategoryWithProductDTO
                {
                    name = c.name,
                    categoryId = c.categoryId,
                    Products = c.Products.Select(p => new ProductDTO
                    {
                        Price = p.Price,
                        Name = p.Name,
                        Description = p.Description,
                    }).ToList()
                }).ToList();

            return categoryDTO;
        }

        public List<CategoryWithProductDTO> GetCategoryByName(string name)
        {
            List<CategoryWithProductDTO> categoryDTO = handmadeContext.Categories
                .Where(c => c.name == name)
                .Select(c => new CategoryWithProductDTO
                {
                    name = c.name,
                    categoryId = c.categoryId,
                    Products = c.Products.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                    }).ToList()
                }).ToList();
            return categoryDTO;
        }

        public CategoryWithProductDTO GetCategoryDTOById(int id)
        {
            CategoryWithProductDTO? categoryDTO = handmadeContext.Categories
                .Where(c=> c.categoryId == id)
                .Select(c=>new CategoryWithProductDTO
                {
                    name = c.name,
                    categoryId = c.categoryId,
                    Products = c.Products.Select(p => new ProductDTO
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                    }).ToList()
                }).FirstOrDefault();
            return categoryDTO;
        }
    }
}
