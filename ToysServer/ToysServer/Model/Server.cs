using System;
using System.Collections.Generic;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;
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

        public Server(string ip, int port)
		{
            localAddr = IPAddress.Parse(ip);
            this.port = port;
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
                    if (result != String.Empty) SendInfo(stream, result);
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
			switch (message.ToLower())
			{
				case "getclients":
					var clients = dbWorker.LoadClients();
                    result = JsonConvert.SerializeObject(clients);
					break;
				case "getsellers":
					var sellers = dbWorker.LoadSellers();
					result = JsonConvert.SerializeObject(sellers);
					break;
				case "gettoys":
					var toys = dbWorker.LoadToys();
					result = JsonConvert.SerializeObject(toys);
					break;
				case "getjournals":
					var journals = dbWorker.LoadJournals();
					result = JsonConvert.SerializeObject(journals);
					break;
				case "getsklads":
					var sklads = dbWorker.LoadSklads();
					result = JsonConvert.SerializeObject(sklads);
					break;
				case "request1":
                    var request1 = dbWorker.Request1();
                    result = JsonConvert.SerializeObject(request1);
                    break;
                case "request2":
                    var request2 = dbWorker.Request2();
                    result = JsonConvert.SerializeObject(request2);
                    break;
                case "request3":
                    var request3 = dbWorker.Request3();
                    result = JsonConvert.SerializeObject(request3);
                    break;
                case "request4":
                    var request4 = dbWorker.Request4();
                    result = JsonConvert.SerializeObject(request4);
                    break;
                case "request5":
                    var request5 = dbWorker.Request5();
                    result = JsonConvert.SerializeObject(request5);
                    break;
            }
            return result;
		}

        // Отправляет сообщение клиенту
		private void SendInfo(NetworkStream stream, string message)
        {
			Byte[] responseData = Encoding.UTF8.GetBytes(message);
			stream.Write(responseData, 0, responseData.Length);
		}
    }
}
