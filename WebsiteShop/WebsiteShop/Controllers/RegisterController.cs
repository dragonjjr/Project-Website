using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using DatabaseIO;
using DatabaseProvider;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace WebsiteShop.Controllers
{
    public class RegisterController : Controller
    {
        static string[] arrHexa = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        public string ArrByteToString(Byte[] arrByte)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in arrByte)
            {
                //get 4 bit first
                sb.Append(arrHexa[(b >> 4)]);
                //get 4 bit second
                sb.Append(arrHexa[(b & 15)]);
            }

            return sb.ToString();
        }
        // GET: Register
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
        public JsonResult CheckRegister(FormCollection data)
        {
            string email = data["email"];
            string pwd = data["pwd"];
            string name = data["name"];
            string address = data["address"];
            string url = data["url"];

            JsonResult jr = new JsonResult();

            DBIO db = new DBIO();

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(pwd) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(address))
            {
                jr.Data = new
                {
                    status = "F",
                    message = "Không thể bỏ trống dữ liệu"
                };
            }
            else
            {
                //kiểm tra username đã tồn tại?
                Account user = db.getUser(email);
                if (user != null)
                {
                    jr.Data = new
                    {
                        status = "F"
                    };
                }
                else
                {
                    //hash password and save info user to database
                    var sha1 = new SHA1CryptoServiceProvider();
                    var bytesPw = Encoding.ASCII.GetBytes(pwd);
                    //hash
                    var bytesPwhashed = sha1.ComputeHash(bytesPw);
                    var strPwHash = ArrByteToString(bytesPwhashed);
                    //

                    //save to database
                    string AuthenCode = Guid.NewGuid().ToString();
                    user = new Account();
                    user.email = email;
                    user.C_password = strPwHash;
                    user.name = name;
                    user.address = address;
                    user.authenCode = AuthenCode;

                    db.addObject(user);
                    db.Save();

                    //send email to verify
                    SendEmail(email,"Verify account",url+"/"+email+"/"+AuthenCode);

                    jr.Data = new
                    {
                        status = "OK"
                    };
                }

            }
            return Json(jr, JsonRequestBehavior.AllowGet);
        }

    }
}