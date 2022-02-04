using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Server.Responses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse()
            : base(StatusCode.BadRequest)
        {

        }
    }
}
