using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
	public partial class AddOrderWindow : Window
	{
		private OrderObject orderObject;

		public AddOrderWindow()
		{
			InitializeComponent();
		}
        public AddOrderWindow(OrderObject orderObject) : this()
        {
			this.orderObject = orderObject;
            DataContext = orderObject;
        }

		private void CreateClick(object sender, RoutedEventArgs e)
		{
			var validationResult = orderObject.ValidationOrder();
			if (validationResult) this.DialogResult = true;
			else MessageBox.Show("Заказ заполнен неверно");
		}
		private void CancelClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
