using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToysServer.Model;

namespace ToysServer.DB
{
	public class DBDeleter
	{
		private SQLiteConnection connection;
		private SQLiteCommand command;

		public DBDeleter(SQLiteConnection connection)
		{
			this.connection = connection;
		}

		public void DeleteRow(string request)
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = request;
				command.ExecuteNonQuery();
			}
			catch { throw new Exception("Не удалось удалить запись"); }
		}

		public void DeleteClient(Client client)
		{
			string request;
			request = $"DELETE FROM Client WHERE idClient = {client.IdClient}";
			DeleteRow(request);
		}

		public void DeleteSeller(Seller seller)
		{
			string request;
			request = $"DELETE FROM Seller WHERE idSeller = {seller.IdSeller}";
			DeleteRow(request);
		}

		public void DeleteSklad(Sklad sklad)
		{
			string request;
			request = $"DELETE FROM Sklad WHERE idSklad = {sklad.IdSklad}";
			DeleteRow(request);
		}

		public void DeleteToy(Toy toy)
		{
			string request;
			request = $"DELETE FROM Toys WHERE idToy = {toy.IdToy}";
			DeleteRow(request);
		}
		
		public void DeleteJournal(Journal journal)
		{
			string request;
			request = $"DELETE FROM Journal WHERE id = {journal.Id}";
			DeleteRow(request);
		}
	}
}
