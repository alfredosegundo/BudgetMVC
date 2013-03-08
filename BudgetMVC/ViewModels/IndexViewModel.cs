using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BudgetMVC.Model.Entity;

namespace BudgetMVC.ViewModels
{
    public class IndexViewModel
    {
        public ModalTemplateViewModel ExpensesModalConfig = new ModalTemplateViewModel { EntityName = typeof(Expense).Name, Date = DateTime.Now };
        public ModalTemplateViewModel RevenuesModalConfig = new ModalTemplateViewModel { EntityName = typeof(Revenue).Name, Date = DateTime.Now };

        public ContributionViewModel ContributionViewModel = new ContributionViewModel();
    }
}