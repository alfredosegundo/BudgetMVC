using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BudgetMVC.Model.Entity
{
    public class EntityWithCreationDate : BudgetEntity
    {
        public DateTime CreationDate { get; set; }
    }
}
