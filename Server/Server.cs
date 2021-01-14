using Server.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server {
    public static class Server {

        public static int Counter = 0;
        static Client Client = new Client();
        public static List<Client> Clients = new List<Client>();

        public static void StartServer(string serverHost, int _serverPort) {
            IPHostEntry ipHost = Dns.GetHostEntry(serverHost);
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _serverPort);
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEndPoint);
            socket.Listen(1000);
            Console.WriteLine("Server has been started on IP: {0}.", ipEndPoint);
            while (true) {
                try {
                    Socket user = socket.Accept();
                    ConnectClinet(user);
                } catch (Exception exp) { 
                    Console.WriteLine("Error: {0}", exp.Message); 
                }
            }
        }

        public static void ConnectClinet(Socket handle) {
            try {
                Clients.Add(Client.Connect(handle));
                Console.WriteLine("Client connected: {0}", handle.RemoteEndPoint);
            } catch (Exception exp) {
                Console.WriteLine("Error : {0}.", exp.Message); 
            }
        }

        public static void DisconnectClinet(Client client) {
            try {
                Client.Disconnect();
                Clients.Remove(client);
                Console.WriteLine("Clinet {0} has been disconnected.", client.Name);
            } catch (Exception exp) { 
                Console.WriteLine("Error : {0}.", exp.Message); 
            }
        }

        public static void WriteMessage(string message) {
            Console.WriteLine(message);
        }
    }
}