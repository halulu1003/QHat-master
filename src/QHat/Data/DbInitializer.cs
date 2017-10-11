using QHat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QHat.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            if (context.Customer.Any())
            {
                return; // DB has been seeded
            }
            var customers = new Customer[]
            {
                new Customer{Name="Carson",Home="1234567",Work="76543241",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Barson",Home="1234567",Work="4314134",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Darson",Home="1234567",Work="4134134",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Earson",Home="1234567",Work="43534534",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Larson",Home="1234567",Work="6542478",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
            };
            foreach (Customer c in customers)
            {
                context.Customer.Add(c);
            }
            context.SaveChanges();
            var categorys = new Category[]
            {
                    new Category { Name="Male"},
                    new Category { Name="Female"},
                    new Category { Name="Children"},
            };
            foreach (Category ca in categorys)
            {
                context.Category.Add(ca);
            }
            context.SaveChanges();
            var suppliers = new Supplier[]
                {
                    new Supplier { Name="ASD",Home="12448482",Work="323656",Mobile="2545223",Email="GDGFSSF@123.com",Address="Larson"},
                    new Supplier { Name="BSD",Home="12448482",Work="323656",Mobile="2545223",Email="GDGFSSF@123.com",Address="Larson"},
                    new Supplier { Name="CSD",Home="12448482",Work="323656",Mobile="2545223",Email="GDGFSSF@123.com",Address="Larson"},
                    new Supplier { Name="DSD",Home="12448482",Work="323656",Mobile="2545223",Email="GDGFSSF@123.com",Address="Larson"},
                    new Supplier { Name="ESD",Home="12448482",Work="323656",Mobile="2545223",Email="GDGFSSF@123.com",Address="Larson"},
                    new Supplier { Name="FSD",Home="12448482",Work="323656",Mobile="2545223",Email="GDGFSSF@123.com",Address="Larson"},
                };
            foreach (Supplier su in suppliers)
            {
                context.Supplier.Add(su);
            }
            context.SaveChanges();
            var hats = new Hat[]
                {
                    new Hat { SupplierID=1,CategoryID=2,HatName="KALSD",Price=12,Description="FAFDFGGSAD",Image="images/hat2.jpg"},
                    new Hat { SupplierID=2,CategoryID=2,HatName="ALSD",Price=8,Description="FAFDFGGSAD",Image="images/hat2.jpg"},
                    new Hat { SupplierID=3,CategoryID=1,HatName="DALSD",Price=11,Description="FAFDFGGSAD",Image="images/hat2.jpg"},
                    new Hat { SupplierID=1,CategoryID=2,HatName="QALSD",Price=16,Description="FAFDFGGSAD",Image="images/hat2.jpg"},
                    new Hat { SupplierID=2,CategoryID=2,HatName="CLSD",Price=7,Description="FAFDFGGSAD",Image="images/hat2.jpg"},
                    new Hat { SupplierID=3,CategoryID=1,HatName="DALSD",Price=14,Description="FAFDFGGSAD",Image="images/hat2.jpg"},
                };
            foreach (Hat h in hats)
            {
                context.Hat.Add(h);
            }
            context.SaveChanges();
        }
    }
}
