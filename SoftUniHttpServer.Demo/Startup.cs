using SoftUniHttpServer.Demo.Controllers;
using SoftUniHttpServer.Demo.Services;
using SoftUniHttpServer.Server;
using SoftUniHttpServer.Server.Routing;

public class Startup
{
    public static async Task Main()
    {
        var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

        server.ServiceCollection
            .Add<UserService>();

        await server.Start();
    }

}