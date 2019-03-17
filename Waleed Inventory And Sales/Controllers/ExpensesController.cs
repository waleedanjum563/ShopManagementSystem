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
    public class ExpensesController : Controller
    {
        public ActionResult SearchP()
        {
            return View();
        }
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
                var expenses = (from expns in db.expense where expns.Date >= datefrom && expns.Date <= dateto select expns).ToList();

                return View(expenses);

            }
            else if (searchtype.Contains("search"))
            {
                var expn = (from expnse in db.expense
                            where expnse.description.Contains(text) || expnse.type.name.Contains(text)
                            || expnse.entity.name.Contains(text)
                            select expnse).ToList();

                return View(expn);
            }
            else if (searchtype.Contains("entitydate"))
            {
                DateTime datefrom = Convert.ToDateTime(DateFrom);
                DateTime dateto = Convert.ToDateTime(DateTo);

                var expn = (from expnse in db.expense
                            where expnse.entity.name.Contains(text) && expnse.Date >= datefrom && expnse.Date <= dateto
                            select expnse).ToList();

                return View(expn);
            }
            else if (searchtype.Contains("typedate"))
            {
                DateTime datefrom = Convert.ToDateTime(DateFrom);
                DateTime dateto = Convert.ToDateTime(DateTo);

                var expn = (from expnse in db.expense
                            where expnse.entity.name.Contains(text) && expnse.Date >= datefrom && expnse.Date <= dateto
                            select expnse).ToList();

                return View(expn);
            }
            else if(searchtype.Contains("type"))
            {
                var expn = (from expnse in db.expense
                            where expnse.type.name.Contains(text)
                            select expnse).ToList();

                return View(expn);
            }
            else if (searchtype.Contains("entity"))
            {
                var expn = (from expnse in db.expense
                            where expnse.entity.name.Contains(text)
                            select expnse).ToList();

                return View(expn);
            }
            return View();
        }

        private DbContextClass db = new DbContextClass();

        // GET: Expenses
        public ActionResult Index()
        {
            var expense = db.expense.Include(e => e.entity).Include(e => e.type);
            return View(expense.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.expense.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.entityId = new SelectList(db.entity.OrderBy(s => s.name), "id", "name");
            ViewBag.typeId = new SelectList(db.expenseType.OrderBy(s => s.name), "id", "name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,typeId,entityId,Date,description,Ammount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.expense.Add(expense);
                db.SaveChanges();

                Global.Money = Global.Money - expense.Ammount;
                return RedirectToAction("Index");
            }

            ViewBag.entityId = new SelectList(db.entity, "id", "name", expense.entityId);
            ViewBag.typeId = new SelectList(db.expenseType, "id", "name", expense.typeId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.expense.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.entityId = new SelectList(db.entity, "id", "name", expense.entityId);
            ViewBag.typeId = new SelectList(db.expenseType, "id", "name", expense.typeId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,typeId,entityId,Date,description,Ammount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.entityId = new SelectList(db.entity, "id", "name", expense.entityId);
            ViewBag.typeId = new SelectList(db.expenseType, "id", "name", expense.typeId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.expense.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.expense.Find(id);
            db.expense.Remove(expense);
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
