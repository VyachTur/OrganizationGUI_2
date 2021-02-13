using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

using System.Collections.Generic;
using System.Diagnostics;

namespace OrganizationGUI.Classes
{
	/// <summary>
	/// Организация
	/// </summary>
	public class Organization : INotifyPropertyChanged
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
		public Organization()
		{
			departments = new ObservableCollection<Department>();
		}

		/// <summary>
		/// Конструктор 1
		/// </summary>
		/// <param name="dep">Департамент</param>
		public Organization(Department dep)
		{
			departments = new ObservableCollection<Department>();
			departments.Add(dep);
		}

		/// <summary>
		/// Конструктор 2.1
		/// </summary>
		/// <param name="name">Наименование организации</param>
		/// <param name="director">Директор организации</param>
		/// <param name="associateDirector">Зам. директора</param>
		/// <param name="departments">Департаменты в организации</param>
		public Organization(string name, Director director, AssociateDirector associateDirector, ObservableCollection<Department> departments)
		{
			this.Name = name;
			this.Dir = director;
			this.AssociateDir = associateDirector;
			this.departments = departments;
		}

		/// <summary>
		/// Конструктор 2.2
		/// </summary>
		/// <param name="name">Наименование организации</param>
		/// <param name="director">Директор организации</param>
		/// <param name="associateDirector">Зам. директора</param>
		public Organization(string name, Director director, AssociateDirector associateDirector)
			: this(name, director, associateDirector, new ObservableCollection<Department>()) {  }

		#endregion


		#region Properties

		/// <summary>
		/// Название организации
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Директор
		/// </summary>
		public Director Dir { get; set; }

		/// <summary>
		/// Заместитель директора
		/// </summary>
		public AssociateDirector AssociateDir { get; set; }

		/// <summary>
		/// Возвращает коллекцию департаментов верхнего уровня в организации
		/// </summary>
		public ObservableCollection<Department> Departments
		{
			get
			{
				return departments ?? new ObservableCollection<Department>();
			}
		}

		/// <summary>
		/// Все департаменты и поддепартаменты
		/// </summary>
		public List<Department> AllDepartments
		{
			get
			{
				// Заполняем коллекцию всех департаментов
				allDepartments = new List<Department>();
				makeAllDepartments(Departments.ToList());

				return allDepartments;
			}
		}

		/// <summary>
		/// Количество департаментов верхнего уровня
		/// </summary>
		public int CountDepartments
		{
			get
			{
				return departments?.Count ?? 0; // если департаментов в организации нет (null), то возвр. 0
			}
		}


		/// <summary>
		/// Зарплата начальника организации (15% от суммы зарплат всех подчиненных,
		/// но не менее 2000000 р.)
		/// </summary>
		public double DirSalary
		{
			get
			{
				// Зарплата начальника = (Зарплата всех сотрудников + Зарплата зам. начальника) * 0,15,
				double sum = (sumSalaryAllWorkers() + AssociateDirSalary) * 0.15;
				sum = Math.Round(sum); // округляем до целых

				return (sum < 2_000_000) ? 2_000_000 : sum;
			}

		}


		/// <summary>
		/// Зарплата зам. начальника организации (15% от суммы зарплат всех подчиненных, 
		/// но не менее 1000000 р.)
		/// </summary>
		public double AssociateDirSalary
		{
			get
			{
				double sum = sumSalaryAllWorkers() * 0.15;
				sum = Math.Round(sum); // округляем до целых

				return (sum < 1_000_000) ? 1_000_000 : sum;
			}
		}


		/// <summary>
		/// Возвращает словарь (Идентификатор департамента, Наименование департамента)
		/// </summary>
		//public Dictionary<int, string> DicIdNameDepartment
		//{
		//	get
		//	{
		//		// Заполняем словарь идентификаторов и имен департаментов
		//		dicDeps = new Dictionary<int, string>();
		//		setDictionaryAllDepartments(Departments.ToList());

		//		return dicDeps;
		//	}
		//}


		#endregion  // Properties




		#region Methods

		/// <summary>
		/// Сумма зарплат всех работников (включая начальников департаментов)
		/// </summary>
		/// <returns>Сумма зарплат</returns>
		private double sumSalaryAllWorkers()
		{
			double sum = 0;

			foreach (var dep in Departments)
			{
				sum += dep.salaryDepWorkers() + dep.LocalBossSalary;
			}

			return sum;
		}


		/// <summary>
		/// Добавление департамента в коллекцию департаментов
		/// </summary>
		/// <param name="dep">Департамент</param>
		public void addDepartment(Department dep)
		{
			departments.Add(dep);
		}

		/// <summary>
		/// Возвращает департамент по его id
		/// </summary>
		/// <param name="id">Идентификатор департамента</param>
		/// <returns>Департамент</returns>
		public Department getDepartmentFromId(int id)
		{
			foreach (Department dep in AllDepartments)
			{
				if (dep.Id == id) return dep;
			}

			return new Department();
		}



		/// <summary>
		/// Вспомогательный метод. Создаем коллекцию всех департаментов и поддепартаментов
		/// </summary>
		/// <param name="deps">Коллекция департаментов</param>
		private static void makeAllDepartments(List<Department> deps)
		{
			for (int i = 0; i < deps.Count; ++i)
			{
				allDepartments.Add(deps[i]);
				makeAllDepartments(deps[i].Departments.ToList());
			}
		}


		/// <summary>
		/// Удаление департамента
		/// </summary>
		/// <param name="dep">Департамент</param>
		public void removeDepartment(Department dep)
		{
			Organization.returnIncludeDepCollection(Departments, dep).Remove(dep);

			// Обновляем интерфейс (зарплаты главных начальников)
			OnPropertyChanged("DirSalary");
			OnPropertyChanged("AssociateDirSalary");
		}

		/// <summary>
		/// Вспомогательный метод. Возвращает коллекцию в которой находится департамент
		/// </summary>
		/// <param name="deps">Коллекция в которой ищем департамент</param>
		/// <param name="dep">Искомый департамент</param>
		/// <returns></returns>
		private static ObservableCollection<Department> returnIncludeDepCollection(ObservableCollection<Department> deps, Department dep)
		{
			// Вспомогательная коллекция
			ObservableCollection<Department> departs = new ObservableCollection<Department>();

			// Если департамент есть в коллекции, то возвращаем эту коллекцию
			if (deps.Contains(dep))
			{
				return deps;
			}

			for (int i = 0; i < deps.Count; ++i)
			{
				// Если departs не содержит элементов, то "ищем" дальше
				if (departs.Count == 0) departs = returnIncludeDepCollection(deps[i].Departments, dep);
			}

			return departs;
		}


		/// <summary>
		/// Вспомогательный метод. Заполняет словарь идентификаторов и наименований департаментов организации
		/// </summary>
		/// <param name="deps">Коллекция департаментов</param>
		//private static void setDictionaryAllDepartments(List<Department> deps)
		//{
		//	for (int i = 0; i < deps.Count; ++i)
		//	{
		//		dicDeps.Add(deps[i].Id, deps[i].Name);

		//		setDictionaryAllDepartments(deps[i].Departments.ToList());
		//	}

		//}


		/// <summary>
		/// Информация об организации
		/// </summary>
		/// <returns>String: Name, CountEmployees, CountDirectors</returns>
		public override string ToString()
		{
			return $"Название организации: { Name } | " +
					$"Имя директора: {Dir?.Name ?? String.Empty} {Dir?.LastName ?? String.Empty} | " +
					$"Имя зама: {AssociateDir?.Name ?? String.Empty} {AssociateDir?.LastName ?? String.Empty} | " +
					$"Количество департаментов верхнего уровня: {CountDepartments} | ";
		}



		#region XML Serialization

		/// <summary>
		/// Сериализует организацию (xml)
		/// </summary>
		/// /// <param name="path">Путь к файлу экспорта (xml)</param>
		public void xmlOrganizationSerializer(string path)
		{
			XElement xeORGANIZATION = new XElement("ORGANIZATION");
			XAttribute xaNAME_ORG = new XAttribute("orgname", this.Name);

			XAttribute xaNAME_DIR = new XAttribute("dirname", this.Dir.Name);
			XAttribute xaLASTNAME_DIR = new XAttribute("dirlastname", this.Dir.LastName);
			XAttribute xaBIRTHDATE_DIR = new XAttribute("dirbirth", this.Dir.BirthDate);

			XAttribute xaNAME_ASSDIR = new XAttribute("assdirname", this.AssociateDir.Name);
			XAttribute xaLASTNAME_ASSDIR = new XAttribute("assdirlastname", this.AssociateDir.LastName);
			XAttribute xaBIRTHDATE_ASSDIR = new XAttribute("assdirbirth", this.AssociateDir.BirthDate);

			// ДЕПАРТАМЕНТЫ ОРГАНИЗАЦИИ
			XElement xeDEPARTMENTS = new XElement("DEPARTMENTS");

			foreach (Department dep in departments)
			{
				XElement xeDEPARTMENT = serializerSubDeps(dep);

				xeDEPARTMENTS.Add(xeDEPARTMENT);
			}

			xeORGANIZATION.Add(xeDEPARTMENTS, xaNAME_ORG, xaNAME_DIR, xaLASTNAME_DIR, xaBIRTHDATE_DIR, xaNAME_ASSDIR, xaLASTNAME_ASSDIR, xaBIRTHDATE_ASSDIR);

			xeORGANIZATION.Save(path);
		}


		/// <summary>
		/// Вспомогательный рекурсивный метод для сереализации департамента и его поддепартаментов
		/// </summary>
		/// <param name="dep">Департамент</param>
		/// <param name="org">Организация</param>
		/// <returns>XML-узел</returns>
		private static XElement serializerSubDeps(Department dep)
		{
			XElement xeDEPARTMENT = new XElement("DEPARTMENT");

			XAttribute xaNAME_DEP = new XAttribute("depname", dep.Name);
			XAttribute xaNAME_DEPBOSS = new XAttribute("depbossname", dep.LocalBoss.Name);
			XAttribute xaLASTNAME_DEPBOSS = new XAttribute("depbosslastname", dep.LocalBoss.LastName);
			XAttribute xaBIRTHDATE_DEPBOSS = new XAttribute("depbossbirth", dep.LocalBoss.BirthDate);

			// СОТРУДНИКИ ОРГАНИЗАЦИИ
			XElement xeEMPLOYEES = new XElement("EMPLOYEES");

			foreach (Employee emp in dep.Employees)
			{
				XElement xeEMPLOYEE = new XElement("EMPLOYEE");

				XAttribute xaNAME_EMP = new XAttribute("empname", emp.Name);
				XAttribute xaLASTNAME_EMP = new XAttribute("emplastname", emp.LastName);
				XAttribute xaBIRTHDATE_EMP = new XAttribute("empbirth", emp.BirthDate);
				XAttribute xaPOST_EMP = new XAttribute("empnamepost", emp.NamePost);
				XAttribute xaSALARY_EMP = new XAttribute("empsalary", emp.Salary / 168);

				xeEMPLOYEE.Add(xaNAME_EMP, xaLASTNAME_EMP, xaBIRTHDATE_EMP, xaPOST_EMP, xaSALARY_EMP);

				xeEMPLOYEES.Add(xeEMPLOYEE);
			}

			// ИНТЕРНЫ ОРГАНИЗАЦИИ
			XElement xeINTERNS = new XElement("INTERNS");

			foreach (Intern intern in dep.Interns)
			{
				XElement xeINTERN = new XElement("INTERN");

				XAttribute xaNAME_INTERN = new XAttribute("internname", intern.Name);
				XAttribute xaLASTNAME_INTERN = new XAttribute("internlastname", intern.LastName);
				XAttribute xaBIRTHDATE_INTERN = new XAttribute("internbirth", intern.BirthDate);
				XAttribute xaSALARY_INTERN = new XAttribute("internsalary", intern.Salary);

				xeINTERN.Add(xaNAME_INTERN, xaLASTNAME_INTERN, xaBIRTHDATE_INTERN, xaSALARY_INTERN);

				xeINTERNS.Add(xeINTERN);
			}

			xeDEPARTMENT.Add(xaNAME_DEP, xaNAME_DEPBOSS, xaLASTNAME_DEPBOSS, xaBIRTHDATE_DEPBOSS);

			xeDEPARTMENT.Add(xeEMPLOYEES);
			xeDEPARTMENT.Add(xeINTERNS);

			if (dep.CountDepartments > 0)
			{
				// ПОДДЕПАРТАМЕНТЫ ДЕПАРТАМЕНТА
				XElement xeSUBDEPARTMENTS = new XElement("DEPARTMENTS");

				foreach (Department department in dep.Departments)
				{
					XElement xeDEP = serializerSubDeps(department);	// рекурсия

					xeSUBDEPARTMENTS.Add(xeDEP);
				}

				xeDEPARTMENT.Add(xeSUBDEPARTMENTS);

			}

			return xeDEPARTMENT;

		}

		#endregion // XML Serialization



		#region XML Deserialization

		/// <summary>
		/// Десериализует организацию (xml)
		/// </summary>
		/// <param name="path">Путь к файлу импорта (xml)</param>
		/// <returns>Организация</returns>
		public static Organization xmlOrganizationDeserializer(string path)
		{
			string xml = File.ReadAllText(path);

			Director dir = Director
							.getInstance(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("dirname").Value,
											XDocument.Parse(xml).Element("ORGANIZATION").Attribute("dirlastname").Value,
											DateTime.Parse(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("dirbirth").Value));
			dir.Name = XDocument.Parse(xml).Element("ORGANIZATION").Attribute("dirname").Value; // если директор (синглтон) уже был
			dir.LastName = XDocument.Parse(xml).Element("ORGANIZATION").Attribute("dirlastname").Value;
			dir.BirthDate = DateTime.Parse(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("dirbirth").Value);

			AssociateDirector assDir = AssociateDirector
										.getInstance(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("assdirname").Value,
														XDocument.Parse(xml).Element("ORGANIZATION").Attribute("assdirlastname").Value,
														DateTime.Parse(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("assdirbirth").Value));
			assDir.Name = XDocument.Parse(xml).Element("ORGANIZATION").Attribute("assdirname").Value;   // если зам. (синглтон) уже был
			assDir.LastName = XDocument.Parse(xml).Element("ORGANIZATION").Attribute("assdirlastname").Value;
			assDir.BirthDate = DateTime.Parse(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("assdirbirth").Value);

			Organization org = new Organization(XDocument.Parse(xml).Element("ORGANIZATION").Attribute("orgname").Value, dir, assDir);

			var colDepsXml = XDocument.Parse(xml)
								.Element("ORGANIZATION")
								.Element("DEPARTMENTS")
								.Elements("DEPARTMENT")
								.ToList();

			// Цикл по департаментам в организации
			foreach (var itemDepXml in colDepsXml)
			{
				Department dep = deserializerSubDeps(itemDepXml, org);

				org.addDepartment(dep); // добавляем созданный отдел в организацию
			}

			return org;
		}

		/// <summary>
		/// Вспомогательный рекурсивный метод для десереализации департамента и его поддепартаментов
		/// </summary>
		/// <param name="xeDep">XML-узел</param>
		/// <returns>Департамент с поддепартаментами</returns>
		private static Department deserializerSubDeps(XElement xeDep, Organization org)
		{
			DepBoss depBoss = new DepBoss(xeDep.Attribute("depbossname").Value,
											xeDep.Attribute("depbosslastname").Value,
											DateTime.Parse(xeDep.Attribute("depbossbirth").Value));

			ObservableCollection<Worker> workers = new ObservableCollection<Worker>();

			// Перебираем сотрудников и добавляем их в коллекцию workers
			foreach (var itemEmpXml in xeDep.Element("EMPLOYEES").Elements())
			{
				workers.Add(new Employee(itemEmpXml.Attribute("empname").Value,
											itemEmpXml.Attribute("emplastname").Value,
											DateTime.Parse(itemEmpXml.Attribute("empbirth").Value),
											itemEmpXml.Attribute("empnamepost").Value,
											int.Parse(itemEmpXml.Attribute("empsalary").Value)));
			}

			// Перебираем интернов и добавляем их в коллекцию workers
			foreach (var itemEmpXml in xeDep.Element("INTERNS").Elements())
			{
				workers.Add(new Intern(itemEmpXml.Attribute("internname").Value,
											itemEmpXml.Attribute("internlastname").Value,
											DateTime.Parse(itemEmpXml.Attribute("internbirth").Value),
											int.Parse(itemEmpXml.Attribute("internsalary").Value)));
			}

			Department dep = new Department(xeDep.Attribute("depname").Value, depBoss, workers, org);

			if (xeDep.Element("DEPARTMENTS") != null)
			{
				foreach (var itemSubdepXml in xeDep.Element("DEPARTMENTS").Elements("DEPARTMENT").ToList())
				{
					// ПОДДЕПАРТАМЕНТЫ ДЕПАРТАМЕНТА
					dep.addDepartment(deserializerSubDeps(itemSubdepXml, org));	// рекурсия
				}
			}

			return dep;
		}

		#endregion  // XML Deserialization


		#endregion  // Methods



		private ObservableCollection<Department> departments;   // департаменты в организации
		//private static Dictionary<int, string> dicDeps;         // словарь идентификаторов и имен всех департаментов в организации
		private static List<Department> allDepartments;			// все департаменты в организации
	}
}
