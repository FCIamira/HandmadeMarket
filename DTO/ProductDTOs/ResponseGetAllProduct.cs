namespace HandmadeMarket.DTO.ProductDTOs
{
    public class ResponseGetAllProduct
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<ProductDTO> Data { get; set; }
    }
}
