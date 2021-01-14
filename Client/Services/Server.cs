using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Services {

    public class Server {

        private string Host { get; set; }

        private int Port { get; set; }

        private Socket Socket { get; set; }

        public bool IsConnection { get; set; } = false;

        public bool Connect() {
            try {
                if (InputConnectionData()) {
                    IPHostEntry ipHost = Dns.GetHostEntry(Host);
                    IPAddress ipAddress = ipHost.AddressList[0];
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, Port);
                    Socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    Socket.Connect(ipEndPoint);
                    IsConnection = true;
                }
                return IsConnection;
            } catch (Exception exception) {
                IsConnection = false;
                throw exception;
            }
        }

        public bool SendMessage(string message) {
            try {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                int bytesSent = Socket.Send(buffer);
                return true;
            } catch (Exception exception) {
                throw exception;
            }
        }

        private bool InputConnectionData() {
            try {
                Console.Write("Host : ");
                Host = Console.ReadLine();
                Console.Write("Port : ");
                Port = Convert.ToInt32(Console.ReadLine());
                return true;
            } catch (Exception exception) {
                throw exception;
            }
        }
    }
}