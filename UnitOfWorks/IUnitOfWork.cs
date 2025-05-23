namespace HandmadeMarket.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IOrderRepo Order { get; }
    ICustomerRepo Customer { get; }
    IProductRepo Product { get; }
    ICartRepo Cart { get; }
    ICategoryRepo Category { get; }
     IOrderItemRepo OrderItem { get; }
     IRatingRepo Rating { get; }
     IShipmentRepo Shipment { get; }
     IWishList WishList { get; }
     ISellerRepo Seller { get; }
    Task SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}