using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.Entity.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMVC.Model.Entity
{
    public class PeriodicMonetaryEntity : MonetaryEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime FirstEvent { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FinalEvent { get; set; }
    }
}
