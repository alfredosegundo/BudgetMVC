using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BudgetMVC.Model.Entity;
using System.Data;

namespace BudgetMVC.Model.EntityFramework
{
    public class BudgetContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PeriodicExpense> PeriodicExpenses { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<PeriodicRevenue> PeriodicRevenues { get; set; }

        public override int SaveChanges()
        {
            DateTime now = DateTime.Now;
            foreach (var entity in ChangeTracker.Entries<EntityWithCreationDate>()
                                                .Where(e => e.State == EntityState.Added)
                                                .Select(e => e.Entity))
            {
                entity.CreationDate = now;
            }

            return base.SaveChanges();
        }
    }
}
