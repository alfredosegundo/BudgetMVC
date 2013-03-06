using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.EntityFramework;
using BudgetMVC.Model.Business;
using NUnit.Framework;

namespace BudgetMVC.Tests.Business
{
    public class PersistentTest
    {
        protected BudgetContext db;
        protected DashboardBusiness business;
        protected ContributorBusiness contributorBusiness;
        protected RevenueBusiness revenueBusiness;
        protected ExpenseBusiness expenseBusiness;
        protected ContributionBusiness contributionBusiness;

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            db.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            db = new BudgetContext();
            db.Database.Delete();
            business = new DashboardBusiness(db);
            contributorBusiness = new ContributorBusiness(db);
            contributionBusiness = new ContributionBusiness(db);
            revenueBusiness = new RevenueBusiness(db);
            expenseBusiness = new ExpenseBusiness(db);
        }
    }
}
