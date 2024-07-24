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
				
				StreamReader reader = new StreamReader(stream, Encoding.ASCII);
				string request = reader.ReadToEnd();
				Console.WriteLine("Received:\n{0}", request);
				
				//Parsing request
				string[] tokens = request.Split(' ');
				if(tokens.Length < 3)
				
				{
					Console.WriteLine("Invalid HTTP request line.");
					client.Close();
					continue;
				}
				
				string method = tokens[0];
				string requestedFile = tokens[1];
				string httpVersion = tokens[2];
				
				Console.WriteLine("Method: {0}", method);
				Console.WriteLine("Requested file: {0}", requestedFile);
				Console.WriteLine("HTTP version: {0}", httpVersion);
				
				client.Close();
			}
		}
		
	}
}