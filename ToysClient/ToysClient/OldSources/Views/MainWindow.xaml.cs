using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Test
{
	public partial class MainWindow : Window
	{
		//private ViewModel viewModel;

		public MainWindow()
		{
			InitializeComponent();
			//viewModel = new ViewModel();
			//DataContext = viewModel;
		}

		public void AuthorInfo(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Автор Коротовский Дмитрий курсан 432 группы ТАТК ГА");
		}
		public void ProgramInfo(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Инструкция:" +
			"\nИспользуйте вкладки для перемещения по таблицам.\n" +
			"Кнопки 'Создать заказ' и 'Создать продукт' позволяют " +
			"добавлять элементы таблицу.\n" +
			"Кнопки 'Обновить' и 'Сбросить' обновляют и сбрасывают " +
			"изменения соответственно.");
		}
		public void ExitClick(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		// private void SearchProducts(object sender, TextChangedEventArgs e)
		// {
		// 	viewModel.CurrentTab = "Products";
		// 	viewModel.SearchString = ProductsText.Text;
		// }
		// private void SearchClients(object sender, TextChangedEventArgs e)
		// {
		// 	viewModel.CurrentTab = "Clients";
		// 	viewModel.SearchString = ClientsText.Text;
		// }
		// private void SearchOrders(object sender, TextChangedEventArgs e)
		// {
		// 	viewModel.CurrentTab = "Orders";
		// 	viewModel.SearchString = OrdersText.Text;
		// }
	}
}
