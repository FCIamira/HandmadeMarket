namespace HandmadeMarket.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IOrderRepo Order { get; }
    IProductRepo Product { get; }

    Task SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}