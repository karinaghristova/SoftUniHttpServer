using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            :base(text, ContentType.PlainText)
        {

        }
    }
}
