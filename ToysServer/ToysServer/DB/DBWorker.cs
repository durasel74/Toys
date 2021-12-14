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
		//private DBAdder adder;

		public DBWorker(string dataBasePath)
		{
			connection = new SQLiteConnection($"Data Source={dataBasePath};Version=3;" +
				"FailIfMissing=False");
			connection.Open();
			selector = new DBSelector(connection);
			requester = new DBRequester(connection);
			//adder = new DBAdder(connection);
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

		//public void AddProduct(ProductFields productFields)
		//{
		//	adder.AddProduct(productFields);
		//}
		//public void AddClient(ClientFields clientFields)
		//{
		//	adder.AddClient(clientFields);
		//}
		//public void AddOrder(OrderFields orderFields, long clientId, long productId)
		//{
		//	adder.AddOrder(orderFields, clientId, productId);
		//}
	}
}
