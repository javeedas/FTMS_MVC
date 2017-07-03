using FTMS.DAL;
using FTMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FTMS.Controllers
{
    public class AccountController : Controller
    {
        DataService dal = new DataService();
        // GET: Account

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {    
            bool isValid=dal.Login(user);
            if(isValid)
            {

                FormsAuthentication.SetAuthCookie(user.username, false);
                return Redirect("/Home");
            }
            else
            {
                ViewBag.Message = "username or password is incorrect";
                return View(user);
                
            }
        }
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            bool isValid = dal.Login(user);
            return Json(dal.Login(user), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUser()
        {
            return View();
                

        }
        [HttpPost]
        public ActionResult AddUser(fUser user)
        {
            int result = dal.AddUser(user);
            if (result > 0)
            {
                ViewBag.message = "User Added successfully";
                return View();
            }

            else
            {
                ViewBag.message = "Error occured while adding user";
                return View();
            }


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

     

    }
}