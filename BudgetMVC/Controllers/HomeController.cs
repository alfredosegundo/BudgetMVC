using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using BudgetMVC.Model.Entity;
using Newtonsoft.Json;
using BudgetMVC.Model.EntityFramework;

namespace BudgetMVC.Controllers
{
    public class HomeController : Controller
    {
        private BudgetContext db = new BudgetContext();
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult InitialData(int month, int year)
        {
            var expenses = db.Expenses.Where(expense => expense.CreationDate.Year == year && expense.CreationDate.Month == month).ToList();
            var revenues = db.Revenues.Where(revenue => revenue.CreationDate.Year == year && revenue.CreationDate.Month == month).ToList();
            var model = new { expenses, revenues };
            var result = Json(model);
            result.Data = JsonConvert.SerializeObject(model);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
