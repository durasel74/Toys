using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Toys
{
	public class DBWorker : IDisposable
	{
		private SQLiteConnection connection;
		private DBSelector selector;
		private DBUpdater updater;
		private DBAdder adder;
		private DBSearcher searcher;

		public DBWorker(string dataBasePath)
		{
			connection = new SQLiteConnection($"Data Source={dataBasePath};Version=3;" +
				"FailIfMissing=False");
			connection.Open();
			selector = new DBSelector(connection);
			updater = new DBUpdater(connection);
			adder = new DBAdder(connection);
			searcher = new DBSearcher();
		}
		~DBWorker() => Dispose();
		public virtual void Dispose() => connection.Close();

		public List<Product> LoadProducts()
		{
			return selector.SelectProducts();
		}
		public List<Client> LoadClients()
		{
			return selector.SelectClients();
		}
		public List<Order> LoadOrders()
		{
			return selector.SelectOrders();
		}

		public void UpdateProducts(List<Product> products)
		{
			updater.UpdateProducts(products);
		}
		public void UpdateClients(List<Client> clients)
		{
			updater.UpdateClients(clients);
		}

		public void AddProduct(ProductFields productFields)
		{
			adder.AddProduct(productFields);
		}
		public void AddClient(ClientFields clientFields)
		{
			adder.AddClient(clientFields);
		}
		public void AddOrder(OrderFields orderFields, long clientId, long productId)
		{
			adder.AddOrder(orderFields, clientId, productId);
		}

		public IList<Product> SearchProducts(IList<Product> products, string query)
		{
			return searcher.SearchProducts(products, query);
		}
		public IList<Client> SearchClients(IList<Client> clients, string query)
		{
			return searcher.SearchClients(clients, query);
		}
		public IList<Order> SearchOrders(IList<Order> orders, string query)
		{
			return searcher.SearchOrders(orders, query);
		}
	}
}
