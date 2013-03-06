using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;

namespace BudgetMVC.Model.Business
{
    public class BusinessFactory
    {
        private Dictionary<Type, BusinessEntity> factoryRepository;
        private BudgetContext db;
        private static object sync = new object();

        public BusinessFactory(BudgetContext db)
        {
            this.db = db;
            factoryRepository = new Dictionary<Type, BusinessEntity>();
        }

        public T Get<T>() where T : BusinessEntity
        {
            var business = (T)factoryRepository[typeof(T)];
            if (business == null)
            {
                lock (sync)
                {
                    if (business == null)
                    {
                        business = (T) typeof(T).GetConstructor(new Type[] { typeof(BudgetContext) }).Invoke(new object[] { db });
                    }
                }
            }
            return (T)factoryRepository[typeof(T)];
        }
    }
}
