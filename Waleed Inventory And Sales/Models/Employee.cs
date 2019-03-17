using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sales_And_Inventory_MIS.Models
{
    public class Employee
    {
        public int id { get; set; }
        
        [Required(ErrorMessage = "Please Enter Value for this")]
        public string name { get; set; }

        public int Ammount{ get; set; }

    }
}