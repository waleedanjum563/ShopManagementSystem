using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sales_And_Inventory_MIS.Models

{
    public class ReturnItem
    {
        public virtual Customer customer { get; set; }
        public int customerid { get; set; }
        public int id { get; set; }
        public virtual Item item { get; set; }
        public int itemid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime returnDate{ get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0")]        
        public int ammount { get; set; }
        public string  description{ get; set; }


    }
}