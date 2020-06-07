using MovieApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;


namespace MovieApp.DAL
{
    public class UserDAL : IUserDAL , IDisposable
    {
        private DBContext _db = new DBContext();

        public bool Registrer(User userInn)
        {
            try
            {
                var newUser = new Users()
                {
                    Fornavn = userInn.fornavn,
                    Etternavn = userInn.etternavn,
                    Adresse = userInn.adresse,
                    Epost = userInn.epost
                };


                byte[] salt = LagSalt();
                byte[] hash = LagHash(userInn.passord, salt);

                newUser.Passord = hash;
                newUser.Salt = salt;

                newUser.AccessLevel = "User";

                try
                {
                    var findCity = _db.CityTable.Find(userInn.postnr);
                    newUser.Poststeder = findCity;
                }
                catch (Exception e)
                {
                    var errorLog = new ErrorFiler();
                    errorLog.WriteError(e.GetType().FullName, "UserDAL, Registrer, FindCity");
                }

                _db.UserTable.Add(newUser);
                _db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, Registrer, Legg til ny bruker");
                return false;
            }
        }

        public bool EmailTaken(User userInn)
        {
            return _db.UserTable.Any(d => d.Epost == userInn.epost);
        }

        public bool ValidatePassword(User innUser)
        {
            var User = _db.UserTable.FirstOrDefault(d => d.Epost == innUser.epost);
            byte[] testPassword = LagHash(innUser.passord, User.Salt);
            bool rightUser = User.Passord.SequenceEqual(testPassword);
            if (rightUser)
            {
                return true;
            }

            return false;
        }

        public bool Update()
        {
            var startDb = new DBInit();

            if (!_db.CityTable.Any())
            {
                try
                {
                    startDb.Init(_db);

                }
                catch (Exception e)
                {
                    var errorLog = new ErrorFiler();
                    errorLog.WriteError(e.GetType().FullName, "DBinit");
                }
                
            }
            return true;
        }

        public string GetZipCode(string postnummer)
        {
           return _db.CityTable.Find(postnummer).Poststed;
        }

        public Users GetUser(User user)
        {
            return _db.UserTable.FirstOrDefault(d => d.Epost == user.epost);
        }

        public Order CompletePurchase(int userId, List<OrderItem> orderItems)
        {
            var user = _db.UserTable.Find(userId);

            var order = new Order()
            {
                OrderItems = orderItems,
                OrderUser = user
            };

            _db.OrderTable.Add(order);
            _db.SaveChanges();

            return order;
        }

        public List<Movie> GetMovies()
        {
            return _db.MovieTable.ToList();
        }

        public Movie GetMovie(int id)
        {
            return _db.MovieTable.Find(id);
        }

        public bool AddMovie(Movie movie, string ImagePath)
        {
            try
            {
                movie.Image = ImagePath;
                _db.MovieTable.Add(movie);
                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, AddMovie");
            }
            return false;
        }

        public bool AdminRegistrer(User userInn)
        {
            try
            {
                var newUser = new Users()
                {
                    Fornavn = userInn.fornavn,
                    Etternavn = userInn.etternavn,
                    Adresse = userInn.adresse,
                    Epost = userInn.epost
                };


                byte[] salt = LagSalt();
                byte[] hash = LagHash(userInn.passord, salt);
                newUser.Passord = hash;
                newUser.Salt = salt;

                newUser.AccessLevel = userInn.accessLevel.ToString();

                try
                {
                    var findCity = _db.CityTable.Find(userInn.postnr);
                    newUser.Poststeder = findCity;
                }
                catch (Exception e)
                {
                    var errorLog = new ErrorFiler();
                    errorLog.WriteError(e.GetType().FullName, "UserDAL, AdminRegistrer, FindCity");
                }

                _db.UserTable.Add(newUser);
                _db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, AdminRegistrer, Legg til ny bruker");
            }

            return false;
        }

        public List<Users> GetUserList()
        {
            return _db.UserTable.ToList();
        }

        public User GetUser(int id)
        {
            try
            {
                var innUser = _db.UserTable.Find(id);

                var outUser = new User()
                {
                    fornavn = innUser.Fornavn,
                    etternavn = innUser.Etternavn,
                    adresse = innUser.Adresse,
                    epost = innUser.Epost,
                    postnr = innUser.Poststeder.Postnummer,
                    accessLevel = innUser.AccessLevel
                };

                return outUser;
            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, GetUser, ingen bruker funnet");
            }

            var errorUser = new User()
            {
                errorMessage = "Ingen bruker funnet"
            };

            return errorUser;
        }

        
        public bool UpdateUser(User innUser)
        {

            try
            {
                var editUser = GetUser(innUser);

                editUser.Fornavn = innUser.fornavn;
                editUser.Etternavn = innUser.etternavn;
                editUser.Adresse = innUser.adresse;
                editUser.Poststeder.Postnummer = innUser.postnr;
                editUser.AccessLevel = innUser.accessLevel;

                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, UpdateUser");
            }

            return false;
        }

        public bool UpdateMovie(Movie innMovie, string ImagePath)
        {
            try
            {
                var editMovie = GetMovie(innMovie.Id);

                editMovie.Title = innMovie.Title;
                editMovie.Actor = innMovie.Actor;
                editMovie.Director = innMovie.Director;
                editMovie.ReleasedYear = innMovie.ReleasedYear;
                editMovie.Genre = innMovie.Genre;
                editMovie.Image = ImagePath;
                editMovie.Price = innMovie.Price;


                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, UpdateMovie");
            }

            return false;
        }

        public bool DeleteOrder(int id)
        {
            try
            {
                var order = _db.OrderTable.Find(id);

                _db.OrderTable.Remove(order);
                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, DeleteOrder");
            }

            return false;

        }

        public bool DeleteMovie(int id)
        {
            try
            {
                var movie = GetMovie(id);

                Debug.WriteLine("movie: " + movie);

                _db.MovieTable.Remove(movie);
                _db.SaveChanges();

                return true;
            } catch (Exception e)
            {
                Debug.WriteLine("ERROR");
                var errorLog = new ErrorFiler();
                errorLog.WriteError(e.GetType().FullName, "UserDAL, DeleteMovie");
            }

            return false;
        }

        public static byte[] LagHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 1000); // 1000 angir hvor mange ganger hash funskjonen skal utføres for økt sikkerhet
            return pbkdf2.GetBytes(keyLength);
        }

        public static byte[] LagSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
        
}
