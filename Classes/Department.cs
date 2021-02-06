using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Linq;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Департамент
	/// </summary>
	public class Department : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		#endregion // INotifyPropertyChanged

		/// <summary>
		/// Поля для сортировки
		/// </summary>
		public enum FIELDSORT
		{
			ID = 1,         // для сортировки по идентификатору сотрудника
			AGE,            // для сортировки по возрасту
			NAME,           // для сортировки по имени
			LNAME,			// для сортировки по фамилии
			NAME_LNAME,		// для сортировки сначала по имени, потом по фамилии
			LNAME_NAME,		// для сортировки сначала по фамилии, потом по имени
		}

		#region Constructors

		/// <summary>
		/// Конструктор по умолчанию
		/// </summary>
		public Department()
		{
			Id = ++countDep;
			workers = new ObservableCollection<Worker>();
			departs = new ObservableCollection<Department>();
		}

		/// <summary>
		/// Конструктор 1
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		public Department(string name, DepBoss localBoss)
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = localBoss;

			workers = new ObservableCollection<Worker>();
			departs = new ObservableCollection<Department>();

		}

		/// <summary>
		/// Конструктор 2.1
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="workers">Работники департамента</param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Department> departs, ObservableCollection<Worker> workers)
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = localBoss;
			this.workers = workers;
			this.departs = departs;

		}

		/// <summary>
		/// Конструктор 2.2
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="departs">Департаменты в текущем департаменте</param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Department> departs)
			: this(name, localBoss, departs, new ObservableCollection<Worker>()) { }

		/// <summary>
		/// Конструктор 2.3
		/// </summary>
		/// <param name="name"></param>
		/// <param name="workers"></param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Worker> workers)
			: this(name, localBoss, new ObservableCollection<Department>(), workers) { }



		#endregion // Constructors


		#region Properties

		/// <summary>
		/// Идентификатор департамента
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// Название департамента
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Начальник департамента
		/// </summary>
		public DepBoss LocalBoss { get; private set; }

		/// <summary>
		/// Возвращает коллекцию поддепартаментов
		/// </summary>
		public ObservableCollection<Department> Departs
		{
			get
			{
				return departs ?? new ObservableCollection<Department>();
			}
		}

		/// <summary>
		/// Возвращает коллекцию работников в текущем департаменте
		/// </summary>
		public ObservableCollection<Employee> Employees
		{
			get
			{
				ObservableCollection<Employee> emps = new ObservableCollection<Employee>();

				foreach (var emp in workers)
				{
					if (emp is Employee) emps.Add(emp as Employee);
				}

				return emps ?? new ObservableCollection<Employee>();

			}
		}

		/// <summary>
		/// Возвращает коллекцию работников в текущем департаменте
		/// </summary>
		public ObservableCollection<Intern> Interns
		{
			get
			{
				ObservableCollection<Intern> interns = new ObservableCollection<Intern>();

				foreach (var intern in workers)
				{
					if (intern is Intern) interns.Add(intern as Intern);
				}

				return interns ?? new ObservableCollection<Intern>();

			}
		}


		/// <summary>
		/// Количество работников в департаменте
		/// </summary>
		public int CountEmployees
		{
			get
			{
				int count = 0;
				foreach (var worker in workers)
				{
					if (worker is Employee) ++count;    // подсчет рабочих
				}

				return count;
			}
		}

		/// <summary>
		/// Количество интернов в департаменте
		/// </summary>
		public int CountInterns
		{
			get
			{
				int count = 0;
				foreach (var worker in workers)
				{
					if (worker is Intern) ++count;    // подсчет интернов
				}

				return count;
			}
		}


		/// <summary>
		/// Зарплата начальника департамента (15% от суммы зарплат всех подчиненных,
		/// но не менее 160000 р.)
		/// </summary>
		public double LocalBossSalary
		{
			get
			{
				double salary = salaryDepWorkers() * 0.15;
				salary = Math.Round(salary);    // округляем до целых

				return (salary < 160_000) ? 160_000 : salary;
			}
		}

		/// <summary>
		/// Количество поддепартаментов в департаменте
		/// </summary>
		public int CountDeparts
		{
			get
			{
				return Departs.Count;
			}
		}

		#endregion // Properties


		#region Methods

		/// <summary>
		/// Добавление департамента в коллекцию поддепартаментов
		/// </summary>
		/// <param name="dep">Департамент</param>
		public void addDepartment(Department dep)
		{
			departs.Add(dep);
		}

		/// <summary>
		/// Вспомогательный метод, возвращает работника департамента по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор работника</param>
		/// <returns>Работник</returns>
		private Worker returnWorkerDepById(int id)
		{
			return workers.Where(item => item.Id == id).First<Worker>();
		}

		/// <summary>
		/// Удаляет работника из департамента по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор работника</param>
		public void removeWorker(int id)
		{
			workers.Remove(returnWorkerDepById(id));
			//workers.Add(new Employee("Шарап", "Сишарпов", new DateTime(1974, 6, 17), "Главный программист", 1_000));

			refreshView();
			refreshLocalBossSalary();
		}

		public void refreshView()
		{
			OnPropertyChanged("Employees");
			OnPropertyChanged("CountEmployees");
		}

		private void refreshLocalBossSalary()
		{
			OnPropertyChanged("LocalBossSalary");
		}


		/// <summary>
		/// Рекурсивный метод, расчет суммы зарплат всех работников департамента (и его поддепартаментов)
		/// </summary>
		/// <returns>Сумма зарплат</returns>
		public double salaryDepWorkers()
		{
			int indexDepartment = Departs.Count;    // количество поддепартаментов
			double sum = salarySumWorkers();

			for (int i = 0; i < indexDepartment; ++i)
			{
				// Зарплаты начальников поддепартаментов и остальных работников поддепартамента (рекурсия)
				sum += Departs[i].LocalBossSalary + Departs[i].salaryDepWorkers();
			}

			return sum;
		}

		/// <summary>
		/// Сумма зарплат всех работников текущего департамента
		/// (вспомогательный метод для метода salaryDepWorkers())
		/// </summary>
		/// <returns>Сумма</returns>
		private int salarySumWorkers()
		{
			int salarySum = 0;

			foreach (var worker in workers)
			{
				salarySum += (worker as ISalary)?.Salary ?? 0;
			}

			return salarySum;
		}


		#region Sorting

		/// <summary>
		/// Сортирует работников в организации по различным критериям (6 критериев)
		/// </summary>
		/// <param name="critSort">Критерии сортировки: FIELDSORT.ID - по идентификатору работника (по умолчанию),
		///                                             FIELDSORT.AGE - по возрасту,
		///                                             FIELDSORT.NAME - по имени,
		///                                             FIELDSORT.LNAME - по фамилии,
		///                                             FIELDSORT.NAME_LNAME - сначала по имени, потом по фамилии,
		///                                             FIELDSORT.LNAME_NAME - сначала по фамилии, потом по имени</param>
		/// <returns>Коллекция работников, отсортированных по выбранному критерию</returns>
		private ObservableCollection<Worker> getSortWorkers(FIELDSORT critSort = FIELDSORT.ID)
		{
			switch (critSort)
			{
				case FIELDSORT.ID:
					// Сортируем всех работников по идентификатору
					return new ObservableCollection<Worker>(workers.OrderBy(item => item.Id).ToList());

				case FIELDSORT.AGE:
					// Сортируем всех работников по возрасту
					return new ObservableCollection<Worker>(workers.OrderBy(item => item.Age).ToList());

				case FIELDSORT.NAME:
					// Сортируем всех работников по имени
					return new ObservableCollection<Worker>(workers.OrderBy(item => item.Name).ToList());

				case FIELDSORT.LNAME:
					// Сортируем всех работников по фамилии
					return new ObservableCollection<Worker>(workers.OrderBy(item => item.LastName).ToList());

				case FIELDSORT.NAME_LNAME:
					// Сортируем всех работников по имени и фамилии
					return new ObservableCollection<Worker>(workers.OrderBy(item => item.Name).ThenBy(item => item.LastName).ToList());

				case FIELDSORT.LNAME_NAME:
					// Сортируем всех работников по фамилии и имени
					return new ObservableCollection<Worker>(workers.OrderBy(item => item.LastName).ThenBy(item => item.Name).ToList());
			}

			return new ObservableCollection<Worker>();
		}

		#endregion	// Sorting


		/// <summary>
		/// Информация о департаменте
		/// </summary>
		/// <returns>String: Id, Name, CountEmployees, CountDirectors</returns>
		public override string ToString()
		{
			return $"| Идентификатор отдела: { Id } | " +
					$"Название отдела: { Name } | " +
					$"Количество сотрудников: { CountEmployees } | " +
					$"Количество поддепартаментов: { CountDeparts } |";
		}

		#endregion // Methods


		#region Fields

		private ObservableCollection<Worker> workers;       // работники департамента
		private ObservableCollection<Department> departs;   // "поддепартаменты"

		private static int countDep = 0;                      // счетчик для идентификатора департамента

		#endregion // Fields

	}
}
