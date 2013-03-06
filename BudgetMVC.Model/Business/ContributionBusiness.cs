using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Model.Business
{
    public class ContributionBusiness : PersistentBusiness<Contribution>
    {
        public ContributionBusiness(BudgetContext db) : base(db) { }

        public IEnumerable<Contribution> GetCurrentContributions(int month, int year)
        {
            return db.Contributions
                .Where(contribution => contribution.InitialDate.Year <= year && contribution.InitialDate.Month <= month)
                .OrderByDescending(c => c.CreationDate);
        }
    }

    class ContributorComparer : IEqualityComparer<Contribution>
    {
        public bool Equals(Contribution x, Contribution y)
        {
            return x.ID.Equals(y.ID);
        }

        public int GetHashCode(Contribution obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
