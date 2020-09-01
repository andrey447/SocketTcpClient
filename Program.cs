using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTcpClient
{
    class Program
    {
        static int serverPort = 8888;
        static IPAddress serverAddress = IPAddress.Parse("127.0.0.1");

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(serverAddress, serverPort);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(localEndPoint);
                Console.Write("Введите сообщение:");
                string message = Console.ReadLine();

                // send the message
                byte[] sendedDataBuffer = Encoding.Unicode.GetBytes(message);
                socket.Send(sendedDataBuffer);

                // receive the message
                byte[] receivedDataBuffer = new byte[256];
                StringBuilder builder = new StringBuilder();
                int receivedBytes = 0;

                do
                {
                    receivedBytes = socket.Receive(receivedDataBuffer, receivedDataBuffer.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(receivedDataBuffer, 0, receivedBytes));
                }
                while (socket.Available > 0);

                Console.WriteLine("ответ сервера: " + builder.ToString());

                // close socket
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}