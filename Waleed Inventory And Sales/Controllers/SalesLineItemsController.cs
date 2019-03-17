using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales_And_Inventory_MIS.Models;

namespace Waleed_Inventory_And_Sales.Controllers
{
    public class SalesLineItemsController : Controller
    {
        private DbContextClass db = new DbContextClass();

        public ActionResult DisplayfoOrder(int? id)
        {
            return View();
        }
        // GET: SalesLineItems
        public ActionResult Index(int? id)
        {
            if (Session["orderid"]==null)
            {
                var salesLineItem = db.salesLineItem.Include(s => s.item).Include(s => s.order);
                return View(salesLineItem.ToList());
            }
            else if(Session["orderid"]!=null)
            {
                int orderid = Convert.ToInt32(Session["orderid"].ToString());
                List<SalesLineItem> salelineitems = (from sli in db.salesLineItem
                                                 where sli.order.id == orderid
                                                 select sli).Include(s => s.order).Include(s => s.item).ToList();
                int total = 0;
                foreach(var sli in salelineitems)
                {
                    total=sli.price + total;
                }

                var order1 = (from order in db.order where order.id == orderid select order).FirstOrDefault();
                order1.total = total;

                db.Entry(order1).State = EntityState.Modified;
                db.SaveChanges();

                if(salelineitems.Count<1)
                {
                    ViewBag.CustomerName = "";
                    ViewBag.OrderDate = "";
                    ViewBag.Total = "";

                }
                else
                {
                    ViewBag.CustomerName = salelineitems[0].order.customer.name;
                    ViewBag.OrderDate = salelineitems[0].order.orderDate;
                    ViewBag.Total = total;

                }

                return View(salelineitems);
            }
            return View();
        }

        // GET: SalesLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = db.salesLineItem.Find(id);
            if (salesLineItem == null)
            {
                return HttpNotFound();
            }
            return View(salesLineItem);
        }


        // GET: SalesLineItems/Create
        public ActionResult Create()
        {
            ViewBag.itemid = new SelectList(db.item.OrderBy(s => s.name), "id", "name");
            //ViewBag.orderid = new SelectList(db.order, "id", "id");
            return View();
        }

        // POST: SalesLineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,itemid,quantity,price")] SalesLineItem salesLineItem,string shellsOrder)
        {
            if (ModelState.IsValid)
            {
                salesLineItem.orderid = Convert.ToInt32(Session["orderid"]);
                db.salesLineItem.Add(salesLineItem);
                db.SaveChanges();

                salesLineItem.price = salesLineItem.quantity * (from d in db.salesLineItem where d.id == salesLineItem.id select d.item.product.price).FirstOrDefault();                
                db.Entry(salesLineItem).State = EntityState.Modified;
                db.SaveChanges();

                if (shellsOrder != null)
                {
                    Session["shellsOrder"] = shellsOrder;
                    Switch.idsToSwitch.Add(salesLineItem.itemid);
 
                }
                var sli2 = (List<int>)(Session["ListShellsItem"]);

                return RedirectToAction("Index", "SalesLineItems");
            }

            ViewBag.itemid = new SelectList(db.item, "id", "name", salesLineItem.itemid);
            ViewBag.orderid = new SelectList(db.order, "id", "id", salesLineItem.orderid);
            return View(salesLineItem);
        }

        // GET: SalesLineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = db.salesLineItem.Find(id);
            if (salesLineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.itemid = new SelectList(db.item, "id", "name", salesLineItem.itemid);
            ViewBag.orderid = new SelectList(db.order, "id", "id", salesLineItem.orderid);
            return View(salesLineItem);
        }

        // POST: SalesLineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,orderid,itemid,quantity,price")] SalesLineItem salesLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesLineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.itemid = new SelectList(db.item, "id", "name", salesLineItem.itemid);
            ViewBag.orderid = new SelectList(db.order, "id", "id", salesLineItem.orderid);
            return View(salesLineItem);
        }

        // GET: SalesLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = db.salesLineItem.Find(id);
            if (salesLineItem == null)
            {
                return HttpNotFound();
            }
            return View(salesLineItem);
        }

        // POST: SalesLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesLineItem salesLineItem = db.salesLineItem.Find(id);
            db.salesLineItem.Remove(salesLineItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
