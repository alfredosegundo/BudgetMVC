using System.Web.Mvc;
using BudgetMVC.Model.Business;
using BudgetMVC.Model.EntityFramework;
using Newtonsoft.Json;
using BudgetMVC.ViewModels;
using BudgetMVC.Model.Entity;
using System;
using System.Data.Entity.Migrations;

namespace BudgetMVC.Controllers
{
    public class HomeController : Controller
    {
        private BudgetContext db = new BudgetContext();

        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        public JsonResult InitialData(int month, int year)
        {
            var incomeBusiness = new DashboardBusiness(db);
            var initialData = incomeBusiness.GetInitialData(month, year);
            var model = new { initialData.expenses, initialData.revenues, initialData.monthBalance };
            var result = new JsonResult
            {
                Data = JsonConvert.SerializeObject(model),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return result;
        }
    }
}
