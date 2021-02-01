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
			Id = ++countIntern;
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

			Id = ++countIntern;
		}

		#endregion  // Constructors

		#region Properties

		/// <summary>
		/// Идентификатор интерна
		/// </summary>
		public override int Id { get; }

		/// <summary>
		/// Зарплата интерна
		/// </summary>
		public int Salary { get; set; }

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


		private static int countIntern = 0;    // количество созданных интернов
	}
}
