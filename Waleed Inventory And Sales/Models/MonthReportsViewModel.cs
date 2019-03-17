using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Waleed_Inventory_And_Sales.Models
{
    public class MonthReportsViewModel
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Autoid { get; set; }
        public String month { get; set; }
        public int sales { get; set; }
        public int cashSales { get; set;}
        public int cashPayments { get; set; }

    }
}