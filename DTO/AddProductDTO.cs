<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
namespace HandmadeMarket.DTO
=======
﻿namespace HandmadeMarket.DTO
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
{
    public class AddProductDTO
    {
      

        public string? Description { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
<<<<<<< HEAD
        public IFormFile? Image { get; set; }
=======
        public string? Image { get; set; }
>>>>>>> f86c7887e775c2545663aef2683b60d7960b5007
        public int categoryId { get; set; }
        public string sellerId { get; set; }
        public bool HasSale { get; set; }
        public decimal SalePercentage { get; set; }

    }
}
