using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test
{
	public class Client : INotifyPropertyChanged
	{
		private long id;
		private string fio;
		private string phoneNumber;

		public Client(long id, string fio, string phoneNumber)
		{
			this.id = id;
			this.fio = fio;
			this.phoneNumber = phoneNumber;
		}

		public string Fio => fio;

		public string PhoneNumber
		{
			get { return phoneNumber; }
			set
			{
				phoneNumber = value;
				OnPropertyChanged("PhoneNumber");
			}
		}

		public static ClientFields GetFieldsFrame()
		{
			return new ClientFields();
		}

		public ClientFields GetFields()
		{
			ClientFields fields = new ClientFields();
			fields.id = id;
			fields.fio = fio;
			fields.phoneNumber = phoneNumber;
			return fields;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	public struct ClientFields
	{
		public long id;
		public string fio;
		public string phoneNumber;
	}
}
