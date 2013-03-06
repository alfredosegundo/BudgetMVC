using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BudgetMVC.ViewModels
{
    public class ModalTemplateViewModel
    {
        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string EntityName { get; set; }
    }
}