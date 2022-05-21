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
    public class PlacementstatusesController : Controller
    {
        private DBPlacements db = new DBPlacements();

        // GET: Placementstatuses
        public ActionResult Index(string searchCompany,string searchStudent, string sort)
        {
            
            var placementstatus1 = db.Placementstatus.Include(p => p.company).Include(p => p.student);
            var placementstatus = placementstatus1;
            
            
                if (Session["role"].ToString().Equals("Student"))
                {
                    string name = Session["uname"].ToString();
                    placementstatus1 = placementstatus.Where(s => s.student.Name.Equals(name));
                }
                else if (Session["role"].ToString().Equals("Admin"))
                {
                    string dept = Session["dept"].ToString();
                    placementstatus1 = placementstatus.Where(s => s.student.Dept.deptName.Equals(dept));
                }
                else
                    return Redirect("/Students/Login");
                ViewBag.StatusSort = sort == "ascs" ? "dscs" : "ascs";
                ViewBag.CompanySort = sort == "ascc" ? "dscc" : "ascc";
                if (!string.IsNullOrEmpty(searchCompany))
                {
                    placementstatus1 = placementstatus1.Where(ps => ps.company.companyName.Contains(searchCompany));
                }
                if (!string.IsNullOrEmpty(searchStudent))
                {
                    placementstatus1 = placementstatus1.Where(ps => ps.student.Name.Contains(searchStudent));
                }
                switch (sort)
                {
                    case "ascc":
                        placementstatus1 = placementstatus1.OrderBy(ps => ps.company.companyName);
                        break;
                    case "dscc":
                        placementstatus1 = placementstatus1.OrderByDescending(ps => ps.company.companyName);
                        break;
                    case "ascs":
                        placementstatus1 = placementstatus1.OrderBy(ps => ps.Status);
                        break;
                    case "dscs":
                        placementstatus1 = placementstatus1.OrderByDescending(ps => ps.Status);
                        break;
                }
                placementstatus = placementstatus1;
                ViewBag.plac = placementstatus.ToList();
                return View();
            /*
            catch (Exception e)
            {
                Console.Write(e);
                //ViewData["plac"] = placementstatus.ToList();
                return View();
            }*/
        }

        // GET: Placementstatuses/Details/5
        public ActionResult Details(int companyId, int studentId)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            Placementstatus placementstatus = db.Placementstatus.Find(companyId, studentId);
            if (placementstatus == null)
            {
                return HttpNotFound();
            }
            return View(placementstatus);
        }

        // GET: Placementstatuses/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["role"].ToString().Equals("Admin"))
                {
                    string dept = Session["dept"].ToString();
                    var students = db.Students.Where(s => s.Dept.deptName.Equals(dept));
                    ViewBag.companyId = new SelectList(db.Companies, "companyId", "companyName");
                    ViewBag.studentId = new SelectList(students, "studentId", "Name");
                    return View();
                }
                else
                    return Redirect("/Students/Login");
            }
            catch(Exception e)
            {
                Console.Write(e);
                return Redirect("/Students/Login");
            }
        }

        // POST: Placementstatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "companyId,studentId,Status")] Placementstatus placementstatus)
        {
            if (ModelState.IsValid)
            {
                db.Placementstatus.Add(placementstatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.companyId = new SelectList(db.Companies, "companyId", "companyName", placementstatus.companyId);
            ViewBag.studentId = new SelectList(db.Students, "studentId", "Name", placementstatus.studentId);
            return View(placementstatus);
        }

        // GET: Placementstatuses/Edit/5
        public ActionResult Edit(int companyId, int studentId)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            Placementstatus placementstatus = db.Placementstatus.Find(companyId, studentId);
            if (placementstatus == null)
            {
                return HttpNotFound();
            }
            var students = db.Students.Where(s => s.Dept.deptName.Equals(Session["dept"].ToString()));
            ViewBag.companyId = new SelectList(db.Companies, "companyId", "companyName", placementstatus.companyId);
            ViewBag.studentId = new SelectList(students, "studentId", "Name", placementstatus.studentId);
            return View(placementstatus);
        }

        // POST: Placementstatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "companyId,studentId,Status")] Placementstatus placementstatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placementstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companyId = new SelectList(db.Companies, "companyId", "companyName", placementstatus.companyId);
            ViewBag.studentId = new SelectList(db.Students, "studentId", "Name", placementstatus.studentId);
            return View(placementstatus);
        }

        // GET: Placementstatuses/Delete/5
        public ActionResult Delete(int companyId, int studentId)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            Placementstatus placementstatus = db.Placementstatus.Find(companyId, studentId);
            if (placementstatus == null)
            {
                return HttpNotFound();
            }
            return View(placementstatus);
        }

        // POST: Placementstatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int companyId, int studentId)
        {
            Placementstatus placementstatus = db.Placementstatus.Find(companyId, studentId);
            db.Placementstatus.Remove(placementstatus);
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
