using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class ServerAsyncNonblocking
{
    TcpListener server = null;
    public ServerAsyncNonblocking(string ip, int port)
    {
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, port);
    }

    public async void Start()
    {
        Console.WriteLine("Starting Server...");
        server.Start();
        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Connected!");

                await HandleConnection(client);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            server.Stop();
        }
    }

    public async Task HandleConnection(TcpClient client)
    {
        var stream = client.GetStream();
        string data = null;
        Byte[] bytes = new Byte[1024];
        int byteCount;
        try
        {
            while ((byteCount = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, byteCount);
                Console.WriteLine($"Thread({Thread.CurrentThread.ManagedThreadId}) received: {data}");
                
                string str = $"Welcome!";
                Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);   
                stream.Write(reply, 0, reply.Length);
                Console.WriteLine($"Thread({Thread.CurrentThread.ManagedThreadId}) sent: {str}");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
            client.Close();
        }
    }

    public static void Run()
    {
        Task.Run(() => {
            var myServer = new ServerAsyncNonblocking("127.0.0.1", 8080);
            myServer.Start();
        });
    }
}