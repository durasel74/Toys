//using System;
//using System.Linq;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Collections.ObjectModel;

//namespace Test
//{
//	public class ViewModel : INotifyPropertyChanged
//	{
//        private DBWorker db;
//        private bool userIsAdmin;
//        private string searchString = "";
//        private ObservableCollection<Product> savedProducts;
//        private ObservableCollection<Client> savedClients;
//        private ObservableCollection<Order> savedOrders;

//        public ViewModel()
//        {
//            db = new DBWorker("DB\\SIUchet.db");
//            AuthorizationCommand.Execute(null);
//            LoadAllTables();
//        }

//        public ObservableCollection<Product> Products { get; private set; }
//        public ObservableCollection<Client> Clients { get; private set; }
//        public ObservableCollection<Order> Orders { get; private set; }
//        public bool UserIsAdmin => userIsAdmin;
//        public string AdminPassword => "xl107";
//        public string CurrentTab { get; set; }

//        public string SearchString
//		{
//            get { return searchString; }
//            set
//			{
//                searchString = value;
//                OnPropertyChanged("SearchString");
//                StartSearch(CurrentTab);
//			}
//		}

//		private ButtonCommand resetCommand;
//		public ButtonCommand ResetCommand
//		{
//			get
//			{
//				return resetCommand ??
//				  (resetCommand = new ButtonCommand(obj =>
//				  {
//					  var table = obj as string;
//					  if (table != null)
//					  {
//						  switch (table)
//						  {
//							  case "Clients":
//								  Clients = new ObservableCollection<Client>(db.LoadClients());
//                                  OnPropertyChanged("Clients");
//								  break;
//							  case "Products":
//								  Products = new ObservableCollection<Product>(db.LoadProducts());
//                                  OnPropertyChanged("Products");
//                                  break;
//							  default: return;
//						  }
//					  }
//				  }));
//			}
//		}

//        private ButtonCommand updateCommand;
//        public ButtonCommand UpdateCommand
//		{
//            get
//            {
//                return updateCommand ??
//                  (updateCommand = new ButtonCommand(obj =>
//                  {
//                      var table = obj as string;
//                      if (table != null)
//                      {
//                          switch (table)
//                          {
//                              case "Clients":
//                                  db.UpdateClients(Clients.ToList());
//                                  break;
//                              case "Products":
//                                  db.UpdateProducts(Products.ToList());
//                                  break;
//                              default: return;
//                          }
//                      }
//                      LoadAllTables();
//                  }));
//            }
//        }

//        private ButtonCommand addOrderCommand;
//        public ButtonCommand AddOrderCommand
//        {
//            get
//            {
//                return addOrderCommand ??
//                  (addOrderCommand = new ButtonCommand(obj =>
//                  {
//                      OrderObject orderObject = new OrderObject(Products, Clients);
//                      AddOrderWindow addOrderWindow = new AddOrderWindow(orderObject);
//                      if (addOrderWindow.ShowDialog() == true)
//                      {
//                          ClientFields clientFields;
//                          ProductFields productFields;
//                          OrderFields orderFields = Order.GetFieldsFrame();
//                          long clientId, productId;

//                          orderFields.date = orderObject.DateNoStr;
//                          orderFields.count = orderObject.Count;
//                          if (orderObject.IsNewClient)
//                          {
//                              clientFields = Client.GetFieldsFrame();
//                              clientFields.fio = orderObject.ClientFio;
//                              clientFields.phoneNumber = orderObject.ClientPhone;
//                              db.AddClient(clientFields);
//                              clientId = Clients[Clients.Count - 1].GetFields().id + 1;
//                          }
//                          else
//                          {
//                              clientFields = orderObject.CurrentClient.GetFields();
//                              clientId = clientFields.id;
//                          }
//                          productFields = orderObject.CurrentProduct.GetFields();
//                          productId = productFields.id;
//                          db.AddOrder(orderFields, clientId, productId);
//                          LoadAllTables();
//                      }
//                  }));
//            }
//        }

//        private ButtonCommand addProductCommand;
//        public ButtonCommand AddProductCommand
//		{
//            get
//            {
//                return addProductCommand ??
//                  (addProductCommand = new ButtonCommand(obj =>
//                  {
//                      ProductObject productObject = new ProductObject();
//                      var addProductWindow = new AddProductWindow(productObject);
//                      if (addProductWindow.ShowDialog() == true)
//					  {
//                          ProductFields productFields = Product.GetFieldsFrame();
//                          productFields.title = productObject.Title;
//                          productFields.cost = productObject.Cost;
//                          productFields.releaseDate = productObject.ReleaseDate;
//                          productFields.description = productObject.Description;
//                          db.AddProduct(productFields);
//                          LoadAllTables();
//					  }
//                  }));
//            }
//        }

//        private ButtonCommand authorizationCommand;
//        public ButtonCommand AuthorizationCommand
//		{
//            get
//            {
//                return authorizationCommand ??
//                  (authorizationCommand = new ButtonCommand(obj =>
//                  {
//                      var authorizationWindow = new AuthorizationWindow(this);
//                      if (authorizationWindow.ShowDialog() == false)
//                          System.Windows.Application.Current.Shutdown();
//                  }));
//            }
//        }

//        private ButtonCommand searchCommand;
//        public ButtonCommand SearchCommand
//		{
//            get
//            {
//                return searchCommand ??
//                  (searchCommand = new ButtonCommand(obj =>
//                  {
//                      var table = obj as string;
//                      if (table != null)
//                      {
//                          switch (table)
//                          {
//                              case "Products":
//                                  SearchProducts();
//                                  break;
//                              case "Clients":
//                                  SearchClients();
//                                  break;
//                              case "Orders":
//                                  SearchOrders();
//                                  break;
//                              default: return;
//                          }
//                      }
//                  }));
//            }
//        }

//		public void LoadAllTables()
//		{
//            Clients = new ObservableCollection<Client>(db.LoadClients());
//            Products = new ObservableCollection<Product>(db.LoadProducts());
//            Orders = new ObservableCollection<Order>(db.LoadOrders());
//            savedProducts = null;
//            savedClients = null;
//            savedOrders = null;
//            OnPropertyChanged("Clients");
//            OnPropertyChanged("Products");
//            OnPropertyChanged("Orders");
//        }
//        public void Authorize(bool isAdmin) => userIsAdmin = isAdmin;

//        public void StartSearch(string table)
//		{
//            SearchCommand.Execute(table);
//        }

//        private void SearchProducts()
//		{
//            if (savedProducts == null) savedProducts = Products;
//            Products = new ObservableCollection<Product>(
//                db.SearchProducts(savedProducts, searchString));
//            OnPropertyChanged("Products");
//        }

//        private void SearchClients()
//		{
//            if (savedClients == null) savedClients = Clients;
//            Clients = new ObservableCollection<Client>(
//                db.SearchClients(savedClients, searchString));
//            OnPropertyChanged("Clients");
//        }

//        private void SearchOrders()
//		{
//            if (savedOrders == null) savedOrders = Orders;
//            Orders = new ObservableCollection<Order>(
//                db.SearchOrders(savedOrders, searchString));
//            OnPropertyChanged("Orders");
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        public void OnPropertyChanged([CallerMemberName] string prop = "")
//        {
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs(prop));
//        }
//    }
//}
