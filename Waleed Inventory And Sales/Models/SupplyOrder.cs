using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Sales_And_Inventory_MIS.Models
{
    public class SupplyOrder
    {
        [Key]
        public int id{ get; set; }
        public virtual Order order { set; get; }
        public int orderid { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime supplyDate{ get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public ICollection<SalesLineItem> salelineitem { get; set; }

    }
}