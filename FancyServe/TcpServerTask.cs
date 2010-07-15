using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace FancyServe
{
    class TcpServerTask : Task<MyServerContext>
    {
        protected override IEnumerator<IAsyncResult> RunCore()
        {
            var list = new TcpListener(IPAddress.Loopback, 7777);
            IAsyncResult ar;

            list.Start();

            while (true)
            {
                yield return ar = list.BeginAcceptSocket(null, null);
                var sock = list.EndAcceptSocket(ar);

                Server.AddTask(new ConnectionTask(sock));

                //int i = 0;
                //foreach (var b in Encoding.UTF8.GetBytes(Header))
                //{
                //    char ch = Header[i++];
                //    Console.WriteLine("{0}: {1}", char.IsControl(ch) ? ' ' : ch, b);
                //}
                //yield break;
            }
        }
    }
}
