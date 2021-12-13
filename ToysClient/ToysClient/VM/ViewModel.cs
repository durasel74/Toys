using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToysClient.Model;

namespace ToysClient.VM
{
	public class ViewModel
	{
		public ViewModel()
		{
			var client = new ClientLan("127.0.0.1", 8888);
			var result = client.SendRequest("getjournals");
			Console.WriteLine(result);
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
