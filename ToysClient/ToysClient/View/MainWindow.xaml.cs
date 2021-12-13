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
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new ViewModel();
		}

		public void AuthorInfo(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Автор Коротовский Дмитрий курсант 432 группы ТАТК ГА");
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
	}
}
