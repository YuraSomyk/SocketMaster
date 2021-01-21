using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Server.Model {
    public static class Server {

        public static string Host {get; set; }

        public static int Port { get; set; }

        public static int Counter { get; set; } = 0;

        public static Socket Socket { get; set; }

        public static Thread Thread { get; set; }

        public static List<Client> ConnectClients = new List<Client>();
    }
}