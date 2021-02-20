using System;
using System.Windows;

namespace OrganizationGUI_2.DialogWindows
{
	/// <summary>
	/// Interaction logic for DialogEditDepartment.xaml
	/// </summary>
	public partial class DialogEditDepartment : Window
	{
		public DialogEditDepartment(string depName)
		{
			InitializeComponent();

			tboxDepName.Text = depName;
			tboxDepName.Focus();
			tboxDepName.SelectAll();
		}

		/// <summary>
		/// Обработчик нажатия кнопки подтверждения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			// Если в текстовом поле есть непробельные символы
			if (tboxDepName.Text.Trim() != String.Empty) DialogResult = true;
		}
	}

}
