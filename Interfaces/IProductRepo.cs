namespace HandmadeMarket.Interfaces
{
    public interface IProductRepo: IGenericRepo<Product>
    {
        Product GetProductByName(string name);
        decimal CalcPriceAfterSale(decimal price , decimal salePercentage);
        IEnumerable<Product> GetProductsHaveSale();

        Task<IEnumerable<TopProductsDTO>> GetTopProductsByHighestNumberOfOrder();
    }
}
