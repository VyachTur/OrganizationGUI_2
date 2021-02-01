using System;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Абстрактный класс работника
	/// </summary>
	public abstract class Worker
	{
		#region Properties

		/// <summary>
		/// Имя работника
		/// </summary>
		public string Name { get; protected set; }

		/// <summary>
		/// Фамилия работника
		/// </summary>
		public string LastName { get; protected set; }

		/// <summary>
		/// Дата рождения работника
		/// </summary>
		public DateTime BirthDate { get; protected set; }

		/// <summary>
		/// Идентификатор работника
		/// </summary>
		public abstract int Id { get; }

		/// <summary>
		/// Возраст работника в годах
		/// </summary>
		public int Age
		{
			get
			{
				int age = DateTime.Now.Year - BirthDate.Year;
				if (BirthDate > DateTime.Now.AddYears(-age)) --age; // для корректного вычисления полных лет
				return age;
			}
		}

		#endregion  // Property

	}
}
