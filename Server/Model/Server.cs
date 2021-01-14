using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Server.Model {
    public class Server {

        public string Host {get; set; }

        public int Port { get; set; }

        public Socket Socket { get; set; }

        public Thread Thread { get; set; }

        public List<Client> ConnectClients = new List<Client>();
    }
}