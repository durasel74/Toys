using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Toys
{
	public class DBUpdater
	{
		private SQLiteConnection connection;
		private SQLiteCommand command;

		public DBUpdater(SQLiteConnection connection)
		{
			this.connection = connection;
		}

		public void UpdateRow(string request)
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = request;
				command.ExecuteNonQuery();
			}
			catch { throw new Exception("Не удалось обновить запись"); }
		}

		public void UpdateClients(List<Client> clients)
		{
			ClientFields fields;
			string request;

			foreach (Client client in clients)
			{
				fields = client.GetFields();
				request = $"UPDATE Clients SET " +
					$"fio = '{fields.fio}'," +
					$"phoneNumber = '{fields.phoneNumber}' " +
					$"WHERE id_client = {fields.id}";
				UpdateRow(request);
			}
		}

		public void UpdateProducts(List<Product> products)
		{
			ProductFields fields;
			string request;

			foreach (Product product in products)
			{
				fields = product.GetFields();
				request = $"UPDATE Products SET " +
					$"title = '{fields.title}'," +
					$"cost = {fields.cost}," +
					$"releaseDate = '{fields.releaseDate.ToString("yyyy-MM-dd")}'," +
					$"description = '{fields.description}' " +
					$"WHERE id_product = {fields.id}";
				UpdateRow(request);
			}
		}
	}
}
