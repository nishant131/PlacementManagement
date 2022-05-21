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
    public class DepartmentsController : Controller
    {
        private DBPlacements db = new DBPlacements();

        // GET: Departments
        public ActionResult Index()
        {
            var departments = db.Departments.Include(d => d.admin);
            if (Session["uname"].ToString().Equals("Super") && Session["role"].ToString().Equals("Super"))
                return View(departments.ToList());
            else
                return Redirect("/Home/Index");
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
                      
                ViewBag.deptId = new SelectList(db.Admins, "adminId", "adminName");
                if (Session["uname"].ToString().Equals("Super") && Session["role"].ToString().Equals("Super"))
                    return View();
                else
                    return Redirect("/Home/Index");
            
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deptId,deptName")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.deptId = new SelectList(db.Admins, "adminId", "adminName", department.deptId);
                return View(department);
            }

            catch (Exception e)
            {
                Console.Write(e);
                return Redirect("/Home/Index");
            }
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            if (Session["uname"].ToString().Equals("Super") && Session["role"].ToString().Equals("Super"))
            {
                ViewBag.deptId = new SelectList(db.Admins, "adminId", "adminName", department.deptId);
                return View(department);
            }
            else
                return Redirect("/Home/Index");
            }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deptId,deptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deptId = new SelectList(db.Admins, "adminId", "adminName", department.deptId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            if (Session["uname"].ToString().Equals("Super") && Session["role"].ToString().Equals("Super"))
            {
                return View(department);
            }
            else
                return Redirect("/Home/Index");
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
