using OrganizationGUI.Classes;
using System;
using System.Windows;

namespace OrganizationGUI_2.DialogWindows.WorkerDialogs
{
	/// <summary>
	/// Interaction logic for DialogEditWorker.xaml
	/// </summary>
	public partial class DialogEditWorker : Window
	{
		/// <summary>
		/// Редактируемый работник
		/// </summary>
		public Worker EditWorker { get; set; }

		/// <summary>
		/// Конструктор диалогового окна
		/// </summary>
		/// <param name="worker"></param>
		public DialogEditWorker(Worker worker)
		{
			InitializeComponent();
			// Размер окна по контенту
			this.SizeToContent = SizeToContent.WidthAndHeight;
			tboxWorkerName.Focus();

			EditWorker = worker;

			tboxWorkerName.Text = EditWorker.Name;
			tboxWorkerSirname.Text = EditWorker.LastName;
			tboxWorkerBirthDate.Text = EditWorker.BirthDate.ToString("dd.MM.yyyy");


			if (EditWorker is Employee)
			{
				tblockWorkerSalry.Text = "Зарплата (рубли, в час): ";
				gridPostWorker.Visibility = Visibility.Visible;
				tboxWorkerPost.Text = (EditWorker as Employee).NamePost;
				tboxWorkerSalary.Text = ((EditWorker as ISalary).Salary / 168).ToString();
			}

			if (EditWorker is Intern)
			{
				tblockWorkerSalry.Text = "Зарплата (рубли, в месяц): ";
				gridPostWorker.Visibility = Visibility.Collapsed;
				tboxWorkerSalary.Text = (EditWorker as ISalary).Salary.ToString();
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
					EditWorker.Name = tboxWorkerName.Text;
					EditWorker.LastName = tboxWorkerSirname.Text;
					EditWorker.BirthDate = DateTime.Parse(tboxWorkerBirthDate.Text);

					if (int.TryParse(tboxWorkerSalary.Text, out int salary)
						&& EditWorker.BirthDate < DateTime.Now
						&& salary > 0)
					{
						(EditWorker as ISalary).Salary = salary;

						if (EditWorker is Employee)
						{
							if (tboxWorkerPost.Text.Trim() != String.Empty)
							{
								(EditWorker as Employee).NamePost = tboxWorkerPost.Text;

								DialogResult = true;
							}
						}

						if (EditWorker is Intern)
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
