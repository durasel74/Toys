//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;

//namespace Test
//{
//	public class DBAdder
//	{
//		private SQLiteConnection connection;
//		private SQLiteCommand command;

//		public DBAdder(SQLiteConnection connection)
//		{
//			this.connection = connection;
//		}

//        public void AddRow(string request)
//        {
//            try
//            {
//                command = new SQLiteCommand(connection);
//                command.CommandText = request;
//                command.ExecuteNonQuery();
//            } catch { throw new Exception("Не удалось добавить запись"); }
//        }

//        public void AddClient(ClientFields fields)
//        {
//            string request;
//            request = $"INSERT INTO Clients(fio, phoneNumber)" +
//                $"VALUES ('{fields.fio}', '{fields.phoneNumber}')";
//            AddRow(request);
//        }

//        public void AddOrder(OrderFields orderFields, long clientId, long productId)
//        {
//            string request;
//            request = $"INSERT INTO Orders(id_client, id_product, count, date)" + 
//                $"VALUES ({clientId}, {productId}, {orderFields.count}, " +
//                $"'{orderFields.date.ToString("yyyy-MM-dd")}')";
//            AddRow(request);
//        }

//        public void AddProduct(ProductFields productFields)
//		{
//            string request;
//            request = $"INSERT INTO Products(title, cost, releaseDate, description)" +
//                $"VALUES ('{productFields.title}', {productFields.cost}, " +
//                $"'{productFields.releaseDate.ToString("yyyy-MM-dd")}', " +
//                $"'{productFields.description}')";
//            AddRow(request);
//		}
//	}
//}
