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
			departments = new ObservableCollection<Department>();
			Org = new Organization();
		}

		/// <summary>
		/// Конструктор 1
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="org">Организация включающая департамент</param>
		public Department(string name, Organization org)
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = new DepBoss();
			workers = new ObservableCollection<Worker>();
			departments = new ObservableCollection<Department>();

			Org = org;
		}

		/// <summary>
		/// Конструктор 2
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="localBoss">Начальник департамента</param>
		/// <param name="org">Организация включающая департамент</param>
		public Department(string name, DepBoss localBoss, Organization org)
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = localBoss;

			workers = new ObservableCollection<Worker>();
			departments = new ObservableCollection<Department>();
			Org = org;
		}

		/// <summary>
		/// Конструктор 3.1
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="localBoss">Начальник департамента</param>
		/// <param name="departments">Коллекция поддепартаментов</param>
		/// <param name="workers">Коллекция работников департамента</param>
		/// <param name="org">Организация включающая департамент</param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Department> departments, ObservableCollection<Worker> workers, Organization org)
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = localBoss;
			this.workers = workers;
			this.departments = departments;
			Org = org;
		}

		/// <summary>
		/// Конструктор 3.2
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="localBoss">Начальник департамента</param>
		/// <param name="workers">Коллекция работников департамента</param>
		/// <param name="org">Организация включающая департамент</param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Worker> workers, Organization org)
			: this(name, localBoss, new ObservableCollection<Department>(), workers, org) { }


		/// <summary>
		/// Конструктор 4
		/// </summary>
		/// <param name="name">Наименование департамента</param>
		/// <param name="departments">Департаменты в текущем департаменте</param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Department> departments) 
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = localBoss;
			this.workers = new ObservableCollection<Worker>();
			this.departments = departments;
			Org = new Organization(this);
		}

		/// <summary>
		/// Конструктор 5
		/// </summary>
		/// <param name="name"></param>
		/// <param name="workers"></param>
		public Department(string name, DepBoss localBoss, ObservableCollection<Worker> workers)
		{
			Id = ++countDep;

			Name = name;
			LocalBoss = localBoss;
			this.workers = workers;
			this.departments = new ObservableCollection<Department>();
			Org = new Organization(this);
		}

		#endregion // Constructors


		#region Properties

		/// <summary>
		/// Идентификатор департамента
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// Название департамента
		/// </summary>
		public string Name 
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
		/// Начальник департамента
		/// </summary>
		public DepBoss LocalBoss { get; set; }

		/// <summary>
		/// Организация в которую входит департамент
		/// </summary>
		public Organization Org { get; private set; }


		/// <summary>
		/// Возвращает коллекцию поддепартаментов
		/// </summary>
		public ObservableCollection<Department> Departments
		{
			get
			{
				return departments ?? new ObservableCollection<Department>();
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
				if (LocalBoss.Name != null) 
				{ 
					double salary = salaryDepWorkers() * 0.15;
					salary = Math.Round(salary);    // округляем до целых

					return (salary < 160_000) ? 160_000 : salary;
				}
				else
				{
					return 0;
				}

			}
		}

		/// <summary>
		/// Количество поддепартаментов в департаменте
		/// </summary>
		public int CountDepartments
		{
			get
			{
				return Departments.Count;
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
			departments.Add(dep);

			refreshView();
			refreshLocalBossSalary();
			refreshBigBossSalary();
		}

		/// <summary>
		/// Добавление работника в коллекцию работников
		/// </summary>
		/// <param name="worker">Работник</param>
		public void addWorker(Worker worker)
		{
			workers.Add(worker);

			refreshView();
			refreshLocalBossSalary();
			refreshBigBossSalary();
		}


		/// <summary>
		/// Удаление департамента
		/// </summary>
		/// <param name="dep">Департамент</param>
		//public void removeDepartment(Department dep)
		//{
		//	// Если департамент есть в коллекции, то удаляем его
		//	if (departments.Contains(dep)) departments.Remove(dep);
		//}

		/// <summary>
		/// Удаление департамента (перегруженный метод)
		/// </summary>
		/// <param name="id">Идентификатор департамента</param>
		//public void removeDepartment(int id)
		//{
		//	// Если департамент с нужным id есть в коллекции, то удаляем его
		//	if (departments.Contains(departments.ToList().Find(item => item.Id == id)))
		//						departments.Remove(departments.ToList().Find(item => item.Id == id));
		//}

		

		/// <summary>
		/// Удаляет работника из департамента по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор работника</param>
		public void removeWorker(int id)
		{
			workers.Remove(returnWorkerDepById(id));

			refreshView();
			refreshLocalBossSalary();
			refreshBigBossSalary();
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
		/// Обновление интерфейса для свойств Employees и CountEmployees
		/// Interns и CountInterns
		/// </summary>
		public void refreshView()
		{
			OnPropertyChanged("Interns");
			OnPropertyChanged("CountInterns");
			OnPropertyChanged("Employees");
			OnPropertyChanged("CountEmployees");
		}

		/// <summary>
		/// Обновление интерфейса для свойства LocalBossSalary
		/// </summary>
		private void refreshLocalBossSalary()
		{
			OnPropertyChanged("LocalBossSalary");
		}

		/// <summary>
		/// Обновление интерфейса для свойств в организации DirSalary и AssociateDirSalary
		/// </summary>
		private void refreshBigBossSalary()
		{
			Org.OnPropertyChanged("DirSalary");
			Org.OnPropertyChanged("AssociateDirSalary");
		}


		/// <summary>
		/// Рекурсивный метод, расчет суммы зарплат всех работников департамента (и его поддепартаментов)
		/// </summary>
		/// <returns>Сумма зарплат</returns>
		public double salaryDepWorkers()
		{
			int indexDepartment = CountDepartments;    // количество поддепартаментов
			double sum = salarySumWorkers();

			for (int i = 0; i < indexDepartment; ++i)
			{
				// Зарплаты начальников поддепартаментов и остальных работников поддепартамента (рекурсия)
				sum += Departments[i].LocalBossSalary + Departments[i].salaryDepWorkers();
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
		public void sortedWorkers(FIELDSORT critSort = FIELDSORT.ID)
		{
			switch (critSort)
			{
				case FIELDSORT.ID:
					// Сортируем всех работников по идентификатору
					workers = new ObservableCollection<Worker>(workers.OrderBy(item => item.Id).ToList());
					break;

				case FIELDSORT.AGE:
					// Сортируем всех работников по возрасту
					workers = new ObservableCollection<Worker>(workers.OrderBy(item => item.Age).ToList());
					break;

				case FIELDSORT.NAME:
					// Сортируем всех работников по имени
					workers = new ObservableCollection<Worker>(workers.OrderBy(item => item.Name).ToList());
					break;

				case FIELDSORT.LNAME:
					// Сортируем всех работников по фамилии
					workers = new ObservableCollection<Worker>(workers.OrderBy(item => item.LastName).ToList());
					break;

				case FIELDSORT.NAME_LNAME:
					// Сортируем всех работников по имени и фамилии
					workers = new ObservableCollection<Worker>(workers.OrderBy(item => item.Name).ThenBy(item => item.LastName).ToList());
					break;

				case FIELDSORT.LNAME_NAME:
					// Сортируем всех работников по фамилии и имени
					workers = new ObservableCollection<Worker>(workers.OrderBy(item => item.LastName).ThenBy(item => item.Name).ToList());
					break;
			}

			refreshView();	// обновляем интерфейс после сортировки
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
					$"Количество поддепартаментов: { CountDepartments } |";
		}

		#endregion // Methods


		#region Fields

		private string name;									// наименование департамента
		private ObservableCollection<Worker> workers;			// работники департамента
		private ObservableCollection<Department> departments;   // "поддепартаменты"

		private static int countDep = 0;						// счетчик для идентификатора департамента

		#endregion // Fields

	}
}
