using Server.Model;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server.Services {
    public class ServerService {
        ClientService clientService;

        public ServerService () {
            clientService = new ClientService();
            SetHostData();
        }

        private Socket InitSocket() {
            var ipHost = Dns.GetHostEntry(Model.Server.Host);
            var ipAddress = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddress, Model.Server.Port);
            Model.Server.Socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Model.Server.Socket.Bind(ipEndPoint);
            Model.Server.Socket.Listen(1000);
            return Model.Server.Socket;
        }

        public void Start() {
            InitSocket();
            Console.Clear();
            Console.WriteLine("Server has been started on IP: {0}.", Model.Server.Socket.LocalEndPoint); 

            while (true) {
                try {
                    Socket clientSocket = Model.Server.Socket.Accept();
                    ConnectClinet(clientSocket, new Client());
                } catch (Exception exp) {
                    Console.WriteLine("Error: {0}", exp.Message);
                }
            }
        }    
        
        private void SetHostData() {
            Console.Write("Host: ");
            Model.Server.Host = Console.ReadLine();

            Console.Write("Port: ");
            Model.Server.Port = Convert.ToInt32(Console.ReadLine());
        }

        public void Commands(string cmd) {
            switch (cmd) {
                case "/list": {
                    int i = 1;
                    foreach (var client in Model.Server.ConnectClients) {
                        Console.WriteLine("[{0}]: {1}", i, client.Name);
                    }
                    break;
                }
                case "/count":{
                    Console.WriteLine(Model.Server.Counter);
                    break;
                }
            }
        }
        
        public void ConnectClinet(Socket socket, Client client) {
            Model.Server.ConnectClients.Add(clientService.Init(socket, client));
            Console.WriteLine("Client {0} connected", client.Name );
        }

        public void DisconnectClinet(Client client) {
            clientService.Disconnect(client);
            Model.Server.ConnectClients.Remove(client);
            Console.WriteLine("Clinet {0} has been disconnected.", client.Name);
        }
    }
}