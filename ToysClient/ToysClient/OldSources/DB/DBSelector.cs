//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;

//namespace Test
//{
//	public class DBSelector
//	{
//		private SQLiteConnection connection;
//		private SQLiteCommand command;

//		public DBSelector(SQLiteConnection connection)
//		{
//			this.connection = connection;
//		}

//		public DataTable LoadTable(string tableName, string fields = "*")
//		{
//			try
//			{
//				command = new SQLiteCommand(connection);
//				command.CommandText = $"SELECT {fields} FROM {tableName}";
//				DataTable data = new DataTable();
//				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
//				adapter.Fill(data);
//				return data;
//			}
//			catch { throw new Exception("Таблица не найдена"); }
//		}

//		public DataTable LoadAllTables(string fields = "*")
//		{
//			try
//			{
//				command = new SQLiteCommand(connection);
//				command.CommandText = $"SELECT {fields} FROM Orders " +
//				$"INNER JOIN Clients ON Orders.id_client = Clients.id_client " +
//				$"INNER JOIN Products ON Orders.id_product = Products.id_product";
//				DataTable data = new DataTable();
//				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
//				adapter.Fill(data);
//				return data;
//			}
//			catch { throw new Exception("Таблица не найдена"); }
//		}

//		public List<Client> SelectClients()
//		{
//			DataTable dataTable = LoadTable("Clients");
//			List<Client> clients = new List<Client>();
//			ClientFields fields = Client.GetFieldsFrame();

//			foreach (DataRow row in dataTable.Rows)
//			{
//				fields.id = row.Field<long>("id_client");
//				fields.fio = row.Field<string>("fio");
//				fields.phoneNumber = row.Field<string>("phoneNumber");
//				clients.Add(new Client(fields.id, fields.fio, fields.phoneNumber));
//			}
//			return clients;
//		}

//		public List<Product> SelectProducts()
//		{
//			DataTable dataTable = LoadTable("Products");
//			List<Product> products = new List<Product>();
//			ProductFields fields = Product.GetFieldsFrame();

//			foreach (DataRow row in dataTable.Rows)
//			{
//				fields.id = row.Field<long>("id_product");
//				fields.title = row.Field<string>("title");
//				fields.cost = row.Field<double>("cost");
//				fields.releaseDate = row.Field<DateTime>("releaseDate");
//				fields.description = row.Field<string>("description");

//				if (fields.description != null)
//				{
//					products.Add(new Product(fields.id, fields.title, fields.cost,
//						fields.releaseDate, fields.description));
//				}
//				else
//				{
//					products.Add(new Product(fields.id, fields.title, fields.cost,
//						fields.releaseDate));
//				}
//			}
//			return products;
//		}

//		public List<Order> SelectOrders()
//		{
//			DataTable dataTable = LoadAllTables("Orders.id, Clients.fio, " +
//				"Products.title, Orders.count, Orders.date");
//			List<Order> orders = new List<Order>();
//			OrderFields fields = Order.GetFieldsFrame();

//			foreach (DataRow row in dataTable.Rows)
//			{
//				fields.id = row.Field<long>("id");
//				fields.clientName = row.Field<string>("fio");
//				fields.productName = row.Field<string>("title");
//				fields.count = row.Field<long>("count");
//				fields.date = row.Field<DateTime>("date");
//				orders.Add(new Order(fields.id, fields.clientName, 
//					fields.productName, fields.count, fields.date));
//			}
//			return orders;
//		}
//	}
//}
