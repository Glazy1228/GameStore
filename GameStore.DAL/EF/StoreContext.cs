using GameStore.DAL.Entities;
using System.Data.Entity;
using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GameStore.DAL.EF
{
    /// <summary>
    /// Data base context
    /// </summary>
    public class StoreContext : DbContext
    {
        public StoreContext(string connectionString) : base(connectionString) { }

        public StoreContext() : base("DefaultConnection") { }

        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Role> Roles { get; set; }
    }   
}
