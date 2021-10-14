using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Echo_Conmcurrent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TcpListener listener = new TcpListener(System.Net.IPAddress.Loopback, 7);

            listener.Start();
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();

                Task.Run(() =>
                {
                    HandleClient(socket);
                }
                );
            }
        }

        public static void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamWriter writer = new StreamWriter(ns);
            StreamReader reader = new StreamReader(ns);
            string message = reader.ReadLine();
            Console.WriteLine("Client wrote: " + message);
            writer.WriteLine(message);
            writer.Flush();
            socket.Close();
        }
    }
}
