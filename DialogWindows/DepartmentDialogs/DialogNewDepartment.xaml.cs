using System;
using System.Windows;

namespace OrganizationGUI_2
{
	/// <summary>
	/// Interaction logic for DialogNewDepartment.xaml
	/// </summary>
	public partial class DialogNewDepartment : Window
	{
		public DialogNewDepartment()
		{
			InitializeComponent();

			tboxDepName.Focus();
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
