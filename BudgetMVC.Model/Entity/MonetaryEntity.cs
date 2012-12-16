using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetMVC.Model.Entity
{
    public class MonetaryEntity : EntityWithCreationDate
    {
        public string Description { get; set; }
        public double Value { get; set; }
    }
}
