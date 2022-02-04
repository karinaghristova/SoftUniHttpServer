using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Server.Responses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string location)
            :base(StatusCode.Found)
        {
            this.Headers.Add(Header.Location, location);
        }
    }
}
