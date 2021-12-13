using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ToysClient.Model;

namespace ToysClient.VM
{
	public class ViewModel : INotifyPropertyChanged
	{
		private ClientLan client;
		public ViewModel()
		{
			client = new ClientLan("127.0.0.1", 8888);
		}

		public ObservableCollection<Client> Clients { get; set; }
		public ObservableCollection<Seller> Sellers { get; set; }
		public ObservableCollection<Sklad> Sklads { get; set; }
		public ObservableCollection<Toy> Toys { get; set; }
		public ObservableCollection<Journal> Journals { get; set; }

		private ButtonCommand clientGetCommand;
		public ButtonCommand ClientGetCommand
		{
			get
			{
				return clientGetCommand ??
				  (clientGetCommand = new ButtonCommand(obj =>
				  {
					  var resultRequest = client.SendRequest("getclients");
					  var result = JsonSerializer.Deserialize<List<Client>>(resultRequest);
					  Clients = new ObservableCollection<Client>(result);
					  OnPropertyChanged("Clients");
					  Console.WriteLine(Clients[0].IdClient);
				  }));
			}
		}

		private ButtonCommand sellerGetCommand;
		public ButtonCommand SellerGetCommand
		{
			get
			{
				return sellerGetCommand ??
				  (sellerGetCommand = new ButtonCommand(obj =>
				  {
					  var resultRequest = client.SendRequest("getsellers");
					  var result = JsonSerializer.Deserialize<List<Seller>>(resultRequest);
					  Sellers = new ObservableCollection<Seller>(result);
					  OnPropertyChanged("Sellers");
				  }));
			}
		}

		private ButtonCommand skladGetCommand;
		public ButtonCommand SkladGetCommand
		{
			get
			{
				return skladGetCommand ??
				  (skladGetCommand = new ButtonCommand(obj =>
				  {
					  var resultRequest = client.SendRequest("getsklads");
					  var result = JsonSerializer.Deserialize<List<Sklad>>(resultRequest);
					  Sklads = new ObservableCollection<Sklad>(result);
					  OnPropertyChanged("Sklads");
				  }));
			}
		}

		private ButtonCommand toyGetCommand;
		public ButtonCommand ToyGetCommand
		{
			get
			{
				return toyGetCommand ??
				  (toyGetCommand = new ButtonCommand(obj =>
				  {
					  var resultRequest = client.SendRequest("gettoys");
					  var result = JsonSerializer.Deserialize<List<Toy>>(resultRequest);
					  Toys = new ObservableCollection<Toy>(result);
					  OnPropertyChanged("Toys");
				  }));
			}
		}

		private ButtonCommand journalGetCommand;
		public ButtonCommand JournalGetCommand
		{
			get
			{
				return journalGetCommand ??
				  (journalGetCommand = new ButtonCommand(obj =>
				  {
					  var resultRequest = client.SendRequest("getjournals");
					  var result = JsonSerializer.Deserialize<List<Journal>>(resultRequest);
					  Journals = new ObservableCollection<Journal>(result);
					  OnPropertyChanged("Journals");
				  }));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
