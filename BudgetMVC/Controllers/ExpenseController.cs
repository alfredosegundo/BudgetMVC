using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BudgetMVC.Model.Entity;
using BudgetMVC.Model.EntityFramework;

namespace BudgetMVC.Controllers
{
    public class ExpenseController : ApiController
    {
        private BudgetContext db = new BudgetContext();

        // GET api/Expense
        public IEnumerable<Expense> GetExpenses()
        {
            return db.Expenses.AsEnumerable();
        }

        // GET api/Expense/5
        public Expense GetExpense(long id)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return expense;
        }

        // PUT api/Expense/5
        public HttpResponseMessage PutExpense(long id, Expense expense)
        {
            if (ModelState.IsValid && id == expense.ID)
            {
                db.Entry(expense).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, expense);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Expense
        public HttpResponseMessage PostExpense(Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, expense);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = expense.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Expense/5
        public HttpResponseMessage DeleteExpense(long id)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Expenses.Remove(expense);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, expense);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}