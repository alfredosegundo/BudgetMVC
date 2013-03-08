using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Tests.Business
{
    [TestFixture]
    public class ContributionBusinessTest : PersistentTest
    {
        DateTime lastMonth = DateTime.Today.AddMonths(-1);

        [Test]
        public void ValidContribution_ContributorSholdHaveOnlyOne()
        {
            var contribution1 = new Contribution { InitialDate = lastMonth, Value = 100, ContributionFactor = 0.5 };
            var contribution2 = new Contribution { InitialDate = lastMonth, Value = 100, ContributionFactor = 0.5 };
            InsertContribution(contribution1);
            InsertContribution(contribution2);

            var contributions = contributionBusiness.GetCurrentContributions(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(contributions.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ValidContribution_IsMostRecent()
        {
            var contribution1 = new Contribution { InitialDate = lastMonth, Value = 200, ContributionFactor = 0.5 };
            var contribution2 = new Contribution { InitialDate = lastMonth.AddMonths(-5), Value = 100, ContributionFactor = 0.5 };
            InsertContribution(contribution1);
            InsertContribution(contribution2);

            var contributions = contributionBusiness.GetCurrentContributions(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(contributions.First().Value, Is.EqualTo(200));
        }

        [Test]
        public void ValidContribution_IsMostRecentByContributor()
        {
            var contribution1 = new Contribution { InitialDate = lastMonth.AddMonths(-5), Value = 200, ContributionFactor = 0.5 };
            var contribution2 = new Contribution { InitialDate = lastMonth.AddMonths(-5), Value = 100, ContributionFactor = 0.5, Contributor = new Contributor { Name = "Ana" } };
            var contribution3 = new Contribution { InitialDate = lastMonth, Value = 200, ContributionFactor = 0.5 };
            var contribution4 = new Contribution { InitialDate = lastMonth, Value = 100, ContributionFactor = 0.5, Contributor = new Contributor { Name = "Ana" } };
            InsertContribution(contribution1);
            InsertContribution(contribution2);
            InsertContribution(contribution3);
            InsertContribution(contribution4);

            var contributions = contributionBusiness.GetCurrentContributions(DateTime.Today.Month, DateTime.Today.Year);

            Assert.That(contributions.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ValidContribution_SholdBeValid_InInitialDateMonth()
        {
            var currentMonth = new DateTime(2013,03,08);
            var contribution1 = new Contribution { InitialDate = currentMonth, Value = 200, ContributionFactor = 0.5 };
            InsertContribution(contribution1);

            var contributions = contributionBusiness.GetCurrentContributions(currentMonth.Month, currentMonth.Year);

            Assert.That(contributions.Count(), Is.EqualTo(1));
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
    }
}
