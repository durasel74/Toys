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
using ToysClient.VM;

namespace ToysClient.View
{
	public partial class MainWindow : Window
	{
		private ViewModel viewModel;
		private TabItem lastTab;

		public MainWindow()
		{
			InitializeComponent();
			viewModel = new ViewModel();
			DataContext = viewModel;
		}

		private void TabChanged(Object sender, SelectionChangedEventArgs args)
		{
			var tc = sender as TabControl;
			if (tc != null && tc.SelectedItem != lastTab)
			{
				var item = tc.SelectedItem as TabItem;
				lastTab = item;
				var header = item?.Header as string;
				if (header != null)
				{
					string request = viewModel.ConvertTabItemToRequest(header);
					viewModel.GetCommand.Execute(request);
				}
			}
		}

		private void AuthorInfo(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Автор Коротовский Дмитрий курсант 432 группы ТАТК ГА");
		}
		private void ProgramInfo(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Инструкция:" +
			"\nИспользуйте вкладки для перемещения по таблицам.\n" +
			"Кнопка 'Обновить' позволяет выводит актульную информацию с сервера");
		}
		private void ExitClick(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
	}
}
