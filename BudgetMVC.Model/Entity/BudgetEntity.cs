using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BudgetMVC.Model.Entity
{
    public class BudgetEntity
    {
        [Key]
        public long ID { get; set; }
    }
}
