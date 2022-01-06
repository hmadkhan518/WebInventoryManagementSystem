using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebInventoryManagementSystem.Controllers
{
    public class PurchaseInvoiceController : Controller
    {
        inventoryDBEntities db = new inventoryDBEntities();
        // GET: PurchaseInvoice
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if(Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                return View(db.purchaseInvoices.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                var data = (from x in db.suppliers select new {x.sup_id , x.sup_name }).ToList();
                ViewBag.suppList = new SelectList(data,"sup_id", "sup_name");
                var pro = (from c in db.products select new { c.pro_id, c.pro_name }).ToList();
                ViewBag.proList = new SelectList(pro, "pro_id", "pro_name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }
    }
}
