using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        private StoreContext db;

        public RoleRepository(StoreContext context)
        {
            db = context;
        }
        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public void Create(Role role)
        {
            db.Roles.Add(role);
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }

        public IEnumerable<Role> Find(Func<Role, Boolean> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
                db.Roles.Remove(role);
        }        
    }
}
