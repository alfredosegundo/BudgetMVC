using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Model.Business
{
    public class RevenueBusiness : PersistentBusiness<Revenue>
    {
        public RevenueBusiness(BudgetContext db) : base(db) {}
    }
}
