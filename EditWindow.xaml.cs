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

namespace OrganizationGUI_2
{
	/// <summary>
	/// Interaction logic for EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		public EditWindow()
		{
			InitializeComponent();
		}


		private void EditWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}

		private void EditWindow_Closed(object sender, EventArgs e)
		{
			this.Owner.Show();	// показываем главное окно
		}

	}
}
