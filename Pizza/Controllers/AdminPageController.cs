using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizza.Controllers
{
    public class AdminPageController : Controller
    {
        PizzaContext db = new PizzaContext();
        // GET: AdminPage
        public ActionResult AdminPg()
        {
            return View();
        }
        public ActionResult ViewCustomers()
        {

            return View(db.ViewCustomers);
        }
        public ActionResult OrderHistory()
        {

            return View(db.OrderHistories);
        }
        public ActionResult AvailableDrink()
        {
            return View(db.CoolDrinks);
        }
        public ActionResult ViewBase()
        {
            return View(db.Bases);
        }
        public ActionResult ViewTopping()
        {
            return View(db.Toppings);
        }
        public ActionResult ViewFeedback()
        {
            return View(db.Feedbacks);
        }
        public ActionResult ViewPizza()
        {
            return View(db.Pizzas);
        }
        public ActionResult UpdatePizza(UpdatePizzaPriceModel update)
        {
            db.UpdatePizzaPrice(update.pizzaName,update.price);
            if(update.pizzaName != null)
            {
                return RedirectToAction("AdminPg");
            }
            return View();
        }
        public ActionResult UpdateDrink(UpdateDrinkPrice update1)
        {
            db.UpdateCoolDrink(update1.flavour, update1.add);
            if (update1.flavour != null)
            {
                return RedirectToAction("AdminPg");
            }
            return View();
        }
        public ActionResult UpdateSize(UpdatePizzaSizePrice update2)
        {
            db.UpdateSizePrice(update2.size, update2.price);
            if (update2.size != null)
            {
                return RedirectToAction("AdminPg");
            }
            return View();
        }
        public ActionResult UpdateTopping(UpdateToppingPrice update3)
        {
            db.UpdatePizzaTopping(update3.topping, update3.price);
            if (update3.topping != null)
            {
                return RedirectToAction("AdminPg");
            }
            return View();
        }
        public ActionResult DeleteCustomer(DeleteCustomer delete)
        {
            db.DeleteCustomer(delete.email);
            if (delete.email != null)
            {
                return RedirectToAction("AdminPg");
            }
            return View();
        }
        public ActionResult DeleteOrder(DeleteOrderHistory delete)
        {
            

            db.DeleteOrderHistory(delete.order);
            
            
             
            
            return View();
        }
        public ActionResult AdminInsertCust(Customer customer)
        {
            if (customer.Title != null)
            {


                db.InsertCustomer(customer.Title, customer.Name, customer.Surname, customer.DateOfBirth, customer.EmailAddress, customer.PhysicalAddress, customer.PrimarySchool, customer.Password, customer.Repassword);
                return RedirectToAction("ViewCustomers");
            }
              return View();  
        }
    }
}