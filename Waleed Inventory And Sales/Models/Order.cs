using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sales_And_Inventory_MIS.Models;
using Waleed_Inventory_And_Sales.Models;

namespace Sales_And_Inventory_MIS.Models
{
    public class Order
    {
        [Key]
        public int id { set; get; }
        
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0")]        
        public int total{
            get;
            set; }
        
        public Boolean paid { get; set; }
        public string description{ get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime orderDate{ get; set; }
        public virtual Customer customer { get; set; }
        public int customerid { get; set; }
        public ICollection<SalesLineItem> salelineitem { get; set; }

    }
}