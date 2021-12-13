using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Toys
{
	public class Product : INotifyPropertyChanged
	{
		private long id;
		private string title;
		private double cost;
		private DateTime releaseDate;
		private string description;

		public Product(long id, string title, double cost, DateTime releaseDate, 
			string description = "")
		{
			this.id = id;
			this.title = title;
			this.cost = cost;
			this.releaseDate = releaseDate;
			this.description = description;
		}

		public string Title => title;
		public string ReleaseDate => releaseDate.ToString("dd.MM.yyyy");

		public double Cost
		{
			get { return cost; }
			set
			{
				cost = value;
				OnPropertyChanged("Cost");
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

		public static ProductFields GetFieldsFrame()
		{
			return new ProductFields();
		}

		public ProductFields GetFields()
		{
			ProductFields fields = new ProductFields();
			fields.id = id;
			fields.title = title;
			fields.cost = cost;
			fields.releaseDate = releaseDate;
			fields.description = description;
			return fields;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}

	public struct ProductFields
	{
		public long id;
		public string title;
		public double cost;
		public DateTime releaseDate;
		public string description;
	}
}
