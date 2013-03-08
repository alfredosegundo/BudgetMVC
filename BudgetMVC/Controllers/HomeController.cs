using System.Web.Mvc;
using BudgetMVC.Model.Business;
using BudgetMVC.Model.EntityFramework;
using Newtonsoft.Json;
using BudgetMVC.ViewModels;
using BudgetMVC.Model.Entity;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BudgetMVC.Controllers
{
    public class HomeController : Controller
    {
        private BudgetContext db = new BudgetContext();

        public ActionResult Index()
        {
            var model = new IndexViewModel();
            var list = db.Contributors.ToList().Select(c => new SelectListItem { Text = c.Name, Value = c.ID.ToString() });
            model.ContributionViewModel.Contributors = list;
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


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
