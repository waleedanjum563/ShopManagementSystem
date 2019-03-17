using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales_And_Inventory_MIS.Models;
using Waleed_Inventory_And_Sales.Models;

namespace Waleed_Inventory_And_Sales.Controllers
{
    public class ReturnItemsController : Controller
    {
        private DbContextClass db = new DbContextClass();
        public ActionResult Search(string DateFrom, string DateTo, string searchtype, string text)
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
                var returnItem = (from retitm in db.ReturnItems where retitm.returnDate >= datefrom && retitm.returnDate <= dateto select retitm).ToList();

                return View(returnItem);

            }
            var ret = (from retitm in db.ReturnItems
                       where retitm.description.Contains(text) || retitm.customer.name.Contains(text)
                        || retitm.customer.mobile.Contains(text) || retitm.customer.company.Contains(text)
                        || retitm.item.name.Contains(text)
                       select retitm).ToList();

            return View(ret);
        }

        // GET: ReturnItems
        public ActionResult Index()
        {
            var returnItems = db.ReturnItems.Include(r => r.customer).Include(r => r.item);
            return View(returnItems.ToList());
        }

        // GET: ReturnItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // GET: ReturnItems/Create
        public ActionResult Create()
        {
            ViewBag.customerid = new SelectList(db.Customers.OrderBy(s => s.name), "id", "name");
            ViewBag.itemid = new SelectList(db.item.OrderBy(s => s.name), "id", "name");
            return View();
        }

        // POST: ReturnItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,customerid,itemid,returnDate,ammount,description")] ReturnItem returnItem)
        {
            if (ModelState.IsValid)
            {
                db.ReturnItems.Add(returnItem);
                db.SaveChanges();

                var itm = (from item in db.item where item.id == returnItem.itemid select item).FirstOrDefault();
                Global.Money = Global.Money-returnItem.ammount;
                Customer customer = db.Customers.Where(cus => cus.id == returnItem.customerid).FirstOrDefault();
                customer.ammount = customer.ammount - returnItem.ammount;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                Transaction transaction = new Transaction();
                transaction.Ammount = returnItem.ammount;
                transaction.Balance = customer.ammount;
                transaction.Date = returnItem.returnDate;
                transaction.Description = "Returned";
                transaction.Customer = customer;
                transaction.Customerid = customer.id;
                transaction.type = TransactionType.Debit;

                db.Transactions.Add(transaction);
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.customerid = new SelectList(db.Customers, "id", "name", returnItem.customerid);
            ViewBag.itemid = new SelectList(db.item, "id", "name", returnItem.itemid);
            return View(returnItem);
        }

        // GET: ReturnItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerid = new SelectList(db.Customers, "id", "name", returnItem.customerid);
            ViewBag.itemid = new SelectList(db.item, "id", "name", returnItem.itemid);
            return View(returnItem);
        }

        // POST: ReturnItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customerid,itemid,returnDate,ammount,description")] ReturnItem returnItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerid = new SelectList(db.Customers, "id", "name", returnItem.customerid);
            ViewBag.itemid = new SelectList(db.item, "id", "name", returnItem.itemid);
            return View(returnItem);
        }

        // GET: ReturnItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnItem returnItem = db.ReturnItems.Find(id);
            if (returnItem == null)
            {
                return HttpNotFound();
            }
            return View(returnItem);
        }

        // POST: ReturnItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReturnItem returnItem = db.ReturnItems.Find(id);
            db.ReturnItems.Remove(returnItem);
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
