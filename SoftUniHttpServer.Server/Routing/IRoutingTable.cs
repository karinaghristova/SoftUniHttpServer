using SoftUniHttpServer.Server.HTTP;

namespace SoftUniHttpServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(Method method, string path, Func<Request, Response> responseFunction);
    }
}
