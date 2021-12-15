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
	public partial class DeleteSkladWindow : Window
	{
		private ViewModel viewModel;

		public DeleteSkladWindow() { InitializeComponent(); }
		public DeleteSkladWindow(ViewModel viewModel) : this()
		{
			this.viewModel = viewModel;
			DataContext = viewModel;
		}

		private void CreateClick(object sender, RoutedEventArgs e)
		{
			if (viewModel.SelectedElement != null) this.DialogResult = true;
			else MessageBox.Show("Склад не выбран");
		}
		private void CancelClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
