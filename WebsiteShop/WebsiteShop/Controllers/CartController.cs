using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using DatabaseProvider;
using WebsiteShop.Models;

namespace WebsiteShop.Controllers
{
    public class CartController : Controller
    {
        DBIO db = new DBIO();

        // GET: Cart
        public ActionResult Index(string email)
        {
            if (Session["user"] == null)
            {
                string url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/login/index";
                Response.Redirect(url);
            }
            Account user = (Account)Session["user"];
            ViewBag.email = user.email;
            ViewBag.name = user.name;
            ViewBag.countProd = db.getAmountProd(user.email);

            //data
            var product = db.getListProduct();
            var InfoCart = db.getInfoCart(user.email);

            ViewData["infoCart"] = from p in product join c in InfoCart on p.productID equals c.productID select new InfoDetail { cart = c, product = p };
           
            return View(ViewData["infoCart"]);
        }

        [HttpPost]
        public JsonResult addProduct(FormCollection data)
        {
            JsonResult jr = new JsonResult();

            string email = data["email"];
            string idPro = data["idPro"];
            int amount = int.Parse(data["amount"]);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(idPro) || string.IsNullOrEmpty(data["amount"]))
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                Cart newProCart = new Cart();
                newProCart.email = email;
                newProCart.productID = idPro;
                newProCart.ID = Guid.NewGuid().ToString();
                newProCart.amount = amount;

                db.addObject(newProCart);
                db.Save();

                jr.Data = new
                {
                    status = "OK"
                };
            }

            return Json(jr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult deleteProd(FormCollection data)
        {
            JsonResult jr = new JsonResult();

            string idProd = data["idPro"];
            string email = data["email"];

            if (String.IsNullOrEmpty(idProd))
            {
                jr.Data = new
                {
                    status = "ER",
                    message = "Không thể bỏ trống dữ liệu"
                };
            }
            else
            {
                Cart ProdCart = new Cart();
                ProdCart = db.getProdCart(email, idProd);
                if (ProdCart == null)
                {
                    jr.Data = new
                    {
                        status = "ER",
                        message = "Dữ liệu không tồn tại"
                    };
                }
                else
                {
                    db.DeleteObject(ProdCart);

                    db.Save();

                    jr.Data = new
                    {
                        status = "OK"
                    };
                }

            }

            return Json(jr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult changeCart(FormCollection data)
        {
            JsonResult jr = new JsonResult();

            string email = data["email"];
            string idPro = data["idPro"];
            int amount = int.Parse(data["amount"]);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(idPro) || string.IsNullOrEmpty(data["amount"]))
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                Cart ProCart = new Cart();
                ProCart = db.getProdCart(email, idPro);
               
                ProCart.amount = amount;
                db.Save();

                jr.Data = new
                {
                    status = "OK"
                };
            }

            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}