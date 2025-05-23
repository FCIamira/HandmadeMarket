using HandmadeMarket.Data;
using HandmadeMarket.Models;

namespace HandmadeMarket.Interfaces
{
    public interface IOrderRepo :IGenericRepo<Order>
    {
        decimal CalcTotalPrice(decimal price, int quantity);

       
    }
}
