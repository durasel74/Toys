using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Toys
{
	public class ProductObject : INotifyPropertyChanged
	{
		private string title;
		private double cost;
		private DateTime releaseDate;
		private string description;

		public ProductObject()
		{
			title = "";
			cost = 0;
			releaseDate = DateTime.Now;
			description = "";
		}

		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}

		public double Cost
		{
			get { return cost; }
			set
			{
				cost = value;
				OnPropertyChanged("Cost");
			}
		}

		public DateTime ReleaseDate
		{
			get { return releaseDate; }
			set
			{
				releaseDate = value;
				OnPropertyChanged("ReleaseDate");
			}
		}

		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				OnPropertyChanged("Description");
			}
		}

		public bool ValidationProduct()
		{
			if (title.Trim() == "") return false;
			if (cost < 0) return false;
			if (releaseDate > DateTime.Now) return false;
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
