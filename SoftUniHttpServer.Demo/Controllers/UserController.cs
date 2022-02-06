﻿using SoftUniHttpServer.Server.Controllers;
using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Demo.Controllers
{
    public class UserController : Controller
    {
        private const string username = "user";

        private const string password = "user123";

        public UserController(Request request) 
            : base(request)
        {
        }

        public Response Login() => View();

        public Response LoginUser()
        {
            Request.Session.Clear();

            var bodyText = "";

            var usernameMatches = Request.Form["username"] == username;
            var passwordMatches = Request.Form["password"] == password;

            if (usernameMatches && passwordMatches)
            {
                Request.Session[Session.SessionUserKey] = "MyUserId";
                var cookies = new CookieCollection();
                cookies.Add(Session.SessionCookieName,
                    Request.Session.Id);

                bodyText = "<h3>Logged successfully!</h3>";

                return Html(bodyText, cookies);
            }

            return Redirect("/Login");
        }

        public Response GetUserData()
        {
            if (Request.Session.ContainsKey(Session.SessionUserKey))
            {
                return Html($"<h3>Currently logged-in user " + $"is with username '{username}'</h3>");
            }

            return Redirect("/Login");
        }

        public Response Logout()
        {
            Request.Session.Clear();

            return Html("<h3>Logged out successfully!</h3>");
        }
    }
}