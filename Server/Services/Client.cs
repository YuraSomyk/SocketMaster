using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.Services {

    public class Client {

        public string Name { get; set; }

        public Socket Socket { get; set; }

        public Thread Thread { get; set; }

        public Client Connect(Socket socket) {
            this.Socket = socket;
            this.Thread = new Thread(()=> Listner());
            this.Thread.IsBackground = true;
            this.Thread.Start();

            return this;
        }

        public void Disconnect() {
            try {
                this.Socket.Close();
                this.Thread.Abort();
            } catch (Exception exp) { 
                Console.WriteLine("Error with end: {0}.", exp.Message); 
            }
        }

        private void Listner() {
            while (true) {
                try {
                    byte[] buffer = new byte[1024];
                    int bytesRec = this.Socket.Receive(buffer);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                    handleCommand(data);
                } catch { 
                    Server.DisconnectClinet(this); 
                    return; 
                }
            }
        }

        private void handleCommand(string data) {
            Console.WriteLine(data);
        }

        public void Send(string command) {
            try {
                int bytesSent = this.Socket.Send(Encoding.UTF8.GetBytes(command));
                if (bytesSent > 0) Console.WriteLine("Success");
            } catch (Exception exp) { 
                Console.WriteLine("Error with send command: {0}.", exp.Message); 
                Server.DisconnectClinet(client); 
            }
        }
    }
}