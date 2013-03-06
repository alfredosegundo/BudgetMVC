using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;
using BudgetMVC.Model.Entity.Enum;
using System.Data.Entity.Migrations;

namespace BudgetMVC.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BudgetContext())
            {
                db.Database.Delete();
                db.Expenses.Add(new Expense { Description = DateTime.Now.ToLongDateString(), Value = DateTime.Now.Second * 0.38 });
                db.SaveChanges();
                var query = from expense in db.Expenses
                            orderby expense.CreationDate
                            select expense;

                Console.WriteLine("Database: " + db.Database.Connection.ConnectionString);
                Console.WriteLine("All expenses in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine("Date: " + item.CreationDate);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

        }

    }
}
