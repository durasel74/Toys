using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToysServer.Model;

namespace ToysServer.DB
{
	public class DBRequester
	{
		private SQLiteConnection connection;
		private SQLiteCommand command;

		public DBRequester(SQLiteConnection connection)
		{
			this.connection = connection;
		}

		public DataTable Request1()
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = "SELECT Sklad.address, Seller.sfm, Seller.phoneNumber FROM " +
					"Journal JOIN Toys ON Journal.idToy = Toys.idToy " +
					"JOIN Sklad ON Toys.idSklad = Sklad.idSklad " +
					"JOIN Seller ON Journal.idSeller = Seller.idSeller";
				DataTable data = new DataTable();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				adapter.Fill(data);
				return data;
			}
			catch { throw new Exception("Таблица не найдена"); }
		}

		public DataTable Request2()
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = "SELECT Toys.name, Toys.releaseDate, Journal.date FROM " +
					"Journal JOIN Toys ON Journal.idToy = Toys.idToy";
				DataTable data = new DataTable();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				adapter.Fill(data);
				return data;
			}
			catch { throw new Exception("Таблица не найдена"); }
		}

		public DataTable Request3()
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = "SELECT Client.sfm, Toys.name, Toys.cost, Journal.count FROM " +
					"Journal JOIN Toys ON Journal.idToy = Toys.idToy " +
					"JOIN Client ON Journal.idClient = Client.idClient";
				DataTable data = new DataTable();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				adapter.Fill(data);
				return data;
			}
			catch { throw new Exception("Таблица не найдена"); }
		}

		public DataTable Request4()
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = "SELECT Client.sfm, Sklad.address FROM " +
					"Journal JOIN Toys ON Journal.idToy = Toys.idToy " +
					"JOIN Sklad ON Toys.idSklad = Sklad.idSklad " +
					"JOIN Client ON Journal.idClient = Client.idClient";
				DataTable data = new DataTable();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				adapter.Fill(data);
				return data;
			}
			catch { throw new Exception("Таблица не найдена"); }
		}

		public DataTable Request5()
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = "SELECT Journal.date, Toys.name, Journal.count, Toys.cost, Client.phoneNumber FROM " +
					"Journal JOIN Toys ON Journal.idToy = Toys.idToy " +
					"JOIN Client ON Journal.idClient = Client.idClient";
				DataTable data = new DataTable();
				SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
				adapter.Fill(data);
				return data;
			}
			catch { throw new Exception("Таблица не найдена"); }
		}
	}
}
