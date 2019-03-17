using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Waleed_Inventory_And_Sales.Models;

namespace Sales_And_Inventory_MIS.Models
{
    public class Payment
    {
        [Key]
        public int id { get; set; }
        public Customer customer { get; set; }
        public int customerid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date{ get; set; }
        public string description { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Ammount { get; set; }

    }
}