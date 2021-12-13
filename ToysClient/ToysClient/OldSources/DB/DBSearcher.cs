//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;

//namespace Test
//{
//	public class DBSearcher
//	{
//		private delegate bool FieldCompare<T>(T product, string query);

//		public IList<Product> SearchProducts(IList<Product> products, string query)
//		{
//			if (query == "") return products;
//			IList<Product> result;

//			result = SearchFields(products, query, (x, y) => x.Title.ToLower() == y.ToLower());
//			if (result.Count > 0) return result;

//			result = SearchFields(products, query, (x, y) => x.Cost.ToString() == y);
//			if (result.Count > 0) return result;

//			result = SearchFields(products, query, (x, y) => x.ReleaseDate == y);
//			if (result.Count > 0) return result;

//			result = SearchFields(products, query, (x, y) => x.Description.ToLower() == y.ToLower());
//			if (result.Count > 0) return result;

//			return result;
//		}

//		public IList<Client> SearchClients(IList<Client> clients, string query)
//		{
//			if (query == "") return clients;
//			IList<Client> result;

//			result = SearchFields(clients, query, (x, y) => x.Fio.ToLower() == y.ToLower());
//			if (result.Count > 0) return result;

//			result = SearchFields(clients, query, (x, y) => x.PhoneNumber == y);
//			if (result.Count > 0) return result;

//			return result;
//		}

//		public IList<Order> SearchOrders(IList<Order> orders, string query)
//		{
//			if (query == "") return orders;
//			IList<Order> result;

//			result = SearchFields(orders, query, (x, y) => x.ClientName.ToLower() == y.ToLower());
//			if (result.Count > 0) return result;

//			result = SearchFields(orders, query, (x, y) => x.ProductName.ToLower() == y.ToLower());
//			if (result.Count > 0) return result;

//			result = SearchFields(orders, query, (x, y) => x.Count.ToString() == y);
//			if (result.Count > 0) return result;

//			result = SearchFields(orders, query, (x, y) => x.Date == y);
//			if (result.Count > 0) return result;

//			return result;
//		}

//		private IList<T> SearchFields<T>(IList<T> tables, string query,
//			FieldCompare<T> comparer)
//		{
//			List<T> foundRows = new List<T>();

//			foreach (var row in tables)
//			{
//				if (comparer(row, query))
//					foundRows.Add(row);
//			}
//			return foundRows;
//		}

//	}
//}
