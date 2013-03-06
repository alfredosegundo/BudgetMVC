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
        [Test]
        public void ExistOnlyOneValidContributionPerContributor()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var contribution1 = new Contribution { InitialDate = lastMonth, Value = 100, ContributionFactor = 0.5 };
            var contribution2 = new Contribution { InitialDate = lastMonth, Value = 100, ContributionFactor = 0.5 };
            InsertContribution(contribution1);
            InsertContribution(contribution2);

            var contributions = contributionBusiness.GetCurrentContributions(DateTime.Today.Month, DateTime.Today.Year);

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
