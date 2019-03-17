using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales_And_Inventory_MIS.Models;
using Waleed_Inventory_And_Sales.App_Start;

namespace Waleed_Inventory_And_Sales.Controllers
{
    public class SupplyOrdersController : Controller
    {
        private DbContextClass db = new DbContextClass();

        public ActionResult Search(string DateFrom, string DateTo,string searchtype,string text)
        {
            if (searchtype.Contains("Filter"))
            {
                if (DateFrom == null || DateTo == null)
                {
                    var order = db.order.Include(o => o.customer);
                    return View(order.ToList());

                }
                DateTime datefrom = Convert.ToDateTime(DateFrom);
                DateTime dateto = Convert.ToDateTime(DateTo);
                var orders = (from supplyodr in db.SupplyOrders where supplyodr.supplyDate >= datefrom && supplyodr.supplyDate <= dateto select supplyodr).ToList();
                
                return View(orders);

            }
            var supplyorders= (from so in db.SupplyOrders where so.location.Contains(text) || so.order.customer.name.Contains(text) || 
                               so.order.description.Contains(text) || so.order.customer.mobile.Contains(text) select so).ToList();

            return View(supplyorders);
        }

        // GET: SupplyOrders
        public ActionResult Index()
        {
            var supplyOrders = db.SupplyOrders.Include(s => s.order);
            return View(supplyOrders.ToList());
        }

        // GET: SupplyOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOrder supplyOrder = db.SupplyOrders.Find(id);
            if (supplyOrder == null)
            {
                return HttpNotFound();
            }
            Session["orderid"] = supplyOrder.orderid;
            return RedirectToAction("Index", "SalesLineItems");

        }

        // GET: SupplyOrders/Create
        public ActionResult Create()
        {
            ViewBag.orderid = new SelectList(db.order.OrderBy(s => s.description), "id", "description");
            return View();
        }

        // POST: SupplyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,orderid,supplyDate,location,description")] SupplyOrder supplyOrder)
        {
            if (ModelState.IsValid)
            {
                db.SupplyOrders.Add(supplyOrder);
                db.SaveChanges();                

                var items = (from SalesLineItems in db.salesLineItem where SalesLineItems.orderid == supplyOrder.orderid select SalesLineItems.item).Include(a=>a.product).ToList();
                List<SalesLineItem> sitems = (from SalesLineItems in db.salesLineItem where SalesLineItems.orderid == supplyOrder.orderid select SalesLineItems).ToList();

                int quantitycheck = 0;

                var itemShellsSupply = Switch.idsToSwitch;

                foreach (var itm in items)
                {
                    if (itemShellsSupply.Count == 0)
                    {
                        itm.quantity = itm.quantity - sitems.First().quantity;
                    }
                    else
                    {
                        Util.SellShell(itm, sitems.First().quantity, db);
                    }
                    if (itm.quantity < 0)
                    {
                        quantitycheck++;
                    }
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    sitems.RemoveAt(0);
                }
                if ( quantitycheck> 0)
                {
                    return new JavaScriptResult { Script = "alert('Successfully registered');" };
                    //var script = "alert('Some Items Quantity goes negative');";
                    //return JavaScript(script);
                }

                return RedirectToAction("Index");
            }

            ViewBag.orderid = new SelectList(db.order, "id", "description", supplyOrder.orderid);
            return View(supplyOrder);
        }

        // GET: SupplyOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOrder supplyOrder = db.SupplyOrders.Find(id);
            if (supplyOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.orderid = new SelectList(db.order, "id", "description", supplyOrder.orderid);
            return View(supplyOrder);
        }

        // POST: SupplyOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,orderid,supplyDate,location,description")] SupplyOrder supplyOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplyOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.orderid = new SelectList(db.order, "id", "description", supplyOrder.orderid);
            return View(supplyOrder);
        }

        // GET: SupplyOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOrder supplyOrder = db.SupplyOrders.Find(id);
            if (supplyOrder == null)
            {
                return HttpNotFound();
            }
            return View(supplyOrder);
        }

        // POST: SupplyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplyOrder supplyOrder = db.SupplyOrders.Find(id);
            db.SupplyOrders.Remove(supplyOrder);
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
