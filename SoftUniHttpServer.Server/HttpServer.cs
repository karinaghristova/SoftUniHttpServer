using SoftUniHttpServer.Server.Common;
using SoftUniHttpServer.Server.HTTP;
using SoftUniHttpServer.Server.Routing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SoftUniHttpServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        private readonly RoutingTable routingTable;

        public readonly IServiceCollection ServiceCollection;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.serverListener = new TcpListener(this.ipAddress, this.port);

            routingTableConfiguration(this.routingTable = new RoutingTable());

            ServiceCollection = new ServiceCollection();
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1", port, routingTable)
        {

        }

        public HttpServer(Action<IRoutingTable> routingTable)
           : this(8080, routingTable)
        {

        }

        public async Task Start()
        {
            serverListener.Start();

            Console.WriteLine($"Server is listening on port {port}");
            Console.WriteLine($"Listening for requests...");

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                _ = Task.Run(async () =>
                {
                    var networkStream = connection.GetStream();

                    string requestText = await ReadRequest(networkStream);
                    Console.WriteLine(requestText);

                    var request = Request.Parse(requestText, ServiceCollection);

                    var response = this.routingTable.MatchRequest(request);

                    AddSession(request, response);

                    await WriteResponse(networkStream, response);

                    connection.Close();
                });
            }
        }

        private static void AddSession(Request request, Response response)
        {
            var sessionExists = request.Session.ContainsKey(Session.SessionCurrentDateKey);

            if (!sessionExists)
            {
                request.Session[Session.SessionCurrentDateKey] = DateTime.Now.ToString();
                response.Cookies.Add(Session.SessionCookieName, request.Session.Id);
            }
        }

        private static async Task WriteResponse(NetworkStream networkStream, Response response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            if (response.FileContent != null)
            {
                responseBytes = responseBytes.Concat(response.FileContent).ToArray();
            }

            await networkStream.WriteAsync(responseBytes);
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];

            StringBuilder request = new StringBuilder();

            int totalBytes = 0;

            do
            {
                int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large");
                }

                request.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            } while (networkStream.DataAvailable);

            return request.ToString();
        }
    }
}
