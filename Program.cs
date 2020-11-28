using System;

namespace multithreaded_tcp_server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please run with an argument: server, serverasync, serverasyncoldsyntax, serverasyncoldsyntaxmanualreset, serverasyncnonblocking, client, clientasync");
            }
            else if(args[0] == "server")
            {
                Server.Run();
            }
            else if(args[0] == "serverasync")
            {
                ServerAsync.Run();
            }
            else if(args[0] == "serverasyncoldsyntax")
            {
                ServerAsyncOldSyntax.Run();
            }
            else if(args[0] == "serverasyncoldsyntaxmanualreset")
            {
                ServerAsyncOldSyntax.Run();
            }
            else if(args[0] == "serverasyncnonblocking")
            {
                ServerAsyncNonblocking.Run();
            }
            else if(args[0] == "client")
            {
                Client.Run();
            }
            else if(args[0] == "clientasync")
            {
                ClientAsync.Run();
            }
            else
            {
                Console.WriteLine("Please run with an argument: server, serverasync, serverasyncoldsyntax, serverasyncoldsyntaxmanualreset, serverasyncnonblocking, client, clientasync");
            }
            Console.ReadLine();
        }
    }
}
