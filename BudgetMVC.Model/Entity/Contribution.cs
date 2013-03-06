using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMVC.Model.Entity
{
    public class Contribution : MonetaryEntity
    {
        public Contributor Contributor { get; set; }
        public double ContributionFactor { get; set; }
        public double RealValue
        {
            get
            {
                return Value * ContributionFactor;
            }
        }
        public DateTime InitialDate { get; set; }
    }
}
