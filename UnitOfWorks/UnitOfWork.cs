
using HandmadeMarket.Models;

namespace HandmadeMarket.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly HandmadeContext applicationDBContext;
    private IOrderRepo _order;
    private IProductRepo _product;

    public UnitOfWork(HandmadeContext applicationDBContext)
    {
        this.applicationDBContext = applicationDBContext;
    }

    public IProductRepo Product
    {
        get
        {
            if (_product is null)
            {
                _product = new ProductRepo(applicationDBContext);
            }
            return _product;
        }
    }

    public IOrderRepo Order
    {
        get
        {
            if (_order is null)
            {
                _order = new OrderRepo(applicationDBContext);
            }
            return _order;
        }
    }

    public async Task BeginTransactionAsync()
    {
        await applicationDBContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await applicationDBContext.Database.CommitTransactionAsync();
    }

    public void Dispose()
    {
        applicationDBContext.Dispose();
    }

    public async Task RollbackAsync()
    {
        await applicationDBContext.Database.RollbackTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await applicationDBContext.SaveChangesAsync();
    }
}