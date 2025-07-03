
Data sources means which provides data 

Types of data sources :

--->flat files like .txt,xml etc they provide data (file hanlding) 

--->collection objects also contains data like array ,arraylist etc (programming like for loops and for each loop etc  )

--->tables also contain data (to reqtrive data sql is used,ado.net etc )

The same data u want to access from different data sources provided in easy way 
then u will use linq 

if u are using linq to access objects in memory objects then it is called linqtoobjects

if u are using linq to access sql then it is called as linq to sql 

thrid party softwares access linq to amazon is also there 



The acronym LINQ stands for Language Integrated Query.

Microsoft’s query language is
fully integrated and offers easy data access from in-memory objects, databases, XML
documents, and many more. 

In your syllabus they have given only linq to objects so we will be doing demos only on this 


syntax will be like sql way but select comes last and from comes first and in between where ,order by and other functionalities can be used .


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
	public class Employee
	{
		public int EmployeeID { set; get; }
		public string FirstName { set; get; }
		public string LastName { set; get; }
		public string City { set; get; }
		public int  Sal { set; get; }

	}

	class DepartmentClass
	{
		public int DepartmentId { get; set; }
		public string Name { get; set; }
	}
	class EmployeeClass
	{
		public int EmployeeId { get; set; }
		public string EmployeeName { get; set; }
		public int DepartmentId { get; set; }//fk 
	}

	public class Employeerepository
	{
		public static List<Employee> Retrive()
		{

			List<Employee> emplist = new List<Employee>()
			{

				new Employee{EmployeeID=101,FirstName="sudha",LastName="rani",City="Bangalore",Sal=34000},
				new Employee{EmployeeID=102,FirstName="Madhu",LastName="sudhan",City="Chennai",Sal=30000},
                 new Employee{EmployeeID=103,FirstName="Kiran ",LastName="Kumar",City="Delhi",Sal=35000},

                new Employee{EmployeeID=104,FirstName="sita",LastName="rani",City="Hyderabad",Sal=25000},

               new Employee{EmployeeID=105,FirstName="rakesh",LastName="sharma",City="Bangalore",Sal=19000},

               new Employee{EmployeeID=106,FirstName="Mahesh",LastName="Babu",City="Mysore",Sal=36000},




			};

			return emplist;

		}

	}
	class Program
	{
		public static void  findvowelcout(string input)
		{

			var vowels=input.Where(x=>"aeiou".Contains(x));
			var vowelscount = vowels.Count();
			Console.WriteLine("The string {0} i shaving {1} vowels:", input, vowelscount);
			Console.WriteLine("\n the vowels are :");
			foreach (var item in vowels)
			{
				Console.Write("\t" + item);
			}

		}
		static void Main(string[] args)
		{

			int[] numbers = new int[] { 12, 34, 55, 66, 77, 89, 100, 23, 41, 82, 89, 51, 39, 45 };
			string[] names = new string[] { "Ravi", "Kiran", "Krishna", "Kumar", "Sita", "Mahesh" };

			//give me all the numbers which are greater than 20 in this numbers array 

			// query syntax because this is written in the form of query okay 

			var query1 = from number in numbers where number > 20 select number;
			// method synatx
			var query2 = numbers.Where<int>(x => x > 20);
			Console.WriteLine("\nnumbers greater than 20 :");
			foreach (var item in query1)
			{
				Console.Write("{0,4}", item);

			}
			

			// give me all the numbers which are  odd numbers 

			var queryodd = from number in numbers where number % 2 != 0 select number;

			var queryodd1 = numbers.Where<int>(x => x % 2 != 0);

			StringBuilder sb = new StringBuilder();
			Console.WriteLine("\n list of odd numers :\n");
			foreach (var item in queryodd1)
			{
				sb.Append(item+"\t");

			}

			Console.WriteLine(sb.ToString());

			Console.WriteLine("\n sum of elements in the array finding:");
			var querysum = (from number in numbers select number).Sum(); // query syntax 
			var querysum2 = numbers.Sum();// method syntax 
			Console.WriteLine("{0}\n", querysum2);

			// give me all the names in names string starting with K 

			var startwithK = from name in names where name.StartsWith("K") select name; //query1 syntax 
			var startwithK2 = names.Where<string>(x => x.StartsWith("K"));// method syantx 
			Console.WriteLine("\nList of names startting with K: ");
			foreach (var item in startwithK2)
			{
				Console.WriteLine("{0}", item);
			}
			Console.WriteLine("\n the nubers of characters in each name :");
			var nofocharsineachname = from name in names select name;//query syntax
			var nofocharsineachname2 = names;// method syntax;;
			foreach (var item in nofocharsineachname2)
			{
				Console.WriteLine("{0,4}--{1}",item, item.Length);
			}

			Console.WriteLine("\nList all the employee or display all the employees\n ");

			var emplist = Employeerepository.Retrive();

			var emplist1 = from emp in emplist select emp;// query syntax
			var emplist2 = emplist;// method syntax
			foreach(Employee emp in emplist2)
			{
				Console.WriteLine("{0}--{1}--{2}--{3}--{4}", 
					emp.EmployeeID, emp.FirstName, emp.LastName, emp.City, emp.Sal);
			}

			//showing few columns only like first name and last name 

			var emplistnames = from emp in emplist
							   select new
							   {
								   emp.FirstName,
								   emp.LastName
							   }; // query syntax

			var emplistnames1 = emplist.Select(x => new { x.FirstName,x.LastName });// method syntax

			Console.WriteLine("\ndisplaying ..first name and last name only ...");
			foreach (var emp in emplistnames)
			{
				Console.WriteLine("{0}--{1}",
					emp.FirstName, emp.LastName);
			}

			Console.WriteLine("concatenate names and give it to me ..");
			var concatnames = from emp in emplist
							  select new
							  {
								 FullName= emp.FirstName + " " + emp.LastName
							  };// query syntax

			var concatnames2 = emplist.Select(x => new {FullName=x.FirstName + " " +x.LastName });//method syntax


			foreach (var item in concatnames2)
			{
				Console.WriteLine("{0}",item.FullName);
			}
			// give me all employees whoes salary is greater than avg salary of all employyes

		
			var avgsalary = emplist.Average<Employee>(x => x.Sal);// method syentax 

		
			var avgsalry1 = from emp in emplist
							 where (emp.Sal > avgsalary) select emp;// query syntax;


			Console.WriteLine("\n The average slary of employee is {0}", avgsalary);

			var greaterthanvg = emplist.Where<Employee>(x => x.Sal > avgsalary);

			foreach (var item in greaterthanvg)
			{
				Console.WriteLine("{0}--{1}", item.FirstName, item.Sal);

			}

			// finind the empname if empid is given this we have done in collection but using linq i want to do it 
			Console.WriteLine("\nenter the  id of the employee for which u want to find the name");
			int id = Convert.ToInt16(Console.ReadLine());
			var checkemp = from emp in emplist where emp.EmployeeID == id select emp;

			Employee empfound = checkemp.FirstOrDefault();
			if(empfound!=null)
			{

				Console.WriteLine("\nThe emp with {0} is having name {1}", empfound.EmployeeID, empfound.FirstName);
			}
			else
			{
				Console.WriteLine("\nemployee not avaiable ");
			}

			// funciton subprogram to find count of vowels in a given string using linq  ;
			Console.WriteLine("\n enter string to find no of vowels in it and count also ..");
			string input = Console.ReadLine();
			findvowelcout(input);

			Console.WriteLine("creating groups for odd and even numbers in the array ....");
			var groups=from number in numbers   group number by number%2!=0 
					   into groupnumbers
					   select new
					   {
						  isodd= groupnumbers.Key,
						   groupnumbers

			           };

			foreach (var group in groups)
			{
				Console.WriteLine(group.isodd);
				foreach(var item in group.groupnumbers)
				{
					Console.WriteLine(item);
				}
				Console.WriteLine();

			}

			Console.WriteLine();
			Console.WriteLine();

			List<DepartmentClass> departments = new List<DepartmentClass>();
			departments.Add(new DepartmentClass
			{
				DepartmentId = 1,
				Name ="Account"
			});
			departments.Add(new DepartmentClass
			{
				DepartmentId = 2,
				Name = "Sales"
			});
			departments.Add(new DepartmentClass
			{
				DepartmentId = 3,
				Name ="Marketing"
			});
			List<EmployeeClass> employees = new List<EmployeeClass>();
			employees.Add(new EmployeeClass
			{
				DepartmentId = 1,
				EmployeeId = 1,
				EmployeeName = "William"
			});
			employees.Add(new EmployeeClass
			{
				DepartmentId = 2,
				EmployeeId = 2,
				EmployeeName = "Miley"
			});
			employees.Add(new EmployeeClass
			{
				DepartmentId = 1,
				EmployeeId = 3,
				EmployeeName = "Benjamin"
			});

			Console.WriteLine("\njoining above two tables based on common columns...");

			var list = (from e in employees
						join d in departments on e.DepartmentId equals d.DepartmentId
						select new
						{
							EmployeeName = e.EmployeeName,
							DepartmentName = d.Name
						});

			foreach (var e in list)
			{
				Console.WriteLine("Employee Name = {0} , Department Name = {1}",
				e.EmployeeName, e.DepartmentName);
			}
			Console.ReadLine();
		}
	}
}

constructor demos 
-------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructorinhiertance
{
    class Vehicle
    {
        public string Make { set; get; }
        public string Model { set; get; }

        public Vehicle(string make,string model)
        {
            Make = make;    
            Model = model;  
            Console.WriteLine($"Vehicle constructor :{Make}--{Model}");
                
        }
    }
    class Car :Vehicle 
    { 

        public int NumberofDoors { set; get; }

        public Car(string make, string model,int noofdoors):base(make,model)
        {
            NumberofDoors = noofdoors;

            Console.WriteLine($"car constructor :{Make}--{Model}--{NumberofDoors}");
        }



    }
     class Program
    {
        static void Main(string[] args)
        {
            Car cc = new Car("Toyota", "camry", 4);
            Console.ReadLine();
        }
    }
}

Enum Demos 
---------
Symbolic name given to integral constants we call it as enums they have to be declared outsdie main you cannot 
declare them inside main .

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumdemo1
{
     class Program
    {

        enum weekdays { sun, mon, tue, wed, thu, fri, sat };
        static void Main(string[] args)
        {

            Console.WriteLine("{0}",(int) weekdays.wed);
            Console.ReadLine();
            
        }
    }
 }



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumdemo2
{
    public enum EmployeeType
    {
        FullTime,
        PartTime,
        Contract
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EmployeeType Type { get; set; }

        public Employee(int id ,string name,EmployeeType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public void displayempolyee()
        {
            Console.WriteLine($"{Id}--{Name}--{Type}");
        }
    }
     class Program
    {
        static void Main(string[] args)
        {

            Employee e1=new Employee(101,"ravi",EmployeeType.FullTime);
            e1.displayempolyee();
            Console.ReadLine();
        }
    }
}

Struct demo 
--------------
They are same as class they are used when you are having some set of data types belonging to same element i create stuct here below struct student i had created all types id,name and marks scored by him are related so i had kept them 
in one group of related type which is struct but then struct wont follow oops concepts like inheritance u cannot implement it like a class 



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace structstudentdemo
{
    public struct Student
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public double[] Marks { set; get; }

        public Student(int id ,string name,int numberofsubjects)
        {
            ID = id;
            Name = name;
            Marks=new double[numberofsubjects];
        }
        // adding marks to array of particular student
        public void AddMarks(double[] marks)
        {
            if(marks.Length!=Marks.Length)
            {
                Console.WriteLine("Cannot add marks as marks count is not matching..");

            }
            else
            {
                for (int i = 0; i < Marks.Length; i++)
                {
                    Marks[i] = marks[i];
                }

            }
            Console.WriteLine($"Marks added to {Name}");
        }

        public double CalculateAvergageMark()
        {
            double sum = 0;
            foreach (double mark in Marks) 
            {
                sum += mark;
            
            }
            return Marks.Length>0?sum/Marks.Length:0;
        }

        public char DetermineGrade ()
        {
            double average = CalculateAvergageMark();
            if(average> 80)
            {
                return 'A';
            }

            else if(average > 70 )
            {
                return 'B';
            }
            else if(average > 60)
            {
                return 'C';
            }
            else if (average > 40)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }
        public void displaydetails()
        {
            Console.WriteLine($"ID:{ID},Name :{Name}");
            Console.WriteLine("Marks:");
            foreach (double mark in Marks)
            {
                Console.Write($"\t{mark}");
            }

            Console.WriteLine($"\nAvergae marks :{CalculateAvergageMark():F2}");
            Console.WriteLine($"\nGrade: {DetermineGrade()}");
        }

    }
     class Program
    {
        static void Main(string[] args)
        {

            Student student1 = new Student(1,"ravi",3);
            student1.AddMarks(new double[] { 86.89, 69.78, 90.67 });
            student1.CalculateAvergageMark();
            student1.DetermineGrade();
            student1.displaydetails();
            Console.ReadLine();
        }
    }
}



Extension Methods :
------------------
Extension methods in C# are a powerful feature that allows you to "add" methods to existing types without modifying the original type or creating a new derived type. This is particularly useful when you want to add functionality to classes that you don't have the source code for or can't modify (such as classes from the .NET framework).

How Extension Methods Work
Extension methods are static methods defined in static classes. They are called as if they were instance methods on the extended type. The first parameter of an extension method specifies which type the method extends, and it is preceded by the this keyword.



using System;

public static class StringExtensions
{
    // Extension method to check if a string is a palindrome
    public static bool IsPalindrome(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;

        int i = 0;
        int j = str.Length - 1;

        while (i < j)
        {
            if (str[i] != str[j])
                return false;
            i++;
            j--;
        }

        return true;
    }
}

class Program
{
    static void Main()
    {
        string example1 = "madam";
        string example2 = "hello";

        // Using the extension method as if it were an instance method
        Console.WriteLine(example1.IsPalindrome()); // Output: True
        Console.WriteLine(example2.IsPalindrome()); // Output: False
    }
}
using System;

public static class IntExtensions
{
    // Extension method to check if an integer is odd
    public static bool IsOdd(this int number)
    {
        // A number is odd if it is not divisible by 2
        return number % 2 != 0;
    }
}

class Program
{
    static void Main()
    {
        int number1 = 5;
        int number2 = 8;

        // Using the extension method IsOdd on integers
        Console.WriteLine($"{number1} is odd: {number1.IsOdd()}"); // Output: 5 is odd: True
        Console.WriteLine($"{number2} is odd: {number2.IsOdd()}"); // Output: 8 is odd: False
    }
}






