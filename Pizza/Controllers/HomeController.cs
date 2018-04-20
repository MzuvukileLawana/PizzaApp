using Pizza.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizza.Controllers
{
    public class HomeController : Controller
    {
        PizzaContext db = new PizzaContext();

        public ActionResult Index()
        {
            if (!Session.IsNewSession)
            {
                if (!Session["EmailAddress"].Equals("NewSession"))
                {
                    ViewBag.Message = true;

                    ViewBag.Message1 = "Hello " + Session["EmailAddress"];
                }

            }else
            {
                if (TempData["found"] != null)
                {
                    ViewBag.Message = TempData["found"];
                    ViewBag.Message1 = TempData["WelcomeMessage"].ToString();
                    ViewBag.Message2 = TempData["successful"].ToString();

                }
                else
                {
                    ViewBag.Message = false;
                }

            }
            

            

            return View();
        }
        public ActionResult Menu(string value)
        {

            //DisplayModel model = new DisplayModel();
            //var price = /*db.Pizzas.Select(t => t.PizzaPrice).FirstOrDefault();*/db.SelectedPizza("Something Meaty Pizza").SingleOrDefault();

            //if(price != null)
            //{
            //    ViewBag.Price = price;

            //}
            ViewBag.Found = false;
            if (!Session.IsNewSession)
            {
                if (!Session["EmailAddress"].Equals("NewSession"))
                {
                    ViewBag.Message = true;

                    ViewBag.Message1 = "Hello " + Session["EmailAddress"];
                }

            }
            
        
            return View();
        }
       
        [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]
        public ActionResult GetPrice( string value)
        {
            try
            {
                string v = Request.QueryString["value"];
                DisplayModel model = new DisplayModel();
                var price = /*db.Pizzas.Select(t => t.PizzaPrice).FirstOrDefault();*/db.SelectedPizza(value).SingleOrDefault();


                Menu(value);

                return Json(new { price = price.ToString() });
            }
            catch (Exception e)
            {
                throw;
            }


        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetPriceOnSize(string size, string currentPrice)
        {
            try
            {
                string v = Request.QueryString["value"];
                DisplayModel model = new DisplayModel();
                var pizzaSizePrice = /*db.Pizzas.Select(t => t.PizzaPrice).FirstOrDefault();*/db.GetPriceOnSizeSelected(size).SingleOrDefault();
                var newPrice = Convert.ToDecimal(currentPrice) + pizzaSizePrice;

                Menu(size);

                return Json(new { price = newPrice.ToString() });
            }
            catch (Exception e)
            {
                throw;
            }


        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetPriceOnTopping(string topping, string currentPrice)
        {
            try
            {
                string v = Request.QueryString["value"];
                DisplayModel model = new DisplayModel();
                var toppingPrice = /*db.Pizzas.Select(t => t.PizzaPrice).FirstOrDefault();*/db.GetPriceOnToppingSelected(topping).SingleOrDefault();
                var newPrice = Convert.ToDecimal(currentPrice) + toppingPrice;
                Menu(topping);

                return Json(new { price = newPrice.ToString() });
            }
            catch (Exception e)
            {
                throw;
            }


        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetPriceOnSelectedFlavour(string drink, string currentPrice)
        {
            try
            {
                string v = Request.QueryString["value"];
                DisplayModel model = new DisplayModel();
                var coolDrinkPrice = /*db.Pizzas.Select(t => t.PizzaPrice).FirstOrDefault();*/db.GetPriceOnSelectedFlavour(drink).SingleOrDefault();
                var newPrice = Convert.ToDecimal(currentPrice) + coolDrinkPrice;

                Menu(drink);

                return Json(new { price = newPrice.ToString() });
            }
            catch (Exception e)
            {
                throw;
            }


        }

        public ActionResult Contact(Feedback feedback)
        {
            try
            {
                ViewBag.Sent = false;
                if (feedback.Name != null)
                {
                    ViewBag.Sent = true;
                    db.SendFeedback(feedback.Name, feedback.Subject, feedback.Comment, feedback.EmailAddress, feedback.PhoneNumber);

                    ViewBag.Successful = "Successufully sent: Thank you for your feedback";


                }
                if (!Session.IsNewSession)
                {
                    if (!Session["EmailAddress"].Equals("NewSession"))
                    {
                        ViewBag.Message = true;

                        ViewBag.Message1 = "Hello " + Session["EmailAddress"];
                    }

                }

                return View();
            }
            catch(Exception e)
            {
                throw;
            }
            
        }
      public ActionResult CustOrderHistory(Customer customer)
        {
            ViewBag.Found = false;
            if (!Session.IsNewSession)
            {
                if (!Session["EmailAddress"].Equals("NewSession"))
                {
                    ViewBag.Message = true;

                    ViewBag.Message1 = "Hello " + Session["EmailAddress"];
                }

            }
            if (Session["EmailAddress"] != null)
            {
                var email = Session["EmailAddress"].ToString();
                var userId = db.Customers.Where(a => a.EmailAddress == email).Select(t => t.EmailAddress).FirstOrDefault();
                if (userId != null)
                {
                    var list = db.CustomerOrderHistory(userId);
                    return View(list);

                }
            }
            

            return View();
        }
        public ActionResult SubmitOrder(OrderViewModel orderViewModel)
        {
            try
            {
                ViewBag.Sent = false;

                if (Session["EmailAddress"] != null)
                {

                    var emailAddress = Session["EmailAddress"].ToString();
                    var userId = db.Customers.Where(a => a.EmailAddress == emailAddress).Select(t => t.CustomerId).FirstOrDefault();
                    var pizzaId = db.Pizzas.Where(a => a.PizzaName == orderViewModel.pizzaName).Select(t => t.PizzaId).FirstOrDefault();
                    var baseId = db.Bases.Where(a => a.SizeOfPizza == orderViewModel.size).Select(t => t.BaseId).FirstOrDefault();
                    var drinkId = db.CoolDrinks.Where(a => a.Flavour == orderViewModel.drink).Select(t => t.DrinkId).FirstOrDefault();
                    var toppingId = db.Toppings.Where(a => a.Description == orderViewModel.topping).Select(t => t.TopId).FirstOrDefault();
                    //saving to Delivery Method
                    var deliveryMethodId = db.SaveDeliveryDetails(orderViewModel.delivery, orderViewModel.address, orderViewModel.number).FirstOrDefault();

                    //Saving to order
                    var orderId = db.SaveOrder(userId, deliveryMethodId, baseId, drinkId ,1, pizzaId, DateTime.Now);

                    ViewBag.Sent = true;
                    return RedirectToAction("CustOrderHistory");
                }
                else
                {
                    ViewBag.Successful = "Unsuccessufully: Please login before you place an order";
                }
                

                //Save To Pizza
           // }
                return RedirectToAction("Menu");
            }
            catch(Exception e)
            {
                throw;
            }
            
        }
    }
    public  class DisplayModel
    {
        [DisplayFormat(DataFormatString="{0:n0}")]
        public decimal? price { get; set; }
    }
}