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
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using Newtonsoft.Json;
using System.Globalization;

namespace Waleed_Inventory_And_Sales.Controllers
{
    public class MonthReportsViewModelsController : Controller
    {
        private DbContextClass db = new DbContextClass();
        // GET: MonthReportsViewModels
        public ActionResult Index3()
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
        .SetXAxis(new XAxis
        {
            Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
        })
        .SetSeries(new Series
        {
            Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
        });

            return View(chart);

        }
        public ActionResult Index2()
        {
            var itemSales=db.salesLineItem.GroupBy(s => s.item).Select(s => new { item = s.Key.name, sum = s.Sum(b => b.quantity) });
           string[] namesOfItems = new string[10];
            int i = 0;
            foreach(var itm in itemSales)
            {
                namesOfItems[i] = itm.item;
                i++;
                if(i==10)
                {
                    break;
                }
            }

            object[] sumOfItems = new object[10];
            i = 0;
            foreach(var itm in itemSales)
            {
                sumOfItems[i] = itm.sum;
                i++;
                if(i==10)
                {
                    break;
                }
            }


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart");
            chart.SetXAxis(new XAxis
            {
                Categories = namesOfItems
            });

            chart.SetSeries(new Series
            {
                Data = new Data(sumOfItems)
            });

            return View(chart);
        }

        public ActionResult Index()
        {
            ViewBag.Years = Enumerable.Range(DateTime.Now.Year-2, 4);
            var monthsReportsList = (from ord in db.order  select new { ord.orderDate, ord.total}).
                GroupBy(ord => ord.orderDate.Month).
                Select(a => new {  name = a.Key, date = a.FirstOrDefault().orderDate, orderTotals = a.Sum(b => b.total) });

            var monthsReportsList2 = (from ord in db.Payments select new { ord.Date, ord.Ammount }).
        GroupBy(ord => ord.Date.Month).
        Select(a => new { name = a.Key, date = a.FirstOrDefault().Date, cashPayments = a.Sum(b => b.Ammount) }).ToList();


            List<MonthReportsViewModel> mrv = new List<MonthReportsViewModel>();
            int i = 0;
            foreach (var m in monthsReportsList.ToList())
            {
                MonthReportsViewModel mm = new MonthReportsViewModel();
                mm.month = m.date.ToString("MMMM", CultureInfo.InvariantCulture);
                mm.cashSales = m.orderTotals;
                mrv.Add(mm);
            }
            foreach (var m in monthsReportsList.ToList())
            {
                if(i<mrv.Count && i<monthsReportsList2.Count )
                    mrv[i].cashPayments = monthsReportsList2[i].cashPayments;
                i++;
            }
            return View(mrv);
        }
        [HttpPost]
        public ActionResult Index(String Years)
        {
            int year = Convert.ToInt32(Years);
            ViewBag.Years = Enumerable.Range(DateTime.Now.Year - 2, 4);
            var monthsReportsList = (from ord in db.order select new { ord.orderDate, ord.total }).Where(a=>a.orderDate.Year.Equals(year)).
                GroupBy(ord => ord.orderDate.Month).
                Select(a => new { name = a.Key, date = a.FirstOrDefault().orderDate, orderTotals = a.Sum(b => b.total) });

            var monthsReportsList2 = (from ord in db.Payments select new { ord.Date, ord.Ammount }).Where(a => a.Date.Year.Equals(year)).
        GroupBy(ord => ord.Date.Month).
        Select(a => new { name = a.Key, date = a.FirstOrDefault().Date, cashPayments = a.Sum(b => b.Ammount) }).ToList();


            List<MonthReportsViewModel> mrv = new List<MonthReportsViewModel>();
            int i = 0;
            foreach (var m in monthsReportsList.ToList())
            {
                MonthReportsViewModel mm = new MonthReportsViewModel();
                mm.month = m.date.ToString("MMMM", CultureInfo.InvariantCulture);
                mm.cashSales = m.orderTotals;
                mrv.Add(mm);
            }
            foreach (var m in monthsReportsList.ToList())
            {
                if (i < mrv.Count)
                    mrv[i].cashPayments = monthsReportsList2[i].cashPayments;
                i++;
            }
            return View(mrv);

        }

    }
}
