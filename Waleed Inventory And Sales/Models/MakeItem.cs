using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sales_And_Inventory_MIS.Models
{
    public class MakeItem
    {
        [Key]
        public int id { get; set; }

        public virtual Item item { get; set; }
       public int itemid { get; set; }
       public string description { get; set; }
      
        [Required]
       [DataType(DataType.Date)]
       [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
       public DateTime Date{ get; set; }
        
       public virtual Employee employee{ get; set; }
       public int employeeid{ get; set; }


       [Required(ErrorMessage = "Please Enter Value for this")]
       public int quantity { get; set; }
       public Boolean  paid{ get; set; }
 
 
    }
}