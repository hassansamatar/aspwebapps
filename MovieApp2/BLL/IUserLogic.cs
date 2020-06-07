using System.Collections.Generic;
using MovieApp.Model;

namespace MovieApp.BLL
{
    public interface IUserLogic
    {
        Order CompletePurchase(int userId, List<OrderItem> orderItems);
        bool EmailTaken(User user);
        Movie GetMovie(int id);
        List<Movie> GetMovies();
        Users GetUser(User user);
        User GetUser(int id);
        string GetZipCode(string postnummer);
        bool Registrer(User user);
        bool Update();
        bool ValidatePassword(User user);
        bool AddMovie(Movie movie, string ImagePath);
        bool AdminRegistrer(User user);
        List<Users> GetUserList();
        bool UpdateUser(User user);
        bool UpdateMovie(Movie movie, string ImagePath);
        bool DeleteOrder(int id);
        bool DeleteMovie(int id);
    }
}