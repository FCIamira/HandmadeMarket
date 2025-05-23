using HandmadeMarket.DTO.ProductDTOs;

namespace HandmadeMarket.Interfaces
{
    public interface IProductRepo: IGenericRepo<Product>
    {
        Product GetProductByName(string name);
        decimal CalcPriceAfterSale(decimal price , decimal salePercentage);
        IEnumerable<Product> GetProductsHaveSale();

        Task<IEnumerable<TopProductsDTO>> GetTopProductsByHighestNumberOfOrder();


        Product GetProductById(int id);
        void EditProduct(int id, AddProductDTO product,string userId);
        void DeleteProduct(int id);

        List<ProductDTO> GetProductsByRanges(decimal min, decimal max);
    }
}
