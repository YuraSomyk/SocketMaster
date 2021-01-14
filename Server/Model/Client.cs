using System.Net.Sockets;
using System.Threading;

namespace Server.Model {
    public class Client {

        public string Name { get; set; }

        public Socket Socket { get; set; }

        public Thread Thread { get; set; }
    }
}