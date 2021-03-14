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
    public class LoginController : Controller
    {
       
        DBIO db = new DBIO();

        // GET: Login
        public ActionResult Index(string email, string code)
        {
            //kiểm tra nếu vẫn còn tồn tại session
            if (Session["user"] != null)
            {
                Response.Redirect("http://webshop.com/home/index");
            }
            else
            {
                if (!String.IsNullOrEmpty(email) && !string.IsNullOrEmpty(code))
                {
                    bool VerifySucc = ActivateAccount(email, code);
                    if (!VerifySucc)
                    {
                        ViewBag.error = "Verify account is not success";
                    }

                }
            }
            return View();
        }

        public bool ActivateAccount(string email,string code)
        {
            Account user = new Account();
            user = db.getUser(email);

            //verify code
            if(String.Concat(user.authenCode.Where(c => !Char.IsWhiteSpace(c))) == code)
            {
                user.authenCode = null;
                db.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// hash
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
        /// </summary>
        
        //kiểm tra đăng nhập bằng ajax
        [HttpPost]
        public JsonResult CheckLogin(FormCollection data)
        {
            string email = data["email"];
            string pwd = data["pwd"];

            JsonResult jr = new JsonResult();

            Account user = db.getUser(email);

            //ko tồn tại user
            if (user == null)
            {
                jr.Data = new
                {
                    status = "N"
                };
            }
            else
            {
                //nhập password đúng?
                //hash password and check
                var sha1 = new SHA1CryptoServiceProvider();
                var bytesPw = Encoding.ASCII.GetBytes(pwd);
                //hash
                var bytesPwhashed = sha1.ComputeHash(bytesPw);
                var strPwHash = ArrByteToString(bytesPwhashed);

                if (String.Concat(user.C_password.Where(c => !Char.IsWhiteSpace(c))) == strPwHash)
                {
                    //kiểm tra tài khoản đã xác thực
                    if(user.authenCode==null)
                    {
                        Session["user"] = user;
                        Session.Timeout = 50;

                        jr.Data = new
                        {
                            status = "OK"
                        };
                    }
                    else
                    {
                        jr.Data = new
                        {
                            status = "Q"
                        };
                    } 
                }
                else
                {
                    jr.Data = new
                    {
                        status = "F"
                    };
                }
            }

            return Json(jr, JsonRequestBehavior.AllowGet);
        }

      

    }
}