using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebInventoryManagementSystem;

namespace WebInventoryManagementSystem.Controllers
{
    public class StocksController : Controller
    {
        private inventoryDBEntities db = new inventoryDBEntities();

        // GET: Stocks
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                var stocks = db.Stocks.Include(s => s.product).Include(s => s.purchaseInvoice);
                return View(stocks.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }

        // GET: Stocks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                ViewBag.st_proID = new SelectList(db.products, "pro_id", "pro_name");
                ViewBag.st_purchaseInvID = new SelectList(db.purchaseInvoices, "pi_id", "pi_id");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "st_id,st_proID,st_proCarton,st_proPieces,st_purchaseInvID")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.st_proID = new SelectList(db.products, "pro_id", "pro_name", stock.st_proID);
            ViewBag.st_purchaseInvID = new SelectList(db.purchaseInvoices, "pi_id", "pi_id", stock.st_purchaseInvID);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Stock stock = db.Stocks.Find(id);
                if (stock == null)
                {
                    return HttpNotFound();
                }
                ViewBag.st_proID = new SelectList(db.products, "pro_id", "pro_name", stock.st_proID);
                ViewBag.st_purchaseInvID = new SelectList(db.purchaseInvoices, "pi_id", "pi_id", stock.st_purchaseInvID);
                return View(stock);
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "st_id,st_proID,st_proCarton,st_proPieces,st_purchaseInvID")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.st_proID = new SelectList(db.products, "pro_id", "pro_name", stock.st_proID);
            ViewBag.st_purchaseInvID = new SelectList(db.purchaseInvoices, "pi_id", "pi_id", stock.st_purchaseInvID);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Stock stock = db.Stocks.Find(id);
                if (stock == null)
                {
                    return HttpNotFound();
                }
                return View(stock);
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
