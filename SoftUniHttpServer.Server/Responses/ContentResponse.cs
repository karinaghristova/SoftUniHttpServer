using SoftUniHttpServer.Server.Common;
using SoftUniHttpServer.Server.HTTP;
using System.Text;

namespace SoftUniHttpServer.Server.Responses
{
    public class ContentResponse : Response
    {
        public ContentResponse(string content, string contentType,
            Action<Request, Response> preRenderAction = null)
            : base(StatusCode.OK)
        {

            Guard.AgainstNull(content);
            Guard.AgainstNull(contentType);

            this.PreRenderAction = preRenderAction;

            this.Headers.Add(Header.ContentType, contentType);

            this.Body = content;
        }

        public override string ToString()
        {
            if (this.Body != null)
            {
                var contentLength = Encoding.UTF8.GetByteCount(this.Body).ToString();
                this.Headers.Add(Header.ContentLength, contentLength);
            }

            return base.ToString();
        }
    }
}
