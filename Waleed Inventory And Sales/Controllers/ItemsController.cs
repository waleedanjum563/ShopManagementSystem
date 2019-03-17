using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales_And_Inventory_MIS.Models;
using System.IO;

namespace Waleed_Inventory_And_Sales.Controllers
{
    public class ItemsController : Controller
    {
        private DbContextClass db = new DbContextClass();

        public ActionResult PrintMaterial()
        {
            return View();
        }
        public ActionResult Stock()
        {
            List<List<int>> quantity = new List<List<int>>();
            List<string> names = new List<string>();
            List<string> colours = new List<string>();

            colours.Add("Red");
            colours.Add("Green");
            colours.Add("Blue");
            colours.Add("White");
            colours.Add("Half White");
            colours.Add("Every");
            colours.Add("Pink");
            colours.Add("Purple");
            colours.Add("Yellow");


            List<Item> items = (from itm in db.item orderby itm.name select itm).Include(i => i.colour).ToList();


            var results =db.item.Where(p=>p.product.Acrylic==true).Include(a=>a.product).GroupBy( p => p.product.name, 
                               p => p.name,
                               (key, g) => new { 
                                                 Name = key, 
                                                 Colours = g.ToList() 
                                               }
                                 );

            ViewBag.Acrylic = results;

            quantity.Add(new List<int>());
            quantity.Add(new List<int>());
            quantity.Add(new List<int>());



            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    quantity[i].Add(i * j);
                }
            }

            ViewBag.quantity = items;
            return View();
        }
        //************************************
        public ActionResult StockWithMaterial()
        {
            List<List<int>> quantity = new List<List<int>>();
            List<string> names = new List<string>();
            List<string> colours = new List<string>();

            colours.Add("Red");
            colours.Add("Green");
            colours.Add("Blue");
            colours.Add("White");
            colours.Add("Half White");
            colours.Add("Every");
            colours.Add("Pink");
            colours.Add("Purple");
            colours.Add("Yellow");


            List<Item> items = (from itm in db.item orderby itm.name select itm).Include(i => i.colour).ToList();


            var results = db.item.Where(p => p.product.Acrylic == true).GroupBy(p => p.product.name,
                               p => p.name,
                               (key, g) => new {
                                   Name = key,
                                   Colours = g.ToList()
                               }
                                 );

            ViewBag.Acrylic = results;

            quantity.Add(new List<int>());
            quantity.Add(new List<int>());
            quantity.Add(new List<int>());



            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    quantity[i].Add(i * j);
                }
            }

            ViewBag.quantity = items;
            return View();
        }
//***********************************************************
        public ActionResult UpdateMaterial(string TextMat3, string TextMat4, string TextMekb, string TextTitanium, string TextCobalt, string TextRaison, string TextBursh,
            string TextA,string TextB,string TextC,string TextD,string TextE,string TextF,string TextG,string TextH,string TextI,string TextJ,string TextK,string TextL,
                string TextM,string TextN,string TextO,string TextP)
        {
            Material.readFromFile();

            if (TextBursh=="")
            {
                 TextBursh="0";
            }
            if (TextCobalt == "")
            {
                TextCobalt = "0";
            }
            if (TextMat3 == "")
            {
                TextMat3 = "0";
            }
            if (TextMat4 == "")
            {
                TextMat4 = "0";
            }
            Material.bursh = Material.bursh + Convert.ToInt32(TextBursh);
            Material.cobalt = Material.cobalt + Convert.ToInt32(TextCobalt);
            Material.mat300 = Material.mat300 + Convert.ToInt32(TextMat3);
            Material.mat450 = Material.mat450 + Convert.ToInt32(TextMat4);
            if (TextMekb == "")
            {
                TextMekb = "0";
            }
            if (TextRaison == "")
            {
                TextRaison = "0";
            }
            if (TextTitanium == "")
            {
                TextTitanium = "0";
            }
            Material.mekb = Material.mekb + Convert.ToInt32(TextMekb);
            Material.raison = Material.raison + Convert.ToInt32(TextRaison);
            Material.titanium = Material.titanium + Convert.ToInt32(TextTitanium);

            if (TextA == "")
            {
                TextA = "0";
            }
            if (TextB == "")
            {
                TextB = "0";
            } if (TextC == "")
            {
                TextC = "0";
            } if (TextD == "")
            {
                TextD = "0";
            } if (TextE == "")
            {
                TextE = "0";
            } if (TextF == "")
            {
                TextF = "0";
            } if (TextG == "")
            {
                TextG = "0";
            } if (TextH == "")
            {
                TextH = "0";
            } if (TextI == "")
            {
                TextI = "0";
            } if (TextJ == "")
            {
                TextJ = "0";
            } if (TextK == "")
            {
                TextK = "0";
            } if (TextL == "")
            {
                TextL = "0";
            } if (TextM == "")
            {
                TextM = "0";
            } if (TextN == "")
            {
                TextN = "0";
            } if (TextO == "")
            {
                TextO = "0";
            } if (TextP == "")
            {
                TextP = "0";
            }
            Material.aerosel = Material.aerosel + Convert.ToInt32(TextA);
              Material.bowl = Material.bowl + Convert.ToInt32(TextB);
              Material.bolt = Material.bolt + Convert.ToInt32(TextC);
              Material.brushcleaner = Material.brushcleaner + Convert.ToInt32(TextD);
              Material.cement = Material.cement + Convert.ToInt32(TextE);
              Material.colour = Material.colour + Convert.ToInt32(TextF);
              
              Material.cuttingcream = Material.cuttingcream + Convert.ToInt32(TextG);
              Material.dori = Material.dori + Convert.ToInt32(TextH);
              Material.jelcoat = Material.jelcoat + Convert.ToInt32(TextI);
              Material.kapra = Material.kapra + Convert.ToInt32(TextJ);
              Material.kharpaichy = Material.kharpaichy + Convert.ToInt32(TextK);
              Material.nut = Material.nut + Convert.ToInt32(TextL);
              Material.plastic = Material.plastic + Convert.ToInt32(TextM);
              Material.plasticbolts = Material.plasticbolts + Convert.ToInt32(TextN);
              Material.plasticparis = Material.plasticparis + Convert.ToInt32(TextO);
              Material.polish = Material.polish + Convert.ToInt32(TextP);

            Material.writeOnFile();
            return RedirectToAction("Display");
        }

        public ActionResult Search(string name, string colour)
        {
            var item = (from itm in db.item where itm.name.Contains(name) && itm.colour.name.Contains(colour) select itm).ToList();
            //            var item = db.item.Include(i => i.colour);

            return View(item);
        }

        public ActionResult Display()
        {
            Material.readFromFile();
            return View();
        }
        // GET: Items
        public ActionResult Index()
        {
            var item = db.item.Include(i => i.colour).Include(i => i.product).OrderBy(i=>i.name);
            return View(item.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public int colourCheckAndMake(string color)
        {
            var col = (from colour in db.colour where colour.name.Equals(color) select colour).FirstOrDefault();
            Colour colr = col;
            if (col == null)
            {
                colr = new Colour();
                colr.name = color;
                db.colour.Add(colr);
                db.SaveChanges();
            }
            return colr.id;
        }

        public ActionResult Create()
        {
            ViewBag.colourid = new SelectList(db.colour.OrderBy(s => s.name), "id", "name");
            ViewBag.productid = new SelectList(db.product.OrderBy(s => s.name), "id", "name");
            return View();
        }
        public JsonResult IsItemExists(string name)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!db.item.Any(x => x.name == name), JsonRequestBehavior.AllowGet);
        }  
        public Boolean ItemIsUnique(Item item)
        {
            var itmprev = (from itm in db.item where itm.name.Equals(item.name) select itm).ToList();
            if(itmprev.Count==0)
            {
                return true;
            }
            return false;

        }
        public string GetItemName(Item item)
        {
            var pro = (from prdt in db.product where prdt.id == item.productid select prdt).FirstOrDefault();
            var col = (from clr in db.colour where clr.id == item.colourid select clr).FirstOrDefault();
             
            return pro.name + "-" + col.name;

        }


        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productid,description,colourid,quantity")] Item item,string choice)
        {
            if (ModelState.IsValid)
            {

                item.name = GetItemName(item);
                
                if (ItemIsUnique(item))
                {
                    db.item.Add(item);
                    db.SaveChanges();

                }
                
                if (choice.Equals("1"))
                {
                    item.colourid = colourCheckAndMake("White");
                    item.quantity = 0;
                    Create(item, "0");

                    item.colourid = colourCheckAndMake("Every");
                    item.quantity = 0;
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Sea Green");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Sky Blue");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Gray");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Pink");
                    Create(item,"0");


                    item.colourid = colourCheckAndMake("Beige");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Burgundy");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Mauve");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Amber Green");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Black");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Brown");
                    Create(item,"0");

                    item.colourid = colourCheckAndMake("Others");
                    Create(item,"0");

                   
                }
                

                return RedirectToAction("Index");
            }

            ViewBag.colourid = new SelectList(db.colour, "id", "name", item.colourid);
            ViewBag.productid = new SelectList(db.product, "id", "name", item.productid);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.colourid = new SelectList(db.colour, "id", "name", item.colourid);
            ViewBag.productid = new SelectList(db.product, "id", "name", item.productid);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,productid,description,colourid,quantity")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.colourid = new SelectList(db.colour, "id", "name", item.colourid);
            ViewBag.productid = new SelectList(db.product, "id", "name", item.productid);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.item.Find(id);
            db.item.Remove(item);
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
