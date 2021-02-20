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
		/// Имя заместителя директора
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
		/// Фамилия заместителя директора
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
		/// Дата рождения заместителя директора
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


		#region Fields

		private string name;                    // имя заместителя директора
		private string lastName;                // фамилия заместителя директора
		private DateTime birthDate;             // дата рождения заместителя директора

		private static AssociateDirector instance;   // поле (инстанс) для синглтона

		#endregion
	}
}
