using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Entity;
using System.Data;

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

        public IEnumerable<T> FindAll()
        {
            return db.Set<T>().AsEnumerable();
        }

        public void Update(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public T Find(long id)
        {
            return db.Set<T>().First(entity => entity.ID == id);
        }

        public void Remove(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public T RemoveById(long id)
        {
            T entity = Get(id);
            if (entity != null)
            {
                db.Set<T>().Remove(entity);
                return entity;
            }
            else
                throw new ObjectNotFoundException();
        }

        private T Get(long id)
        {
            return db.Set<T>().FirstOrDefault(entity => entity.ID == id);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
