using System;
using System.Collections.Generic;
using ToysServer.DB;
using ToysServer.Model;

namespace ToysServer
{
	class Program
	{
		static DBWorker worker;

		static void Main(string[] args)
		{
			worker = new DBWorker("DB\\ToysBD.db");
			var rows = worker.LoadJournals();

			foreach (var i in rows)
				Console.WriteLine(i.Count);
		}
	}
}
