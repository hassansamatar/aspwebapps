using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApp.BLL;
using MovieApp.Controllers;
using MovieApp.Model;
using MovieApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Routing;

namespace MovieApp.UnitTest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void RegustrerUser()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var actionResult = (ViewResult)controller.RegistrerUser();

            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void RegistrerUser_User_Taken()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var testUser = new User { epost = "Duplicate" };

            var actionResult = (ViewResult)controller.RegistrerUser(testUser);

            var result = (User)actionResult.Model;

            Debug.WriteLine("ERROR: "+result.errorMessage);
            
            Assert.AreEqual("Denne eposten er allerede i bruk", result.errorMessage);
            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void RegistrerUser_Post_OK()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var newUser = new User()
            {
                fornavn = "FirstName",
            };

            var result = (RedirectToRouteResult)controller.RegistrerUser(newUser);

            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Dashbord");

        }

        [TestMethod]
        public void RegistrerUser_Post__DB_Fail()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var newUser = new User()
            {
                fornavn = "Fail",
            };

            var actionResult = (ViewResult)controller.RegistrerUser(newUser);

            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void Registrer_User_Post_Model_Fail()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var innUser = new User();

            controller.ViewData.ModelState.AddModelError("fornavn", "Ikke oppgitt fornavn");

            var actionResult = (ViewResult)controller.RegistrerUser(innUser);

            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void MovieList()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var movie = new Movie()
            {
                Title = "Movie",
                Director = "Director",
                Actor = "Actor",
                Genre = "Genre",
                Price = 199
            };

            var movieListExpected = new List<Movie>()
            {
                movie,
                movie,
                movie
            };

            var actionResult = (ViewResult)controller.MovieList();

            var result = (List<Movie>)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(movieListExpected[i].Title, result[i].Title);
                Assert.AreEqual(movieListExpected[i].Director, result[i].Director);
                Assert.AreEqual(movieListExpected[i].Actor, result[i].Actor);
                Assert.AreEqual(movieListExpected[i].Genre, result[i].Genre);
                Assert.AreEqual(movieListExpected[i].Price, result[i].Price);
            }
        }

        [TestMethod]
        public void AddMovie()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var actionResult = (ViewResult)controller.AddMovie();

            Assert.AreEqual(actionResult.ViewName, "");

        }

        [TestMethod]
        public void AddMovie_Post_OK()
        {

            var newMovie = new Movie()
            {
                Title = "Title",
            };

            // Create Mock HttpContext, server and image file
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockServer = new Mock<HttpServerUtilityBase>();
            var mockFile = new Mock<HttpPostedFileBase>();

            // Setup server, httpcontext and file
            mockServer.Setup(x => x.MapPath(@"~\Content\jpg\")).Returns(@"~\Content\jpg\");
            mockHttpContext.Setup(x => x.Server).Returns(mockServer.Object);
            mockFile.Setup(x => x.FileName).Returns("image.jpg");

            // Setup controller and controllercontext
            var controller = new AdminController(new UserLogic(new UserDALStub()));
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller); 

            // Act
            var result = (RedirectToRouteResult)controller.AddMovie(newMovie, mockFile.Object);

            // Assert
            mockFile.Verify(x => x.SaveAs(@"~\Content\jpg\image.jpg"));
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Dashbord");

        }

        [TestMethod]
        public void AddMovie_File_Upload_Fail()
        {
            var newMovie = new Movie()
            {
                Title = "Title",
            };

            // Create Mock HttpContext, server and image file
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockServer = new Mock<HttpServerUtilityBase>();
            var mockFile = new Mock<HttpPostedFileBase>();

            // Setup server, httpcontext and file
            mockServer.Setup(x => x.MapPath(@"~\Content\jpg\")).Returns(@"~\Content\jpg\");
            mockHttpContext.Setup(x => x.Server).Returns(mockServer.Object);
            mockFile.Setup(x => x.FileName).Returns("fail.jpg");

            // Setup controller and controllercontext
            var controller = new AdminController(new UserLogic(new UserDALStub()));
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            // Act
            var result = (ViewResult)controller.AddMovie(newMovie, mockFile.Object);

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void AddMovie_Model_Fail()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var innMovie = new Movie();
            var mockFile = new Mock<HttpPostedFileBase>();

            controller.ViewData.ModelState.AddModelError("Title", "Ikke oppgitt tittel");

            var actionResult = (ViewResult)controller.AddMovie(innMovie, mockFile.Object);

            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void UserList()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var newUser = new Users()
            {
                Id = 1,
                Fornavn = "Fornavn",
                Etternavn = "Etternavn",
                Adresse = "Adresse",
                Epost = "post@epost.no",

            };

            var userListExpected = new List<Users>()
            {
                newUser,
                newUser,
                newUser
            };

            var actionResult = (ViewResult)controller.UserList();

            var result = (List<Users>)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(userListExpected[i].Fornavn, result[i].Fornavn);
                Assert.AreEqual(userListExpected[i].Etternavn, result[i].Etternavn);
                Assert.AreEqual(userListExpected[i].Adresse, result[i].Adresse);
                Assert.AreEqual(userListExpected[i].Epost, result[i].Epost);

            }

        }

        [TestMethod]
        public void EditUser_Get_User()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            int id = 1;

            var result = (ViewResult)controller.EditUser(id);

            var expectedUser = new User()
            {
                fornavn = "Fornavn",
                etternavn = "Etternavn",
                adresse = "Adresse",
                epost = "post@epost.no"
            };

            var actionResult = (User)result.Model;

            Assert.AreEqual(result.ViewName, "");

            Assert.AreEqual(actionResult.fornavn, expectedUser.fornavn);
            Assert.AreEqual(actionResult.etternavn, expectedUser.etternavn);
            Assert.AreEqual(actionResult.adresse, expectedUser.adresse);
            Assert.AreEqual(actionResult.epost, expectedUser.epost);

        }

        [TestMethod]
        public void EditUser_Get_User_Not_Found()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            int id = 0;

            var actionResult = (ViewResult)controller.EditUser(id);

            var result = (User)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.id, 0);

        }

        [TestMethod]
        public void EditUser_Post_Ok()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var newUser = new User()
            {
                fornavn = "NewFirstName"
            };

            var result = (RedirectToRouteResult)controller.EditUser(newUser);

            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "UserList");

        }

        [TestMethod]
        public void EditUser_Post_DB_Fail()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var newUser = new User()
            {
                fornavn = "Fail",
            };

            var actionResult = (ViewResult)controller.EditUser(newUser);

            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditUser_Post_Model_fail()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var innUser = new User();

            controller.ViewData.ModelState.AddModelError("fornavn", "Ikke oppgitt fornavn");

            var actionResult = (ViewResult)controller.EditUser(innUser);

            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EditMovie_Get_Movie()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            int id = 1;

            var result = (ViewResult)controller.EditMovie(id);

            var expectedMovie = new Movie()
            {
                Title = "Tittel",
                Actor = "Actor",
                Director = "Director",
                Genre = "Genre"
            };

            var actionResult = (Movie)result.Model;

            Assert.AreEqual(result.ViewName, "");

            Assert.AreEqual(actionResult.Title, expectedMovie.Title);
            Assert.AreEqual(actionResult.Actor, expectedMovie.Actor);
            Assert.AreEqual(actionResult.Director, expectedMovie.Director);
            Assert.AreEqual(actionResult.Genre, expectedMovie.Genre);
        }

        [TestMethod]
        public void EditMovie_Get_Movie_Not_Found()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            int id = 0;

            var actionResult = (ViewResult)controller.EditMovie(id);

            var result = (Movie)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.Id, 0);
        }

        [TestMethod]
        public void EditMovie_Post_File_Upload_Fail()
        {
            var newMovie = new Movie()
            {
                Title = "Title",
            };

            // Create Mock HttpContext, server and image file
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockServer = new Mock<HttpServerUtilityBase>();
            var mockFile = new Mock<HttpPostedFileBase>();

            // Setup server, httpcontext and file
            mockServer.Setup(x => x.MapPath(@"~\Content\jpg\")).Returns(@"~\Content\jpg\");
            mockHttpContext.Setup(x => x.Server).Returns(mockServer.Object);
            mockFile.Setup(x => x.FileName).Returns("fail.jpg");

            // Setup controller and controllercontext
            var controller = new AdminController(new UserLogic(new UserDALStub()));
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            // Act
            var result = (ViewResult)controller.EditMovie(newMovie, mockFile.Object);

            // Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod]
        public void EditMovie_Post_Ok()
        {
            var newMovie = new Movie()
            {
                Title = "Title",
            };

            // Create Mock HttpContext, server and image file
            var mockHttpContext = new Mock<HttpContextBase>();
            var mockServer = new Mock<HttpServerUtilityBase>();
            var mockFile = new Mock<HttpPostedFileBase>();

            // Setup server, httpcontext and file
            mockServer.Setup(x => x.MapPath(@"~\Content\jpg\")).Returns(@"~\Content\jpg\");
            mockHttpContext.Setup(x => x.Server).Returns(mockServer.Object);
            mockFile.Setup(x => x.FileName).Returns("image.jpg");

            // Setup controller and controllercontext
            var controller = new AdminController(new UserLogic(new UserDALStub()));
            controller.ControllerContext = new ControllerContext(mockHttpContext.Object, new RouteData(), controller);

            // Act
            var result = (RedirectToRouteResult)controller.EditMovie(newMovie, mockFile.Object);

            // Assert
            mockFile.Verify(x => x.SaveAs(@"~\Content\jpg\image.jpg"));
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "MovieList");
        }

        [TestMethod]
        public void EditMovie_Post_Model_Fail()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            var innMovie = new Movie();
            var mockFile = new Mock<HttpPostedFileBase>();

            controller.ViewData.ModelState.AddModelError("Title", "Ikke oppgitt tittel");

            var actionResult = (ViewResult)controller.EditMovie(innMovie, mockFile.Object);

            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void UserInfo()
        {
            var controller = new AdminController(new UserLogic(new UserDALStub()));

            int id = 1;

            var result = (ViewResult)controller.UserInfo(id);

            var expectedUser = new Users()
            {
                Fornavn = "Fornavn",
                Etternavn = "Etternavn",
                Adresse = "Adresse",
                Epost = "post@epost.no"
            };

            var actionResult = (Users)result.Model;

            Assert.AreEqual(result.ViewName, "");

            Assert.AreEqual(actionResult.Fornavn, expectedUser.Fornavn);
            Assert.AreEqual(actionResult.Etternavn, expectedUser.Etternavn);
            Assert.AreEqual(actionResult.Adresse, expectedUser.Adresse);
            Assert.AreEqual(actionResult.Epost, expectedUser.Epost);
        }

        [TestMethod]
        public void DeleteOrder()
        {
            int id = 1;
            int id2 = 1;

            var controller = new AdminController(new UserLogic(new UserDALStub()));
            var result = controller.DeleteOrder(id, id2);

            var redirectResult = (RedirectToRouteResult)result;

            Assert.AreEqual(redirectResult.RouteName, "");
            Assert.AreEqual(redirectResult.RouteValues.Values.First(), 1);
            Assert.AreEqual(redirectResult.RouteValues.Values.Last(), "UserInfo");

        }

    }
}
