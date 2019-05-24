using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IServices 
    {
        IAccountService AccountService { get; }
        IOrderService OrderService { get; }
        IGameEditor GameEditor { get; }
        IImageEditor ImageEditor { get; }
        IUserEditor UserEditor { get; }
        void Dispose();
    }
}
