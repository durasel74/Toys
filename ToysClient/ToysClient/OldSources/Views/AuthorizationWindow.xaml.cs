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

namespace Test
{
	public partial class AuthorizationWindow : Window
	{
		// private ViewModel viewModel;

		public AuthorizationWindow()
		{
			InitializeComponent();
		}
		//public AuthorizationWindow(ViewModel viewModel) : this()
		//{
		//	this.viewModel = viewModel;
		//	DataContext = viewModel;
		//}

		//private void AdminClick(object sender, RoutedEventArgs e)
		//{
		//	string password = Password.Password;
		//	if (password == viewModel.AdminPassword)
		//	{
		//		viewModel.Authorize(true);
		//		this.DialogResult = true;
		//	}
		//	else MessageBox.Show("Пароль введен неверно!");
		//}
		//private void ImplClick(object sender, RoutedEventArgs e)
		//{
		//	viewModel.Authorize(false);
		//	this.DialogResult = true;
		//}
	}
}
