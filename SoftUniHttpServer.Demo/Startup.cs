using SoftUniHttpServer.Demo.Controllers;
using SoftUniHttpServer.Server;
using SoftUniHttpServer.Server.Routing;

public class Startup
{
    public static async Task Main()
    {
        await new HttpServer(routes => routes
           .MapControllers()).Start();
    }

}