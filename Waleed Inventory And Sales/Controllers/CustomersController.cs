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
    public class CustomersController : Controller
    {
        private DbContextClass db = new DbContextClass();

        // GET: Customers
        public ActionResult Payments(int? id)
        {
            List<Payment> payments = (from pay in db.Payments where pay.customerid == id select pay).Include(p => p.customer).ToList();
            return View(payments);
        }

        // GET: Customers
        public ActionResult CustomerHistory(int? id)
        {
            Session["CustomerId"] = id;

            Transaction ch = new Transaction();
            ch.id = 1;

            //db.customerHistory.Add(ch);
            return View();
        }

        public ActionResult Index()
        {
            Material.readFromFile();
            return View(db.Customers.ToList());
        }

        public ActionResult Search(string name)
        {
            if(name=="")
            {
                return View();
            }

            var customers = (from customer in db.Customers where customer.name.Contains(name) || customer.mobile.Contains(name) || customer.company.Contains(name) || customer.Email.Contains(name) || customer.Location.Contains(name) select customer).ToList();
            //List<Customer> cust = new List<Customer>();
            //foreach(var customer in customers)
            //{
            //    cust.Add(customer);
            //}    
            return View(customers);
        }

        

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,Location,Email,mobile,company,ammount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,Location,Email,mobile,company,ammount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
