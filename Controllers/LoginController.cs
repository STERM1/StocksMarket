using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StocksMarket.Models;

namespace StocksMarket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginAction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAction(Login lg)
        {
            if (ModelState.IsValid) // Check the model state for any validation errors
            {
                if (lg.IsValid(lg.username, lg.password)) // Calls the Login class IsValid() for existence of the user in the database. returns true if user is valid
                {
                    Session["LoggedInUser"] = lg.username.ToString();
                    return RedirectToAction("StockPage", "Home"); // Return the "Show.cshtml" view if user is valid
                }
                else
                {
                    ViewBag.Message = "Invalid Username or Password";
                    return View(); //return the same view with message "Invalid Username or Password"
                }
            }
            else
            {
                return View(); // Return the same view with validation errors.
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            //FormsAuthentication.SignOut();
            return RedirectToAction("LoginAction", "Login");
        }
    }
}