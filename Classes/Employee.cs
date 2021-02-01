using System;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Рабочий (сотрудник)
	/// </summary>
	public class Employee : Worker, IPost, ISalary
	{
		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public Employee() { Id = ++countEmp; }

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
			Id = ++countEmp;
		}


		#endregion  // Constructors


		#region Properties

		/// <summary>
		/// Идентификатор рабочего
		/// </summary>
		public override int Id { get; }

		/// <summary>
		/// Наименование должности
		/// </summary>
		public string NamePost { get; set; }

		/// <summary>
		/// Зарплата сотрудника
		/// </summary>
		public int Salary { get; set; }

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


		private static int countEmp = 0;    // количество созданных рабочих
	}
}
