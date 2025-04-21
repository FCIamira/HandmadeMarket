namespace HandmadeMarket.Interfaces
{
    public interface IProductRepo: IGenericRepo<Product>
    {
        ProductDTO GetProductByName(string name);
        decimal CalcPriceAfterSale(decimal price , decimal salePercentage);
        IEnumerable<Product> GetProductsHaveSale();


        ProductDTO GetProductById(int id);
        void EditProduct(int id, AddProductDTO product);
        void DeleteProduct(int id);

        List<ProductDTO> GetProductsByRanges(decimal min, decimal max);
    }
}
