
using HandmadeMarket.DTO;
using HandmadeMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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

        public decimal CalcPriceAfterSale(decimal price, decimal salePercentage)
        {
           decimal priceAfterSale = price - (price * salePercentage);
            return priceAfterSale;
        }


        //public void AddProduct(AddProductDTO product)
        //{
        //    Product productDTO = new Product
        //    {
        //        sellerId = product.sellerId,
        //        categoryId = product.categoryId,
        //        ProductId = product.ProductId,
        //        Description = product.Description,
        //        Name = product.Name,
        //        Price = product.Price,
        //        Stock = product.Stock,
        //        Image = product.Image
        //    };
        //    context.Add(productDTO);
        //}

        public void DeleteProduct(int id)
        {
            Product? product = context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefault(p => p.ProductId == id);
            context.Products.Remove(product);
        }

        //public void EditProduct(int id, AddProductDTO product)
        //{
        //    Product? p = context.Products
        //        .Where(p => p.ProductId == id)
        //        .FirstOrDefault();

        //    p.Description = product.Description;
        //    p.Name = product.Name;
        //    p.Price = product.Price;
        //    p.Stock = product.Stock;
        //    p.Image = product.Image;
        //    p.categoryId = product.categoryId;
        //    p.sellerId = product.sellerId;

        //    context.Update(p);


        //}
        //public  void EditProduct(int id, AddProductDTO product)
        //{
        //    Product? p = context.Products.FirstOrDefault(p => p.ProductId == id);

        //    if (p == null) return; 

        public void EditProduct(int id, AddProductDTO product)
        {
            Product? p = context.Products
                .Where(p => p.ProductId == id)
                .FirstOrDefault();

            p.Description = product.Description;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Stock = product.Stock;

            if (product.Image != null && product.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                     product.Image.CopyToAsync(stream);
                }

                p.Image = "/uploads/" + uniqueFileName; // مسار الصورة في قاعدة البيانات
            }

            p.Image = product.Image;
            p.categoryId = product.categoryId;
            p.sellerId = product.sellerId;

            context.Update(p);
             context.SaveChangesAsync();

        }

        //public IEnumerable<Product> GetAll()
        //{

        //    return context.Products.Include(p => p.Ratings);

        //}

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

        public Product GetProductByName(string name)
        {
            Product? product = context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> GetProductsHaveSale()
        {
            IEnumerable<Product> products = context.Products
                .Include(p => p.Ratings)
                .Where(s => s.HasSale);
            return products;

        }


        public async Task<IEnumerable<TopProductsDTO>> GetTopProductsByHighestNumberOfOrder()
        {
            return await context.Items
                .Include(oi => oi.Product)
                    .ThenInclude(p => p.Category)
                .Include(oi => oi.Product)
                    .ThenInclude(p => p.Seller)
                .GroupBy(oi => oi.Product)
                .Select(g => new TopProductsDTO
                {
                    ProductId = g.Key.ProductId,
                    Name = g.Key.Name,
                    Price = g.Key.Price,
                    ImageUrl = g.Key.Image,
                    OrderCount = g.Count(),
                    TotalQuantity = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(x => x.OrderCount)
                .Take(10)
                .ToListAsync();

        }
       public List<ProductDTO> GetProductsByRanges(decimal min, decimal max)
        {
            List<Product?> products = context.Products.Where(p=>p.Price <= max && p.Price >= min).ToList();
            List<ProductDTO> result = new List<ProductDTO>();
            foreach (var product in products)
            {
                ProductDTO dto = new ProductDTO
                {
                    ProductId= product.ProductId,
                    Price = product.Price,
                    Name = product.Name,
                    Description = product.Description,
                    Image = product.Image,
                    Stock = product.Stock
                };
                result.Add(dto);
            }
            return result;

        }
    }
}
