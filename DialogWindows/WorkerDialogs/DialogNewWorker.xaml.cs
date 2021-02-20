using OrganizationGUI.Classes;
using System;
using System.Windows;

namespace OrganizationGUI_2.DialogWindows.WorkerDialogs
{
	/// <summary>
	/// Interaction logic for DialogNewWorker.xaml
	/// </summary>
	public partial class DialogNewWorker : Window
	{
		/// <summary>
		/// Создаваемый работник
		/// </summary>
		public Worker NewWorker { get; set; }

		public DialogNewWorker(Worker worker)
		{
			InitializeComponent();

			// Размер окна по контенту
			this.SizeToContent = SizeToContent.WidthAndHeight;
			tboxWorkerName.Focus();

			NewWorker = worker;

			if (NewWorker is Employee)
			{
				tblockWorkerSalry.Text = "Зарплата (рубли, в час): ";
				gridPostWorker.Visibility = Visibility.Visible;
			}

			if (NewWorker is Intern)
			{
				tblockWorkerSalry.Text = "Зарплата (рубли, в месяц): ";
				gridPostWorker.Visibility = Visibility.Collapsed;
			}
		}

		/// <summary>
		/// Обработчик нажатия кнопки подтверждения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			// Если в текстовом поле есть непробельные символы
			if (tboxWorkerName.Text.Trim() != String.Empty
				&& tboxWorkerSirname.Text.Trim() != String.Empty
				&& tboxWorkerBirthDate.Text.Trim() != String.Empty
				&& tboxWorkerSalary.Text.Trim() != String.Empty)
			{
				if (DateTime.TryParse(tboxWorkerBirthDate.Text, out DateTime birth))
				{
					NewWorker.Name = tboxWorkerName.Text;
					NewWorker.LastName = tboxWorkerSirname.Text;
					NewWorker.BirthDate = DateTime.Parse(tboxWorkerBirthDate.Text);

					if (int.TryParse(tboxWorkerSalary.Text, out int salary)
						&& NewWorker.BirthDate < DateTime.Now
						&& salary > 0)
					{
						(NewWorker as ISalary).Salary = salary;

						if (NewWorker is Employee)
						{
							if (tboxWorkerPost.Text.Trim() != String.Empty)
							{
								(NewWorker as Employee).NamePost = tboxWorkerPost.Text;

								DialogResult = true;
							}
						}

						if (NewWorker is Intern)
						{
							DialogResult = true;
						}
					}
				}

			}

			// Если ответ некорректен
			if (!DialogResult ?? true) MessageBox.Show("Введите корректные данные!");
		}
	}
}
