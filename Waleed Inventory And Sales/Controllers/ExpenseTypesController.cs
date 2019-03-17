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
    public class ExpenseTypesController : Controller
    {
        private DbContextClass db = new DbContextClass();

        // GET: ExpenseTypes
        public ActionResult Index()
        {
            return View(db.expenseType.ToList());
        }

        // GET: ExpenseTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseType expenseType = db.expenseType.Find(id);
            if (expenseType == null)
            {
                return HttpNotFound();
            }
            return View(expenseType);
        }

        // GET: ExpenseTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                db.expenseType.Add(expenseType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseType);
        }

        // GET: ExpenseTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseType expenseType = db.expenseType.Find(id);
            if (expenseType == null)
            {
                return HttpNotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description")] ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseType);
        }

        // GET: ExpenseTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseType expenseType = db.expenseType.Find(id);
            if (expenseType == null)
            {
                return HttpNotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpenseType expenseType = db.expenseType.Find(id);
            db.expenseType.Remove(expenseType);
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
