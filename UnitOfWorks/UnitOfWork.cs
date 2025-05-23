
using HandmadeMarket.Models;

namespace HandmadeMarket.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly HandmadeContext applicationDBContext;
    private IOrderRepo _order;
    private IProductRepo _product;
    private ICartRepo _cartRepo;
    private ICategoryRepo _categoryRepo;
    private IOrderItemRepo _orderItemRepo;
    private IRatingRepo _ratingRepo;
    private IShipmentRepo _shipmentRepo;
    private IWishList _wishList;
    private ISellerRepo _sellerRepo;


    public UnitOfWork(HandmadeContext applicationDBContext)
    {
        this.applicationDBContext = applicationDBContext;
    }

    #region Product
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
    #endregion

    #region Order
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
    #endregion

    #region Seller
    public ISellerRepo Seller
    {
        get
        {
            if (_sellerRepo is null)
            {
                _sellerRepo = new SellerRepo(applicationDBContext);
            }
            return _sellerRepo;
        }
    } 
    #endregion


    #region WishList
    public IWishList WishList
    {
        get
        {
            if (_wishList is null)
            {
                _wishList = new WishListRepo(applicationDBContext);
            }
            return _wishList;
        }
    } 
    #endregion

    #region ShipmentRepo
    public IShipmentRepo Shipment
    {
        get
        {
            if (_shipmentRepo is null)
            {
                _shipmentRepo = new ShipmentRepo(applicationDBContext);
            }
            return _shipmentRepo;
        }
    } 
    #endregion

    #region RatingRepo
    public IRatingRepo Rating
    {
        get
        {
            if (_ratingRepo is null)
            {
                _ratingRepo = new RatingRepo(applicationDBContext);
            }
            return _ratingRepo;
        }
    } 
    #endregion

    #region OrderItemRepo
    public IOrderItemRepo OrderItem
    {
        get
        {
            if (_orderItemRepo is null)
            {
                _orderItemRepo = new OrderItemRepo(applicationDBContext);
            }
            return _orderItemRepo;
        }
    } 
    #endregion

    #region CategoryRepo
    public ICategoryRepo Category
    {
        get
        {
            if (_categoryRepo is null)
            {
                _categoryRepo = new CategoryRepo(applicationDBContext);
            }
            return _categoryRepo;
        }
    }

    #endregion

    #region cartRepo
    public ICartRepo Cart
    {
        get
        {
            if (_cartRepo is null)
            {
                _cartRepo = new CartRepo(applicationDBContext);
            }
            return _cartRepo;
        }
    } 
    #endregion

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