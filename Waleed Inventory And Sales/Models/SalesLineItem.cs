using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Sales_And_Inventory_MIS.Models;

namespace Sales_And_Inventory_MIS.Models
{
    public class SalesLineItem
    {
        public virtual Order order{ get; set; }
        public int orderid{ get; set; }

        [Key]
        public int id { get; set; }

        public virtual Item item { get; set; }
        public int itemid{ get; set; }

        public int quantity{ get; set; }
        public int price
        {
            set;
            get;
        }


    }
}