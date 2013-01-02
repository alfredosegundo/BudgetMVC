using System;
using System.Linq;
using BudgetMVC.Model.Business;
using BudgetMVC.Model.Entity;
using BudgetMVC.Model.EntityFramework;
using NUnit.Framework;
using BudgetMVC.Model.DTO;

namespace BudgetMVC.Tests.Business
{
    [TestFixture]
    public class DashboardTest
    {
        private BudgetContext db;
        private DashboardBusiness business;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            db = new BudgetContext();
            business = new DashboardBusiness(db);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            db.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            db.Database.Delete();
        }

        [Test]
        public void Empty()
        {
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);
            Assert.AreEqual(0, data.monthBalance);
            Assert.AreEqual(0, data.expenses.Count());
            Assert.AreEqual(0, data.revenues.Count());
        }

        [Test]
        public void PositiveBalance()
        {
            Insert(new Revenue { Value = 120.0 });
            Insert(new Expense { Value = 73.48 });
            Save();
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);
            Assert.AreEqual(120 - 73.48, data.monthBalance);
            Assert.AreEqual(120, GetRevenueSum(ref data));
            Assert.AreEqual(73.48, GetExpensesSum(ref data));
        }

        [Test]
        public void NegativeBalance()
        {
            Insert(new Revenue { Value = 35.98 });
            Insert(new Expense { Value = 73.48 });
            Save();
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);
            Assert.AreEqual(35.98 - 73.48, data.monthBalance);
            Assert.AreEqual(35.98, GetRevenueSum(ref data));
            Assert.AreEqual(73.48, GetExpensesSum(ref data));
        }

        [Test]
        public void ContributionAddedSinceLastMonth()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            InsertContribution(new Contribution { InitialDate = lastMonth, Value = 1560, ContributionFactor = 0.78 });
            Insert(new Revenue { Value = 120.0 });
            Insert(new Expense { Value = 73.48 });
            Save();
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);
            Assert.AreEqual(1263.32, data.monthBalance);
            Assert.AreEqual(1336.80, GetRevenueSum(ref data));
            Assert.AreEqual(73.48, GetExpensesSum(ref data));
        }

        [Test]
        public void ContributionNotValidYet()
        {
            var nextMonth = DateTime.Today.AddMonths(1);
            InsertContribution(new Contribution { InitialDate = nextMonth, Value = 1560, ContributionFactor = 0.78 });
            Insert(new Revenue { Value = 120.0 });
            Insert(new Expense { Value = 73.48 });
            Save();
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);
            Assert.AreEqual(120.0 - 73.48, data.monthBalance);
            Assert.AreEqual(120.0, GetRevenueSum(ref data));
            Assert.AreEqual(73.48, GetExpensesSum(ref data));
        }

        private void Insert<T>(T entity) where T : BudgetEntity
        {
            db.Set<T>().Add(entity);
        }

        private void Save()
        {
            db.SaveChanges();
        }

        private void InsertContribution(Contribution contribution)
        {
            var alfredo = new Contributor { Name = "Alfredo" };
            Insert(alfredo);
            contribution.Contributor = alfredo;
            Insert(contribution);
        }

        private static double GetRevenueSum(ref InitialData data)
        {
            return data.revenues.Sum(revenue => ((Revenue)revenue).Value);
        }

        private static double GetExpensesSum(ref InitialData data)
        {
            return data.expenses.Sum(expense => ((Expense)expense).Value);
        }
    }
}
