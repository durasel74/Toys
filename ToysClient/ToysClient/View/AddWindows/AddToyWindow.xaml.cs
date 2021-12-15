using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToysClient.Model;

namespace ToysClient.View
{
	public partial class AddToyWindow : Window
	{
		public AddToyWindow()
		{
			InitializeComponent();
			this.NewToy = new Toy();
			DataContext = NewToy;
		}

		public Toy NewToy { get; set; }

		private void CreateClick(object sender, RoutedEventArgs e)
		{
			if (NewToy.Name != String.Empty &&
				NewToy.ReleaseDate != String.Empty)
			{
				this.DialogResult = true;
			}
			else MessageBox.Show("Данные о игрушке заполнены неверно");
		}
		private void CancelClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
