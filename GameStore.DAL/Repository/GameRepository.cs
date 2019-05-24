using GameStore.DAL.EF;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace GameStore.DAL.Repository
{
    public class GameRepository : IRepositoryGame
    {
        private StoreContext db;

        public GameRepository(StoreContext context)
        {
            db = context;
        }
        public IEnumerable<Game> GetAll()
        {
            return db.Games;
        }

        public Game Get(int id)
        {
            return db.Games.Find(id);
        }

        public void Create(Game game)
        {
            db.Games.Add(game);
        }

        public void Update(Game game)
        {
            db.Entry(game).State = EntityState.Modified;
        }

        public IEnumerable<Game> Find(Func<Game, Boolean> predicate)
        {
            return db.Games.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Game game = db.Games.Find(id);
            if (game != null)
                db.Games.Remove(game);
        }

        public IEnumerable<Game> Search(string name)
        {
            var games = db.Games;
            List<Game> result = new List<Game>();
            foreach (Game g in games)
            {
                if (g.Name.ToLower().Contains(name.ToLower()))
                {
                    result.Add(g);
                }
            }
            return result;
        }
    }
}
