using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.DTO;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Model.Business
{
    public class DashboardBusiness
    {
        private BudgetContext db;
        private ContributionBusiness contributionBusiness;

        public DashboardBusiness(BudgetContext db)
        {
            this.db = db;
            this.contributionBusiness = new ContributionBusiness(db);
        }

        public InitialData GetInitialData(int month, int year)
        {
            var expenses = Find<Expense>(month, year);
            var revenues = Find<Revenue>(month, year);
            revenues = revenues.Union(CurrentContributionsAsRevenues(month, year)).ToList();
            var monthBalance = revenues.Sum(revenue => revenue.Value) - expenses.Sum(expense => expense.Value);
            return new InitialData(expenses.ToArray<object>(), revenues.ToArray<object>(), monthBalance);
        }

        private IEnumerable<Revenue> CurrentContributionsAsRevenues(int month, int year)
        {
            return GetCurrentContributions(month, year).Select(contribution => new Revenue { Description = contribution.Description, Value = contribution.RealValue, CreationDate = contribution.CreationDate });
        }

        private IEnumerable<T> Find<T>(int month, int year) where T : EntityWithCreationDate
        {
            return db.Set<T>().Where(revenue => revenue.CreationDate.Year == year && revenue.CreationDate.Month == month).ToList();
        }

        private IEnumerable<Contribution> GetCurrentContributions(int month, int year)
        {
            return contributionBusiness.GetCurrentContributions(month, year);
        }
    }
}
