﻿using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Reflection;

namespace Microsoft.eShopWeb.Infrastructure.Data
{

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Store> Store {get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        

          builder.Entity<StockPerStore>()
                .HasKey(t => new { t.ItemId, t.StoreId });

            builder.Entity<StockPerStore>()
                .HasOne(c => c.CatalogItems)
                .WithMany(sp => sp.StockPerStore)
                .HasForeignKey(c => c.ItemId);

            builder.Entity<StockPerStore>()
                .HasOne(s => s.Stores)
                .WithMany(sp => sp.StockPerStore)
                .HasForeignKey(s => s.StoreId);
       }
    }
}
