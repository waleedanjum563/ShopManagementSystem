using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sales_And_Inventory_MIS.Models;

namespace Waleed_Inventory_And_Sales.Models
{
    public enum TransactionType
    {
        Credit,
        Debit,
    }
    public class Transaction
    {
        [Key]
        public int id { get; set; }
        public virtual Customer Customer { get; set; }
        public int  Customerid { get; set; }
        public virtual Order order { get; set; }
        public int? Orderid { get; set; }
        public virtual Payment Payment { get; set; }
        public int? Paymentid { get; set; }
        public int Ammount { get; set; }
        public TransactionType type { get; set; }
        public int Balance{ get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}