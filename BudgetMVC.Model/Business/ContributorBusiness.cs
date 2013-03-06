using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Model.Business
{
    public class ContributorBusiness : PersistentBusiness<Contributor>
    {
        public ContributorBusiness(BudgetContext db) : base(db) { }

        public Contributor GetByName(string name)
        {
            return db.Contributors.Where(c => c.Name == name).FirstOrDefault();
        }

        public new void Insert(Contributor contributor)
        {
            if (GetByName(contributor.Name) == null)
            {
                base.Insert(contributor);
            }
            else
            {
                throw new InvalidOperationException("O nome deve ser único");
            }
        }
    }
}
