using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPServer

{
	class Program
	{
		static void Main(string[] args)
		{
			IPAddress localHost = IPAddress.Parse("127.0.0.1");
			int port = 8080;
			
			TcpListener listener = new TcpListener(localHost, port);
			
			listener.Start();
			Console.WriteLine("Server started and listening on {0}:{1}", localHost, port);
			
			while (true)
			
			{
				Console.WriteLine("Waiting for a connection...");
				
				TcpClient client = listener.AcceptTcpClient();
				Console.WriteLine("Connected!");
				
				NetworkStream stream = client.GetStream();
                byte[] message = new byte[4];
                stream.Read(message, 0, message.Length);

                string clientMessage = Encoding.ASCII.GetString(message);
                Console.WriteLine("Received: {0}", clientMessage);

                string response = clientMessage.Trim().ToLower() == "ping" ? "pong" : "unknown";
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("Sent: {0}", response);
				
				
				client.Close();
			}
		}
		
	}
}