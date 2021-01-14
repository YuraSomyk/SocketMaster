using System;
using System.Threading;

namespace Server {
    class Program {

        private const string _serverHost = "localhost";
        private const int _serverPort = 9933;
        private static Thread _serverThread;

        static void Main(string[] args) {
            _serverThread = new Thread(() => Server.StartServer(_serverHost, _serverPort));
            _serverThread.IsBackground = true;
            _serverThread.Start();

            while (true) {
                CommandsHandler(Console.ReadLine());
            }
        }

        private static void CommandsHandler(string cmd) {
            cmd = cmd.ToLower();

            switch (cmd) {
                case "/list": {
                    int countUsers = Server.Clients.Count;

                    for (int i = 0; i < countUsers; i++) {
                        Console.WriteLine("[{0}]: {1}", i, Server.Clients[i].Name);
                    }
                    break;
                }
                case "/count":{
                    Console.WriteLine(Server.Counter);
                    break;
                }
                default:
                    break;
            }
        }
    }
}