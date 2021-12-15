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
	public partial class AddJournalWindow : Window
	{
		public AddJournalWindow()
		{
			InitializeComponent();
			this.NewJournal = new Journal();
			DataContext = NewJournal;
		}

		public Journal NewJournal { get; set; }

		private void CreateClick(object sender, RoutedEventArgs e)
		{
			if (NewJournal.Date != String.Empty)
			{
				this.DialogResult = true;
			}
			else MessageBox.Show("Данные о клиенте заполнены неверно");
		}
		private void CancelClick(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
