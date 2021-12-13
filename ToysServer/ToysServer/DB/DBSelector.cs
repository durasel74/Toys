using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToysServer.Model;

namespace ToysServer.DB
{
	public class DBSelector
	{
		private SQLiteConnection connection;
		private SQLiteCommand command;

		public DBSelector(SQLiteConnection connection)
		{
			this.connection = connection;
		}

		public DataTable LoadTable(string tableName, string fields = "*")
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = $"SELECT {fields} FROM {tableName}";
				DataTable data = new DataTable();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				adapter.Fill(data);
				return data;
			}
			catch { throw new Exception("Таблица не найдена"); }
		}

		//public DataTable LoadAllTables(string fields = "*")
		//{
		//	try
		//	{
		//		command = new SQLiteCommand(connection);
		//		command.CommandText = $"SELECT {fields} FROM Orders " +
		//		$"INNER JOIN Clients ON Orders.id_client = Clients.id_client " +
		//		$"INNER JOIN Products ON Orders.id_product = Products.id_product";
		//		DataTable data = new DataTable();
		//		SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
		//		adapter.Fill(data);
		//		return data;
		//	}
		//	catch { throw new Exception("Таблица не найдена"); }
		//}

		public List<Sklad> SelectSklad()
		{
			DataTable dataTable = LoadTable("Sklad");
			List<Sklad> sklads = new List<Sklad>();

			Sklad newSklad;
			foreach (DataRow row in dataTable.Rows)
			{
				newSklad = new Sklad();
				newSklad.IdSklad = row.Field<long>("idSklad");
				newSklad.Address = row.Field<string>("address");
				sklads.Add(newSklad);
			}
			return sklads;
		}

		public List<Client> SelectClient()
		{
			DataTable dataTable = LoadTable("Client");
			List<Client> clients = new List<Client>();

			Client newClient;
			foreach (DataRow row in dataTable.Rows)
			{
				newClient = new Client();
				newClient.IdClient = row.Field<long>("idClient");
				newClient.Sfm = row.Field<string>("sfm");
				newClient.PhoneNumber = row.Field<string>("phoneNumber");
				clients.Add(newClient);
			}
			return clients;
		}

		public List<Seller> SelectSeller()
		{
			DataTable dataTable = LoadTable("Seller");
			List<Seller> sellers = new List<Seller>();

			Seller newSeller;
			foreach (DataRow row in dataTable.Rows)
			{
				newSeller = new Seller();
				newSeller.IdSeller = row.Field<long>("idSeller");
				newSeller.Sfm = row.Field<string>("sfm");
				newSeller.PhoneNumber = row.Field<string>("phoneNumber");
				sellers.Add(newSeller);
			}
			return sellers;
		}

		public List<Toy> SelectToy()
		{
			DataTable dataTable = LoadTable("Toys");
			List<Toy> toys = new List<Toy>();

			Toy newToy;
			foreach (DataRow row in dataTable.Rows)
			{
				newToy = new Toy();
				newToy.IdToy = row.Field<long>("idToy");
				newToy.IdSklad = row.Field<long>("idSklad");
				newToy.Name = row.Field<string>("name");
				newToy.Cost = row.Field<double>("cost");
				newToy.ReleaseDate = row.Field<string>("releaseDate");
				newToy.Info = row.Field<string>("info");
				toys.Add(newToy);
			}
			return toys;
		}

		public List<Journal> SelectJournal()
		{
			DataTable dataTable = LoadTable("Journal");
			List<Journal> journal = new List<Journal>();

			Journal newJournal;
			foreach (DataRow row in dataTable.Rows)
			{
				newJournal = new Journal();
				newJournal.Id = row.Field<long>("id");
				newJournal.IdToy = row.Field<long>("idToy");
				newJournal.IdClient = row.Field<long>("idClient");
				newJournal.IdSeller = row.Field<long>("idSeller");
				newJournal.Count = row.Field<long>("count");
				newJournal.Date = row.Field<string>("date");
				journal.Add(newJournal);
			}
			return journal;
		}
	}
}
