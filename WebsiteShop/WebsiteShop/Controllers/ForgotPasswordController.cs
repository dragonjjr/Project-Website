using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using DatabaseProvider;

namespace WebsiteShop.Controllers
{
    public class ForgotPasswordController : Controller
    {
        DBIO db = new DBIO();
        // GET: ForgotPassword
        public ActionResult Index()
        {
            return View();
        }

        public bool SendEmail(string email, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("testweb18ck1@gmail.com", "ShoppingWeb");
                    var receiverEmail = new MailAddress(email);
                    var password = "Abcxyz123";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public JsonResult forgotPwd(FormCollection data)
        {
            string email = data["email"];
            string url= data["url"];
            string code= Guid.NewGuid().ToString();

            var jr = new JsonResult();

            //send email to verify
            SendEmail(email, "Verify account", url + "/" + email+"/"+code);
            //save code
            Account user = new Account();
            user=db.getUser(email);
            user.authenCode = code;
            db.Save();

            jr.Data = new
            {
                status = "OK"
            };

            return Json(jr, JsonRequestBehavior.AllowGet);
        }

    }
}
