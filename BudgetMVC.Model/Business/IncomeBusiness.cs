using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.DTO;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Model.Business
{
    public class IncomeBusiness
    {
        private BudgetContext db;

        public IncomeBusiness(BudgetContext db)
        {
            this.db = db;
        }

        public InitialData GetInitialData(int month, int year)
        {
            var expenses = GetExpenses(month, year).ToList();
            var revenues = GetRevenues(month, year).ToList();
            revenues = revenues.Union(CurrentContributionsAsRevenues(month, year)).ToList();
            var monthBalance = revenues.Sum(revenue => revenue.Value) - expenses.Sum(expense => expense.Value);
            return new InitialData(expenses.ToArray<object>(), revenues.ToArray<object>(), monthBalance);
        }

        private IEnumerable<Revenue> CurrentContributionsAsRevenues(int month, int year)
        {
            return GetCurrentContributions(month, year).ToList().Select(contribution => new Revenue { Description = contribution.Description, Value = contribution.Value * contribution.ContributionFactor, CreationDate = contribution.CreationDate });
        }

        private IQueryable<Revenue> GetRevenues(int month, int year)
        {
            return db.Revenues.Where(revenue => revenue.CreationDate.Year == year && revenue.CreationDate.Month == month);
        }

        private IQueryable<Contribution> GetCurrentContributions(int month, int year)
        {
            return db.Contributions.Where(contribution => contribution.FinalDate == null && contribution.InitialDate <= new DateTime(year,month,1));
        }

        private IQueryable<Expense> GetExpenses(int month, int year)
        {
            return db.Expenses.Where(expense => expense.CreationDate.Year == year && expense.CreationDate.Month == month);
        }
    }
}
