using System.Net.Sockets; // подключение готовых библиотек TCP
using System.Net;
using System.Text;

namespace Server // пространство имен
{
    class OurServer
    {
        TcpListener server; // принимает подключения от клиентов

        public OurServer()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);
            server.Start();

            LoopClients(); // ловит новых клиентов
        }
        void LoopClients()
        {
            while (true) // бесконечный цикл
            {
                TcpClient client = server.AcceptTcpClient(); // получение клиента на сервере
                Thread thread = new Thread(() => HandleClient(client)); // направляем клиета в отдельный поток и удерживаем
                thread.Start();
            }
        }
        void HandleClient(TcpClient client) // функция держит соединение с клиентом
        {
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            while (true)
            {
                string message = sReader.ReadLine();
                Console.WriteLine($"Клиент написал - {message}");
                Console.WriteLine("Дайте сообщение клиенту: ");
                string answer = Console.ReadLine();
                sWriter.WriteLine(answer);
                sWriter.Flush();
            }
        }
    }
}