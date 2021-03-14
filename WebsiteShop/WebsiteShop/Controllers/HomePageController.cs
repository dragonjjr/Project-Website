using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using DatabaseProvider;

namespace WebsiteShop.Controllers
{
    public class HomePageController : Controller
    {
        DBIO db = new DBIO();

        // GET: HomePage
        public ActionResult Index()
        {
            Account user = (Account)Session["user"];
            if (user!=null)
            {
                ViewBag.email = user.email;
                ViewBag.name = user.name;
            }

            List<Product> listProduct = new List<Product>();
            listProduct = db.getListProduct();
            return View(listProduct);
        }


        public void Logout()
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                Response.Redirect("http://webshop.com/Login/index");
            }  
        }
    }
}