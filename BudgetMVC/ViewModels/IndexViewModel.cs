using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BudgetMVC.ViewModels
{
    public class IndexViewModel
    {
        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}