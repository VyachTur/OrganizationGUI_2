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

namespace OrganizationGUI_2.DialogWindows
{
	/// <summary>
	/// Interaction logic for DialogTransferDepartment.xaml
	/// </summary>
	public partial class DialogTransferDepartment : Window
	{
		public DialogTransferDepartment(string depName, Dictionary<int, string> dicIdName)
		{
			InitializeComponent();

			tblockDepIn.Text = depName;

			foreach (var pair in dicIdName)
			{
				cboxDepNames.Items.Add(pair.Value);
			}

			

			cboxDepNames.Focus();
		}

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			// Если в текстовом поле есть непробельные символы
			if (cboxDepNames.SelectedItem != null) DialogResult = true;
			else MessageBox.Show("Не выбран департамент в который перемещаем текущий!");
		}
	}
}
