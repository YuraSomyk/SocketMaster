using Server.Model;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server.Services {
    public class ServerService {

        ClientService clientService { get; set; }

        public ServerService () {
            clientService = new ClientService();
        }

        private void SetHostData(Model.Server server) {
            Console.Write("Host: ");
            server.Host = "localhost";
            Console.Write("Port: ");
            server.Port = 9933;
        }

        private Socket InitSocket(Model.Server server) {
            SetHostData(server);
            var ipHost = Dns.GetHostEntry(server.Host);
            var ipAddress = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddress, server.Port);
            server.Socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server.Socket.Bind(ipEndPoint);
            server.Socket.Listen(1000);
            return server.Socket;
        }

        public void Start(Model.Server server) {
            InitSocket(server);
            Console.Clear();
            Console.WriteLine("Server has been started on IP: {0}.", server.Socket.LocalEndPoint); 

            while (true) {
                try {
                    Socket clientSocket = server.Socket.Accept();
                    ConnectClinet(server, clientSocket);
                } catch (Exception exp) {
                    Console.WriteLine("Error: {0}", exp.Message);
                }
            }
        }      

        public void Commands(string cmd) {
            switch (cmd) {
                case "/list": {
                        //int countUsers = Server.Clients.Count;

                        //for (int i = 0; i < countUsers; i++) {
                        //    Console.WriteLine("[{0}]: {1}", i, Server.Clients[i].Name);
                        //}
                        Console.WriteLine("list");
                    break;
                }
                case "/count":{
                    Console.WriteLine("count");
                    break;
                }
                default:
                    break;
            }
        }
        
        public void ConnectClinet(Model.Server server, Socket socket) {
            server.ConnectClients.Add(clientService.Init(socket, new Client()));
            Console.WriteLine("Client connected: ");
        }

        public void DisconnectClinet(Model.Server server, Client client) {
            clientService.Disconnect(client);
            server.ConnectClients.Remove(client);
            Console.WriteLine("Clinet {0} has been disconnected.", client.Name);
        }
    }
}