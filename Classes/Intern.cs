using System;


namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Интерн
	/// </summary>
	public class Intern : Worker, ISalary
	{
		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public Intern()
		{
			Id = ++countWorker;
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="name">Имя</param>
		/// <param name="lastName">Фамилия</param>
		/// <param name="birthDate">Дата рождения</param>
		/// <param name="salary">Зарплата в месяц</param>
		public Intern(string name, string lastName, DateTime birthDate, int salary)
		{
			Name = name;
			LastName = lastName;
			BirthDate = birthDate;
			Salary = salary;

			Id = ++countWorker;
		}

		#endregion  // Constructors

		#region Properties

		/// <summary>
		/// Имя интерна
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
		/// Фамилия интерна
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
		/// Дата рождения интерна
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
		/// Идентификатор интерна
		/// </summary>
		public override int Id { get; }

		/// <summary>
		/// Зарплата интерна
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
		/// Информация об интерне
		/// </summary>
		/// <returns>String: Id, Name, LastName, BirthDate, NamePost, Salary</returns>
		public override string ToString()
		{
			return $"| Идентификатор интерна: { Id } | " +
					$"Имя интерна: { Name } | " +
					$"Фамилия интерна: { LastName } | " +
					$"Дата рождения интерна: { BirthDate } | " +
					$"Зарплата интерна: { Salary } |";
		}

		#endregion  // Methods

		private string name;                    // имя интерна
		private string lastName;                // фамилия интерна
		private DateTime birthDate;             // дата рождения интерна

		private int salary;    // зарплата интерна
	}
}
