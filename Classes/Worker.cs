using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
		public abstract string Name { get; set; }

		/// <summary>
		/// Фамилия работника
		/// </summary>
		public abstract string LastName { get; set; }

		/// <summary>
		/// Дата рождения работника
		/// </summary>
		public abstract DateTime BirthDate { get; set; }

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


		#region Fields

		protected static int countWorker = 0;	// количество созданных работников (нач. деп., сотрудники, интерны)

		#endregion


		

	}
}
