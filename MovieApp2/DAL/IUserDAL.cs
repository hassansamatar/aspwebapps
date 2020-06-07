using System.Collections.Generic;
using MovieApp.Model;

namespace MovieApp.DAL
{
    public interface IUserDAL
    {
        bool AddMovie(Model.Movie movie, string ImagePath);
        Order CompletePurchase(int userId, List<OrderItem> orderItems);
        bool EmailTaken(Model.User userInn);
        Movie GetMovie(int id);
        List<Movie> GetMovies();
        Users GetUser(User user);
        Model.User GetUser(int id);
        string GetZipCode(string postnummer);
        bool Registrer(Model.User userInn);
        bool Update();
        bool ValidatePassword(User user);
        bool AdminRegistrer(User userInn);
        List<Users> GetUserList();
        bool UpdateUser(User user);
        bool UpdateMovie(Movie movie, string ImagePath);
        bool DeleteOrder(int id);
        bool DeleteMovie(int id);
    }
}