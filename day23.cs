PROXY PATTERN
**************

The Proxy Pattern is a structural design pattern that provides a placeholder or surrogate for another object to control access to it.

🔍 Real-world Analogy
Think of a security guard at a gate. The guard doesn't do the actual work inside the building but:

Checks if you have access.

Lets you in or stops you.

Here, the guard is the proxy for the building's access.

💡 Intent of Proxy Pattern
“Provide a surrogate or placeholder for another object to control access to it.”

You use a proxy when:

You want to add security, logging, lazy loading, caching, etc.

You don't want the client to interact directly with the real object.

Scenario: Role-based Access to Student Records
You have a system where:

Admin users can view and modify student records.

Guest users can only view student records.

We'll use a proxy to control what operations a user can perform based on their role.

🔧 Step-by-Step Implementation in C#

1️⃣ Subject Interface

public interface IStudentDataAccess
{
    void DisplayStudent(int id);
    void UpdateStudent(int id, string name);
}
2️⃣ Real Object – Actual Access to Data

public class RealStudentDataAccess : IStudentDataAccess
{
    public void DisplayStudent(int id)
    {
        Console.WriteLine($"Displaying student record with ID: {id}");
    }

    public void UpdateStudent(int id, string name)
    {
        Console.WriteLine($"Updating student ID {id} to new name: {name}");
    }
}
3️⃣ Proxy Class – Controls Access Based on Role

public class StudentDataProxy : IStudentDataAccess
{
    private RealStudentDataAccess realAccess = new RealStudentDataAccess();
    private string role;

    public StudentDataProxy(string userRole)
    {
        role = userRole.ToLower();
    }

    public void DisplayStudent(int id)
    {
        realAccess.DisplayStudent(id);
    }

    public void UpdateStudent(int id, string name)
    {
        if (role == "admin")
        {
            realAccess.UpdateStudent(id, name);
        }
        else
        {
            Console.WriteLine("Access Denied: You do not have permission to update student records.");
        }
    }
}
4️⃣ Client Code (Main Method)

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Logged in as Admin:");
        IStudentDataAccess adminAccess = new StudentDataProxy("admin");
        adminAccess.DisplayStudent(101);
        adminAccess.UpdateStudent(101, "Amit");

        Console.WriteLine("\nLogged in as Guest:");
        IStudentDataAccess guestAccess = new StudentDataProxy("guest");
        guestAccess.DisplayStudent(102);
        guestAccess.UpdateStudent(102, "John"); // Should be blocked

        Console.ReadLine();
    }
}
✅ Output

Logged in as Admin:
Displaying student record with ID: 101
Updating student ID 101 to new name: Amit

Logged in as Guest:
Displaying student record with ID: 102
Access Denied: You do not have permission to update student records.
📌 Key Takeaways
The Proxy (StudentDataProxy) controls access to the real object (RealStudentDataAccess).

Roles are enforced without modifying the actual business logic.

Great for adding security, validation, logging, or lazy loading behavior.


COMMAND PATTERN 
****************

✅ What is the Command Pattern?
The Command Pattern is a behavioral design pattern where you:

Encapsulate a request as an object.

This allows you to:

Parameterize methods with different commands.

Queue requests, log operations, or even undo actions.

🔧 Real-World Analogy
Think of a TV Remote:

Pressing a button (e.g., Volume Up) sends a command to the TV.

You don’t need to know how the TV works.

You can even program the remote to send sequences or undo.

🧱 Components of the Command Pattern
Role	Description
Command	An interface with an Execute() method
ConcreteCommand	Implements the command interface
Receiver	The actual object that performs the action
Invoker	The object that sends the command
Client	Sets everything up

  
cenario: Smart Home Remote with Lights and Fan
1️⃣ Command Interface

public interface ICommand
{
    void Execute();
}
2️⃣ Receiver Classes

public class Light
{
    public void TurnOn() => Console.WriteLine("Light is ON");
    public void TurnOff() => Console.WriteLine("Light is OFF");
}

public class Fan
{
    public void Start() => Console.WriteLine("Fan is spinning");
    public void Stop() => Console.WriteLine("Fan stopped");
}
3️⃣ Concrete Command Classes

public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}

public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}

public class FanOnCommand : ICommand
{
    private Fan _fan;

    public FanOnCommand(Fan fan)
    {
        _fan = fan;
    }

    public void Execute()
    {
        _fan.Start();
    }
}

public class FanOffCommand : ICommand
{
    private Fan _fan;

    public FanOffCommand(Fan fan)
    {
        _fan = fan;
    }

    public void Execute()
    {
        _fan.Stop();
    }
}
4️⃣ Invoker Class (Remote Control)

public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }
}
5️⃣ Client Code (Main Method)

class Program
{
    static void Main(string[] args)
    {
        // Receiver objects
        Light livingRoomLight = new Light();
        Fan ceilingFan = new Fan();

        // Commands
        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);
        ICommand fanOn = new FanOnCommand(ceilingFan);
        ICommand fanOff = new FanOffCommand(ceilingFan);

        // Invoker
        RemoteControl remote = new RemoteControl();

        Console.WriteLine("Turning on the light:");
        remote.SetCommand(lightOn);
        remote.PressButton();

        Console.WriteLine("Starting the fan:");
        remote.SetCommand(fanOn);
        remote.PressButton();

        Console.WriteLine("Turning off the light:");
        remote.SetCommand(lightOff);
        remote.PressButton();

        Console.WriteLine("Stopping the fan:");
        remote.SetCommand(fanOff);
        remote.PressButton();
    }
}
✅ Output

Turning on the light:
Light is ON
Starting the fan:
Fan is spinning
Turning off the light:
Light is OFF
Stopping the fan:
Fan stopped

📌 Advantages of Command Pattern
Decouples sender and receiver

Supports undo/redo

Can store or queue commands

Makes adding new commands easy (open/closed principle)

Developers build applications with good and tidy designs using their knowledge and experience. But over time, applications might develop bugs. The application design must be altered for every change request or new feature request. After some time, we might need to put in a lot of effort, even for simple tasks, and it might require a full working knowledge of the entire system. But we can't blame change or new feature requests. They are part of software development. We can't stop them or refuse them either. So who is the culprit here? It is the design of the application.

The following are the design flaws that cause damage to software, mostly.

Putting more stress on classes by assigning more responsibilities to them. (A lot of functionality not related to a class.)
Forcing the classes to depend on each other. If classes depend on each other (in other words, tightly coupled), then a change in one will affect the other.
Spreading duplicate code in the system/application.

SOLID principles are the design principles that enable us to manage several software design problems. Robert C. Martin compiled these principles in the 1990s. These principles provide us with ways to move from tightly coupled code and little encapsulation to the desired results of loosely coupled and encapsulated real business needs properly. SOLID is an acronym for the following.

S: Single Responsibility Principle (SRP)
O: Open-closed Principle (OCP)
L: Liskov substitution Principle (LSP)
I: Interface Segregation Principle (ISP)
D: Dependency Inversion Principle (DIP)


OOAD means object oriented analysis and design here design means DRY ,divide and conquer and change of request 

check presentations and one image etc to tell how classes are created okay 


Single Responsibility Principle (SRP) - Real-Time Example in C#
-------------------------------------------------------------------
The Single Responsibility Principle (SRP) is one of the SOLID principles, which states that a class should have only one reason to change, meaning it should only have one job or responsibility. If a class has more than one responsibility, it can lead to code that is difficult to maintain and modify.


create one folder Day11Projects 


and write the first commnd like this 

dotnet new console -o srpdemo --use-program-main

add one class into the projet right click and c# and select class and drop down write Person and press enter then namespaes will come automatically 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace srpdemo
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

and in the main program the code is like this 

namespace srpdemo;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to my application!");

        // Ask for user information
        Person user = new Person();

        Console.Write("What is your first name: ");
        user.FirstName = Console.ReadLine();

        Console.Write("What is your last name: ");
        user.LastName = Console.ReadLine();

        // Checks to be sure the first and last names are valid
        if (string.IsNullOrWhiteSpace(user.FirstName))
        {
            Console.WriteLine("You did not give us a valid first name!");
            Console.ReadLine();
            return;
        }

        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            Console.WriteLine("You did not give us a valid last name!");
            Console.ReadLine();
            return;
        }

        // Create a username for the person
        Console.WriteLine($"Your username is {user.FirstName.Substring(0, 1)} {user.LastName}");

        Console.WriteLine("Press Enter to Close the Application ");
        Console.ReadLine();


    }
}


Now i am dividing the responsiblities like this into seperate classes means i am adding some classes like how i added Person class


StandardMessages.cs 
-----------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace srpdemo
{
    public class StandardMessages
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to my application!");
        }

        public static void EndApplication()
        {
            Console.Write("Press enter to close...");
            Console.ReadLine();
        }

        public static void DisplayValidationError(string fieldName)
        {
            Console.WriteLine($"You did not give us a valid {fieldName}!");
        }
    }
}

PersonDataCapture.cs
---------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace srpdemo
{
    public class PersonDataCapture
    {
        public static Person Capture()
        {
            // Ask for user information
            Person output = new Person();

            Console.Write("What is your first name: ");
            output.FirstName = Console.ReadLine();

            Console.Write("What is your last name: ");
            output.LastName = Console.ReadLine();

            return output;
        }
    }
}


AccountGenerator.cs
----------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace srpdemo
{
    public class AccountGenerator
    {
        public static void CreateAccount(Person user)
        {
            // Create a username for the person
            Console.WriteLine($"Your username is {user.FirstName.Substring(0, 1)} {user.LastName}");
        }
    }
}


PersonValidator.cs
------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace srpdemo
{
    public class PersonValidator
    {
        public static bool Validate(Person person)
        {
            // Checks to be sure the first and last names are valid
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                StandardMessages.DisplayValidationError("first name");
                return false;
            }

            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                StandardMessages.DisplayValidationError("last name");
                return false;
            }

            return true;
        }
    }
}


Now the final Program.cs file will look like this 


namespace srpdemo;

class Program
{
    static void Main(string[] args)
    {
        StandardMessages.WelcomeMessage();

        Person user = PersonDataCapture.Capture();

        bool isUserValid = PersonValidator.Validate(user);

        if (isUserValid == false)
        {
            StandardMessages.EndApplication();
            return;
        }

        AccountGenerator.CreateAccount(user);

        StandardMessages.EndApplication();

    }
}


so imagine this you are doing it for one person suppose 100 persons are there in it will be very much cubersome to make a note of 100 students in the main method only okay .

The Open/Closed Principle (OCP), one of the SOLID principles, states that a class should be open for extension but closed for modification. This means that the behavior of a class should be extendable without modifying its source code. In real-world applications, this is achieved by using abstraction, inheritance, and interfaces.

ocp viloation when new proeprty emptype came 
----------------------------------------------------

version 1 of the program 
-----------------------------
namespace ocp;
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }



    public Employee()
    {

    }

    public Employee(int id, string name)
    {
        this.Id = id;
        this.Name = name;

    }

    public override string ToString()
    {
        return string.Format($"Employee Id is : {Id} and Name : {Name}");
    }

}
class Program
{
    static void Main(string[] args)
    {
        Employee e1 = new Employee(101, "ravi");
        Employee e2 = new Employee(102, "mahesh");
        Console.WriteLine(e1);
        Console.WriteLine(e2);
    }
}

new requiremnt has come which based on emptype provide the employee bonus if he is contract employee 
provide 10% bonus and if he is temperory employee provide 5% bonus 


new requiremnt has come which based on emptype provide the employee bonus if he is permenant  employee 
provide 10% bonus and if he is temperory employee provide 5% bonus 

Now i will try to mdifify the above code like this for the requiemnt 


namespace ocp;

public enum EmpType
{
    permenant,
    temperory
}
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public EmpType EmployeeType { get; set; }

    public Employee()
    {

    }

    public Employee(int id, string name, EmpType empType)
    {
        this.Id = id;
        this.Name = name;
        this.EmployeeType = empType;
    }

    public decimal CalculateBonus(decimal salary)
    {
        if (this.EmployeeType == EmpType.permenant)
        {
            return salary * 0.1M;
        }
        else
        {
            return salary * 0.05M;
        }
    }

    public override string ToString()
    {
        return string.Format($"Employee Id is : {Id} and Name : {Name}");
    }

}
class Program
{
    static void Main(string[] args)
    {
        Employee e1 = new Employee(101, "ravi",EmpType.permenant);
        Employee e2 = new Employee(102, "mahesh",EmpType.temperory);
        Console.WriteLine($"{e1} and bonus:{e1.CalculateBonus(30000)}");
        Console.WriteLine($"{e2} and bonus:{e2.CalculateBonus(30000)}");
    }
}


But above i am violating ocp princile i am modifyng my code my class for new change of request 

in the above code i had modified my main class employee as per new request means i am open for modification which should not be 
so i will chnage the code now as per condtion that we should be open for extension 

folowing ocp 
--------------
in the above code i had modified my main class employee as per new request means i am open for modification which should not be 
so i will chnage the code now as per condtion that we should be open for extension 

  
  public abstract  class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

      
        public Employee()
        {

        }

        public Employee(int id, string name)
        {
            this.Id = id;
            this.Name = name;
           
        }

        public abstract decimal caluclateBonus(decimal salary);
       

        public override string ToString()
        {
            return string.Format($"Employee Id is : {Id} and Name : {Name}");
        }

    }

    class temperaoryemp : Employee
    {

      
        public temperaoryemp()
        {
                
        }
        public temperaoryemp(int id,string name):base(id,name)
        {
               
        }
        public override decimal caluclateBonus(decimal salary)
        {
            return salary * 0.05M;
        }
    }

    class Permenantemp : Employee
    {
        public Permenantemp()
        {
                
        }
        public Permenantemp(int id,string name):base(id,name)
        {
                
        }
        public override decimal caluclateBonus(decimal salary)
        {
            return salary * 0.01M;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Employee e1 = new Permenantemp(101, "ravi");
            Employee e2 = new temperaoryemp(102,"mahesh");
            Console.WriteLine(e1.ToString() +" and bonus is:"+e1.caluclateBonus(30000));
            Console.WriteLine(e2.ToString() + " and bonus is:" + e2.caluclateBonus(30000));

            Console.ReadLine();
        }
    }
}




Liskov Sustitution Principle
-----------------------------
The Liskov Substitution Principle (LSP), one of the SOLID principles, states that subtypes must be substitutable for their base types without affecting the correctness of the program. In other words, objects of a derived class should be able to replace objects of the base class without altering the desirable properties of the program.

Real-Time Scenario: Vehicle and Transportation System
Imagine a transportation system where we have different types of vehicles, such as cars and bicycles. Both vehicles can perform operations like starting the engine or driving, but some vehicles (e.g., bicycles) do not have an engine.

In this case, violating the Liskov Substitution Principle (LSP) might involve creating a base Vehicle class with an engine-related method and forcing all vehicles (even those without an engine, like bicycles) to implement it. The correct approach is to ensure that subclasses only inherit functionality that makes sense for them.


Step 1: Violating the Liskov Substitution Principle (LSP)
In this example, let's see what happens when we force all vehicle types (including bicycles) to implement engine-related methods, even though not all vehicles have engines.




From the above program if any new employee who is contract employee  is coming and he dont want bonus then i am changing the program like this 

  class contractemp : Employee
    {

        public contractemp()
        {
                
        }
        public contractemp(int id, string name) : base(id, name)
        {

        }
        public override decimal caluclateBonus(decimal salary)
        {
            throw new NotImplementedException();
        }
    }


Then at run time only i will know that he dont want bonus i will excute the code as usual here in main also 
so above code i take and will execute like this below 

namespace ocp;

public enum EmpType
{
    permenant,
    temperory
}
public abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }


    public Employee()
    {

    }

    public Employee(int id, string name)
    {
        this.Id = id;
        this.Name = name;

    }

    public abstract decimal CalculateBonus(decimal salary);


    public override string ToString()
    {
        return string.Format($"Employee Id is : {Id} and Name : {Name}");
    }

}

class temperaoryemp : Employee
{


    public temperaoryemp()
    {

    }
    public temperaoryemp(int id, string name) : base(id, name)
    {

    }
    public override decimal CalculateBonus(decimal salary)
    {
        return salary * 0.05M;
    }
}

class Permenantemp : Employee
{
    public Permenantemp()
    {

    }
    public Permenantemp(int id, string name) : base(id, name)
    {

    }
    public override decimal CalculateBonus(decimal salary)
    {
        return salary * 0.1M;
    }
}
// dont want bonus 
class contractemp : Employee
{

    public contractemp()
    {

    }
    public contractemp(int id, string name) : base(id, name)
    {

    }
    public override decimal CaluclateBonus(decimal salary)
    {
        throw new NotImplementedException();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Employee e1 = new Permenantemp(101, "ravi");
        Employee e2 = new temperaoryemp(102, "mahesh");
        Employee e3 = new contractemp(103, "kiran");
        Console.WriteLine($"{e1} and bonus:{e1.CalculateBonus(30000)}");
        Console.WriteLine($"{e2} and bonus:{e2.CalculateBonus(30000)}");
        Console.WriteLine($"{e3} and bonus:{e3.CalculateBonus(30000)}");
    }
}
so when i build the applicaiton i am getting error at run time i am getting it not showing any compile time error 
but why run time i am getting beasue code is not implemnted and i will know at run time so here 
class e3 object is not proeprly substituted in base class employee so what to do in code change 

add one interface like this 

    interface  EmpBonus
    {
         decimal caluclateBonus(decimal salary);
         void displayemp();
    }

and implemnt this for contract employee 

 class contractemp : EmpBonus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public contractemp()
        {

        }
        public contractemp(int id, string name)
        {
            Id = id;
            Name = name;

        }
        public decimal caluclateBonus(decimal salary)
        {
            return 0;

        }
      public void displayemp()
    {
        Console.WriteLine($"Employee Id is :{this.Id}  and Name : {this.Name}");
    }
    }



so final code is like here i am making the e3 object to be substiutitateble into employee using interface 

namespace ocp;

public enum EmpType
{
    permenant,
    temperory
}
public abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }


    public Employee()
    {

    }

    public Employee(int id, string name)
    {
        this.Id = id;
        this.Name = name;

    }

    public abstract decimal CalculateBonus(decimal salary);


    public override string ToString()
    {
        return string.Format($"Employee Id is : {Id} and Name : {Name}");
    }

}

class temperaoryemp : Employee
{


    public temperaoryemp()
    {

    }
    public temperaoryemp(int id, string name) : base(id, name)
    {

    }
    public override decimal CalculateBonus(decimal salary)
    {
        return salary * 0.05M;
    }
}

class Permenantemp : Employee
{
    public Permenantemp()
    {

    }
    public Permenantemp(int id, string name) : base(id, name)
    {

    }
    public override decimal CalculateBonus(decimal salary)
    {
        return salary * 0.1M;
    }
}
interface EmpBonus
{
    decimal caluclateBonus(decimal salary);
    void displayemp();
}
class contractemp : EmpBonus
{

    public int Id { get; set; }
    public string Name { get; set; }

    public contractemp()
    {

    }
    public contractemp(int id, string name)
    {
        Id = id;
        Name = name;

    }
    public decimal caluclateBonus(decimal salary)
    {
        return 0;

    }

    public void displayemp()
    {
        Console.WriteLine($"Employee Id is :{this.Id}  and Name : {this.Name}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Employee e1 = new Permenantemp(101, "ravi");
        Employee e2 = new temperaoryemp(102, "mahesh");
        EmpBonus e3 = new contractemp(103, "kiran");
        Console.WriteLine($"{e1} and bonus:{e1.CalculateBonus(30000)}");
        Console.WriteLine($"{e2} and bonus:{e2.CalculateBonus(30000)}");
        e3.displayemp();
        Console.WriteLine($" bonus: {e3.caluclateBonus(30000)} ");
    }
}

  

Interface Segregation Principle 
-----------------------------------
Interface Segregation Principle 
-----------------------------------

The Interface Segregation Principle (ISP), the fourth of the SOLID principles, states that clients should not be forced to depend on interfaces they do not use. In other words, it's better to have smaller, specific interfaces rather than a large, general-purpose interface that tries to cover too many unrelated functionalities.

Real-World Scenario: Printer System
Let's consider a Printer System where we have different types of printers. Some printers can print, scan, and fax (multi-function printers), while others might only print. If we create a single interface that defines all three functionalities (print, scan, and fax), we would be violating the Interface Segregation Principle (ISP) because simple printers, which can only print, would be forced to implement methods for scanning and faxing, even though they don't need them.


Step 1: Violating the Interface Segregation Principle (ISP)
In the following example, we have a single IPrinter interface that defines methods for printing, scanning, and faxing. Even though not all printers support scanning or faxing, they are forced to implement these methods, which violates ISP.

Example of ISP Violation:

open a new project using  the command--> dotnet new console -o ISP --use-program-main from the folder Day11Projects 


and paste the following code below 


using System;

public interface IPrinter
{
    void Print(string content);
    void Scan(string content);
    void Fax(string content);
}

public class MultiFunctionPrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning content: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing content: " + content);
    }
}

public class SimplePrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }

    public void Scan(string content)
    {
        // This printer doesn't support scanning, but we are forced to implement it.
        throw new NotImplementedException("SimplePrinter cannot scan.");
    }

    public void Fax(string content)
    {
        // This printer doesn't support faxing, but we are forced to implement it.
        throw new NotImplementedException("SimplePrinter cannot fax.");
    }
}

and final code which will run give run time  error is 
---------------------------------------------
  namespace ISP;
public interface IPrinter
{
    void Print(string content);
    void Scan(string content);
    void Fax(string content);
}
public class MultiFunctionPrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning content: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing content: " + content);
    }
}

public class SimplePrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }

    public void Scan(string content)
    {
        // This printer doesn't support scanning, but we are forced to implement it.
        throw new NotImplementedException("SimplePrinter cannot scan.");
    }

    public void Fax(string content)
    {
        // This printer doesn't support faxing, but we are forced to implement it.
        throw new NotImplementedException("SimplePrinter cannot fax.");
    }
}
class Program
{
    static void Main(string[] args)
    {
        IPrinter generalobj = new MultiFunctionPrinter();
        generalobj.Print("multifunctiondocuemnt1 for printing");
        generalobj.Fax("multi function printer faxing");
        generalobj.Scan("multi function printinscaning");

        generalobj = new SimplePrinter();
        generalobj.Print("simple priter printing");
        generalobj.Fax("faxin using simple printer") ;// for this i am getting unhandled exception 
    }
}

Problems:
The SimplePrinter class is forced to implement methods like Scan() and Fax(), even though it doesn’t support these functionalities. This violates the Interface Segregation Principle.
If the interface changes (e.g., adding a new method for color printing), all classes implementing the interface must be updated, even if they don't use that feature.


Step 2: Refactor Using the Interface Segregation Principle (ISP)
To adhere to ISP, we will:

Break down the IPrinter interface into smaller, more specific interfaces that represent distinct functionalities like printing, scanning, and faxing.
Have each class implement only the interfaces that are relevant to their capabilities.


1. ISpecific Interfaces
We will define three separate interfaces: IPrinter, IScanner, and IFax. Each interface will represent a single responsibility.

IPrinter Interface (For Printing Only):

public interface IPrinter
{
    void Print(string content);
}


IScanner Interface (For Scanning Only):

public interface IScanner
{
    void Scan(string content);
}


IFax Interface (For Faxing Only):

public interface IFax
{
    void Fax(string content);
}


2. MultiFunctionPrinter Class (Implements All Interfaces)
The MultiFunctionPrinter can print, scan, and fax, so it will implement all three interfaces.

public class MultiFunctionPrinter : IPrinter, IScanner, IFax
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning content: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing content: " + content);
    }
}


3. SimplePrinter Class (Implements Only IPrinter)
The SimplePrinter class can only print, so it will implement only the IPrinter interface, adhering to the Interface Segregation Principle.


public class SimplePrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }
}

4. Main Program (Testing the ISP-Compliant Code)
Now we can use the different types of printers based on their specific functionality.


class Program
{
    static void Main(string[] args)
    {
        // Using MultiFunctionPrinter, which supports printing, scanning, and faxing
        MultiFunctionPrinter multiFunctionPrinter = new MultiFunctionPrinter();
        multiFunctionPrinter.Print("MultiFunctionPrinter: Document 1");
        multiFunctionPrinter.Scan("MultiFunctionPrinter: Document 1");
        multiFunctionPrinter.Fax("MultiFunctionPrinter: Document 1");

        // Using SimplePrinter, which only supports printing
        SimplePrinter simplePrinter = new SimplePrinter();
        simplePrinter.Print("SimplePrinter: Document 2");

        Console.ReadLine();
    }
}

final code 
-------------
namespace ISP;

public interface IPrinter
{
    void Print(string content);
}

public interface IScanner
{
    void Scan(string content);
}
public interface IFax
{
    void Fax(string content);
}
public class MultiFunctionPrinter : IPrinter, IScanner, IFax
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning content: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing content: " + content);
    }
}

public class SimplePrinter : IPrinter
{
    public void Print(string content)
    {
        Console.WriteLine("Printing content: " + content);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Using MultiFunctionPrinter, which supports printing, scanning, and faxing
        MultiFunctionPrinter multiFunctionPrinter = new MultiFunctionPrinter();
        multiFunctionPrinter.Print("MultiFunctionPrinter: Document 1");
        multiFunctionPrinter.Scan("MultiFunctionPrinter: Document 1");
        multiFunctionPrinter.Fax("MultiFunctionPrinter: Document 1");

        // Using SimplePrinter, which only supports printing
        SimplePrinter simplePrinter = new SimplePrinter();
        simplePrinter.Print("SimplePrinter: Document 2");

        Console.ReadLine();
    }
}


sample output 
--------------
Printing content: MultiFunctionPrinter: Document 1
Scanning content: MultiFunctionPrinter: Document 1
Faxing content: MultiFunctionPrinter: Document 1
Printing content: SimplePrinter: Document 2


Explanation of Changes:
Interface Segregation: Instead of having one large interface (IPrinter) that covers all functionalities (printing, scanning, faxing), we broke it down into smaller interfaces (IPrinter, IScanner, IFax), ensuring that each class implements only the functionalities it supports.
MultiFunctionPrinter Class: Implements all three interfaces (IPrinter, IScanner, IFax) because it supports all functionalities.
SimplePrinter Class: Implements only the IPrinter interface because it can only print. It no longer has to implement Scan() and Fax(), which are irrelevant to its functionality.
Benefits of Applying the Interface Segregation Principle (ISP):
Flexibility:

Classes implement only the interfaces that are relevant to them. They are not forced to implement methods they don’t need.
Maintainability:

If a change is made to one interface, only the classes that use that interface are affected. Other classes that don’t use it remain untouched.
Separation of Concerns:

Each interface is focused on a specific responsibility. This keeps the design clean and focused, making the system easier to extend and maintain.
Avoiding Unnecessary Dependencies:

Classes like SimplePrinter are not dependent on functionalities like scanning or faxing, which they don’t support. This avoids unnecessary complexity and potential errors.
Conclusion:
In this Printer System example, we applied the Interface Segregation Principle (ISP) by creating smaller, more focused interfaces (IPrinter, IScanner, IFax), rather than one large, general interface. This allows each class (e.g., MultiFunctionPrinter, SimplePrinter) to implement only the functionalities it needs, making the system more modular, maintainable, and flexible. By adhering to ISP, we avoid bloated interfaces and ensure that classes are not forced to depend on methods they don't use.






The Dependency Inversion Principle (DIP) 
------------------------------------------
is the last principle in the SOLID principles of object-oriented design. It states that high-level modules should not depend on low-level modules; both should depend on abstractions. Additionally, abstractions should not depend on details; details should depend on abstractions.

In simple terms:

High-level modules (e.g., business logic) should not directly depend on low-level modules (e.g., specific classes that implement certain functionality). Instead, both should depend on interfaces or abstract classes (i.e., abstractions).
This ensures that the high-level modules are not tightly coupled to the specific implementations, making the system more flexible, maintainable, and easier to extend.



First example my own which is violating DIP
----------------------------------------------
First from the main terminal means main folder write this command -->dotnet new console -o DIP --use-program-main

 a project will be created and then 
Here first addd one class using right click the project of DIP  C# and then class give name as Employee say enter 
and add prperties like below 

Employee.cs 
-------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIP
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
Now add another class like above EmpRespo and add below code 

  EmpRespo.cs
  ------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIP
{
    public class EmpRespo
    {
        // In-memory list to store employees
        private readonly List<Employee> _employees = new List<Employee>();

        // Method to add an employee to the collection
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            Console.WriteLine($"Employee {employee.Name} added.");
        }

        // Method to list all employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }
    }
}
main program.cs 
----------------
namespace DIP;

class Program
{
    static void Main(string[] args)
    {
        EmpRespo employeeRepository = new EmpRespo();

        // Add employees directly via EmployeeRepository
        employeeRepository.AddEmployee(new Employee { Id = 1, Name = "John Doe" });
        employeeRepository.AddEmployee(new Employee { Id = 2, Name = "Jane Smith" });

        // List employees directly via EmployeeRepository
        Console.WriteLine("Employee List:");
        foreach (var employee in employeeRepository.GetAllEmployees())
        {
            Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}");
        }

        Console.ReadLine();



    }
}


Now make the methods private both to them in EmpRespo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIP
{
    public class EmpRespo
    {
        // In-memory list to store employees
        private readonly List<Employee> _employees = new List<Employee>();

        // Method to add an employee to the collection
        private void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            Console.WriteLine($"Employee {employee.Name} added.");
        }

        // Method to list all employees
        private IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }
    }
}
Now in main program you cannot access the features 

means dotnet build will give me errrors 

now the methds  which i had made private first put them in some interface and let EmpReespo class implement that interface 
  so main method will not be knowing from where methods are coming 
so add one interface IEMP using right click c# interface and give name as IEMP press enter and 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIP
{
    public interface IEMP
    {
        void AddEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployees();
    }
}

now go to EmpRespo code 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIP
{
    public class EmpRespo :IEMP
    {
        // In-memory list to store employees
        private readonly List<Employee> _employees = new List<Employee>();

        // Method to add an employee to the collection
        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            Console.WriteLine($"Employee {employee.Name} added.");
        }

        // Method to list all employees
        public  IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }
    }
}


and the main progrsm will be like this 

  namespace DIP;

class Program
{
    static void Main(string[] args)
    {
        IEMP employeeRepository = new EmpRespo();

        // Add employees directly via EmployeeRepository
        employeeRepository.AddEmployee(new Employee { Id = 1, Name = "John Doe" });
        employeeRepository.AddEmployee(new Employee { Id = 2, Name = "Jane Smith" });

        // List employees directly via EmployeeRepository
        Console.WriteLine("Employee List:");
        foreach (var employee in employeeRepository.GetAllEmployees())
        {
            Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}");
        }

        Console.ReadLine();



    }
}





