using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;
using System.Data;

namespace BudgetMVC.Model.Business
{
    public class ContributionBusiness : PersistentBusiness<Contribution>
    {
        public ContributionBusiness(BudgetContext db) : base(db) { }

        public IEnumerable<Contribution> GetCurrentContributions(int month, int year)
        {
            var lastDayOfMonth = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            var query = from contributions in db.Contributions
                        where contributions.InitialDate <= lastDayOfMonth
                        group contributions by contributions.Contributor into g
                        select g.OrderByDescending(c => c.InitialDate).FirstOrDefault();
            return query.ToList();
        }

        public new void Update(Contribution contribution)
        {
            MakeFactorBetweenOneAndZero(contribution);
            base.Update(contribution);
        }

        private static void MakeFactorBetweenOneAndZero(Contribution contribution)
        {
            if (contribution.ContributionFactor < 0.0)
                contribution.ContributionFactor *= -1.0;
            if (contribution.ContributionFactor > 1.0)
                contribution.ContributionFactor /= 100.0;
        }
    }
}
