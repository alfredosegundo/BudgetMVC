using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetMVC.Model.Entity.Enum;

namespace BudgetMVC.Model.Entity
{
    public class PeriodicMonetaryEntity : MonetaryEntity
    {
        public DateTime FirstEvent { get; set; }
        public DateTime FinalEvent { get; set; }
        public Periodicity Periodicity { get; set; }
    }
}
