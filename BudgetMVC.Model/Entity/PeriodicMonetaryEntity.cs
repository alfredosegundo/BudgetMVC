using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetMVC.Model.Entity
{
    public class PeriodicMonetaryEntity : MonetaryEntity
    {
        public DateTime FirstPayment { get; set; }
        public uint Cycles { get; set; }
    }
}
