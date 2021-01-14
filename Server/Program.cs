using Server.Model;
using Server.Services;
using System;
using System.Threading;

namespace Server {
    class Program {
        static Model.Server server;
        static ServerService serverService;

        static void Main(string[] args) {
            server = new Model.Server();
            serverService = new ServerService();

            server.Thread = new Thread(() => serverService.Start(server));
            server.Thread.IsBackground = true;
            server.Thread.Start();

            while (true) {
                serverService.Commands(Console.ReadLine());
            }
        }
    }
}