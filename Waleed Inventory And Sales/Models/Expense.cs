using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;



namespace Sales_And_Inventory_MIS.Models
{
    public class Expense
    {
        [Key]
        public int id { get; set; }
        public virtual ExpenseType type { get; set; }
        public int  typeId { get; set; }
        public virtual Entity entity { get; set; }
        public int entityId{ get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string description { get; set; }
        public int Ammount { get; set; }
    }
}