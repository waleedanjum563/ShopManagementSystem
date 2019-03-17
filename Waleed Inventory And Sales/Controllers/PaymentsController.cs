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
    public class PaymentsController : Controller
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
                var pymnts = (from payments in db.Payments where payments.Date >= datefrom && payments.Date <= dateto select payments).ToList();

                return View(pymnts);

            }
            var payment = (from pymnts in db.Payments
                           where pymnts.description.Contains(text) || pymnts.customer.name.Contains(text)
                           || pymnts.customer.mobile.Contains(text) || pymnts.customer.company.Contains(text)
                                select pymnts).ToList();

            return View(payment);
        }

        //**************

        //****************


        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.customer);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.customerid = new SelectList(db.Customers.OrderBy(s => s.name), "id", "name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,customerid,Date,description,Ammount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();

                var customer = (from cust in db.Customers where cust.id == payment.customerid select cust).FirstOrDefault();
                customer.ammount = customer.ammount - payment.Ammount;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                Global.Money = Global.Money + payment.Ammount;

                Transaction transaction = new Transaction();
                transaction.Ammount = payment.Ammount;
                transaction.Balance = customer.ammount;
                transaction.Date = payment.Date;
                transaction.Description = "";
                transaction.Payment = payment;
                transaction.Paymentid = payment.id;
                transaction.Customer = customer;
                transaction.Customerid = customer.id;
                transaction.type = TransactionType.Debit;

                db.Transactions.Add(transaction);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.customerid = new SelectList(db.Customers, "id", "name", payment.customerid);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerid = new SelectList(db.Customers, "id", "name", payment.customerid);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customerid,Date,description,Ammount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerid = new SelectList(db.Customers, "id", "name", payment.customerid);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
