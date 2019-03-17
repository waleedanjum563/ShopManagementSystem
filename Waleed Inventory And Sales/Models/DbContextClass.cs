using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Waleed_Inventory_And_Sales.Models;

namespace Sales_And_Inventory_MIS.Models
{
    public class DbContextClass : DbContext
    {
        public DbSet<Transaction> Transactions{ get; set; }
        public DbSet<Entity> entity{ get; set; }
        public DbSet<Product> product { get; set; }

        public DbSet<Expense> expense { get; set; }
        public DbSet<ExpenseType> expenseType { get; set; }

        public DbSet<Item> item{ get; set; }
        public DbSet<Colour> colour{ get; set; }
        public DbSet<MakeItem> makeItem{ get; set; }
        public DbSet<Customer> Customers { get; set; }//update stock

        public DbSet<SalesLineItem> salesLineItem { get; set; }//update stock
        public DbSet<Order> order { get; set; }

        public System.Data.Entity.DbSet<Sales_And_Inventory_MIS.Models.SupplyOrder> SupplyOrders { get; set; }

        public System.Data.Entity.DbSet<Sales_And_Inventory_MIS.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<Sales_And_Inventory_MIS.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<Sales_And_Inventory_MIS.Models.ReturnItem> ReturnItems { get; set; }//update stock

        public System.Data.Entity.DbSet<Waleed_Inventory_And_Sales.Models.MonthReportsViewModel> MonthReportsViewModels { get; set; }
    }

}