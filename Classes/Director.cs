using System;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Класс директора (синглтон, т.к. директор в организации один)
	/// </summary>
	public class Director : Worker
	{
		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		protected Director() { }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="name">Имя</param>
		/// <param name="lastName">Фамилия</param>
		/// <param name="birthDate">Дата рождения</param>
		/// <param name="salary">Зарплата</param>
		protected Director(string name, string lastName, DateTime birthDate)
		{
			Name = name;
			LastName = lastName;
			BirthDate = birthDate;

			Id = 1;
		}

		#endregion  // Constructors

		#region Properties

		/// <summary>
		/// Идентификатор директора
		/// </summary>
		public override int Id { get; }

		#endregion  // Properties

		#region Methods

		public static Director getInstance(string name, string lastName, DateTime birthDate)
		{
			if (instance == null)
				instance = new Director(name, lastName, birthDate);
			return instance;
		}

		/// <summary>
		/// Информация о директоре
		/// </summary>
		/// <returns>String: Id, Name, LastName, BirthDate, Salary</returns>
		public override string ToString()
		{
			return $"| Идентификатор директора: { Id } | " +
					$"Имя директора: { Name } | " +
					$"Фамилия директора: { LastName } | " +
					$"Дата рождения директора: { BirthDate } | ";
		}

		#endregion  // Methods

		private static Director instance;   // поле (инстанс) для синглтона

	}
}
