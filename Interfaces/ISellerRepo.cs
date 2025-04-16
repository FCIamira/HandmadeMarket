namespace HandmadeMarket.Interfaces
{
    public interface ISellerRepo: IGenericRepo<Seller>
    {
        IQueryable<SellerWithProductsDTO>  GetAllSellersWithProducts();
        SellerWithProductsDTO GetSellerWithProductsById(int id);
        SellerWithProductsDTO GetSellerWithProductsByStoreName(string storeName);
        SellerWithProductsDTO DeleteSellerWithProductsById(int id);
        void AddSeller(SellerDTO sellerDTO);
        void EditSeller( int id, SellerDTO sellerDTO);
    }
}
