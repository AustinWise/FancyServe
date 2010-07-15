using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace FancyServe
{
    class ConnectionTask : Task<MyServerContext>
    {
        private Socket sock;
        string Header = "HTTP/1.1 200 OK\r\nContent-Length: {0}\r\nConnection: close\r\nContent-Type: text/html; charset=UTF-8\r\n\r\n{1}";
        private string Response = "<html><body><h1>Turtles</h1>Turtles are pretty cool.</body></html>";

        public ConnectionTask(Socket sock)
        {
            this.sock = sock;
        }

        protected override IEnumerator<IAsyncResult> RunCore()
        {
            IAsyncResult ar;
            var bytes = Encoding.UTF8.GetBytes(string.Format(Header, Response.Length, Response));
            int bytesSent = 0;

            while (bytesSent < bytes.Length)
            {
                yield return ar = sock.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, null, null);
                int sent = sock.EndSend(ar);
                Console.WriteLine("wrote: {0}", sent);
                bytesSent += sent;
            }

            sock.Close();
        }
    }
}
