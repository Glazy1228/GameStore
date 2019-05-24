using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        GameDTO GetGame(int? id);
        IEnumerable<GameDTO> GetGames();
        IEnumerable<OrderDTO> GetOrders();
        void Dispose();
    }
}
