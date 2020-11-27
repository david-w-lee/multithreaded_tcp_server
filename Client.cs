using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

public class Client
{
    public static void Connect(string ip, int port, int clientId)
    {
        try 
        {
            TcpClient client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();
            for(int i = 0; i < 2; i++)
            {
                // Convert message to ASCII byte stream and send message to server
                string message = $"Hello, I am Client {clientId}.";
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Client({clientId}) sent: {message}");

                // Read response message from server.
                Byte[] buffer = new Byte[1024];
                int byteCount = stream.Read(buffer, 0, buffer.Length);
                string response = System.Text.Encoding.ASCII.GetString(buffer, 0, byteCount);
                Console.WriteLine($"Client({clientId}) received: {response}");
                Thread.Sleep(1000);
            }
            stream.Close();
            client.Close();
        }
        catch (Exception e) 
        {
            Console.WriteLine("Exception: {0}", e);
        }
        Console.Read();
    }

    public static void Run()
    {
        for(int i=0; i < 2; i++)
        {
            int clientId = i+1;
            Task.Run(() => 
            {
                Connect("127.0.0.1", 8080, clientId);
            });
        }
    }
}