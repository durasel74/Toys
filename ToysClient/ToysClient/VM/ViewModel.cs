using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
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
						  if (command != null && command != String.Empty)
						  {
							  //var resultRequest = client.SendRequest(command);
							  //if (resultRequest == String.Empty) return;
							  //RequestPipeline(command, resultRequest);
						  }
					  }));
			}
		}

		//private ButtonCommand addProductCommand;
		//public ButtonCommand AddProductCommand
		//{
		//	get
		//	{
		//		return addProductCommand ??
		//		  (addProductCommand = new ButtonCommand(obj =>
		//		  {
		//			  ProductObject productObject = new ProductObject();
		//			  var addProductWindow = new AddProductWindow(productObject);
		//			  if (addProductWindow.ShowDialog() == true)
		//			  {
		//				  ProductFields productFields = Product.GetFieldsFrame();
		//				  productFields.title = productObject.Title;
		//				  productFields.cost = productObject.Cost;
		//				  productFields.releaseDate = productObject.ReleaseDate;
		//				  productFields.description = productObject.Description;
		//				  db.AddProduct(productFields);
		//				  LoadAllTables();
		//			  }
		//		  }));
		//	}
		//}

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

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
