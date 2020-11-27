using System;

namespace multithreaded_tcp_server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid argument");
            }
            else if(args[0] == "server"){
                Server.Run();
            }
            else if(args[0] == "client"){
                Client.Run();
            }
            Console.ReadLine();
        }
    }
}
