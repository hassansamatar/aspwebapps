using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using MovieApp.BLL;
using MovieApp.Model;


namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private IUserLogic _db;

        public HomeController()
        {
            _db = new UserLogic();
        }

        public HomeController (IUserLogic stub)
        {
            _db = stub;
        }

        public ActionResult Index()
        {

            Update();

            if (HttpContext.User.Identity.IsAuthenticated && Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
            }

            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<int>();
            }


            var allMovies = _db.GetMovies();
            return View(allMovies);
        }

        public ActionResult Registrer()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Registrer(User userInn)
        {

            if (_db.EmailTaken(userInn))
            {
                userInn.errorMessage = "Denne eposten er allerede i bruk";
                return View(userInn);
            }

            if (ModelState.IsValid)
            {
                _db.Registrer(userInn);
            }

            return RedirectToAction("Index");
        }

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string ReturnUrl)
        {

            var User = _db.GetUser(user);
            if (_db.ValidatePassword(user))
            {
                FormsAuthentication.SetAuthCookie(User.AccessLevel, false);
                Session["UserName"] = User.Fornavn;
                Session["UserId"] = User.Id;

                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            } 
            else
            {
                return View(user);
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Logout(User outUser)
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            Session["UserId"] = 0;
            Response.Redirect("Index");
            return RedirectToAction("Logout");
        }

        public ActionResult Update()
        {
            _db.Update();

            return RedirectToAction("Index");
        }

       
        public string GetZipCode(string postnummer)
        {
            var jsonSerializer = new JavaScriptSerializer();
            try
            {
                string json = jsonSerializer.Serialize(_db.GetZipCode(postnummer));
                return json;
            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "HomeController, GetZipCode");
                string json = jsonSerializer.Serialize("Ugyldig postnummer");
                return json;
            }
        }
 
        public ActionResult AddToCart(int id)
        {
            var cart = (List<int>)Session["Cart"];

            cart.Add(id);

            Session["Cart"] = cart;

            var movie = _db.GetMovie(id);

            return View(movie);
        }
        
        public ActionResult ListCart()
        {
            var cart = (List<int>)Session["Cart"];

            var orderItems = new List<OrderItem>();

            foreach (var i in cart)
            {
                var movie = _db.GetMovie(i);

                var orderItem = new OrderItem()
                {
                    Movie = movie
                };

                orderItems.Add(orderItem);
            }

            return View(orderItems);
        }
        
        public ActionResult CompletePurchase()
        {

            int userId = (int)Session["UserId"];

            var cart = (List<int>)Session["Cart"];

            var orderItems = new List<OrderItem>();

            foreach(var i in cart)
            {

                var movie = _db.GetMovie(i);

                var orderItem = new OrderItem()
                {
                    Movie = movie
                };

                orderItems.Add(orderItem);
            }

            var order = _db.CompletePurchase(userId, orderItems);

            Session["Cart"] = new List<int>();

            return View(order);
        }

        [Authorize]
        public ActionResult Betaling()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Betaling(Betaling innBetaling)
        {

            if (ModelState.IsValid)
            {
                    return RedirectToAction("CompletePurchase");
            }
            return View();
        }
    }
}