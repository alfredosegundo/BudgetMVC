using System.Web.Mvc;
using BudgetMVC.Model.Business;
using BudgetMVC.Model.EntityFramework;
using Newtonsoft.Json;

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
            var incomeBusiness = new DashboardBusiness(db);
            var initialData = incomeBusiness.GetInitialData(month, year);
            var model = new { initialData.expenses, initialData.revenues, initialData.monthBalance };
            var result = Json(model);
            result.Data = JsonConvert.SerializeObject(model);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
