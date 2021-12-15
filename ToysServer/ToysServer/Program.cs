using System;
using System.Collections.Generic;
using ToysServer.DB;
using ToysServer.Model;

namespace ToysServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server("127.0.0.1", 8888);
			Console.WriteLine("Запуск сервера...");
			//Console.WriteLine(server.ProcessRequest("getclients"));
            server.Start();
            Console.WriteLine("Сервер запущен!");

            Console.ReadKey();
        }
    }
}
