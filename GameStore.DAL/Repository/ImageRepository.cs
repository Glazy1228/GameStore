using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Repository
{
    public class ImageRepository : IRepository<Image>
    {
        private StoreContext db;

        public ImageRepository(StoreContext context)
        {
            db = context;
        }
        public IEnumerable<Image> GetAll()
        {
            return db.Images;
        }

        public Image Get(int gameId)
        {
            return db.Images.Find(gameId);
        }

        public void Create(Image image)
        {
            db.Images.Add(image);
        }

        public void Update(Image image)
        {
            db.Entry(image).State = EntityState.Modified;
        }

        public IEnumerable<Image> Find(Func<Image, Boolean> predicate)
        {
            return db.Images.Where(predicate).ToList();
        }

        public void Delete(int? id)
        {
            Image image = db.Images.Find(id);
            if (image != null)
                db.Images.Remove(image);
        }
    }
}
