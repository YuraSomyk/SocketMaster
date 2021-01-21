using Server.Model;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.Services {
    public class ClientService {
        public Client Init(Socket socket, Client client) {
            client.Socket = socket;
            client.Name = socket.LocalEndPoint.ToString();
            client.Thread = new Thread(() => Listner(client));
            client.Thread.IsBackground = true;
            client.Thread.Start();

            return client;
        }

        public void Disconnect(Client client) {
            try {
                client.Socket.Close();
                client.Thread.Abort();
            } catch (Exception exp) { 
                Console.WriteLine("Error with end: {0}.", exp.Message); 
            }
        }

        private void Listner(Client client) {
            while (true) {
                try {
                    byte[] buffer = new byte[1024];
                    int bytesRec = client.Socket.Receive(buffer);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                    int message = Convert.ToInt32(data);
                    Model.Server.Counter = Increment(Model.Server.Counter, message);
                    Console.WriteLine(Model.Server.Counter);
                } catch { 
                    return; 
                }
            }
        }

        private int Increment(int count, int message) {
            return count + message;
        }

        public void SendMessage(string command, Client client) {
            try {
                int bytesSent = client.Socket.Send(Encoding.UTF8.GetBytes(command));
                if (bytesSent > 0) Console.WriteLine("Success");
            } catch (Exception exp) { 
                Console.WriteLine("Error with send command: {0}.", exp.Message); 
            }
        }
    }
}