using System;
using GameStore.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Image> Images { get; }
        IRepository<User> Users { get; }
        IRepositoryGame Games { get; }
        IRepository<Order> Orders { get; }
        IRepository<Key> Keys { get; }
        IRepository<Role> Roles { get; }
        void Save();
    }
}
