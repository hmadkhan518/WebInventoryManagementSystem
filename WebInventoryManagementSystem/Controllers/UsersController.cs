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
    public class UsersController : Controller
    {
        private inventoryDBEntities db = new inventoryDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }
        private void createCombo()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                ViewBag.u_roleID = new SelectList(db.roles, "r_id", "r_name");
                createCombo();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "u_id,u_name,u_username,u_password,u_phone,u_email,u_status,u_roleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.u_roleID = new SelectList(db.roles, "r_id", "r_name", user.u_roleID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
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
                User user = db.Users.Find(id);
                createCombo();
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.u_roleID = new SelectList(db.roles, "r_id", "r_name", user.u_roleID);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
            
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "u_id,u_name,u_username,u_password,u_phone,u_email,u_status,u_roleID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.u_roleID = new SelectList(db.roles, "r_id", "r_name", user.u_roleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else if(Session["role"].ToString() == "Admin" || Session["role"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
