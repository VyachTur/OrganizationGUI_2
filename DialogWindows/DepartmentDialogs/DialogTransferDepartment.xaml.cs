using System;
using System.Collections.Generic;
using System.Windows;

namespace OrganizationGUI_2.DialogWindows
{
	/// <summary>
	/// Interaction logic for DialogTransferDepartment.xaml
	/// </summary>
	public partial class DialogTransferDepartment : Window
	{
		public DialogTransferDepartment(int id, string depName, Dictionary<int, string> dicIdName, string nameOrg)
		{
			InitializeComponent();

			tblockDepIn.Text = depName;

			cboxDepNames.Items.Add(nameOrg);

			foreach (var pair in dicIdName)
			{
				// Добавляем в список ComboBox все наименования и id департаментов кроме перемещаемого
				if (pair.Key != id) cboxDepNames.Items.Add($"{pair.Value} (Id: {pair.Key})");
			}

			cboxDepNames.Focus();
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
				// Если выбрана организация, то выходим и присваиваем ToDepID ноль
				if (cboxDepNames.SelectedIndex == 0)
				{
					ToDepID = 0;
					DialogResult = true;
				}


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
