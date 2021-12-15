using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using ToysServer.Model;

namespace ToysServer.DB
{
	public class DBChanger
	{
		private SQLiteConnection connection;
		private SQLiteCommand command;

		public DBChanger(SQLiteConnection connection)
		{
			this.connection = connection;
		}

		public void ChangeRow(string request)
		{
			try
			{
				command = new SQLiteCommand(connection);
				command.CommandText = request;
				command.ExecuteNonQuery();
			}
			catch { throw new Exception("Не удалось обновить запись"); }
		}

		public void ChangeClient(List<Client> clients)
		{
			string request;
			foreach (var client in clients)
			{
				request = $"UPDATE Client SET sfm = '{client.Sfm}', " +
				$"phoneNumber = '{client.PhoneNumber}' WHERE idClient = {client.IdClient}";
				ChangeRow(request);
			}
		}

		public void ChangeSeller(List<Seller> sellers)
		{
			string request;
			foreach (var seller in sellers)
			{
				request = $"UPDATE Seller SET sfm = '{seller.Sfm}', " +
				$"phoneNumber = '{seller.PhoneNumber}' WHERE idSeller = {seller.IdSeller}";
				ChangeRow(request);
			}
		}

		public void ChangeSklad(List<Sklad> sklads)
		{
			string request;
			foreach (var sklad in sklads)
			{
				request = $"UPDATE Sklad SET address = '{sklad.Address}' " +
					$"WHERE idSklad = {sklad.IdSklad}";
				ChangeRow(request);
			}
		}

		public void ChangeToy(List<Toy> toys)
		{
			string request;
			foreach (var toy in toys)
			{
				request = $"UPDATE Toys SET idSklad = {toy.IdSklad}, " +
				$"name = '{toy.Name}', cost = {toy.Cost}, " +
				$"releaseDate = '{toy.ReleaseDate}', info = '{toy.Info}' " +
				$"WHERE idToy = {toy.IdToy}";
				ChangeRow(request);
			}
		}

		public void ChangeJournal(List<Journal> journals)
		{
			string request;
			foreach (var journal in journals)
			{
				request = $"UPDATE Journal SET idToy = {journal.IdToy}, " +
				$"idClient = {journal.IdClient}, idSeller = {journal.IdSeller}, " +
				$"count = {journal.Count}, date = '{journal.Date}' " +
				$"WHERE id = {journal.Id}";
				ChangeRow(request);
			}
		}
	}
}
