using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Waleed_Inventory_And_Sales.Models;

namespace Sales_And_Inventory_MIS.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Please Enter Value for this")]
        public string name{ get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        public string Location{ get; set; }

        public string Email{ get; set; }

        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Wrong mobile")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "Required!")]
        public string company{ get; set; }

        [Column(TypeName = "Money")]
         public int ammount { get; set; }
        public ICollection<Transaction> Transactions{ get; set; }

    }
}