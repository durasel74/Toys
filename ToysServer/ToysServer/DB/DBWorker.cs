using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToysServer.Model;

namespace ToysServer.DB
{
	/// <summary>
	/// Является прослойкой между приложением и базой данных.
	/// </summary>
	public class DBWorker : IDisposable
	{
		private SQLiteConnection connection;
		private DBSelector selector;
		private DBRequester requester;
		private DBAdder adder;
		private DBDeleter deleter;
		private DBChanger changer;

		public DBWorker(string dataBasePath)
		{
			connection = new SQLiteConnection($"Data Source={dataBasePath};Version=3;" +
				"FailIfMissing=False");
			connection.Open();
			selector = new DBSelector(connection);
			requester = new DBRequester(connection);
			adder = new DBAdder(connection);
			deleter = new DBDeleter(connection);
			changer = new DBChanger(connection);
		}
		~DBWorker() => Dispose();
		public virtual void Dispose() => connection.Close();

		public List<Sklad> LoadSklads() => selector.SelectSklad();
		public List<Client> LoadClients() => selector.SelectClient();
		public List<Seller> LoadSellers() => selector.SelectSeller();
		public List<Toy> LoadToys() => selector.SelectToy();
		public List<Journal> LoadJournals() => selector.SelectJournal();

		public DataTable Request1() => requester.Request1();
		public DataTable Request2() => requester.Request2();
		public DataTable Request3() => requester.Request3();
		public DataTable Request4() => requester.Request4();
		public DataTable Request5() => requester.Request5();

		public void AddClient(Client client) => adder.AddClient(client);
		public void AddSeller(Seller seller) => adder.AddSeller(seller);
		public void AddSklad(Sklad sklad) => adder.AddSklad(sklad);
		public void AddToy(Toy toy) => adder.AddToy(toy);
		public void AddJournal(Journal journal) => adder.AddJournal(journal);

		public void DeleteClient(Client client) => deleter.DeleteClient(client);
		public void DeleteSeller(Seller seller) => deleter.DeleteSeller(seller);
		public void DeleteSklad(Sklad sklad) => deleter.DeleteSklad(sklad);
		public void DeleteToy(Toy toy) => deleter.DeleteToy(toy);
		public void DeleteJournal(Journal journal) => deleter.DeleteJournal(journal);

		public void ChangeClient(List<Client> clients) => changer.ChangeClient(clients);
	}
}
