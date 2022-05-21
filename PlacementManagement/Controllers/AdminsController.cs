using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlacementManagement.Models;

namespace PlacementManagement.Controllers
{
    public class AdminsController : Controller
    {
        private DBPlacements db = new DBPlacements();

        // GET: Admins
        public ActionResult Index()
        {
            var admins = db.Admins.Include(a => a.dept);
//            admins = admins.Where(a => a.adminName.Equals(Session["uname"].ToString()));
            return View(admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string adminName, string Password)
        {
            string uname = adminName;
            string pass = Password;
            var admin = from a in db.Admins
                        select a;
            var check = admin.Where(data => data.adminName.Equals(uname)).FirstOrDefault();
            if(adminName.Equals("superuser1") && Password.Equals("superuser1"))
            {
                Session["uname"] = "Super";
                Session["role"] = "Super";
                return Redirect("/Departments/Index");
            }
            if (check == null) 
                return RedirectToAction("Login", "Admins");
            if (!check.Password.Equals(pass))
                return Redirect("/Admins/Login");
            else
            {
                Session["uname"] = check.adminName.ToString();
                Session["role"] = "Admin";
                Session["dept"] = check.dept.deptName.ToString();
                return Redirect("/Home/Index");
            }
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            ViewBag.adminId = new SelectList(db.Departments, "deptId", "deptName");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminId,adminName,Password,email")] Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.adminId = new SelectList(db.Departments, "deptId", "deptName", admin.adminId);
                return Redirect("/Admins/Login");
            }
            catch(Exception e)
            {
                Console.Write(e);
                return Redirect("/Admins/Login");
            }
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.adminId = new SelectList(db.Departments, "deptId", "deptName", admin.adminId);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminId,adminName,Password,email")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adminId = new SelectList(db.Departments, "deptId", "deptName", admin.adminId);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
