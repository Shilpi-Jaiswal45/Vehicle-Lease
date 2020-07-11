using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaseVehicle.Models;
using LeaseVehicle.Helpers;
using RegistrationAndLogin.Models;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LeaseVehicle.Areas.User.Controllers
{
    public class UserLoginController : Controller
    {
        public VehicleLeasingEntities db = new VehicleLeasingEntities();
        // GET: User/UserLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserDetail userdetails)
        {
            if (ModelState.IsValid)
            {
                string password, EncryptedPassword;
                password = userdetails.Password;
                EncryptedPassword = PasswordSecurity.EncryptPassword(password);
                userdetails.Password = EncryptedPassword;
                db.UserDetails.Add(userdetails);

                if (db.SaveChanges() > 0)
                {
                    string receiver = userdetails.Email;
                    string subject = "Registrating";
                    string message = "You are registered in Vehicle Leasing now you can login";
                    if (MailSending.SendMail(receiver, subject, message))
                        return RedirectToAction("Login");
                    return View(userdetails);
                }


            }
            return View(userdetails);

        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserDetail userdetails)
        {
            string EncryptedPassword, UserPassword;
            UserPassword = userdetails.Password;
            EncryptedPassword = PasswordSecurity.EncryptPassword(UserPassword);
            var user = db.UserDetails.Where(x => x.Email == userdetails.Email && x.Password == EncryptedPassword).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username and Password");
            }
            else
            {
                Session["username"] = user.FirstName;
                Session["userid"] = user.UserId;
                Session["mobile"] = user.Contact;
                Session["address"] = user.Address;
                Session["email"] = user.Email;
                System.Diagnostics.Debug.WriteLine(Session["username"] + " " + Session["userid"] + " " + Session["mobile"]);
                return RedirectToAction("ListProduct", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            string message = "";
            List<UserDetail> users = new List<UserDetail>();
            var emailVerify = db.UserDetails.Where(x => x.Email == Email).FirstOrDefault();
            if (emailVerify != null)
            {
               message = "<a href=\"LINK\">Reset Password Here</a>";
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "/User/UserLogin/ResetUserPassword?Email=" + Email + "");
                message = message.Replace("LINK", link);
                bool flag = MailSending.SendMail(Email, "ResetPassword", message);
                TempData["Message"] = "Please Check your Email to Reset Password!!";
                return RedirectToAction("Login", "UserLogin");
            }
            else
            {
                TempData["Msg"] = "Sorry!  Email Address does not Exits!!";
            }
            return View();
            //string message = "";

            //using (VehicleLeasingEntities1 db = new VehicleLeasingEntities1())
            //{
            //    var account = db.UserDetails.Where(a => a.Email == Email).FirstOrDefault();
            //    if(account != null)
            //    {
            //        string restCode = Guid.NewGuid().ToString();

            //    }
            //    else
            //    {
            //        message = "Account not found";
            //    }
            //}
            //ViewBag.Message = message;
            //return View();

        }
        public ActionResult ResetUserPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }
        [HttpPost]
        public ActionResult ResetUserPassword(String Email, String Password)
        {
            if (Email == "")
            {
                TempData["Msg"] = "Email not Exists!";
            }
            else
            {
                UserDetail user = new UserDetail();
                user.Password = Password;
                var EncryptedPassword = PasswordSecurity.EncryptPassword(user.Password);
                var result = UpdatePassword(Email, EncryptedPassword);
                if(result!=false)


                //HttpResponseMessage Res = await GlobalVariables.client.GetAsync("Users/UpdateUserPassword?Email=" + Email + "&Password=" + Password);
                
                {
                    TempData["Message"] = "Password Successfully Reset.";
                    return View();
                }
            }
            return View();
        }
        public bool UpdatePassword(string Email, string Password)
        {
            bool status = false;
            UserDetail user = db.UserDetails.Where(x => x.Email == Email).FirstOrDefault();
            user.Password = Password;
            db.Entry(user).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                status = true;
            }
            return status;
        }
        //public string UpdatePassword(string Email,string EncryptedPassword)
        //{

        //}

        //    public ActionResult ResetPassword(string id)
        //    {
        //        //Verify the reset password link
        //        //Find account associated with this link
        //        //redirect to reset password page
        //        if (string.IsNullOrWhiteSpace(id))
        //        {
        //            return HttpNotFound();
        //        }

        //        using (VehicleLeasingEntities1 db = new VehicleLeasingEntities1())
        //        {
        //            var user = db.UserDetails.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
        //            if (user != null)
        //            {
        //                ResetPasswordModel model = new ResetPasswordModel();
        //                model.ResetCode = id;
        //                return View(model);
        //            }
        //            else
        //            {
        //                return HttpNotFound();
        //            }
        //        }
        //    }


        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult ResetPassword(ResetPasswordModel model)
        //    {
        //        var message = "";
        //        if (ModelState.IsValid)
        //        {
        //            using (VehicleLeasingEntities1 dc = new VehicleLeasingEntities1())
        //            {
        //                var user = dc.UserDetails.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
        //                if (user != null)
        //                {
        //                    user.Password = Crypto.Hash(model.NewPassword);
        //                    user.ResetPasswordCode = "";
        //                    dc.Configuration.ValidateOnSaveEnabled = false;
        //                    dc.SaveChanges();
        //                    message = "New password updated successfully";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            message = "Something invalid";
        //        }
        //        ViewBag.Message = message;
        //        return View(model);
        //    }

        //}
    }
}
