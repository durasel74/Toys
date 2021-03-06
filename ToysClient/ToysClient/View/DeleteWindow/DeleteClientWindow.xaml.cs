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
using ToysClient.VM;

namespace ToysClient.View
{
	public partial class DeleteClientWindow : Window
	{
		private ViewModel viewModel;

		public DeleteClientWindow() { InitializeComponent(); }
		public DeleteClientWindow(ViewModel viewModel) : this()
		{
			this.viewModel = viewModel;
			DataContext = viewModel;
		}

		private void CreateClick(object sender, RoutedEventArgs e)
		{
			if (viewModel.SelectedElement != null) this.DialogResult = true;
			else MessageBox.Show("Покупатель не выбран");
		}
		private void CancelClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
