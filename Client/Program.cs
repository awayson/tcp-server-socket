using System;
using System.IO;
using System.Net.Sockets;
using System.Text;


string server = "127.0.0.1";
int port = 8080;

try
{
	TcpClient client = new TcpClient(server, port);
	
	NetworkStream stream = client.GetStream();
	
	string message = "ping";
	byte[] messageBytes = Encoding.ASCII.GetBytes(message);
	stream.Write(messageBytes, 0, messageBytes.Length);
	Console.WriteLine("Sent: {0}", message);

	byte[] response = new byte[4];
	stream.Read(response, 0, response.Length);
	string responseMessage = Encoding.ASCII.GetString(response);
	Console.WriteLine("Received: {0}", responseMessage);
	
	client.Close();
}
catch (Exception e)
{
	Console.WriteLine("Exception: {0}", e.Message);
}

