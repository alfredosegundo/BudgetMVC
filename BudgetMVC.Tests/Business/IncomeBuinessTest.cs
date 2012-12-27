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
    public class IncomeBuinessTest
    {
        private BudgetContext db;
        private IncomeBusiness business;
        private int month;
        private int year;

        [SetUp]
        public void SetUp()
        {
            db = new BudgetContext();
            db.Database.Delete();
            month = DateTime.Today.Month;
            year = DateTime.Today.Year;
            business = new IncomeBusiness(db);
        }

        [Test]
        public void InitialData_Empty()
        {
            var data = business.GetInitialData(month, year);
            Assert.AreEqual(0, data.monthBalance);
            Assert.AreEqual(0, data.expenses.Count());
            Assert.AreEqual(0, data.revenues.Count());
        }

        [Test]
        public void InitialData_MonthBalance_Positive()
        {
            InsertRevenue(120.0);
            InsertExpense(73.48);
            Save();
            var data = business.GetInitialData(month, year);
            Assert.AreEqual(120 - 73.48, data.monthBalance);
            Assert.AreEqual(120, GetRevenueSum(ref data));
            Assert.AreEqual(73.48, GetExpensesSum(ref data));
        }

        [Test]
        public void InitialData_MonthBalance_Negative()
        {
            InsertRevenue(35.98);
            InsertExpense(73.48);
            Save();
            var data = business.GetInitialData(month, year);
            Assert.AreEqual(35.98 - 73.48, data.monthBalance);
            Assert.AreEqual(35.98, GetRevenueSum(ref data));
            Assert.AreEqual(73.48, GetExpensesSum(ref data));
        }

        [Test]
        public void InitialData_WithPeriodicRevenue()
        {
            InsertContribution(DateTime.Today.AddMonths(-1), 1560, 0.78);
            Save();
            var data = business.GetInitialData(month, year);
            Assert.AreEqual(1560 * 0.78, data.monthBalance);
            Assert.AreEqual(1560 * 0.78, GetRevenueSum(ref data));
            Assert.AreEqual(0, GetExpensesSum(ref data));
        }

        [Test]
        public void InitialData_WithPeriodicRevenue_PeriodRevenueNotValidYet()
        {
            InsertContribution(DateTime.Today.AddMonths(2), 1560, 0.78);
            Save();
            var data = business.GetInitialData(month, year);
            Assert.AreEqual(0, data.monthBalance);
            Assert.AreEqual(0, GetRevenueSum(ref data));
            Assert.AreEqual(0, GetExpensesSum(ref data));
        }

        private void InsertRevenue(double revenueValue)
        {
            db.Revenues.Add(new Revenue { Description = "Any", Value = revenueValue });
        }

        private void Save()
        {
            db.SaveChanges();
        }

        private void InsertExpense(double value)
        {
            db.Expenses.Add(new Expense { Description = "Any", Value = value });
        }

        private void InsertContribution(DateTime initialDate, int value, double factor)
        {
            var alfredo = new Contributor { Name = "Alfredo" };
            db.Contributors.Add(alfredo);
            db.Contributions.Add(new Contribution { Contributor = alfredo, InitialDate = initialDate, Value = value, ContributionFactor = factor });
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
