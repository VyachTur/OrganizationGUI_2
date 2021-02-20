using System;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Начальник департамента
	/// </summary>
	public class DepBoss : Worker
	{
		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public DepBoss()
		{
			Id = ++countWorker;
			BirthDate = DateTime.Now;   // для корректного отображения возраста (костыль)
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="name">Имя</param>
		/// <param name="lastName">Фамилия</param>
		/// <param name="birthDate">Дата рождения</param>
		public DepBoss(string name, string lastName, DateTime birthDate)
		{
			Name = name;
			LastName = lastName;
			BirthDate = birthDate;

			Id = ++countWorker;
		}

		#endregion  // Constructors

		#region Properties

		/// <summary>
		/// Имя начальника департамента
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
		/// Фамилия начальника департамента
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
		/// Дата рождения начальника департамента
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
		/// Идентификатор рабочего
		/// </summary>
		public override int Id { get; }

		#endregion  // Properties

		#region Methods

		/// <summary>
		/// Информация о рабочем
		/// </summary>
		/// <returns>String: Id, Name, CountPositions</returns>
		public override string ToString()
		{
			return $"| Идентификатор рабочего: { Id } | " +
					$"Имя рабочего: { Name } | " +
					$"Фамилия рабочего: { LastName } | " +
					$"Дата рождения рабочего: { BirthDate } | " +
					$"Должность сотрудника: начальник департамента | ";
		}

		#endregion  // Methods

		#region Fields

		private string name;                    // имя начальника департамента
		private string lastName;                // фамилия начальника департамента
		private DateTime birthDate;             // дата рождения начальника департамента

		#endregion


	}
}
