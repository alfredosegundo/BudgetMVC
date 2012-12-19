using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetMVC.Model.Entity;
using BudgetMVC.Model.EntityFramework;

namespace BudgetMVC.Controllers
{
    public class ExpensesController : Controller
    {
        private BudgetContext db = new BudgetContext();

        //
        // GET: /Expenses/

        public ActionResult Index()
        {
            return View(db.Expenses.ToList());
        }

        //
        // GET: /Expenses/Details/5

        public ActionResult Details(long id = 0)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        //
        // GET: /Expenses/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Expenses/Create

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        //
        // GET: /Expenses/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        //
        // POST: /Expenses/Edit/5

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        //
        // GET: /Expenses/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        //
        // POST: /Expenses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}