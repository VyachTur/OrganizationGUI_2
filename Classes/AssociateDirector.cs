using System;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Заместитель директора организации (синглтон, т.к. зам один в организации)
	/// </summary>
	public class AssociateDirector : Worker
	{
		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		protected AssociateDirector() { }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="name">Имя</param>
		/// <param name="lastName">Фамилия</param>
		/// <param name="birthDate">Дата рождения</param>
		/// <param name="salary">Зарплата</param>
		protected AssociateDirector(string name, string lastName, DateTime birthDate)
		{
			Name = name;
			LastName = lastName;
			BirthDate = birthDate;

			Id = 1;
		}

		#endregion  // Constructors

		#region Properties

		/// <summary>
		/// Идентификатор заместителя директора
		/// </summary>
		public override int Id { get; }

		#endregion  // Properties

		#region Methods

		/// <summary>
		/// Возвращает инстанс зам. директора
		/// </summary>
		/// <param name="name"></param>
		/// <param name="lastName"></param>
		/// <param name="salary"></param>
		/// <returns></returns>
		public static AssociateDirector getInstance(string name, string lastName, DateTime birthDate)
		{
			if (instance == null)
				instance = new AssociateDirector(name, lastName, birthDate);
			return instance;
		}

		/// <summary>
		/// Информация о заместителе директора
		/// </summary>
		/// <returns>String: Id, Name, LastName, BirthDate, Salary</returns>
		public override string ToString()
		{
			return $"| Идентификатор заместителя директора: { Id } | " +
					$"Имя заместителя директора: { Name } | " +
					$"Фамилия заместителя директора: { LastName } | " +
					$"Дата рождения заместителя директора: { BirthDate } | ";
		}

		#endregion  // Methods

		private static AssociateDirector instance;   // поле (инстанс) для синглтона

	}
}
