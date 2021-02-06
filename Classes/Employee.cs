using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Рабочий (сотрудник)
	/// </summary>
	public class Employee : Worker, IPost, ISalary, INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		#endregion // INotifyPropertyChanged

		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public Employee() { Id = ++countWorker; }

		/// <summary>
		/// Конструктор 1
		/// </summary>
		/// <param name="name">Имя сотрудника</param>
		/// <param name="lastName">Фамилия сотрудника</param>
		/// <param name="birthDate">Дата рождения</param>
		/// <param name="namePost">Наименование должности</param>
		/// <param name="salary">Зарплата в час</param>
		public Employee(string name, string lastName, DateTime birthDate, string namePost, int salary)
		{
			Name = name;
			LastName = lastName;
			BirthDate = birthDate;
			NamePost = namePost;
			Salary = salary * 168;  // умножаем на 168 рабочих часов в месяце

			Id = ++countWorker;
		}


		#endregion  // Constructors


		#region Properties

		/// <summary>
		/// Имя сотрудника
		/// </summary>
		public override string Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
				OnPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Фамилия сотрудника
		/// </summary>
		public override string LastName
		{
			get
			{
				return lastName;
			}

			set
			{
				lastName = value;
				OnPropertyChanged("LastName");
			}
		}

		/// <summary>
		/// Дата рождения сотрудника
		/// </summary>
		public override DateTime BirthDate
		{
			get
			{
				return birthDate;
			}

			set
			{
				birthDate = value;
				OnPropertyChanged("BirthDate");
			}
		}

		/// <summary>
		/// Идентификатор сотрудника
		/// </summary>
		public override int Id { get; }

		/// <summary>
		/// Наименование должности
		/// </summary>
		public string NamePost
		{
			get 
			{ 
				return namePost;
			}

			set
			{ 
				namePost = value;
				OnPropertyChanged("NamePost"); 
			} 
		}

		/// <summary>
		/// Зарплата сотрудника
		/// </summary>
		public int Salary 
		{ 
			get
			{
				return salary;
			}

			set
			{
				salary = value;
				OnPropertyChanged("Salary");
			} 
		}

		#endregion  // Properties

		#region Methods

		/// <summary>
		/// Информация о рабочем
		/// </summary>
		/// <returns>String: Id, Name, LastName, BirthDate, NamePost, Salary</returns>
		public override string ToString()
		{
			return $"| Идентификатор рабочего: { Id } | " +
					$"Имя рабочего: { Name } | " +
					$"Фамилия рабочего: { LastName } | " +
					$"Дата рождения рабочего: { BirthDate } | " +
					$"Должность рабочего: { NamePost } | " +
					$"Зарплата рабочего: { Salary } |";
		}

		#endregion  // Methods


		#region Fields

		private string name;                    // имя сотрудника
		private string lastName;                // фамилия сотрудника
		private DateTime birthDate;             // дата рождения сотрудника

		private string namePost;	// наименование должности сотрудника
		private int salary;         // зарплата сотрудника

		#endregion   // Fields


	}
}
