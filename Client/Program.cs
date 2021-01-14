using Client.Services;

namespace Client { 
    class Program {
        static bool isMenu = true;

        static void Main(string[] args) {
            var server = new Server();
            var menu = new Menu();

            while (isMenu) {
                isMenu = menu.GetMenu(server);
            }
        }
    }
}