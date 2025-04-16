
using HandmadeMarket.Models;
using static System.Net.Mime.MediaTypeNames;

namespace HandmadeMarket.Repository
{
    public class ProductRepo:GenericRepo<Product>, IProductRepo
    {
        HandmadeContext context;
        public ProductRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public void AddProduct(AddProductDTO product)
        {
            Product productDTO = new Product
            {
                sellerId = product.sellerId,
                categoryId = product.categoryId,
                ProductId = product.ProductId,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Image = product.Image
            };
            context.Add(productDTO);
        }

        public void DeleteProduct(int id)
        {
            Product? product = context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefault(p => p.ProductId == id);
            context.Products.Remove(product);
        }

        public void EditProduct(int id, AddProductDTO product)
        {
            Product? p = context.Products
                .Where(p => p.ProductId == id)
                .FirstOrDefault();

            p.Description = product.Description;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Stock = product.Stock;
            p.Image = product.Image;
            p.categoryId = product.categoryId;
            p.sellerId = product.sellerId;

            context.Update(p);

        }

        public IQueryable<ProductDTO> GetAllProduct()
        {
            IQueryable<ProductDTO> products = context.Products
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Image = p.Image
                });
            return products;

        }

        public ProductDTO GetProductById(int id)
        {
            ProductDTO? productDTO = context.Products
                .Where(p => p.ProductId == id)
                .Select( p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Image = p.Image
                }).FirstOrDefault();
            return productDTO;
        }

        public ProductDTO GetProductByName(string name)
        {
            ProductDTO? productDTO = context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Image = p.Image
                }).FirstOrDefault();
            return productDTO;
        }
    }
}
