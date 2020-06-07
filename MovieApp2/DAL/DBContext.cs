using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using MovieApp.Model;

namespace MovieApp.DAL
{

    public class DBContext : DbContext
    {
        public DBContext() : base("name=DB")
        {
            Database.CreateIfNotExists();

        }
        public DbSet<Users> UserTable { get; set; }
        public DbSet<City> CityTable { get; set; }
        public DbSet<Movie> MovieTable { get; set; }
        public DbSet<OrderItem> OrderItemTable { get; set; }
        public DbSet<Order> OrderTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderItems)
                .WithRequired()           
                .WillCascadeOnDelete();
        }

    }
}