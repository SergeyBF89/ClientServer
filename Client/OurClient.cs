using System.Net.Sockets; // подключение готовых библиотек TCP
using System.Text;


namespace Client // пространство имен
{
    class OurClient
    {
        private TcpClient client; // отправляет данные от клиента к серверу
        private StreamWriter sWriter;
        private StreamReader sReader;

       public OurClient()
        {    // подключение к серверу и создание клиента
            client = new TcpClient("127.0.0.1", 5555);
            sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            sReader = new StreamReader(client.GetStream(), Encoding.UTF8);

            HandleCommunication(); // держит соединение с сервером
        }
        void HandleCommunication() // метод поддерживающий постоянное подключение к серверу
        {
            while (true) // бесконечное соединение с сервером
            {
                Console.Write("> ");
                string message = Console.ReadLine(); // строка которую передаем клиенту
                sWriter.WriteLine(message); // отправка сообщения серверу
                sWriter.Flush(); // отпрака сообщения не медленно 
                string answerServer = sReader.ReadLine();
                Console.WriteLine($"Сервер ответил -> {answerServer}");
            }
        }
    }
}