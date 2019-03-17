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
    public class MakeItemsController : Controller
    {
        public ActionResult Search(string DateFrom, string DateTo, string searchtype, string text)
        {
                if (DateFrom == null || DateTo == null)
                {
                    var order = db.order.Include(o => o.customer);
                    return View(order.ToList());

                }
                DateTime datefrom = Convert.ToDateTime(DateFrom);
                DateTime dateto = Convert.ToDateTime(DateTo);
                var orders = (from mkitms in db.makeItem where mkitms.Date >= datefrom && mkitms.Date <= dateto select mkitms).ToList();

                return View(orders);


        }

        private DbContextClass db = new DbContextClass();

        // GET: MakeItems
        public ActionResult Index()
        {
            Material.readFromFile();
            var makeItem = db.makeItem.Include(m => m.employee).Include(m => m.item);
            return View(makeItem.ToList());
        }

        // GET: MakeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakeItem makeItem = db.makeItem.Find(id);
            if (makeItem == null)
            {
                return HttpNotFound();
            }
            return View(makeItem);
        }

        // GET: MakeItems/Create
        public ActionResult Create()
        {
            ViewBag.employeeid = new SelectList(db.Employees.OrderBy(s => s.name), "id", "name");
            ViewBag.itemid = new SelectList(db.item.OrderBy(s => s.name), "id", "name");
            return View();
        }

        public ActionResult CreateFrame()
        {

            ViewBag.employeeid = new SelectList(db.Employees.OrderBy(s => s.name), "id", "name");
            ViewBag.itemid = new SelectList(db.item.OrderBy(s => s.name), "id", "name");

            return View();
        }
        // POST: MakeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFrame([Bind(Include = "id,itemid,description,Date,employeeid,quantity,paid")] MakeItem makeItem)
        {
            if (ModelState.IsValid)
            {
                db.makeItem.Add(makeItem);
                db.SaveChanges();

                if (makeItem.item.product.borderframe == true)
                {
                    var item = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    var productmake = (from prdt in db.product where prdt.id == item.productid select prdt).FirstOrDefault();

                    var shellitem = makeItem.item.product;
                    {
                        shellitem.BorderFrame_quantity = shellitem.BorderFrame_quantity + makeItem.quantity;
                        db.Entry(shellitem).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (makeItem.item.product.description.Contains("bno:"))
                    {
                        string bdno = makeItem.item.product.description.Remove(0, makeItem.item.product.description.IndexOf("bno:"));
                        var prdts = (from pro in db.product where pro.description.Contains(bdno) && pro.id != productmake.id select pro).ToList();

                        foreach (var p in prdts)
                        {
                            p.BorderFrame_quantity = p.BorderFrame_quantity + makeItem.quantity;
                            db.Entry(p).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                    }
                }


            }
            return RedirectToAction("Index");

        }
        public ActionResult CreateShell()
        {

            ViewBag.employeeid = new SelectList(db.Employees.OrderBy(s => s.name), "id", "name");
            ViewBag.itemid = new SelectList(db.item.OrderBy(s => s.name), "id", "name");

            return View();


        }        // POST: MakeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShell([Bind(Include = "id,itemid,description,Date,employeeid,quantity,paid")] MakeItem makeItem)
        {
            if (ModelState.IsValid)
            {
                db.makeItem.Add(makeItem);
                db.SaveChanges();

                var item = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                var productmake = (from prdt in db.product where prdt.id == item.productid select prdt).FirstOrDefault();

                //shell
                if (makeItem.item.product.Acrylic == true)
                {
                    var shellitem = makeItem.item.product;
                    {
                        shellitem.shell_quantity = shellitem.shell_quantity + makeItem.quantity;
                        db.Entry(shellitem).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (makeItem.item.name.Contains("Right"))
                    {
                        string namel = makeItem.item.product.name.Remove(makeItem.item.name.IndexOf("Right"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();
                        if (pr != null)
                        {
                            foreach(var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }
                    else if (makeItem.item.name.Contains("right"))
                    {
                        string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("right"));

                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }
                    else if (makeItem.item.name.Contains("Left"))
                    {
                        string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("Left"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }
                    else if (makeItem.item.name.Contains("left"))
                    {
                        string namel = makeItem.item.product.name.Remove(makeItem.item.name.IndexOf("left"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }
                    //************************************
                    else if (makeItem.item.name.Contains("Simple"))
                    {
                        string namel = makeItem.item.product.name.Remove(makeItem.item.name.IndexOf("Simple"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }
                    else if (makeItem.item.name.Contains("simple"))
                    {
                        string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("left"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }


                    else if (makeItem.item.name.Contains("Border"))
                    {
                        string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("left"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }
                    else if (makeItem.item.name.Contains("border"))
                    {
                        string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("left"));
                        var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                        if (pr != null)
                        {
                            foreach (var p in pr)
                            {
                                p.shell_quantity = p.shell_quantity + makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }


                }


            }
            return RedirectToAction("Index");
        }

        // POST: MakeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,itemid,description,Date,employeeid,quantity,paid")] MakeItem makeItem)
        {
            Material.readFromFile();
            try {
                if (ModelState.IsValid)
                {
                    db.makeItem.Add(makeItem);
                    db.SaveChanges();
                    //Quantity Items
                    var item = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    var productmake = (from prdt in db.product where prdt.id == item.productid select prdt).FirstOrDefault();

                    item.quantity = makeItem.quantity + item.quantity;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();


                    //Employee
                    var employe = (from employee in db.Employees where employee.id == makeItem.employeeid select employee).FirstOrDefault();
                    employe.Ammount = employe.Ammount + (makeItem.quantity * item.product.Emp_Charges);
                    db.Entry(employe).State = EntityState.Modified;
                    db.SaveChanges();

                    //shell
                    if (makeItem.item.product.Acrylic == true)
                    {
                        var shellitem = makeItem.item.product;
                        {
                            shellitem.shell_quantity = shellitem.shell_quantity - makeItem.quantity;
                            db.Entry(shellitem).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        if (makeItem.item.name.Contains("Right"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("Right"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("right"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("right"));

                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("Left"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("Left"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("left"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("left"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("Simple"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("Simple"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("simple"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("simple"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }


                        else if (makeItem.item.name.Contains("Border"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("Border"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("border"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("border"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }
                        else if (makeItem.item.name.Contains("boder"))
                        {
                            string namel = makeItem.item.name.Remove(makeItem.item.name.IndexOf("boder"));
                            var pr = (from prdt in db.product where prdt.name.Contains(namel) && prdt.id != productmake.id select prdt).ToList();

                            if (pr != null)
                            {
                                foreach (var p in pr)
                                {
                                    p.shell_quantity = p.shell_quantity - makeItem.quantity;
                                    db.Entry(p).State = EntityState.Modified;
                                    db.SaveChanges();

                                }
                            }
                        }



                    }
                    if (makeItem.item.product.borderframe == true)
                    {
                        var shellitem = makeItem.item.product;
                        {
                            shellitem.BorderFrame_quantity = shellitem.BorderFrame_quantity - makeItem.quantity;
                            db.Entry(shellitem).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        if (makeItem.item.product.description.Contains("bno:"))
                        {
                            string bdno = makeItem.item.product.description.Remove(0, makeItem.item.product.description.IndexOf("bno:"));
                            var prdts = (from pro in db.product where pro.description.Contains(bdno) && pro.id != productmake.id select pro).ToList();

                            foreach (var p in prdts)
                            {
                                p.BorderFrame_quantity = p.BorderFrame_quantity - makeItem.quantity;
                                db.Entry(p).State = EntityState.Modified;
                                db.SaveChanges();

                            }
                        }
                    }

                    //Material
                    //*****************************************************
                    var raison = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    //Materi = makeItem.quantity + item.quantity;
                    Material.raison = Material.raison - (makeItem.quantity *item.product.raison);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.mat300 = Material.mat300 - (makeItem.quantity * item.product.Mat300);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.mat450 = Material.mat450 - (makeItem.quantity * item.product.Mat450);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    var mekb = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    //Materi = makeItem.quantity + item.quantity;
                    Material.mekb = Material.mekb - (makeItem.quantity * item.product.Mekb);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    var cobalt = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    //Materi = makeItem.quantity + item.quantity;
                    Material.cobalt = Material.cobalt - (makeItem.quantity * item.product.cobalt);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    var Titanium = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    //Materi = makeItem.quantity + item.quantity;
                    Material.titanium = Material.titanium - (makeItem.quantity * item.product.Titanium);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    var bursh = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
                    //Materi = makeItem.quantity + item.quantity;
                    Material.bursh = Material.bursh - (makeItem.quantity * item.product.bursh);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.aerosel = Material.aerosel - (makeItem.quantity * item.product.Aerosel);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.bolt = Material.bolt - (makeItem.quantity * item.product.bolts);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    //*************
                    Material.bowl = Material.bowl - (makeItem.quantity * item.product.bowl);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.brushcleaner = Material.brushcleaner - (makeItem.quantity * item.product.BrushCleaner);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.cement = Material.cement - (makeItem.quantity * item.product.cement);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.colour = Material.colour - (makeItem.quantity * item.product.colour);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.cuttingcream = Material.cuttingcream - (makeItem.quantity * item.product.cuttingcream);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();


                    Material.dori = Material.dori - (makeItem.quantity * item.product.dori);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();


                    Material.dori = Material.dori - (makeItem.quantity * item.product.dori);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();


                    Material.jelcoat = Material.jelcoat - (makeItem.quantity * item.product.JelCoat);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();


                    Material.kapra = Material.kapra - (makeItem.quantity * item.product.Kapra);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.kharpaichy = Material.kharpaichy - (makeItem.quantity * item.product.khrpaichy);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.nut = Material.nut - (makeItem.quantity * item.product.nut);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();



                    Material.plasticbolts = Material.plasticbolts - (makeItem.quantity * item.product.plastic_nut_Bolts);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.plasticparis = Material.plasticparis - (makeItem.quantity * item.product.Plasticparis);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

                    Material.polish = Material.polish - (makeItem.quantity * item.product.Polish);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();

            
        }

            }
            catch (Exception e)
            {
                ViewBag.employeeid = new SelectList(db.Employees, "id", "name", makeItem.employeeid);
                ViewBag.itemid = new SelectList(db.item, "id", "name", makeItem.itemid);
                return View(makeItem);


            }
            finally
            {
                Material.writeOnFile();
            }
            return RedirectToAction("Index");

        }

        // GET: MakeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakeItem makeItem = db.makeItem.Find(id);
            if (makeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeid = new SelectList(db.Employees, "id", "name", makeItem.employeeid);
            ViewBag.itemid = new SelectList(db.item, "id", "name", makeItem.itemid);
            return View(makeItem);
        }

        // POST: MakeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,itemid,description,Date,employeeid,quantity,paid")] MakeItem makeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(makeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeid = new SelectList(db.Employees, "id", "name", makeItem.employeeid);
            ViewBag.itemid = new SelectList(db.item, "id", "name", makeItem.itemid);
            return View(makeItem);
        }

        // GET: MakeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakeItem makeItem = db.makeItem.Find(id);
            if (makeItem == null)
            {
                return HttpNotFound();
            }
            return View(makeItem);
        }

        // POST: MakeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material.readFromFile();

            MakeItem makeItem = db.makeItem.Find(id);
            var item = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
            //Material
            //*****************************************************
            var raison = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
            Material.raison = Material.raison + (makeItem.quantity * item.product.raison);
            Material.mat300 = Material.mat300 + (makeItem.quantity * item.product.Mat300);
            Material.mat450 = Material.mat450 + (makeItem.quantity * item.product.Mat450);
            var mekb = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
            Material.mekb = Material.mekb + (makeItem.quantity * item.product.Mekb);
            var cobalt = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
            Material.cobalt = Material.cobalt + (makeItem.quantity * item.product.cobalt);
            var Titanium = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
            Material.titanium = Material.titanium + (makeItem.quantity * item.product.Titanium);
            var bursh = (from itm in db.item where itm.id == makeItem.itemid select itm).FirstOrDefault();
            Material.bursh = Material.bursh + (makeItem.quantity * item.product.bursh);
            Material.aerosel = Material.aerosel + (makeItem.quantity * item.product.Aerosel);
            Material.bolt = Material.bolt + (makeItem.quantity * item.product.bolts);
            Material.bowl = Material.bowl + (makeItem.quantity * item.product.bowl);
            Material.brushcleaner = Material.brushcleaner + (makeItem.quantity * item.product.BrushCleaner);
            Material.cement = Material.cement + (makeItem.quantity * item.product.cement);
            Material.colour = Material.colour + (makeItem.quantity * item.product.colour);
            Material.cuttingcream = Material.cuttingcream + (makeItem.quantity * item.product.cuttingcream);
            Material.dori = Material.dori + (makeItem.quantity * item.product.dori);
            Material.jelcoat = Material.jelcoat + (makeItem.quantity * item.product.JelCoat);
            Material.kapra = Material.kapra + (makeItem.quantity * item.product.Kapra);
            Material.kharpaichy = Material.kharpaichy +(makeItem.quantity * item.product.khrpaichy);            
            Material.nut = Material.nut + (makeItem.quantity * item.product.nut);
            Material.plasticbolts = Material.plasticbolts + (makeItem.quantity * item.product.plastic_nut_Bolts);
            Material.plasticparis = Material.plasticparis + (makeItem.quantity * item.product.Plasticparis);
            Material.polish = Material.polish + (makeItem.quantity * item.product.Polish);

            db.makeItem.Remove(makeItem);
            db.SaveChanges();
            Material.writeOnFile();

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
