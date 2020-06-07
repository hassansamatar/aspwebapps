using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MovieApp.DAL
{
    public class DBInit
    {
        public void Init(DBContext context)
        {
            var Movie1 = new Model.Movie
            {
                Title = "Solo: A Star War Story",
                Genre = "Action",
                Actor = "Emila Clarke",
                Director = "Ron Haward",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img.jpg"
            };

            var Movie2 = new Model.Movie
            {
                Title = "The Meg",
                Genre = "Action",
                Actor = "Ruby Rose",
                Director = "Jon Turteltaub",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img1.jpg"
            };

            var Movie3 = new Model.Movie
            {
                Title = "Venom",
                Genre = "Action",
                Actor = "Tom Hardy",
                Director = "Ruben Fleisher",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img2.jpg",

            };

            var Movie4 = new Model.Movie
            {
                Title = "Misson Impossible: Fallout",
                Genre = "Action",
                Actor = "Tom Cruise",
                Director = "Christopher McQuarrie",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img3.jpg"
            };

            var Movie5 = new Model.Movie
            {
                Title = "Black Panther",
                Genre = "Adventure",
                Actor = "Chadwic Boseman",
                Director = "Ryan Coogler",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img4.jpg"
            };

            var Movie6 = new Model.Movie
            {
                Title = "Alpha",
                Genre = "Action",
                Actor = "Kodi Smit-McPhee",
                Director = "Albert Hughes",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img5.jpg"
            };

            var Movie7 = new Model.Movie
            {
                Title = "Jonny English Strikes Again",
                Genre = "Comedy",
                Actor = "Rowan Atkinson",
                Director = "David Kerr",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img6.jpg"
            };

            var Movie8 = new Model.Movie
            {
                Title = "The Equalize 2",
                Genre = "Action",
                Actor = "Denzel Washington",
                Director = "Antonie Fuqua",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img7.jpg"
            };

            var Movie9 = new Model.Movie
            {
                Title = "Tag",
                Genre = "Comedy",
                Actor = "Jenny Renner",
                Director = "Jeff Tosmic",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img8.jpg"

            };

            var Movie10 = new Model.Movie
            {
                Title = "Uncle Drew",
                Genre = "Comedy",
                Actor = "Kyrie Irving",
                Director = "Charles Stone lll",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img9.jpg"

            };

            var Movie11 = new Model.Movie
            {
                Title = "Night School",
                Genre = "Comedy",
                Actor = "Kevin Hart",
                Director = "Malcom D.Lee",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img10.jpg"

            };

            var Movie12 = new Model.Movie
            {
                Title = "Game Night",
                Genre = "Comedy",
                Actor = "Jason Batman",
                Director = "John Francis Daley",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img11.jpg"

            };

            var Movie13 = new Model.Movie
            {
                Title = "Life Of The Party",
                Genre = "Comedy",
                Actor = "Melissa McCarthy",
                Director = "Melissa McCarthy",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img12.jpg"
            };

            var Movie14 = new Model.Movie
            {
                Title = "Jurrasic World",
                Genre = "Adventure",
                Actor = "Chris Pratt",
                Director = "J.A. Bayona",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img13.jpg"
            };

            var Movie15 = new Model.Movie
            {
                Title = "Avvengers: Infinity War",
                Genre = "Adventure",
                Actor = "Robert D.Jr.",
                Director = "Joe Russo",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img14.jpg"

            };
            var Movie16 = new Model.Movie
            {
                Title = "Ant-Man and The Wasp",
                Genre = "Adventure",
                Actor = "Paul Rudd",
                Director = "Peython Reed",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img15.jpg"
            };
            /*
            var Movie17 = new Model.Movie
            {
                Title = "Next Gen",
                Genre = "Adventure",
                Actor = "JAson Sudeikis",
                Director = "Kevin R.Adams.",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img16.jpg"
            };

            var Movie18 = new Model.Movie
            {
                Title = "Spiderman: Into the spider-verse",
                Genre = "Adventure",
                Actor = "Nicholas Cage",
                Director = "Bob Persichetti",
                ReleasedYear = 2018,
                Price = 199,
                Image = "jpg/img17.jpg"
            };
            */

            context.MovieTable.Add(Movie1);
            context.MovieTable.Add(Movie2);
            context.MovieTable.Add(Movie3);
            context.MovieTable.Add(Movie4);
            context.MovieTable.Add(Movie5);
            context.MovieTable.Add(Movie6);
            context.MovieTable.Add(Movie7);
            context.MovieTable.Add(Movie8);
            context.MovieTable.Add(Movie9);
            context.MovieTable.Add(Movie10);
            context.MovieTable.Add(Movie11);
            context.MovieTable.Add(Movie12);
            context.MovieTable.Add(Movie13);
            context.MovieTable.Add(Movie14);
            context.MovieTable.Add(Movie15);
            context.MovieTable.Add(Movie16);
           // context.MovieTable.Add(Movie17);
           // context.MovieTable.Add(Movie18);


            string path = HttpContext.Current.Server.MapPath(@"~\Content\Postnummerregister-ansi.txt");

            string[] lines = System.IO.File.ReadAllLines(path);



            foreach (var line in lines)
            {
                string[] words = line.Split('\t');
                var postedsRad = new Model.City()
                {
                    Postnummer = words[0],
                    Poststed = words[1]
                };

                context.CityTable.Add(postedsRad);
                context.SaveChanges();
            }

            var defaultAdmin = new Model.Users
            {
                Fornavn = "Administrator",
                Etternavn = "",
                Adresse = "",
                Epost = "Admin",
                AccessLevel = "Admin"

            };

            byte[] salt = UserDAL.LagSalt();
            byte[] hash = UserDAL.LagHash("admin", salt);
            defaultAdmin.Passord = hash;
            defaultAdmin.Salt = salt;

            context.UserTable.Add(defaultAdmin);
            context.SaveChanges();

        }
    }
}