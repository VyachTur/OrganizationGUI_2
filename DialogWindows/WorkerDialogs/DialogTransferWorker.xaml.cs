using OrganizationGUI.Classes;
using System;
using System.Windows;

namespace OrganizationGUI_2.DialogWindows.WorkerDialogs
{
	/// <summary>
	/// Interaction logic for DialogTransferWorker.xaml
	/// </summary>
	public partial class DialogTransferWorker : Window
	{
		public DialogTransferWorker(Organization organization)
		{
			InitializeComponent();

			foreach (Department department in organization.AllDepartments)
			{
				cboxDepNames.Items.Add(new String($"{department.Name} (Id: {department.Id})"));
			}
		}

		/// <summary>
		/// Идентификатор департамента в который необходимо переместить
		/// </summary>
		public int ToDepID { get; private set; }

		/// <summary>
		/// Обработчик нажатия кнопки подтверждения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			// Если выбран элемент
			if (cboxDepNames.SelectedItem != null)
			{
				string selectedString = cboxDepNames.SelectedItem.ToString();   // выбранный item
				int posId = selectedString.IndexOf("Id:") + 4;                  // позиция id
				int lenId = selectedString.Length - posId - 1;                  // длина id

				string selectedId = selectedString.Substring(posId, lenId);     // "вырезаем" id

				if (int.TryParse(selectedId, out int id))
				{
					ToDepID = id;
					DialogResult = true;
				}

			}
			else
			{
				MessageBox.Show("Не выбран департамент в который перемещаем текущий!");
			}
		}
	}
}
