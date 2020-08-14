using ASG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASG.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel registrationModel)
        {
            try
            {
                using (RegistrationCtl db = new RegistrationCtl())
                {
                    db.insert(registrationModel);
                    ViewBag.sucess = "Save Sucessfully";
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
            }

            return View();
        }

        public JsonResult IsEmailExits(string Email)
        {
            int value = 0;
            try
            {
                using (RegistrationCtl db = new RegistrationCtl())
                {
                    value = db.IsEmailExist(Email);
                }
            }
            catch (Exception ex)
            {
                value = 0;
            }
            return value == 0 ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(RegistrationModel registrationModel)
        {
            int value = 0;
            try
            {
                using (RegistrationCtl db = new RegistrationCtl())
                {
                    value = db.IsLogin(registrationModel);
                    if (value == 1)
                    {
                        return RedirectToAction("Dashboard", "UserManagement");
                    }
                    else
                    {
                        ViewBag.error = "invalid username and password.";
                    }
                }
            }
            catch (Exception ex)
            {
                value = 0;
                ViewBag.error = "Something missing";
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ForgotPasswordBody()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(RegistrationModel registrationModel)
        {
            try
            {
                using (RegistrationCtl db = new RegistrationCtl())
                {
                    db.Update(registrationModel);
                    ViewBag.sucess = "Save Sucessfully";
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
            }
            return View();
        }

    }
}