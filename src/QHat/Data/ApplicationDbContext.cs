﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QHat.Models;

namespace QHat.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hat> Hat { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderitem> Orderitems { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public object Customers { get; internal set; }
        public DbSet<Customer> Customer { get; set; }
        //public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }        public DbSet<Supplier> Supplier { get; set; }                

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Entity<Hat>().ToTable("Hat");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Orderitem>().ToTable("Orderitem");
            modelBuilder.Entity<Orderitem>().HasOne(p => p.Order).WithMany(o => o.Orderitems).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

        }
                

        public DbSet<ShoppingCart> ShoppingCart { get; set; }
    }
}
