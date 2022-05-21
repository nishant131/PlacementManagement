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
    public class CompaniesController : Controller
    {
        private DBPlacements db = new DBPlacements();

        // GET: Companies
        public ActionResult Index(string searchCompany, string sort)
        {
            var companies = from c in db.Companies
                            select c;
            ViewBag.CompSort = sort == "ascc" ? "dscc" : "ascc";
            ViewBag.MinReqSort = sort == "ascmr" ? "dscmr" : "ascmr";
            ViewBag.AvgPackSort = sort == "ascap" ? "dscap" : "ascap";
            ViewBag.ArrDateSort = sort == "ascad" ? "dscad" : "ascad";
            if (!string.IsNullOrEmpty(searchCompany))
            {
                companies = companies.Where(c => c.companyName.Contains(searchCompany));
            }
            switch (sort)
            {
                case "ascc":
                    companies = companies.OrderBy(c => c.companyName);
                    break;
                case "dscc":
                    companies = companies.OrderByDescending(c => c.companyName);
                    break;
                case "ascmr":
                    companies = companies.OrderBy(c => c.minRequirements);
                    break;
                case "dscmr":
                    companies = companies.OrderByDescending(c => c.minRequirements);
                    break;
                case "ascap":
                    companies = companies.OrderBy(c => c.avgPackage);
                    break;
                case "dscap":
                    companies = companies.OrderByDescending(c => c.avgPackage);
                    break;
                case "ascad":
                    companies = companies.OrderBy(c => c.arrivalDate);
                    break;
                case "dscad":
                    companies = companies.OrderByDescending(c => c.arrivalDate);
                    break;
            }
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "companyId,companyName,location,minRequirements,avgPackage,email,arrivalDate")] Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Companies.Add(company);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(company);
            }
            catch(Exception e)
            {
                Console.Write(e);
                return Redirect("/Companies/Index");
            }
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "companyId,companyName,location,minRequirements,avgPackage,email,arrivalDate")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Session["role"].ToString().Equals("Admin"))
                return Redirect("/Students/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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