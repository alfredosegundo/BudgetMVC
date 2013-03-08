using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BudgetMVC.Model.Entity;
using System.Web.Mvc;

namespace BudgetMVC.ViewModels
{
    public class ContributionViewModel
    {
        public Contributor Contributor { get; set; }
        public double ContributionFactor { get; set; }
        public double Value { get; set; }
        public DateTime InitialDate { get; set; }
        public string Description { get; set; }

        public IEnumerable<SelectListItem> Contributors { get; set; }
    }
}