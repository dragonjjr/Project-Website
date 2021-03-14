using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using DatabaseProvider;

namespace WebsiteShop.Controllers
{
    public class OrderSuccessController : Controller
    {
        DBIO db = new DBIO();
        // GET: OrderSuccess
        public ActionResult Index(string idOrder)
        {
            if (Session["user"] == null)
            {
                string url = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/login/index";
                Response.Redirect(url);
            }

            Orders order = new Orders();
            order=db.getOrder(idOrder);
            ViewBag.amount = db.getAmountOrderDetail(idOrder);

            ViewBag.total = db.totalPriceOrder(idOrder);

            return View(order);
        }
        
        [HttpPost]
        public JsonResult SaveOrder(FormCollection data)
        {
            JsonResult jr = new JsonResult();
           
            string name = data["name"];
            string phone = data["phone"];
            string address = data["address"];

            if(string.IsNullOrEmpty(name) ||string.IsNullOrEmpty(name) ||string.IsNullOrEmpty(name))
            {
                jr.Data = new
                {
                    status = "ER"
                };
            }
            else
            {
                Account user = (Account)Session["user"];

                if(user==null)
                {
                    jr.Data = new
                    {
                        status = "ER"
                    };
                }
                else
                {
                    int numOrder = db.getAmountOrder();
                    Orders od = new Orders();
                    od.ID = "DH" + (numOrder+1).ToString();
                    od.name = name;
                    od.numPhone = phone;
                    od.DeliAddress = address;
                    od.email = user.email;
                    od.status = "Shipping";
                    od.orderDate = DateTime.Now;
                    od.deliveryDate = null;

                    db.addObject(od);
                    db.Save();

                    jr.Data = new
                    {
                        status = "OK",
                        idOrder = od.ID
                    };
                }

            }

            return Json(jr, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDetailOrder(FormCollection data)
        {
            JsonResult jr = new JsonResult();
      
            string idOrder = data["idOrder"];
            string idProd = data["idProd"];
            int amount= int.Parse(data["amount"]);

            if (string.IsNullOrEmpty(idOrder) || string.IsNullOrEmpty(idProd) || string.IsNullOrEmpty(data["amount"]))
            {
                jr.Data = new
                {
                    status = "ER"
                };
            }
            else
            {
                Orders od = new Orders();
                od = db.getOrder(idOrder);


                if (od == null)
                {
                    jr.Data = new
                    {
                        status = "ER"
                    };
                }
                else
                {
                    DetailOrder dtO = new DetailOrder();
                    dtO.ID = idOrder;
                    dtO.ProductID = idProd;
                    dtO.amount = amount;
                   
                    //save
                    db.addObject(dtO);
                    db.Save();

                    //delete product from cart
                    Account user = (Account)Session["user"];
                    Cart proBought = new Cart();
                    proBought = db.getProdCart(user.email, idProd);
                    db.DeleteObject(proBought);
                    db.Save();

                    jr.Data = new
                    {
                        status = "OK",
                    };
                }
            }

            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}