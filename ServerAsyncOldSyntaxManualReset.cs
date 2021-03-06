using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class ServerAsyncOldSyntaxManualReset
{
    public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

    TcpListener server = null;
    public ServerAsyncOldSyntaxManualReset(string ip, int port)
    {
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, port);
    }

    public void Start()
    {
        Console.WriteLine("Starting Server...");
        server.Start();
        AsyncAcceptLoop();
    }

    private void AsyncAcceptLoop()
    {
        try
        {
            while(true)
            {
                tcpClientConnected.Reset();
                Console.WriteLine("Waiting for a connection...");
                IAsyncResult result = server.BeginAcceptTcpClient(AcceptTcpClientCallback, null);
                tcpClientConnected.WaitOne();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            server.Stop();
        }
    }

    private void AcceptTcpClientCallback(IAsyncResult result)
    {
        TcpClient client = server.EndAcceptTcpClient(result);
        Console.WriteLine("Connected!");
        
        tcpClientConnected.Set();

        HandleConnection(client);
    }

    public void HandleConnection(TcpClient client)
    {
        var stream = client.GetStream();
        string data = null;
        Byte[] bytes = new Byte[1024];
        int byteCount;
        try
        {
            while ((byteCount = stream.Read(bytes, 0, bytes.Length)) != 0)
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
            var myServer = new ServerAsyncOldSyntaxManualReset("127.0.0.1", 8080);
            myServer.Start();
        });
    }
}