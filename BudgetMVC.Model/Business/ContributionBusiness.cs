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
            var query = from contributions in db.Contributions
                        where contributions.InitialDate <= new DateTime(year, month, 1)
                        group contributions by contributions.Contributor into g
                        select g.OrderByDescending(c => c.InitialDate).FirstOrDefault();
            return query.ToList();
        }
    }
}
