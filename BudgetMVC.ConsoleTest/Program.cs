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
                Console.WriteLine("[OK] Delete database");
                db.Contributors.Add(new Contributor { Name = "Alfredo" });
                db.Contributors.Add(new Contributor { Name = "Ana" });
                db.SaveChanges();
                Console.WriteLine("[OK] Save changes");
                var query = from c in db.Contributors
                            orderby c.Name
                            select c;

                Console.WriteLine("Database: " + db.Database.Connection.ConnectionString);
                Console.WriteLine("All contributors in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine("Name: " + item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

        }

    }
}
