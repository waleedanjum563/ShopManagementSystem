using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Sales_And_Inventory_MIS.Models
{
    public class Item
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public virtual Product product { get; set; }
        public int productid { get; set; }

        public string description{ get; set; }
        
        public virtual Colour colour{ get; set; }
        public int colourid { get; set; }

        
        [Required(ErrorMessage = "Please Enter Value for this")]
        public int quantity { get; set; }
       
    }
}