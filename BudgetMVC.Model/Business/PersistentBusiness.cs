using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Model.Business
{
    public class PersistentBusiness<T> where T : BudgetEntity
    {
        protected BudgetContext db;

        public PersistentBusiness(BudgetContext db)
        {
            this.db = db;
        }

        public void Insert(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }
    }
}
