﻿using SoftUniHttpServer.Demo.Models;
using SoftUniHttpServer.Server.Controllers;
using SoftUniHttpServer.Server.HTTP;
using System.Text;
using System.Web;

namespace SoftUniHttpServer.Demo.Controllers
{
    public class HomeController : Controller
    {
        private const string FileName = "content.txt";

        public HomeController(Request request)
            :base(request)
        {

        }

        public Response Index() => Text("Hello from the server!");

        public Response Redirect() => Redirect("https://softuni.bg");

        public Response Html() => View();

        public Response HtmlFormPost()
        {
            string name = Request.Form["Name"];
            string age = Request.Form["Age"];

            var model = new FormViewModel()
            {
                Name = name,
                Age = int.Parse(age)
            };

            return View(model);
        }

        public Response Session()
        {
            var sessionExists = Request.Session.ContainsKey(Server.HTTP.Session.SessionCurrentDateKey);

            var bodyText = "";

            if (sessionExists)
            {
                var currentDate = Request.Session[Server.HTTP.Session.SessionCurrentDateKey];
                bodyText = $"Stored date: {currentDate}";
            }
            else
            {
                bodyText = "Current date stored!";
            }

            return Text(bodyText);
        }

        public Response Cookies()
        {
            var requestHasCookies = Request.Cookies.Any(c => c.Name != Server.HTTP.Session.SessionCookieName);

            var bodyText = "";

            if (requestHasCookies)
            {
                var cookieText = new StringBuilder();
                cookieText.AppendLine("<h1>Cookies</h1>");

                cookieText.Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in Request.Cookies)
                {
                    cookieText.Append("<tr>");
                    cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    cookieText.Append("</tr>");
                }
                cookieText.Append("</table>");

                bodyText = cookieText.ToString();

                return Html(bodyText);
            }
            else
            {
                bodyText = "<h1>Cookies set!</h1>";
            }

            var cookies = new CookieCollection();

            if (!requestHasCookies)
            {
                cookies.Add("My-Cookie", "My-Value");
                cookies.Add("My-Second-Cookie", "My-Second-Value");
            }

            return Html(bodyText, cookies);
        }

        public Response DownloadContent() => File(FileName);

        public Response Content() => View();
    }
}
