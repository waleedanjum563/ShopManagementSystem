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
    public class OrdersController : Controller
    {
        private DbContextClass db = new DbContextClass();

        // GET: Orders

        public ActionResult Search(string DateFrom, string DateTo)
        {
            if(DateFrom==null || DateTo==null)
            {
                var order = db.order.Include(o => o.customer).Include(o=>o.salelineitem);
                    return View(order.ToList());

            }
            DateTime datefrom = Convert.ToDateTime(DateFrom);
            DateTime dateto = Convert.ToDateTime(DateTo);

            var orders = (from ordr in db.order where ordr.orderDate>=datefrom && ordr.orderDate<=dateto select ordr).Include(s=>s.salelineitem).ToList();
            return View(orders);
        }

        public ActionResult SearchOrderById(string billno)
        {

            var orders = (from ordr in db.order where ordr.description.Contains(billno) select ordr).ToList();
            return View(orders);
        }


        public ActionResult Index()
        {
            var order = db.order.Include(o => o.customer).OrderBy(o => o.orderDate);
            return View(order.ToList());
        }
        public ActionResult DisplayOrders(int choice,int? id)
        {
            if (choice == 1)
            {
                var orderr = db.order.Include(o => o.customer).Where(o=>o.customerid==id);
                return View(orderr.ToList());

                //var order1 = db.order.Include(o => o.customer);

                //List<Order> idd = (from odr in order1 select odr).ToList();

                //Customer customer = db.Customers.Find(id);
                //var orderr = (from odr in db.order where odr.customerid == 1 select odr);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult BillsForCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("DisplayOrders", new { choice= 1, id=id }); 
 
//            var order = db.order.Include(o => o.customer);
//            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            Session["orderid"] = id;
            return RedirectToAction( "Index", "SalesLineItems");

        }

        public ActionResult AddItems(int? id)
        {
            return RedirectToAction("Create", "SalesLineItems");
        }
        public ActionResult ShowItems(int? id)
        {
            Session["orderid"] = id;
            return RedirectToAction("Index", "SalesLineItems");
        }
        public ActionResult Finish()
        {
            int orderid = int.Parse(Session["orderid"].ToString());
            
            var customer = (from orders in db.order where orders.id == orderid select orders.customer).FirstOrDefault();
            var order = (from orders in db.order where orders.id == orderid select orders).FirstOrDefault();

            customer.ammount = customer.ammount + order.total;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();

            //Order Transaction Added
            Transaction transaction = new Transaction();
            transaction.Ammount = order.total;
            transaction.Balance = customer.ammount;
            transaction.Date = order.orderDate;
            transaction.Description = "";
            transaction.order = order;
            transaction.Orderid = order.id;
            transaction.Customer = customer;
            transaction.Customerid = customer.id;
            transaction.type = TransactionType.Credit;

            db.Transactions.Add(transaction);
            db.SaveChanges();

            if (order.paid.Equals(true))
            {
                SupplyOrder supply = new SupplyOrder();
                supply.description = order.description;
                supply.location = "Shop";
                supply.orderid = order.id;
                supply.supplyDate = order.orderDate;
                SupplyOrdersController s = new SupplyOrdersController();
                s.Create(supply);

                Payment payment = new Payment();
                payment.customerid = order.customerid;
                payment.Date = order.orderDate;
                payment.Ammount = order.total;
                payment.description = order.description;

                PaymentsController pay = new PaymentsController();
                pay.Create(payment);
            }
            else
            {
                if (Session["Supply"].ToString() == "on")
                {
                    SupplyOrder supply = new SupplyOrder();
                    supply.description = order.description;
                    supply.location = "Shop";
                    supply.orderid = order.id;
                    supply.supplyDate = order.orderDate;

                    SupplyOrdersController s = new SupplyOrdersController();
                    s.Create(supply);

                }
                if (Session["Payment"].ToString() == "on")
                {
                    Payment payment = new Payment();
                    payment.customerid = order.customerid;
                    payment.Date = order.orderDate;
                    payment.Ammount = order.total;
                    payment.description = order.description;

                    PaymentsController pay = new PaymentsController();
                    pay.Create(payment);
                }
            }
            ///Transaction
            return RedirectToAction("Index", "Customers");
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.customerid = new SelectList(db.Customers.OrderBy(s=>s.name), "id", "name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,orderDate,description,customerid,paid")] Order order,string Supply,string Payment,string shellsOrder)
        {

            if (ModelState.IsValid)
            {
                Switch.idsToSwitch = new List<int>();
                order.total = 0;
                db.order.Add(order);
                db.SaveChanges();
                Session["orderid"] = order.id;
                if (Supply == null)
                    Session["Supply"] = "off";
                else
                    Session["Supply"] = Supply;
                if(Payment==null)
                    Session["Payment"] = "off";
                else
                    Session["Payment"] = Payment;

               return RedirectToAction("Index");
            }

            ViewBag.customerid = new SelectList(db.Customers, "id", "name", order.customerid);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerid = new SelectList(db.Customers, "id", "name", order.customerid);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,total,orderDate,customerid")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerid = new SelectList(db.Customers, "id", "name", order.customerid);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.order.Find(id);
            db.order.Remove(order);
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
