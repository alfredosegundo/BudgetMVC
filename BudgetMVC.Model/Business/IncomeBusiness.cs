using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.DTO;
using BudgetMVC.Model.EntityFramework;

namespace BudgetMVC.Model.Business
{
    public class IncomeBusiness
    {
        private BudgetContext db;

        public IncomeBusiness(BudgetContext db)
        {
            this.db = db;
        }

        public InitialData GetInitiaData(int month, int year)
        {
            var expenses = db.Expenses.Where(expense => expense.CreationDate.Year == year && expense.CreationDate.Month == month).ToList();
            var revenues = db.Revenues.Where(revenue => revenue.CreationDate.Year == year && revenue.CreationDate.Month == month).ToList();
            var monthBalance = revenues.Sum(revenue => revenue.Value) - expenses.Sum(expense => expense.Value);
            return new InitialData(expenses.ToArray<object>(), revenues.ToArray<object>(), monthBalance);
        }
    }
}
