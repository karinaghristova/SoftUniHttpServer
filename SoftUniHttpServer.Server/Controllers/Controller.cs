using SoftUniHttpServer.Server.HTTP;
using SoftUniHttpServer.Server.Responses;

namespace SoftUniHttpServer.Server.Controllers
{
    public class Controller
    {
        protected Request Request { get; private init; }

        public Controller(Request request)
        {
            this.Request = request;
        }

        protected Response Text(string text) => new TextResponse(text);
        protected Response Html(string text) => new HtmlResponse(text);
        protected Response Html(string html, CookieCollection cookies)
        {
            var response = new HtmlResponse(html);

            if (cookies != null)
            {
                foreach (var cookie in cookies)
                {
                    response.Cookies.Add(cookie.Name, cookie.Value);
                }
            }

            return response;
        }
        protected Response BadRequest() => new BadRequestResponse();
        protected Response Unauthorized() => new UnauthorizedResponse();
        protected Response NotFound() => new NotFoundResponse();
        protected Response Redirect(string location) => new RedirectResponse(location);
        protected Response File(string fileName) => new FileResponse(fileName);
    }
}
