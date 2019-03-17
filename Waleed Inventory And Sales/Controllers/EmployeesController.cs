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
    public class EmployeesController : Controller
    {
        private DbContextClass db = new DbContextClass();

        public Entity getEntityId(string name)
        {
            var entity = (from entit in db.entity where entit.name.Equals(name) select entit).FirstOrDefault();
            if(entity==null)
            {
                entity = new Entity();
                entity.name = name;
                db.entity.Add(entity);
                db.SaveChanges();        

            }
            return entity;
        }
        public ExpenseType getTypeId(string name)
        {
            var entity = (from entit in db.expenseType where entit.name.Equals(name) select entit).FirstOrDefault();
            if (entity == null)
            {
                entity = new ExpenseType();
                entity.name = name;
                db.expenseType.Add(entity);
                db.SaveChanges();

            }
            return entity;
        }
        public ActionResult Pay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees
        public ActionResult Payment(string Ammount,string name)
        {
            Expense expense = new Expense();
            expense.Ammount = Convert.ToInt32(Ammount);
            expense.Date = DateTime.Now;
            expense.entityId = getEntityId(name).id;
            expense.typeId = getTypeId("Employee").id;
            
            db.expense.Add(expense);
            db.SaveChanges();

            var emp = (from employe in db.Employees where employe.name.Equals(name) select employe).FirstOrDefault();
            int amunt = Convert.ToInt32(Ammount);

            emp.Ammount = emp.Ammount - amunt;
            db.Entry(emp).State = EntityState.Modified;
            db.SaveChanges();


            Global.Money = Global.Money - expense.Ammount;
            return RedirectToAction("Index");
        }
 
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }
        public ActionResult Items(int? id)
        {
            Session["emp_id"] = id;
            var mkitems = (from mkitem in db.makeItem where mkitem.employeeid == id select mkitem).ToList();
            return View(mkitems);
        }
        public ActionResult Filter(string DateFrom,string DateTo)
        {
            DateTime datefrom = Convert.ToDateTime(DateFrom);
            DateTime dateto = Convert.ToDateTime(DateTo);

            int id = Convert.ToInt32((Session["emp_id"]).ToString());
            var mkitems = (from mkitem in db.makeItem where mkitem.employeeid == id && mkitem.Date>=datefrom && mkitem.Date<=dateto select mkitem).ToList();
            return View(mkitems);

            //return JavaScript("alert('some Message')");
            //var mkitems = (from mkitem in db.makeItem where mkitem.employeeid == id select mkitem).ToList();
            //return View(mkitems);
        }


        public ActionResult Search()
        {
            return View(db.Employees.ToList());
        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,Ammount")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,Ammount")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
