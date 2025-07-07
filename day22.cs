solid principles (week3 day 1) 
------------------
https://olympus1.mygreatlearning.com/online_session/recordings?access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJtZW50b3JlZF9zZXNzaW9uX2lkIjoxMjYzMDMzfQ.IRdNwYRW-j6AE39Ygk1rRrcPi8R5seRIZzl9sndzcZg

design patterns Week3 day 2
---------------------------------
https://olympus1.mygreatlearning.com/online_session/recordings?access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJtZW50b3JlZF9zZXNzaW9uX2lkIjoxMjYzMDM1fQ.T9_xtraJfVHXP_jF7yuLyxJzErYU36rxJlyO7lwgW1A


Great Learning:
-----------------
https://olympus.mygreatlearning.com/login

Name:	Raghavendra Ponde
Email:	raghuponde@yahoo.com
password :Raghu#821519

Understanding the Importance of Design Patterns in Software Design
********************************************************************
Design patterns are proven, reusable solutions to common problems in software design. They represent best practices refined over time by experienced developers and are used to solve recurring design problems in object-oriented software development. By using design patterns, developers can create robust, maintainable, and scalable systems.

Conclusion: Why Design Patterns Matter
Design patterns are critical to software design because they provide:

Reusability: Proven solutions that can be applied in various scenarios, reducing redundancy.
Maintainability: Encouraging loose coupling and separation of concerns, making systems easier to maintain.
Flexibility: Promoting extensible designs that allow for future growth without extensive code changes.
Collaboration: Making it easier for teams to communicate using well-known solutions, improving teamwork.
Best Practices: Ensuring that code follows established principles and is more readable, robust, and scalable.
Design patterns are more than just reusable code snippets; they represent the essence of good software architecture, making them an essential tool for developers looking to build effective and efficient systems.

singleton design patterns
----------------------------


The Singleton Pattern ensures that a class has only one instance and provides a global point of access to that instance. This is commonly used for managing global configuration settings, logging, or shared resources.

using System;
using System.Collections.Generic;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class EmployeeManager
{
    // Static variable to hold the single instance of EmployeeManager
    private static EmployeeManager _instance;

    private List<Employee> _employees;

    // Private constructor to prevent direct instantiation
    private EmployeeManager()
    {
        _employees = new List<Employee>();
        Console.WriteLine("EmployeeManager initialized.");
    }

    // Public static method to provide global access to the single instance
    public static EmployeeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EmployeeManager();
            }
            return _instance;
        }
    }

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
        Console.WriteLine($"Employee {employee.Name} added.");
    }

    public void ListEmployees()
    {
        Console.WriteLine("List of employees:");
        foreach (var emp in _employees)
        {
            Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Access the Singleton instance of EmployeeManager
        EmployeeManager manager1 = EmployeeManager.Instance;
        manager1.AddEmployee(new Employee { Id = 1, Name = "John Doe" });

        // Access the same Singleton instance of EmployeeManager from another variable
        EmployeeManager manager2 = EmployeeManager.Instance;
        manager2.AddEmployee(new Employee { Id = 2, Name = "Jane Smith" });

        // List employees using either reference (manager1 or manager2)
        manager1.ListEmployees();  // or manager2.ListEmployees()

        // Confirm that manager1 and manager2 are referencing the same instance
        Console.WriteLine(ReferenceEquals(manager1, manager2));  // This will print: True

        Console.ReadLine();
    }
}

Lab exanple 
------------
Program.cs 
----------
using System;

namespace UAHighschool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\nEnter your choice:\n1. Set School Name\n2. Get School Name\n3. Exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        SetSchoolName();
                        break;
                    case "2":
                        GetSchoolName();
                        break;
                    case "3":
                        Console.WriteLine("Thank you!!");
                        return;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }

        private static void SetSchoolName()
        {
            Highschool school = Highschool.GetSchoolInstance();
            Console.WriteLine("\nEnter the School Name:");
            string name = Console.ReadLine();
            school.schoolName = name;
            Console.WriteLine($"School name set to {school.schoolName}");
        }

        private static void GetSchoolName()
        {
            Highschool school = Highschool.GetSchoolInstance();
            if (string.IsNullOrWhiteSpace(school.schoolName))
            {
                Console.WriteLine("School name is not set");
            }
            else
            {
                Console.WriteLine($"School name is {school.schoolName}");
            }
        }
    }
}

Highschool.cs 
--------------
namespace UAHighschool
{
    public class Highschool
    {
        private static Highschool instance = null;
        public string schoolName;

        // Private constructor to prevent direct instantiation
        private Highschool() {}

        // Public method to return the single instance of Highschool
        public static Highschool GetSchoolInstance()
        {
            if (instance == null)
            {
                instance = new Highschool();
            }
            return instance;
        }
    }
}

2. Factory Method Pattern - Real-World Demo: Document Creator
The Factory Method Pattern defines an interface for creating objects but lets subclasses decide which class to instantiate. It’s useful when the exact type of object to be created depends on a specific condition.

Example: Document Creator (Factory Method Pattern)
In this example, we will implement a document creation system that generates different types of documents like PDFDocument and WordDocument depending on the user's input.

using System;

// Product Interface
public interface IDocument
{
    void Open();
    void Save();
}

// Concrete Product 1: PDF Document
public class PDFDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening PDF document.");
    }

    public void Save()
    {
        Console.WriteLine("Saving PDF document.");
    }
}

// Concrete Product 2: Word Document
public class WordDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening Word document.");
    }

    public void Save()
    {
        Console.WriteLine("Saving Word document.");
    }
}

// Abstract Factory Class
public abstract class DocumentFactory
{
    // Factory Method
    public abstract IDocument CreateDocument();
}

// Concrete Factory 1: PDF Document Factory
public class PDFDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new PDFDocument();
    }
}

// Concrete Factory 2: Word Document Factory
public class WordDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

// Client Code
class Program
{
    static void Main(string[] args)
    {
        // PDF Document Creation
        DocumentFactory pdfFactory = new PDFDocumentFactory();
        IDocument pdfDocument = pdfFactory.CreateDocument();
        pdfDocument.Open();
        pdfDocument.Save();

        // Word Document Creation
        DocumentFactory wordFactory = new WordDocumentFactory();
        IDocument wordDocument = wordFactory.CreateDocument();
        wordDocument.Open();
        wordDocument.Save();

        Console.ReadLine();
    }
}



Explanation:
The DocumentFactory defines the Factory Method CreateDocument() for creating documents.
PDFDocumentFactory and WordDocumentFactory are concrete factories that return specific document types (PDFDocument, WordDocument).
The client code doesn’t need to know the specifics of which document type is created—it relies on the factory.


another example just like this 

using System;

// Product Interface
public interface IEmployee
{
    void DisplayEmployeeDetails();
    void CalculateSalary();
}

// Concrete Product 1: Full-Time Employee
public class FullTimeEmployee : IEmployee
{
    public void DisplayEmployeeDetails()
    {
        Console.WriteLine("Displaying Full-Time Employee details.");
    }

    public void CalculateSalary()
    {
        Console.WriteLine("Calculating Full-Time Employee salary.");
    }
}

// Concrete Product 2: Part-Time Employee
public class PartTimeEmployee : IEmployee
{
    public void DisplayEmployeeDetails()
    {
        Console.WriteLine("Displaying Part-Time Employee details.");
    }

    public void CalculateSalary()
    {
        Console.WriteLine("Calculating Part-Time Employee salary.");
    }
}

// Abstract Factory Class
public abstract class EmployeeFactory
{
    // Factory Method
    public abstract IEmployee CreateEmployee();
}

// Concrete Factory 1: Full-Time Employee Factory
public class FullTimeEmployeeFactory : EmployeeFactory
{
    public override IEmployee CreateEmployee()
    {
        return new FullTimeEmployee();
    }
}

// Concrete Factory 2: Part-Time Employee Factory
public class PartTimeEmployeeFactory : EmployeeFactory
{
    public override IEmployee CreateEmployee()
    {
        return new PartTimeEmployee();
    }
}

// Client Code
class Program
{
    static void Main(string[] args)
    {
        // Full-Time Employee Creation
        EmployeeFactory fullTimeFactory = new FullTimeEmployeeFactory();
        IEmployee fullTimeEmployee = fullTimeFactory.CreateEmployee();
        fullTimeEmployee.DisplayEmployeeDetails();
        fullTimeEmployee.CalculateSalary();

        // Part-Time Employee Creation
        EmployeeFactory partTimeFactory = new PartTimeEmployeeFactory();
        IEmployee partTimeEmployee = partTimeFactory.CreateEmployee();
        partTimeEmployee.DisplayEmployeeDetails();
        partTimeEmployee.CalculateSalary();

        Console.ReadLine();
    }
}

Lab Example 
-------------
Prrogram.cs
----------
namespace GadgetHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the gadget type (mobile/laptop):");
            string gadgetType = Console.ReadLine();
            Console.WriteLine("Enter the name of the gadget:");
            string gadgetName = Console.ReadLine();

            // Get the gadget instance from the factory
            Gadget gadget = GadgetFactory.GetGadget(gadgetType, gadgetName);
            if (gadget != null)
            {
                Console.WriteLine($"Gadget created: {gadget.gadgetName}");
            }
        }
    }
}

Gadget.cs
-----------
namespace GadgetHub // Do NOT change the Namespace Name
{
    public class Gadget // Do NOT change the Class Name
    {
        public string gadgetName;
    }
}
LaptopGadget.cs
-------------------
namespace GadgetHub
{
    public class LaptopGadget : Gadget
    {
        public LaptopGadget(string name)
        {
            this.gadgetName = name;
            Console.WriteLine($"You have chosen Laptop Gadget and its name is {gadgetName}");
        }
    }
}


MobileGadget.cs
-------------------
namespace GadgetHub
{
    public class MobileGadget : Gadget
    {
        public MobileGadget(string name)
        {
            this.gadgetName = name;
            Console.WriteLine($"You have chosen Mobile Gadget and its name is {gadgetName}");
        }
    }
}
GadgetFactory.cs
---------------
namespace GadgetHub
{
    public class GadgetFactory
    {
        public static Gadget GetGadget(string gadgetType, string gadgetName)
        {
            gadgetType = gadgetType.ToLower();
            if (gadgetType == "mobile")
            {
                return new MobileGadget(gadgetName);
            }
            else if (gadgetType == "laptop")
            {
                return new LaptopGadget(gadgetName);
            }
            else
            {
                Console.WriteLine("Invalid gadget type");
                return null;
            }
        }
    }
}

