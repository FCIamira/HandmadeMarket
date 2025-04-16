namespace HandmadeMarket.Interfaces
{
    public interface IProductRepo: IGenericRepo<Product>
    {
        IQueryable<ProductDTO> GetAllProduct();
        ProductDTO GetProductById(int id);
        ProductDTO GetProductByName(string name);
        void AddProduct(AddProductDTO product);
        void EditProduct(int id, AddProductDTO product);
        void DeleteProduct(int id);
    }
}
