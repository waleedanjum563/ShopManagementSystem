using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;



namespace Sales_And_Inventory_MIS.Models
{
    public class Entity
    {
        [Key]
        public int id {get; set; }

        public string name { get; set; }
        public string description { get; set; }


    }
}