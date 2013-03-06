using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.Tests.Business
{
    [TestFixture]
    public class ContributorBusinessTest : PersistentTest
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ContributorUniquess()
        {
            contributorBusiness.Insert(new Contributor { Name = "Alfredo" });
            contributorBusiness.Insert(new Contributor { Name = "Alfredo" });
        }
    }
}
