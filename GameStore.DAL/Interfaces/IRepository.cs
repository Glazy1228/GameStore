using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;

namespace GameStore.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
    public interface IRepositoryGame 
    {
        IEnumerable<Game> GetAll();
        Game Get(int id);
        IEnumerable<Game> Find(Func<Game, Boolean> predicate);
        void Create(Game item);
        void Update(Game item);
        void Delete(int id);
        IEnumerable<Game> Search(string name);
    }
}
