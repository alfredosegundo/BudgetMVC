using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetMVC.Model.Entity
{
    public class Contribution : MonetaryEntity
    {
        public Contributor Contributor { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
