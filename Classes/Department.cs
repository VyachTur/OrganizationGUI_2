using System;
using System.Collections.ObjectModel;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Департамент
	/// </summary>
	public class Department
	{
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
		public string Name { get; set; }

		/// <summary>
		/// Начальник департамента
		/// </summary>
		public DepBoss LocalBoss { get; set; }

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
		public void addDepartament(Department dep)
		{
			departs.Add(dep);
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
