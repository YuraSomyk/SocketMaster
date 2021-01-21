using System;

namespace Client.Services {
    public class Menu {

        public bool GetMenu(Server server) {
            try {
                Console.Write(PrintHeader());
                var command = Convert.ToInt32(Console.ReadLine());
                return SelectCommand(command, server);
            } catch (Exception) {
                return true;
            }
        }

        private string PrintHeader() {
            Console.Clear();
            string header = "Choose section:\n" +
            "1) Connect\n" +
            "2) Send message\n" +
            "3) Get sum\n" +
            "4) Close program\n" +
            "\r\nSelect an option: ";

            return header;
        }

        private bool SelectCommand(int commandId, Server server) {
            switch (commandId) {
                case 1:
                    Connection(server);
                    Console.ReadLine();
                    return true;
                case 2:
                    SendMessage(server);
                    Console.ReadLine();
                    return true;
                case 3:
                    return false;
            }

            return true;
        }

        private void Connection(Server server) {
            try {
                if (!server.IsConnection && server.Connect()) {
                    Console.WriteLine("З'єднання встановлено!");
                } else {
                    Console.WriteLine("З'єднання було встановлено швидше!");
                }
            } catch {
                Console.WriteLine("Не вдалося встановити з'єднання!");
            }
        }

        private void SendMessage(Server server) {
            try {
                Console.Write("Введіть повідомлення :");
                var message = Console.ReadLine();

                if (!server.IsConnection) throw new Exception(); 

                server.SendMessage(message);

                Console.WriteLine("Повідомлення надіслано!");
            } catch {
                Console.WriteLine("Не вдалося надіслати!");
            }
        }

        public void Disconect(Server server) {
            try {
                server.Disconnect();
                Console.WriteLine("З'єднання розірвано!");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}