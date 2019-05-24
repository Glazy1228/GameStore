using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Repository
{
    class KeyRepository : IRepository<Key>
    {
        private StoreContext db;

        public KeyRepository(StoreContext context)
        {
            db = context;
        }
        public IEnumerable<Key> GetAll()
        {
            return db.Keys;
        }

        public Key Get(int id)
        {
            return db.Keys.Find(id);
        }

        public void Create(Key key)
        {
            db.Keys.Add(key);
        }

        public void Update(Key key)
        {
            db.Entry(key).State = EntityState.Modified;
        }

        public IEnumerable<Key> Find(Func<Key, Boolean> predicate)
        {
            return db.Keys.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            Key key = db.Keys.Find(id);
            if (key != null)
                db.Keys.Remove(key);
        }
    }
}
