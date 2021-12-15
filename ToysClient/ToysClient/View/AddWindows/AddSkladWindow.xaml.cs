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
	public partial class AddSkladWindow : Window
	{
		public AddSkladWindow()
		{
			InitializeComponent();
			this.NewSklad = new Sklad();
			DataContext = NewSklad;
		}

		public Sklad NewSklad { get; set; }

		private void CreateClick(object sender, RoutedEventArgs e)
		{
			if (NewSklad.Address != String.Empty)
				this.DialogResult = true;
			else MessageBox.Show("Данные о клиенте заполнены неверно");
		}
		private void CancelClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
