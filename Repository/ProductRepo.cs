using HandmadeMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;
using HandmadeMarket.DTO.ProductDTOs;
using HandmadeMarket.Data;

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
            decimal priceAfterSale = price - (price * (salePercentage / 100));
            return priceAfterSale;
        }


      

        public void DeleteProduct(int id)
        {
            Product? product = context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefault(p => p.ProductId == id);
            context.Products.Remove(product);
        }
        public void EditProduct(int id, AddProductDTO dto, string userId)
        {
            var existingProduct = context.Products.Find(id);
            if (existingProduct == null) return;

           
            existingProduct.Name = dto.Name;
            existingProduct.Description = dto.Description;
            existingProduct.Price = dto.Price;
            existingProduct.Stock = dto.Stock;
            existingProduct.SalePercentage = dto.SalePercentage;

            context.Products.Update(existingProduct); 
        }


        public IEnumerable<Product> GetAll()
        {

            return context.Products.Include(p => p.Ratings);

        }

        public Product GetProductById(int id)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == id);
        }


        public Product GetProductByName(string name)
        {
            Product? product = context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .FirstOrDefault();
            return product;
        }

        //public IEnumerable<Product> GetProductsHaveSale()
        //{
        //    IEnumerable<Product> products = context.Products
        //        .Include(p => p.Ratings)
        //        .Where(s => s.HasSale);
        //    return products;

        //}
        public IEnumerable<Product> GetProductsHaveSale()
        {
            return context.Products
                .Where(p => p.SalePercentage > 0 && p.PriceAfterSale < p.Price)
                .ToList();
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
