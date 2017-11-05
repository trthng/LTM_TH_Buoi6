using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai2_Server
{
    class Program
    {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 9999;

        static ASCIIEncoding encoding = new ASCIIEncoding();

        public static void Main()
        {
            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");
                TcpListener listener = new TcpListener(address, PORT_NUMBER);
                listener.Start();
                Console.WriteLine("Server started on " + listener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection...");
                Socket socket = listener.AcceptSocket();
                Console.WriteLine("Connection received from " + socket.RemoteEndPoint);              
                byte[] data = new byte[BUFFER_SIZE];
                socket.Receive(data);
                string str = encoding.GetString(data);
                socket.Send(encoding.GetBytes("Hello " + str));
                socket.Close();
                listener.Stop();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            Console.Read();

        }
    }
}
