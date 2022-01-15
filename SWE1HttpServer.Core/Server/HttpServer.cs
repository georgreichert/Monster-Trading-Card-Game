using Server.Core.Client;
using Server.Core.Listener;
using Server.Core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Core.Server
{
    public class HttpServer : IServer
    {
        private readonly IListener listener;
        private readonly IRouter router;
        private bool isListening;

        public HttpServer(IPAddress address, int port, IRouter router)
        {
            listener = new Listener.HttpListener(address, port);
            this.router = router;
        }

        public void Start()
        {
            listener.Start();
            isListening = true;

            List<Thread> threads = new();
            while (isListening)
            {
                var client = listener.AcceptClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(client);
                threads.Add(t);
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        public void Stop()
        {
            isListening = false;
            listener.Stop();
        }

        private void HandleClient(object oClient)
        {
            IClient client = oClient as IClient;
            if (client == null)
            {
                throw new ArgumentException("Can't cast oClient to client!");
            }

            var request = client.ReceiveRequest();

            Response.Response response;
            try
            {
                var command = router.Resolve(request);
                if (command != null)
                {
                    response = command.Execute();
                }
                else
                {
                    response = new Response.Response()
                    {
                        StatusCode = Response.StatusCode.BadRequest
                    };
                }
            }
            catch (RouteNotAuthorizedException)
            {
                response = new Response.Response()
                {
                    StatusCode = Response.StatusCode.Unauthorized
                };
            }

            client.SendResponse(response);
        }
    }
}
