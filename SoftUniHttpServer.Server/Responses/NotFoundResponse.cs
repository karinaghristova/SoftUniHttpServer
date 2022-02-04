using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Server.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse() 
            : base(StatusCode.NotFound)
        {

        }
    }
}
