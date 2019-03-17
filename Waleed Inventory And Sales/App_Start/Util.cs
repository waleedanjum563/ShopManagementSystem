using Sales_And_Inventory_MIS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Waleed_Inventory_And_Sales.App_Start
{
    public class Util
    {
        public static Boolean updateShellQuantity(Product product, int quantity, DbContextClass db)
        {
            product.shell_quantity = product.shell_quantity - quantity;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return true;

        }
        public static List<Product>  getAllShellsProduct()
        {
            DbContextClass db = new DbContextClass();

            var prdts = db.product.Where(pr => pr.Acrylic.Equals(true) ).ToList();
            var distinctProducts = new List<Product>();

            foreach(var prdt in prdts)
            {
                if (prdt.name.Count() > 10)
                    prdt.name = prdt.name.Remove(10, prdt.name.Count() - 10);
                var dist = distinctProducts.Where(distinct => distinct.name.Equals(prdt.name));
                if(dist==null||dist.Count()==0)
                {
                    distinctProducts.Add(prdt);
                }
            }
            return distinctProducts;
        }
        public static void SellShell(Item item, int quantity, DbContextClass db)
        {
            var itemSell = (from itm in db.item where itm.id == item.id select itm).FirstOrDefault();
            var productSell = (from prdt in db.product where prdt.id == item.productid select prdt).FirstOrDefault();
            //shell
            if (itemSell.product.Acrylic == true)
            {
                var shellitem = itemSell.product;
                {
                    Util.updateShellQuantity(productSell, quantity, db);

                }
                if (item.name.ToLower().Contains("right"))
                {
                    string namel = productSell.name.Remove(productSell.name.ToLower().IndexOf("right"));
                    var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productSell.id select prdt).ToList();
                    if (pr != null)
                    {
                        foreach (var p in pr)
                        {
                            updateShellQuantity(p, quantity, db);

                        }
                    }
                }
                else if (item.name.ToLower().Contains("left"))
                {
                    string namel = item.name.Remove(item.name.ToLower().IndexOf("left"));
                    var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productSell.id select prdt).ToList();

                    if (pr != null)
                    {
                        foreach (var p in pr)
                        {
                            updateShellQuantity(p, quantity, db);
                        }
                    }
                }
                //************************************
                else if (item.name.ToLower().Contains("simple"))
                {
                    string namel = item.product.name.Remove(item.name.IndexOf("simple"));
                    var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productSell.id select prdt).ToList();

                    if (pr != null)
                    {
                        foreach (var p in pr)
                        {
                            updateShellQuantity(p, quantity, db);

                        }
                    }
                }
                else if (item.name.ToLower().Contains("border"))
                {
                    string namel = item.name.Remove(item.name.ToLower().IndexOf("border"));
                    var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productSell.id select prdt).ToList();

                    if (pr != null)
                    {
                        foreach (var p in pr)
                        {
                            updateShellQuantity(p, quantity, db);

                        }
                    }
                }

            }
        }
    }
}