using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using BudgetMVC.Model.Entity;
using Newtonsoft.Json;

namespace BudgetMVC.Controllers
{
    public class HomeController : Controller
    {
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

        public JsonResult InitialData()
        {
            var dummyExpense = new Expense() { Description = "Descrição", CreationDate = DateTime.Now, Value = 1.9 };
            var dummyRevenue = new Revenue() { Description = "Descrição", CreationDate = DateTime.Now, Value = 333.90 };
            var model = new { expenses = new List<Expense>() { dummyExpense }, revenues = new List<Revenue>() { dummyRevenue } };
            var result = Json(model);
            result.Data = JsonConvert.SerializeObject(model);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
