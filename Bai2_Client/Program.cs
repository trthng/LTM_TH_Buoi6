using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai2_Client
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
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", PORT_NUMBER);
                Stream stream = client.GetStream();
                Console.WriteLine("Connected to Y2Server.");
                Console.Write("Enter your name: \n");
                string str = Console.ReadLine();
                byte[] data = encoding.GetBytes(str);
                stream.Write(data, 0, data.Length);
                data = new byte[BUFFER_SIZE];
                stream.Read(data, 0, BUFFER_SIZE);
                Console.WriteLine(encoding.GetString(data));
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            Console.Read();
        }
    }
}
