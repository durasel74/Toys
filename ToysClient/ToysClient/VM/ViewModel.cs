using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using Newtonsoft.Json;
using ToysClient.Model;
using ToysClient.View;

namespace ToysClient.VM
{
	public class ViewModel : INotifyPropertyChanged
	{
		private ClientLan client;
		private string currentRequest;
		private string requestInfo;
		private bool userIsAdmin;
		private Object selectedElement;

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
			AuthorizationCommand.Execute(null);
		}

		public ObservableCollection<Client> Clients { get; set; }
		public ObservableCollection<Seller> Sellers { get; set; }
		public ObservableCollection<Sklad> Sklads { get; set; }
		public ObservableCollection<Toy> Toys { get; set; }
		public ObservableCollection<Journal> Journals { get; set; }
		public ObservableCollection<string> RequestVariants { get; set; }
		public DataTable RequestTable { get; set; }
		public bool UserIsAdmin => userIsAdmin;
		public string AdminPassword => "xl107";

		public Object SelectedElement
		{
			get { return selectedElement; }
			set
			{
				selectedElement = value;
				OnPropertyChanged("SelectedElement");
			}
		}

		public string CurrentRequest
		{
			get { return currentRequest; }
			set
			{
				currentRequest = value;
				switch (currentRequest)
				{
					case "Запрос 1":
						RequestInfo = "Вывод склада, с которого была куплена игрушка, ФИО продавца и его номер телефона";
						break;
					case "Запрос 2":
						RequestInfo = "Название игрушки, дата создания и дата ее покупки";
						break;
					case "Запрос 3":
						RequestInfo = "ФИО покупателя, купленная игрушка, стоимость и количество";
						break;
					case "Запрос 4":
						RequestInfo = "ФИО покупателя и адрес склада с которого велась покупка";
						break;
					case "Запрос 5":
						RequestInfo = "Дата, игрушка, количество, стоимость и номер телефона покупателя";
						break;
				}
				OnPropertyChanged("CurrentRequest");
			}
		}

		public string RequestInfo
		{
			get { return requestInfo; }
			set
			{
				requestInfo = value;
				OnPropertyChanged("RequestInfo");
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
					  if (command != null && command != String.Empty)
					  {
						  string resultRequest = "";
						  try { resultRequest = client.SendRequest(command); }
						  catch { return; }
						  if (resultRequest == String.Empty) return;
						  RequestPipeline(command, resultRequest);
					  }
				  }));
			}
		}

		private ButtonCommand addCommand;
		public ButtonCommand AddCommand
		{
			get
			{
				return addCommand ??
					  (addCommand = new ButtonCommand(obj =>
					  {
						  var command = obj as string;
						  if (command != null)
						  {
							  switch (command.ToLower())
							  {
								  case "addclient":
									  AddClient();
									  break;
								  case "addseller":
									  AddSeller();
									  break;
								  case "addsklad":
									  AddSklad();
									  break;
								  case "addtoy":
									  AddToy();
									  break;
								  case "addjournal":
								  	  AddJournal();
									  break;
							  }
						  }
					  }));
			}
		}

		private ButtonCommand deleteCommand;
		public ButtonCommand DeleteCommand
		{
			get
			{
				return deleteCommand ??
					  (deleteCommand = new ButtonCommand(obj =>
					  {
						  var command = obj as string;
						  if (command != null)
						  {
							  switch (command.ToLower())
							  {
								  case "deleteclient":
									  DeleteClient();
									  break;
								  case "deleteseller":
									  DeleteSeller();
									  break;
								  case "deletesklad":
									  DeleteSklad();
									  break;
									case "deletetoy":
									  DeleteToy();
									  break;
								  case "deletejournal":
									  DeleteJournal();
									  break;
							  }
						  }
					  }));
			}
		}

		private ButtonCommand changeCommand;
		public ButtonCommand ChangeCommand
		{
			get
			{
				return changeCommand ??
					  (changeCommand = new ButtonCommand(obj =>
					  {
						  var command = obj as string;
						  if (command != null)
						  {
							  switch (command.ToLower())
							  {
								  case "changeclient":
									  ChangeClient();
									  break;
								//   case "addseller":
								// 	  AddSeller();
								// 	  break;
								//   case "addsklad":
								// 	  AddSklad();
								// 	  break;
								//   case "addtoy":
								// 	  AddToy();
								// 	  break;
								//   case "addjournal":
								//   	  AddJournal();
								// 	  break;
							  }
						  }
					  }));
			}
		}

		private ButtonCommand authorizationCommand;
		public ButtonCommand AuthorizationCommand
		{
			get
			{
				return authorizationCommand ??
				  (authorizationCommand = new ButtonCommand(obj =>
				  {
					  var authorizationWindow = new AuthorizationWindow(this);
					  if (authorizationWindow.ShowDialog() == false)
						  System.Windows.Application.Current.Shutdown();
				  }));
			}
		}

		public void Authorize(bool isAdmin) => userIsAdmin = isAdmin;
		public string ConvertTabItemToRequest(string header)
		{
			return header.ToLower() switch
			{
				"покупатели" => "getclients",
				"продавцы" => "getsellers",
				"склады" => "getsklads",
				"игрушки" => "gettoys",
				"журнал" => "getjournals",
				_ => ""
			};
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

		private void AddClient()
		{
			var addClientWindow = new AddClientWindow();
			if (addClientWindow.ShowDialog() == true)
			{
				var newClient = addClientWindow.NewClient;
				string json = JsonConvert.SerializeObject(newClient);
				string command = "addclient/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на создание не был выполнен");
				else
					GetCommand.Execute("getclients");
			}
		}

		private void AddSeller()
		{
			var addSellerWindow = new AddSellerWindow();
			if (addSellerWindow.ShowDialog() == true)
			{
				var newSeller = addSellerWindow.NewSeller;
				string json = JsonConvert.SerializeObject(newSeller);
				string command = "addseller/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на создание не был выполнен");
				else
					GetCommand.Execute("getsellers");
			}
		}

		private void AddSklad()
		{
			var addSkladWindow = new AddSkladWindow();
			if (addSkladWindow.ShowDialog() == true)
			{
				var newSklad = addSkladWindow.NewSklad;
				string json = JsonConvert.SerializeObject(newSklad);
				string command = "addsklad/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на создание не был выполнен");
				else
					GetCommand.Execute("getsklads");
			}
		}

		private void AddToy()
		{
			var addToyWindow = new AddToyWindow();
			if (addToyWindow.ShowDialog() == true)
			{
				var newToy = addToyWindow.NewToy;
				string json = JsonConvert.SerializeObject(newToy);
				string command = "addtoy/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на создание не был выполнен");
				else
					GetCommand.Execute("gettoys");
			}
		}

		private void AddJournal()
		{
			var addJournalWindow = new AddJournalWindow();
			if (addJournalWindow.ShowDialog() == true)
			{
				var newJournal = addJournalWindow.NewJournal;
				string json = JsonConvert.SerializeObject(newJournal);
				string command = "addjournal/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на создание не был выполнен");
				else
					GetCommand.Execute("getJournals");
			}
		}

		private void DeleteClient()
		{
			var deleteClientWindow = new DeleteClientWindow(this);
			if (deleteClientWindow.ShowDialog() == true)
			{
				var delClient = SelectedElement as Client;
				if (delClient == null) return;
				string json = JsonConvert.SerializeObject(delClient);
				string command = "deleteclient/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на удаление не был выполнен");
				else
					GetCommand.Execute("getclients");
				SelectedElement = null;
			}
		}

		private void DeleteSeller()
		{
			var deleteSellerWindow = new DeleteSellerWindow(this);
			if (deleteSellerWindow.ShowDialog() == true)
			{
				var delSeller = SelectedElement as Seller;
				if (delSeller == null) return;
				string json = JsonConvert.SerializeObject(delSeller);
				string command = "deleteseller/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на удаление не был выполнен");
				else
					GetCommand.Execute("getsellers");
				SelectedElement = null;
			}
		}

		private void DeleteSklad()
		{
			var deleteSkladWindow = new DeleteSkladWindow(this);
			if (deleteSkladWindow.ShowDialog() == true)
			{
				var delSklad = SelectedElement as Sklad;
				if (delSklad == null) return;
				string json = JsonConvert.SerializeObject(delSklad);
				string command = "deletesklad/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на удаление не был выполнен");
				else
					GetCommand.Execute("getsklads");
				SelectedElement = null;
			}
		}

		private void DeleteToy()
		{
			var deleteToyWindow = new DeleteToyWindow(this);
			if (deleteToyWindow.ShowDialog() == true)
			{
				var delToy = SelectedElement as Toy;
				if (delToy == null) return;
				string json = JsonConvert.SerializeObject(delToy);
				string command = "deletetoy/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на удаление не был выполнен");
				else
					GetCommand.Execute("gettoys");
				SelectedElement = null;
			}
		}

		private void DeleteJournal()
		{
			var deleteJournalWindow = new DeleteJournalWindow(this);
			if (deleteJournalWindow.ShowDialog() == true)
			{
				var delJournal = SelectedElement as Journal;
				if (delJournal == null) return;
				string json = JsonConvert.SerializeObject(delJournal);
				string command = "deletejournal/" + json;
				var resultRequest = client.SendRequest(command);
				if (resultRequest == String.Empty) return;
				if (resultRequest.ToLower() != "ok")
					MessageBox.Show("Запрос на удаление не был выполнен");
				else
					GetCommand.Execute("getjournals");
				SelectedElement = null;
			}
		}

		private void ChangeClient()
		{
			List<Client> clients = new List<Client>(Clients);
			string json = JsonConvert.SerializeObject(clients);
			string command = "changeclients/" + json;
			var resultRequest = client.SendRequest(command);
			if (resultRequest == String.Empty) return;
			if (resultRequest.ToLower() != "ok")
				MessageBox.Show("Запрос на изменение не был выполнен");
			else
				GetCommand.Execute("getclients");
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
