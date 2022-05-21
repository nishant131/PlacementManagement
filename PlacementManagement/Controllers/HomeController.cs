using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlacementManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["uname"] == null|| Session["uname"].ToString().Equals("dummy"))
                return Redirect("/Students/Login");
            return View();
        }
        public ActionResult Logout()
        {
            Session["uname"]=null;
            Session["role"] = null;
            Session["dept"] = null;
            return Redirect("/Students/Login");
        }
    }
}