using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMVC.Model.Entity
{
    public class EntityWithCreationDate : BudgetEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }
    }
}
