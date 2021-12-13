using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Test
{
	public class OrderObject : INotifyPropertyChanged
	{
        private DateTime date;
        private long count;
        private bool isNewClient;
        private string clientFio;
        private string clientPhone;
        private Client currentClient;
        private Product currentProduct;

        public OrderObject(ObservableCollection<Product> products,
            ObservableCollection<Client> clients)
        {
            date = DateTime.Now;
            count = 1;
            isNewClient = false;
            clientFio = "";
            clientPhone = "";
            Products = products;
            Clients = clients;
        }

        public string Date => date.ToString("dd.MM.yyyy");
        public DateTime DateNoStr => date;
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<Client> Clients { get; private set; }

        public long Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public bool IsNewClient
        {
            get { return isNewClient; }
            set
            {
                isNewClient = value;
                OnPropertyChanged("IsNewClient");
            }
        }

        public string ClientFio
        {
            get { return clientFio; }
            set
            {
                clientFio = value;
                OnPropertyChanged("ClientFio");
            }
        }

        public string ClientPhone
        {
            get { return clientPhone; }
            set
            {
                clientPhone = value;
                OnPropertyChanged("ClientPhone");
            }
        }

        public Client CurrentClient
        {
            get { return currentClient; }
            set
            {
                currentClient = value;
                OnPropertyChanged("CurrentClient");
            }
        }

        public Product CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                currentProduct = value;
                OnPropertyChanged("CurrentProduct");
            }
        }

        public bool ValidationOrder()
		{
            if (CurrentProduct == null) return false;
            if (IsNewClient)
            {
                if (ClientFio.Trim() == "") return false;
                if (ClientPhone.Trim() == "") return false;
            }
            else if (CurrentClient == null) return false;
            return true;
		}

        public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
