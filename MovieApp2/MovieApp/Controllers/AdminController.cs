using MovieApp.BLL;
using MovieApp.Model;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MovieApp.Controllers
{
    [AuthorizeUser(Users = "Admin")]
    public class AdminController : Controller
    {
        private IUserLogic _db;

        public AdminController()
        {
            _db = new UserLogic();
        }

        public AdminController(IUserLogic stub)
        {
            _db = stub;
        }

        // GET: Admin
        public ActionResult Dashbord()
        {
            if (HttpContext.User.Identity.IsAuthenticated && Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
            }

            return View();
        }
        public ActionResult RegistrerUser()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegistrerUser(User userInn)
        {

            if (_db.EmailTaken(userInn))
            {
                userInn.errorMessage = "Denne eposten er allerede i bruk";
                return View(userInn);
            }

            if (ModelState.IsValid)
            {
                var registrerSuccess =_db.AdminRegistrer(userInn);
                if (registrerSuccess)
                {
                    return RedirectToAction("Dashbord");
                }

            }

            return View(userInn);
        }
   
        public ActionResult MovieList()
        {
            List<Movie> allMovies = _db.GetMovies();
            return View(allMovies);
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movieInn, HttpPostedFileBase file)
        {
            StringBuilder ImagePath = new StringBuilder("jpg/");
            
            if (ModelState.IsValid)
            {
                try
                {
                    file.SaveAs(HttpContext.Server.MapPath(@"~\Content\jpg\") + file.FileName);

                    ImagePath.Append(file.FileName);

                    var success = _db.AddMovie(movieInn, ImagePath.ToString());

                    if (success)
                    {
                        return RedirectToAction("Dashbord");
                    }
                }
                catch (Exception e)
                {
                    var error = new ErrorFiler();
                    error.WriteError(e.GetType().FullName, "Failure in file upload");
                }   
            }

            return View();
        }

        public ActionResult UserList()
        {
            List<Users> userlist = _db.GetUserList();

            return View(userlist);
        } 

        public ActionResult EditUser(int id)
        {
            var user = _db.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {

            if (ModelState.IsValid)
            {
                var updateSuccess = _db.UpdateUser(user);
                if (updateSuccess)
                {
                    return RedirectToAction("UserList");
                }
            }

            return View(user);
  
        }

        public ActionResult EditMovie(int id)
        {
            var movie = _db.GetMovie(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie movieInn, HttpPostedFileBase file)
        {
    
            StringBuilder ImagePath = new StringBuilder("jpg/");


            if (ModelState.IsValid)
                {
                try
                {
                 file.SaveAs(HttpContext.Server.MapPath(@"~\Content\jpg\") + file.FileName);

                 ImagePath.Append(file.FileName);

                 var success = _db.UpdateMovie(movieInn, ImagePath.ToString());

                    if (success)
                    {
                        return RedirectToAction("MovieList");
                    }

                }
                catch (Exception e)
                {
                    var error = new ErrorFiler();
                        error.WriteError(e.GetType().FullName, "Failure in file upload");
                }
            }
            return View();
        }
        

        public ActionResult UserInfo(int id)
        {
            var user = _db.GetUser(id);
            var displayUser = _db.GetUser(user);
            return View(displayUser);
        }

        public ActionResult DeleteOrder(int id, int id2)
        {
            _db.DeleteOrder(id2);
            return RedirectToAction("UserInfo", new { ID = id });
        }
       
    }
}