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



