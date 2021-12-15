using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToysServer.Model;

namespace ToysServer.DB
{
	public class DBAdder
	{
		private SQLiteConnection connection;
		private SQLiteCommand command;

		public DBAdder(SQLiteConnection connection)
		{
			this.connection = connection;
		}

		public void AddRow(string request)
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = request;
				command.ExecuteNonQuery();
			}
			catch { throw new Exception("Не удалось добавить запись"); }
		}

		public void AddClient(Client client)
		{
			string request;
			request = $"INSERT INTO Client(sfm, phoneNumber)" +
				$"VALUES ('{client.Sfm}', '{client.PhoneNumber}')";
			AddRow(request);
		}

		public void AddSeller(Seller seller)
		{
			string request;
			request = $"INSERT INTO Seller(sfm, phoneNumber)" +
				$"VALUES ('{seller.Sfm}', '{seller.PhoneNumber}')";
			AddRow(request);
		}
	}
}
