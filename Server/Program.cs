using Server.Model;
using Server.Services;
using System;
using System.Threading;

namespace Server {
    class Program {
        static ServerService serverService;

        static void Main(string[] args) {
            serverService = new ServerService();

            Model.Server.Thread = new Thread(() => serverService.Start());
            Model.Server.Thread.IsBackground = true;
            Model.Server.Thread.Start();

            while (true) {
                serverService.Commands(Console.ReadLine());
            }
        }
    }
}