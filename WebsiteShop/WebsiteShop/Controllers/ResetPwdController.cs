using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DatabaseIO;
using DatabaseProvider;


namespace WebsiteShop.Controllers
{
    public class ResetPwdController : Controller
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
        DBIO db = new DBIO();

        // GET: ResetPwd
        public ActionResult Index(string email,string code)
        {   
            if (!String.IsNullOrEmpty(email) && !string.IsNullOrEmpty(code))
            {
                Account user = new Account();
                user = db.getUser(email);

                if(String.Concat(user.authenCode.Where(c => !Char.IsWhiteSpace(c))) == code)
                {
                    user.authenCode = null;
                    db.Save();

                    ViewBag.email = email;
                    ViewBag.code = code;
                    return View();
                }     
            }

            return null;
        }

        [HttpPost]
        public JsonResult Reset(FormCollection data)
        {
            JsonResult jr = new JsonResult();

            string email = data["email"];
            string newPwd = data["pwd"];

            Account user = new Account();
            user = db.getUser(email);

            if(user!=null)
            {
                //hash password and save info user to database
                var sha1 = new SHA1CryptoServiceProvider();
                var bytesPw = Encoding.ASCII.GetBytes(newPwd);
                //hash
                var bytesPwhashed = sha1.ComputeHash(bytesPw);
                var strPwHash = ArrByteToString(bytesPwhashed);
                //

                user.C_password = strPwHash;
                db.Save();
                jr.Data = new
                {
                    status = "OK"
                };
            }
            else
            {
                jr.Data = new
                {
                    status = "F"
                };
            }
        
            return Json(jr, JsonRequestBehavior.AllowGet);
        }
    }
}