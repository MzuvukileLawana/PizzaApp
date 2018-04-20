using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizza.Controllers
{
    public class AuthenticationController : Controller
    {
        PizzaContext db = new PizzaContext();
        // GET: Authentication
        public ActionResult SignUp(Customer customer)
        {
            try
            {
                ViewBag.Sent = false;

                if (customer.Title != null)
                {
                    db.InsertCustomer(customer.Title, customer.Name, customer.Surname, customer.DateOfBirth, customer.EmailAddress, customer.PhysicalAddress, customer.PrimarySchool, customer.Password, customer.Repassword);
                    ViewBag.Sent = true;
                    ViewBag.Successful = "Successufully signed up: Thank you for joining us";
                }
                return RedirectToAction("Index", "Home");
              

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public ActionResult CustomerLogin(Customer customer)
        {
            try
            {


                    var result = db.Login(customer.EmailAddress, customer.Password).SingleOrDefault();

                    if (result != null)
                    {
                        Session["EmailAddress"] = customer.EmailAddress;

                    TempData["found"] = true;
                    TempData["WelcomeMessage"] = "Hello " + customer.EmailAddress;
                    TempData["successful"] = "Successfully logged in";
                  
                    
                }

               return RedirectToAction("Index","Home");

            }
            catch(Exception e)
            {
                throw;
            }
            
        }
        public ActionResult ResetPassword(Customer customer)
        {
            ViewBag.Sent = false;
            if (customer.EmailAddress != null)
            {
                db.ResetPassord(customer.EmailAddress, customer.PrimarySchool, customer.Password, customer.Repassword);
                ViewBag.Sent = true;
                ViewBag.Successful = "Successufully signed up: Thank you for joining us";
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult LogOut(Customer customer)
        {
            try
            {
                
                Session["EmailAddress"] = "NewSession";
                TempData["found"] = null;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public ActionResult AdminLogin(Admin admin)
        {
            var results = db.AdminLogin(admin.UserName, admin.Password).SingleOrDefault();
            if(results != null)
            {
                return RedirectToAction("AdminPg", "AdminPage");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
       
 
    }
}