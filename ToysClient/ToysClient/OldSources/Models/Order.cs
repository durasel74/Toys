using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test
{
	public class Order : INotifyPropertyChanged
	{
		private long id;
		private string clientName;
		private string productName;
		private long count;
		private DateTime date;

		public Order(long id, string clientName, string productName, 
			long count, DateTime date)
		{
			this.id = id;
			this.clientName = clientName;
			this.productName = productName;
			this.count = count;
			this.date = date;
		}

		public string ClientName => clientName;
		public string ProductName => productName;
		public long Count => count;
		public string Date => date.ToString("dd.MM.yyyy");

		public static OrderFields GetFieldsFrame()
		{
			return new OrderFields();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	public struct OrderFields
	{
		public long id;
		public string clientName;
		public string productName;
		public long count;
		public DateTime date;
	}
}
