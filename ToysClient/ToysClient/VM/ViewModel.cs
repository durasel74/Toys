using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ToysClient.Model;

namespace ToysClient.VM
{
	public class ViewModel : INotifyPropertyChanged
	{
		private ClientLan client;
		private string currentRequest;

		public ViewModel()
		{
			client = new ClientLan("127.0.0.1", 8888);
			RequestVariants = new ObservableCollection<string>() {
				"Запрос 1",
				"Запрос 2",
				"Запрос 3",
				"Запрос 4",
				"Запрос 5",
			};
			CurrentRequest = "Запрос 1";
		}

		public ObservableCollection<Client> Clients { get; set; }
		public ObservableCollection<Seller> Sellers { get; set; }
		public ObservableCollection<Sklad> Sklads { get; set; }
		public ObservableCollection<Toy> Toys { get; set; }
		public ObservableCollection<Journal> Journals { get; set; }
		public ObservableCollection<string> RequestVariants { get; set; }
		public DataTable RequestTable { get; set; }

		public string CurrentRequest
		{
			get { return currentRequest; }
			set
			{
				currentRequest = value;
				OnPropertyChanged("CurrentRequest");
			}
		}

		private ButtonCommand requestCommand;
		public ButtonCommand RequestCommmand
		{
			get
			{
				return requestCommand ??
				  (requestCommand = new ButtonCommand(obj =>
				  {
					  string command = ConvertRequestVariant(CurrentRequest);
					  var resultRequest = client.SendRequest(command);
					  if (resultRequest == String.Empty) return;
					  RequestTable = JsonConvert.DeserializeObject<DataTable>(resultRequest);
					  OnPropertyChanged("RequestTable");
				  }));
			}
		}

		private ButtonCommand getCommand;
		public ButtonCommand GetCommand
		{
			get
			{
				return getCommand ??
				  (getCommand = new ButtonCommand(obj =>
				  {
					  var command = obj as string;
					  if (command != null)
					  {
						  var resultRequest = client.SendRequest(command);
						  if (resultRequest == String.Empty) return;
						  RequestPipeline(command, resultRequest);
					  }
				  }));
			}
		}

		private string ConvertRequestVariant(string variant)
		{
			return variant.ToLower() switch
			{
				"запрос 1" => "request1",
				"запрос 2" => "request2",
				"запрос 3" => "request3",
				"запрос 4" => "request4",
				"запрос 5" => "request5",
				_ => ""
			};
		}

		private void RequestPipeline(string command, string json)
		{
			switch (command.ToLower())
			{
				case "getclients":
					DeserializeClients(json);
					break;
				case "getsellers":
					DeserializeSellers(json);
					break;
				case "getsklads":
					DeserializeSklads(json);
					break;
				case "gettoys":
					DeserializeToys(json);
					break;
				case "getjournals":
					DeserializeJournals(json);
					break;
			}
		}

		private void DeserializeClients(string json)
		{
			var result = JsonConvert.DeserializeObject<List<Client>>(json);
			Clients = new ObservableCollection<Client>(result);
			OnPropertyChanged("Clients");
		}

		private void DeserializeSellers(string json)
		{
			var result = JsonConvert.DeserializeObject<List<Seller>>(json);
			Sellers = new ObservableCollection<Seller>(result);
			OnPropertyChanged("Sellers");
		}

		private void DeserializeSklads(string json)
		{
			var result = JsonConvert.DeserializeObject<List<Sklad>>(json);
			Sklads = new ObservableCollection<Sklad>(result);
			OnPropertyChanged("Sklads");
		}

		private void DeserializeToys(string json)
		{
			var result = JsonConvert.DeserializeObject<List<Toy>>(json);
			Toys = new ObservableCollection<Toy>(result);
			OnPropertyChanged("Toys");
		}

		private void DeserializeJournals(string json)
		{
			var result = JsonConvert.DeserializeObject<List<Journal>>(json);
			Journals = new ObservableCollection<Journal>(result);
			OnPropertyChanged("Journals");
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
