using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.DAL;
using MovieApp.Model;

namespace MovieApp.BLL
{
    public class UserLogic : IUserLogic
    {
        private IUserDAL _db;

        public UserLogic()
        {
            _db = new UserDAL();
        }

        public UserLogic(IUserDAL stub)
        {
            _db = stub;
        }
        public bool Registrer (User user)
        {
            
            return _db.Registrer(user);
        }

        public List<Movie> GetMovies()
        {
            return _db.GetMovies();
        }

        public bool EmailTaken(User user)
        {
            return _db.EmailTaken(user);
        } 

        public bool ValidatePassword(User user)
        {
            return _db.ValidatePassword(user);
        }

        public bool Update()
        {
            _db.Update();
            return true;
        }

        public string GetZipCode(string postnummer)
        {
            return _db.GetZipCode(postnummer);
        }

        public Users GetUser(User user)
        {
            return _db.GetUser(user);
        }

        public Model.Movie GetMovie(int id)
        {
            return _db.GetMovie(id);
        }

        public Order CompletePurchase(int userId, List<Model.OrderItem> orderItems)
        {
            var order = _db.CompletePurchase(userId, orderItems);
            return order;
        }

        public bool AddMovie(Movie movie, string ImagePath)
        {
            return _db.AddMovie(movie, ImagePath);
        }

        public bool AdminRegistrer(User user)
        {
            return _db.AdminRegistrer(user);
        }

        public List<Users> GetUserList()
        {
            return _db.GetUserList();
        }

        public User GetUser(int id)
        {
            return _db.GetUser(id);
        }

        public bool UpdateUser(User user)
        {
            return _db.UpdateUser(user);
        }

        public bool UpdateMovie(Movie movie, string ImagePath)
        {
            return _db.UpdateMovie(movie, ImagePath);
        }

        public bool DeleteOrder(int id)
        {
            return _db.DeleteOrder(id);
        }

        public bool DeleteMovie(int id)
        {
            return _db.DeleteMovie(id);
        }
    }   
}
