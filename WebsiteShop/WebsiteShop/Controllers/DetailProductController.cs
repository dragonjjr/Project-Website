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
    public class DetailProductController : Controller
    {
        DBIO db = new DBIO();

        // GET: DetailProduct
        public ActionResult Index(string idProduct)
        {
            DetailsProduct infoProduct = new DetailsProduct();

            infoProduct.product= db.getProduct(idProduct);
            infoProduct.Comments = db.getComments(idProduct);
            
            if(Session["user"]!=null)
            {
                Account user = (Account)Session["user"];
                ViewBag.email = user.email;
                ViewBag.name = user.name;
            }

            return View(infoProduct);
        }

        [HttpPost]
        public JsonResult addComment(FormCollection data)
        {
            JsonResult jr = new JsonResult();

            //recieve data
            string email = data["email"];
            string idProduct = data["idPro"];
            string content = data["content"];

            if (String.IsNullOrEmpty(email) || string.IsNullOrEmpty(idProduct) || string.IsNullOrEmpty(content))
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
            else
            {
                //init and save data
                Comments cm = new Comments();
                cm.ID = Guid.NewGuid().ToString();
                cm.productID = idProduct;
                cm.email = email;
                cm.C_content = content;
                cm.datePost = DateTime.Now;

                db.addObject(cm);
                db.Save();

                jr.Data = new
                {
                    //return date to display view
                    date = cm.datePost.ToString(),
                    status = "OK"
                };
            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}