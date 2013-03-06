using System;
using System.Linq;
using BudgetMVC.Model.Business;
using BudgetMVC.Model.Entity;
using BudgetMVC.Model.EntityFramework;
using NUnit.Framework;
using BudgetMVC.Model.DTO;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;

namespace BudgetMVC.Tests.Business
{
    [TestFixture]
    public class DashboardTest : PersistentTest
    {
        [Test]
        public void Empty()
        {
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);
            
            Assert.That(0, Is.EqualTo(data.monthBalance));
            Assert.That(0, Is.EqualTo(data.expenses.Count()));
            Assert.That(0, Is.EqualTo(data.revenues.Count()));
        }

        [Test]
        public void PositiveBalance()
        {
            revenueBusiness.Insert(new Revenue { Value = 120.0 });
            expenseBusiness.Insert(new Expense { Value = 73.48 });
            
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(data.monthBalance, Is.EqualTo(120 - 73.48));
        }

        [Test]
        public void NegativeBalance()
        {
            revenueBusiness.Insert(new Revenue { Value = 35.98 });
            expenseBusiness.Insert(new Expense { Value = 73.48 });

            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(data.monthBalance, Is.EqualTo(35.98 - 73.48));
        }

        [Test]
        public void ContributionAddedSinceLastMonth()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var contribution = new Contribution { InitialDate = lastMonth, Value = 1560, ContributionFactor = 0.78 };
            InsertContribution(contribution);
            
            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(data.monthBalance, Is.EqualTo(contribution.RealValue));
        }

        [Test]
        public void ContributionNotValidYet()
        {
            var nextMonth = DateTime.Today.AddMonths(1);
            InsertContribution(new Contribution { InitialDate = nextMonth, Value = 1560, ContributionFactor = 0.78 });

            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(data.monthBalance, Is.EqualTo(0));
        }

        [Test]
        public void TwoContributorsSinceLastMonth()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var contrib1 = new Contribution { InitialDate = lastMonth, Value = 1560, ContributionFactor = 0.78 };
            var contrib2 = new Contribution { InitialDate = lastMonth, Value = 1560, ContributionFactor = 0.78, Contributor = new Contributor { Name = "Ana" } };
            InsertContribution(contrib1);
            InsertContribution(contrib2);

            var data = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(data.monthBalance, Is.EqualTo(contrib1.RealValue + contrib2.RealValue));
            
        }

        [Test]
        public void TwoContributorsTwoContributions()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var twoMonthsAgo = DateTime.Today.AddMonths(-2);
            var alfredoContrib = new Contribution { InitialDate = lastMonth, Value = 1560, ContributionFactor = 0.78 };
            var anaFirstContrib = new Contribution { InitialDate = twoMonthsAgo, Value = 1560, ContributionFactor = 0.78 , Contributor = new Contributor { Name = "Ana" }};
            var anaSecondContrib = new Contribution { InitialDate = lastMonth, Value = 1560, ContributionFactor = 0.78, Contributor = new Contributor { Name = "Ana" } };
            InsertContribution(alfredoContrib);
            InsertContribution(anaFirstContrib);
            InsertContribution(anaSecondContrib);

            var monthBalance = business.GetInitialData(DateTime.Today.Month, DateTime.Today.Year).monthBalance;
            var expected = alfredoContrib.RealValue + anaSecondContrib.RealValue;

            Assert.That(monthBalance, Is.EqualTo(expected));
        }

        private void InsertContribution(Contribution contribution)
        {
            if (contribution.Contributor == null)
            {
                contribution.Contributor = new Contributor { Name = "Alfred" };
            }
            var contributor = contributorBusiness.GetByName(contribution.Contributor.Name);
            if (contributor != null)
            {
                contribution.Contributor = contributor;
            }
            contributionBusiness.Insert(contribution);
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
