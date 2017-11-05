using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bai1_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList sockList = new ArrayList(2);
            ArrayList copyList = new ArrayList(2);
            Socket main = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            byte[] data = new byte[1024]; string stringData; int recv;
            main.Bind(iep);
            main.Listen(2);
            Console.WriteLine("Waiting for 2 clients...");
            Socket client1 = main.Accept();
            IPEndPoint iep1 = (IPEndPoint)client1.RemoteEndPoint; client1.Send(Encoding.ASCII.GetBytes("Welcome to my server"));
            Console.WriteLine("Connected to {0}", iep1.ToString()); sockList.Add(client1);
            Console.WriteLine("Waiting for 1 more client...");
            Socket client2 = main.Accept(); IPEndPoint iep2 = (IPEndPoint)client2.RemoteEndPoint; client2.Send(Encoding.ASCII.GetBytes("Welcome to my server"));
            Console.WriteLine("Connected to {0}", iep2.ToString());
            sockList.Add(client2);
            while (true)
            {
                copyList = new ArrayList(sockList);
                Console.WriteLine("Monitoring {0} sockets...", copyList.Count);
                Socket.Select(copyList, null, null, 10000000);
                foreach (Socket client in copyList)
                {
                    data = new byte[1024];
                    recv = client.Receive(data);
                    int n = BitConverter.ToInt32(data, 0);
                    Console.WriteLine("N = {0}", n);
                    int[] Array = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        data = new byte[10];
                        recv = client.Receive(data);
                        Array[i] = BitConverter.ToInt32(data, 0);
                    }
                    int tong = 0;
                    for (int i = 0; i < n; i++)
                    {
                        tong += Array[i];
                    }
                    data = new byte[10];
                    data = BitConverter.GetBytes(tong);
                    client.Send(data, sizeof(int), SocketFlags.None);
                    client.Close();
                }
            }
        }
    }
}
