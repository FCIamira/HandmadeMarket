using Microsoft.AspNetCore.Http;
namespace HandmadeMarket.DTO;
﻿namespace HandmadeMarket.DTO
{
    public class AddProductDTO
    {
      

        public string? Description { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
        public string? Image { get; set; }
        public int categoryId { get; set; }
        public string sellerId { get; set; }
        public bool HasSale { get; set; }
        public decimal SalePercentage { get; set; }

    }
}
