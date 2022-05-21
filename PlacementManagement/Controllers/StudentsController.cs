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
    public class StudentsController : Controller
    {
        private DBPlacements db = new DBPlacements();

        // GET: Students
        public ActionResult Index()
        {

            var students = db.Students.Include(s => s.Dept);
            if (Session["role"].ToString().Equals("Admin"))
                return View(students.ToList());
            else
                return Redirect("/Home/Index");
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        public ActionResult Login()
        {
            Session["uname"] = "dummy";
            Session["role"] = "dummy";
            Session["dept"] = "dummy";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Name, string Password)
        {
            string uname = Name;
            string pass = Password;
            var student = from st in db.Students
                          select st;
            var check = student.Where(data => data.Name.Equals(uname)).FirstOrDefault();
            if (check == null)
                return RedirectToAction("Login", "Students");
            if (!check.Password.Equals(pass))
                return Redirect("/Students/Login");
            else
            {
                Session["uname"] =check.Name.ToString();
                Session["role"] = "Student";
                Session["dept"] = check.Dept.deptName.ToString();
                return Redirect("/Home/Index");
            }
        }
        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.deptId = new SelectList(db.Departments, "deptId", "deptName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentId,Name,Password,Gender,Age,CPI,MobNumber,email,otherdetails,deptId")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.deptId = new SelectList(db.Departments, "deptId", "deptName", student.deptId);
                return Redirect("/Students/Login");
            }
            catch(Exception e)
            {
                Console.Write(e);
                return Redirect("/Students/Login");
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptId = new SelectList(db.Departments, "deptId", "deptName", student.deptId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentId,Name,Password,Gender,Age,CPI,MobNumber,email,otherdetails,deptId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deptId = new SelectList(db.Departments, "deptId", "deptName", student.deptId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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