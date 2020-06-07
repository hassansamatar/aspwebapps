using System;
using System.Collections.Generic;

namespace MovieApp.DAL
{
    public class UserDALStub : IUserDAL
    {
        public bool Registrer(Model.User userInn)
        {
            if (userInn.fornavn == "")
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public List<Model.Movie> GetMovies()
        {
            var MovieList = new List<Model.Movie>();

            var Movie = new Model.Movie()
            {
                Title = "Movie",
                Director = "Director",
                Actor = "Actor",
                Genre = "Genre",
                Price = 199
            };

            MovieList.Add(Movie);
            MovieList.Add(Movie);
            MovieList.Add(Movie);

            return MovieList;
        }

        public bool EmailTaken(Model.User userInn)
        {
            if (userInn.epost == "Duplicate")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidatePassword(Model.User user)
        {
            /*         if (user.id == 0)
                     {
                         var User1 = new Model.Users()
                         {
                             Fornavn = ""
                         };

                         return User1;
                     }
                     else
                     {
                         var User2 = new Model.Users()
                         {
                             Id = 1,
                             Fornavn = "Fornavn",
                             Etternavn = "Etternavn",
                             Adresse = "Adressen 1",
                             Epost = "epost@mail.com",
                             Passord = "passord",
                             Poststeder = new Model.City()
                             {
                                 Postnummer = "1234",
                                 Poststed = "By"
                             }
                         };

                         return User2; 


        } */
            return false;
        }

        public bool Update()
        {
            return true;
        }

        public string GetZipCode(string postnummer)
        {
            if (postnummer == "")
            {
                return "";
            }
            else
            {
                return "1234";
            }
        }

        public Model.Users GetUser(Model.User user)
        {
            if (user.fornavn == "Fornavn") {

                var returnUser = new Model.Users()
                {
                    Fornavn = "Fornavn",
                    Etternavn = "Etternavn",
                    Adresse = "Adresse",
                    Epost = "post@epost.no"
                };

                return returnUser;
            }
            else
            {

                var nullUser = new Model.Users()
                {
                    Id = 0,
                    Fornavn = "Fail"
                };

                return nullUser;
            }
        }

        public Model.Movie GetMovie(int id)
        {
            if (id == 1)
            {
                var returnMovie = new Model.Movie()
                {
                    Title = "Tittel",
                    Actor = "Actor",
                    Director = "Director",
                    Genre = "Genre"
                };

                return returnMovie;
            }
            else
            {
                var Movie = new Model.Movie()
                {
                    Title = "Fail",
                };

                return Movie;
            }
        }

        public Model.Order CompletePurchase(int userId, List<Model.OrderItem> orderItems)
        {
            if (userId == 0)
            {
                var order1 = new Model.Order()
                {
                    OrderUser = new Model.Users()
                    {
                        Id = 0
                    }
                };

                return order1;
            }
            else
            {
                var order2 = new Model.Order()
                {
                    
                    OrderUser = new Model.Users()
                    {
                        Id = 1,
                        Fornavn = "Fornavn",
                        Etternavn = "Etternavn",
                        Adresse = "Adressen 1",
                        Epost = "epost@mail.com",
                       // Passord = "passord",
                    }
                };

                return order2;
            }

        }

        public bool AddMovie(Model.Movie movie, string ImagePath)
        {
            if (movie.Title == "Title" && ImagePath == "jpg/image.jpg")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool AdminRegistrer(Model.User userInn)
        {

            if (userInn.fornavn == "Fail")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Model.Users> GetUserList()
        {
            var newUser = new Model.Users()
            {
                Id = 1,
                Fornavn = "Fornavn",
                Etternavn = "Etternavn",
                Adresse = "Adresse",
                Epost = "post@epost.no",

            };

            var userList = new List<Model.Users>()
            {
                newUser,
                newUser,
                newUser
            };

            return userList; 
        }

        public Model.User GetUser(int id)
        {
            if (id == 1)
            {
                var returnUser = new Model.User()
                {
                    fornavn = "Fornavn",
                    etternavn = "Etternavn",
                    adresse = "Adresse",
                    epost = "post@epost.no"
                };

                return returnUser;
            }
            else
            {
                var nullUser = new Model.User()
                {
                    id = 0
                };

                return nullUser;
            }
        }

        public bool UpdateUser(Model.User user)
        {
            if (user.fornavn == "NewFirstName")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateMovie(Model.Movie innMovie, String ImagePath)
        {

            if (innMovie.Title == "Title" && ImagePath == "jpg/image.jpg")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteOrder(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool DeleteMovie(int id)
        {
            return true;
        }
    }
}
