using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Server.Responses
{
    public class UnauthorizedResponse : Response
    {
        public UnauthorizedResponse()
            :base(StatusCode.Unauthorized)
        {

        }
    }
}
