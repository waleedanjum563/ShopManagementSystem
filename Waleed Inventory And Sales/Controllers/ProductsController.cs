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
    public class ProductsController : Controller
    {
        private DbContextClass db = new DbContextClass();

        // GET: Products
        public ActionResult AllShells()
        {
            var shells = Util.getAllShellsProduct();
            return View(shells.ToList());            
        }
        public  ActionResult Index(String name)
        {
            if(name==null)
            {
                //return View(orders) ;
                return View(db.product.OrderBy(o => o.name).ToList());

            }
            var orders = (from prdt in db.product where prdt.name.Contains(name) select prdt).ToList();
            //if (name.Equals("borderframe"))
            if (Request.IsAjaxRequest())
            {
                try
                {
                    return PartialView("_searchRes", orders);
                }
                catch (Exception)
                {
                    ViewBag.notAjax = "Ajax Partial View Err";
                    return View(orders);
                }
            }
            else
            {
                ViewBag.notAjax = "notAjax";
                return View(orders);

            }
        }
        [HttpGet]
        public ActionResult UpdateMaterial(int? id)
        {
            if(Request.IsAjaxRequest())
            {
               int productId = Convert.ToInt32(Session["Productid"]);
               int productIdToChangeMaterial = Convert.ToInt32(id);
               Product product= db.product.Where(pp => pp.id == productId).FirstOrDefault();
               Product productToChange = db.product.Where(pc => pc.id == productIdToChangeMaterial).FirstOrDefault();

                productToChange.JelCoat = product.JelCoat;
                productToChange.cobalt = product.cobalt;
                productToChange.Aerosel = product.Aerosel;
                productToChange.BrushCleaner = product.BrushCleaner;

                productToChange.Kapra = product.Kapra;
                productToChange.khrpaichy = product.khrpaichy;
                productToChange.Mat300 = product.Mat300;
                productToChange.Mat450 = product.Mat450;

                productToChange.Mekb = product.Mekb;
                productToChange.nut = product.nut;
                productToChange.plastic = product.plastic;
                productToChange.Plasticparis = product.Plasticparis;
                productToChange.plastic_nut_Bolts = product.plastic_nut_Bolts;
                productToChange.Polish = product.Polish;
                productToChange.raison = product.raison;
                productToChange.Titanium = product.Titanium;
                Edit(productToChange);


                List<Product> p = new List<Product>();
                p.Add(productToChange);

                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Naveed\Documents\DbBackup\Materials.txt", true))
                {
                    file.WriteLine(p.FirstOrDefault().name);
                }
                return PartialView("_searchRes",p);
            }
            return RedirectToAction("Index");
        }
        public ActionResult ProductList()
        {
            return View();
        }
        public ActionResult SelectMaterial(int? id)
        {
            List<Product> pro = db.product.Where(a => a.id == id).ToList();
            if(pro!=null)
            {
                Session["Productid"] = Convert.ToInt32(id);
            }
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Naveed\Documents\DbBackup\Materials.txt", true))
            {
                file.WriteLine("");
                file.WriteLine("*****************************");
                file.WriteLine(pro.FirstOrDefault().name);
            }
            ViewBag.setClass = "true";
            return PartialView("_searchRes", pro);
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,Emp_Charges,description,borderframe,BorderFrame_quantity,Acrylic,shell_quantity,raison,Mat300,Mat450,cobalt,Mekb,Titanium,bursh,JelCoat,Aerosel,BrushCleaner,plastic,colour,Plasticparis,nut,bolts,plastic_nut_Bolts,khrpaichy,Polish,Kapra,bowl,cuttingcream,dori,cement")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult likeProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,Emp_Charges,description,borderframe,BorderFrame_quantity,Acrylic,shell_quantity,raison,Mat300,Mat450,cobalt,Mekb,Titanium,bursh,JelCoat,Aerosel,BrushCleaner,plastic,colour,Plasticparis,nut,bolts,plastic_nut_Bolts,khrpaichy,Polish,Kapra,bowl,cuttingcream,dori,cement")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.product.Find(id);
            db.product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string name)
        {
            var orders = (from prdt in db.product where prdt.name.Contains(name) select prdt).ToList();
            //if (name.Equals("borderframe"))
            if(Request.IsAjaxRequest())
            {
                try
                {
                    return PartialView("_searchRes", orders);
                }
                catch(Exception)
                {
                    ViewBag.notAjax = "Ajax Partial View Err";
                    return Index("");
                }
            }
            else
            {
                ViewBag.notAjax = "notAjax";
                return Index("");

            }
            //return View(orders) ;
            
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
