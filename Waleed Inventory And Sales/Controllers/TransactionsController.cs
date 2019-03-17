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
    public class TransactionsController : Controller
    {
        private DbContextClass db = new DbContextClass();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Customer).Include(t => t.order).Include(t => t.Payment);
            return View(transactions.ToList());
        }

        // GET: Transactions
        public ActionResult CustomerTransactions(int? Customerid)
        {
            if(Customerid==null)
            {
                Session["Customer"] = null;
                Session["Search"] = true;
                return View(db.Transactions.Include(Trans=>Trans.Customer).OrderByDescending(o => o.Date).ToList());
            }
            Session["Search"] = null;
            Session["Customer"] = db.Customers.Where(cust => cust.id == Customerid).FirstOrDefault();
            List<Transaction> transactions = db.Transactions.Where(cust=>cust.Customerid==Customerid).Include(t => t.Customer).OrderByDescending(o=>o.Date).ToList();
            return View(transactions);
        }
        // GET: Transactions
        public ActionResult Search(string DateFrom, string DateTo)
        {
            if (DateFrom == null || DateTo == null)
            {
                return View(db.Transactions.Include(Trans => Trans.Customer).OrderByDescending(o=>o.Date).ToList());
            }
            DateTime datefrom = Convert.ToDateTime(DateFrom);
            DateTime dateto = Convert.ToDateTime(DateTo);

            Session["Customer"] = null;
            Session["Search"] = true;
            Session["Detailed"] = true;

            List<Transaction> transactions = db.Transactions.Where(trans => trans.Date >= datefrom && trans.Date<=dateto).Include(t => t.Customer).Include(trans => trans.Payment).Include(trans=>trans.order).Include(trans=>trans.order.salelineitem).OrderByDescending(o => o.Date).ToList();

            return View(transactions);
        }


        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.Customerid = new SelectList(db.Customers, "id", "name");
            ViewBag.Orderid = new SelectList(db.order, "id", "description");
            ViewBag.Paymentid = new SelectList(db.Payments, "id", "description");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Customerid,Orderid,Paymentid,Ammount,type,Balance,Description,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customerid = new SelectList(db.Customers, "id", "name", transaction.Customerid);
            ViewBag.Orderid = new SelectList(db.order, "id", "description", transaction.Orderid);
            ViewBag.Paymentid = new SelectList(db.Payments, "id", "description", transaction.Paymentid);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customerid = new SelectList(db.Customers, "id", "name", transaction.Customerid);
            ViewBag.Orderid = new SelectList(db.order, "id", "description", transaction.Orderid);
            ViewBag.Paymentid = new SelectList(db.Payments, "id", "description", transaction.Paymentid);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Customerid,Orderid,Paymentid,Ammount,type,Balance,Description,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customerid = new SelectList(db.Customers, "id", "name", transaction.Customerid);
            ViewBag.Orderid = new SelectList(db.order, "id", "description", transaction.Orderid);
            ViewBag.Paymentid = new SelectList(db.Payments, "id", "description", transaction.Paymentid);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
