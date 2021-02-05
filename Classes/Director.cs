using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Класс директора (синглтон, т.к. директор в организации один)
	/// </summary>
	public class Director : Worker, INotifyPropertyChanged
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
		/// Имя директора
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
		/// Фамилия директора
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
		/// Дата рождения директора
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


		#region Fields

		private string name;                    // имя директора
		private string lastName;                // фамилия директора
		private DateTime birthDate;             // дата рождения директора

		private static Director instance;   // поле (инстанс) для синглтона

		#endregion

		

	}
}
