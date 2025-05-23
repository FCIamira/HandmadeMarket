﻿namespace HandmadeMarket.DTO.ProductDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public decimal? SalePercentage { get; set; }
        public decimal? PriceAfterSale { get; set; }
        public int CategoryId { get; set; }

    }
}
