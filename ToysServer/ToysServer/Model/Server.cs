using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using ToysServer.DB;

namespace ToysServer.Model
{
	/// <summary>
	/// Сервер, принимает и обрабатывает запросы.
	/// </summary>
	public class Server
	{
        private DBWorker dbWorker;
        private TcpListener server;
        private Task task;
        private readonly IPAddress localAddr;
        private readonly int port;
        private string jsonMessage;

        public Server(string ip, int port)
		{
            localAddr = IPAddress.Parse(ip);
            this.port = port;
            jsonMessage = "";
            task = new Task(RunServer);
            dbWorker = new DBWorker("DB\\ToysBD.db");
		}
        ~Server() => Dispose();
        public void Dispose()
        {
            server.Stop();
            task.Dispose();
        }

        /// <summary>
        /// Запускает работу сервера.
        /// </summary>
		public void Start()
        {
            task.Start();
        }

        private void RunServer()
        {
            server = new TcpListener(localAddr, port);
            server.Start();

            while (true)
            {
                try { ReadRequest(); }
                catch
                {
                    server.Stop();
                    break;
                }
            }
        }

        // Читает запрос и выполняет ответ
        private void ReadRequest()
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            try
            {
                if (stream.CanRead)
                {
                    byte[] readBuffer = new byte[1024];
                    StringBuilder completeMessage = new StringBuilder();
                    do
                    {
                        int bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                        completeMessage.AppendFormat("{0}",
                            Encoding.UTF8.GetString(readBuffer, 0, bytesRead));
                    }
                    while (stream.DataAvailable);

                    var result = ProcessRequest(completeMessage.ToString());
                    SendInfo(stream, result);
                }
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        // Обработка пришедшего запроса
        public string ProcessRequest(string message)
		{
            string result = "";
            string requestAddress = ParseMessage(message);
			switch (requestAddress.ToLower())
			{
				case "getclients":
					var clients = dbWorker.LoadClients();
                    result = JsonConvert.SerializeObject(clients);
                    Console.WriteLine("Отправка таблицы Покупатели");
					break;
				case "getsellers":
					var sellers = dbWorker.LoadSellers();
					result = JsonConvert.SerializeObject(sellers);
                    Console.WriteLine("Отправка таблицы Продавцы");
					break;
				case "gettoys":
					var toys = dbWorker.LoadToys();
					result = JsonConvert.SerializeObject(toys);
                    Console.WriteLine("Отправка таблицы Игрушки");
					break;
				case "getjournals":
					var journals = dbWorker.LoadJournals();
					result = JsonConvert.SerializeObject(journals);
                    Console.WriteLine("Отправка таблицы Журнал");
					break;
				case "getsklads":
					var sklads = dbWorker.LoadSklads();
					result = JsonConvert.SerializeObject(sklads);
                    Console.WriteLine("Отправка таблицы Склады");
					break;
				case "request1":
                    var request1 = dbWorker.Request1();
                    result = JsonConvert.SerializeObject(request1);
                    Console.WriteLine("Выполнение запроса 1");
                    break;
                case "request2":
                    var request2 = dbWorker.Request2();
                    result = JsonConvert.SerializeObject(request2);
                    Console.WriteLine("Выполнение запроса 2");
                    break;
                case "request3":
                    var request3 = dbWorker.Request3();
                    result = JsonConvert.SerializeObject(request3);
                    Console.WriteLine("Выполнение запроса 3");
                    break;
                case "request4":
                    var request4 = dbWorker.Request4();
                    result = JsonConvert.SerializeObject(request4);
                    Console.WriteLine("Выполнение запроса 4");
                    break;
                case "request5":
                    var request5 = dbWorker.Request5();
                    result = JsonConvert.SerializeObject(request5);
                    Console.WriteLine("Выполнение запроса 5");
                    break;
                case "addclient":
                    result = AddClient();
                    Console.WriteLine("Добавление клиента");
                    break;
                case "addseller":
                    result = AddSeller();
                    Console.WriteLine("Добавление продавца");
                    break;
                case "addsklad":
                    result = AddSklad();
                    Console.WriteLine("Добавление склада");
                    break;
                case "addtoy":
                    result = AddToy();
                    Console.WriteLine("Добавление игрушки");
                    break;
                case "addjournal":
                    result = AddJournal();
                    Console.WriteLine("Добавление записи в журнал");
                    break;
                case "deleteclient":
                    result = DeleteClient();
                    Console.WriteLine("Удаление клиента");
                    break;
                case "changeclient":
                    result = ChangeClient();
                    Console.WriteLine("Обновление клиентов");
                    break;
            }
            return result;
		}

        private string AddClient()
		{
            string result = "";
            try
            {
                var newClient = JsonConvert.DeserializeObject<Client>(jsonMessage);
                dbWorker.AddClient(newClient);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string AddSeller()
        {
            string result = "";
            try
            {
                var newSeller = JsonConvert.DeserializeObject<Seller>(jsonMessage);
                dbWorker.AddSeller(newSeller);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string AddSklad()
        {
            string result = "";
            try
            {
                var newSklad = JsonConvert.DeserializeObject<Sklad>(jsonMessage);
                dbWorker.AddSklad(newSklad);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string AddToy()
		{
            string result = "";
            try
            {
                var newToy = JsonConvert.DeserializeObject<Toy>(jsonMessage);
                dbWorker.AddToy(newToy);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string AddJournal()
		{
            string result = "";
            try
            {
                var newJournal = JsonConvert.DeserializeObject<Journal>(jsonMessage);
                dbWorker.AddJournal(newJournal);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string DeleteClient()
        {
            string result = "";
            try
            {
                var newClient = JsonConvert.DeserializeObject<Client>(jsonMessage);
                dbWorker.DeleteClient(newClient);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string ChangeClient()
        {
            string result = "";
            try
            {
                var clients = JsonConvert.DeserializeObject<List<Client>>(jsonMessage);
                dbWorker.ChangeClient(clients);
                result = "OK";
            }
            catch { result = "Error"; }
            return result;
        }

        private string ParseMessage(string message)
		{
            var deviderIndex = message.IndexOf('/');
            string requestAddress = message;
            if (deviderIndex == -1) jsonMessage = "";
            else
			{
                requestAddress = message.Substring(0, deviderIndex);
                if (message.Length - 1 == requestAddress.Length)
                    jsonMessage = "";
                else
                    jsonMessage = message.Substring(deviderIndex + 1);
            }
            return requestAddress;
		}

        // Отправляет сообщение клиенту
		private void SendInfo(NetworkStream stream, string message)
        {
			Byte[] responseData = Encoding.UTF8.GetBytes(message);
			stream.Write(responseData, 0, responseData.Length);
		}
    }
}
