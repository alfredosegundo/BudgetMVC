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
    public class RevenuesController : Controller
    {
        private BudgetContext db = new BudgetContext();

        //
        // GET: /Revenues/

        public ActionResult Index()
        {
            return View(db.Revenues.ToList());
        }

        //
        // GET: /Revenues/Details/5

        public ActionResult Details(long id = 0)
        {
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        //
        // GET: /Revenues/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Revenues/Create

        [HttpPost]
        public ActionResult Create(Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                db.Revenues.Add(revenue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(revenue);
        }

        //
        // GET: /Revenues/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        //
        // POST: /Revenues/Edit/5

        [HttpPost]
        public ActionResult Edit(Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(revenue);
        }

        //
        // GET: /Revenues/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Revenue revenue = db.Revenues.Find(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        //
        // POST: /Revenues/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Revenue revenue = db.Revenues.Find(id);
            db.Revenues.Remove(revenue);
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