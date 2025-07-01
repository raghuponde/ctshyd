   
answer 1:
---------------

    1 namespace PasswordGenerator //DO NOT change the namespace name
    2 {
    3     public class Program //DO NOT change the class name
    4     {
    5         public static void Main(string[] args) //DO NOT change the 'Main' method signature
    6         {
    7             //Implement your code here 
    8             Console.WriteLine("Enter user name");
    9             string username = Console.ReadLine();
   10             string password = "";
   11             
   12             if(username.Length < 8){
   13                 password += username.Substring(0,1).ToUpper();
   14                 password += username.Substring(1).ToLower();
   15                 password += '@';
   16                 int size = 8 - password.Length;
   17                 for(int i = 0; i<size; i++){
   18                     password += (i+1);
   19                 }
   20                 Console.WriteLine(password);
   21             }
   22             else{
   23                 Console.WriteLine("Name must be less than 8 characters");
   24             }
   25         }
   26      }
   27 }
   
   
   answer 2 
   ----------
   Program.cs
    1 namespace ExperimentTracker   //DO NOT change the namespace name
    2 {
    3     public class Program    //DO NOT change the class name
    4     {
    5         public static List<Experiment> ExperimentalData { get; set; } = new List<Experiment>();
    6 
    7         public static void Main(string[] args) //DO NOT change the 'Main' method signature
    8         {
    9             //DO NOT change the given code
   10             Experiment experiment = new Experiment();
   11             ChemicalInventory inventory = new ChemicalInventory();
   12             int choice;
   13             do
   14             {
   15                 Console.WriteLine("1.To add the experiment");
   16                 Console.WriteLine("2.To find the Reaction rate");
   17                 Console.WriteLine("3.Exit");
   18                 Console.WriteLine("Enter your choice");
   19                 choice = Convert.ToInt32(Console.ReadLine());
   20                 if (choice != 1 && choice != 2 && choice != 3)
   21                 {
   22                     Console.WriteLine("Invalid choice");
   23                     return;
   24                 }
   25                 switch (choice)
   26                 {
   27                     case 1:
   28                         Console.WriteLine("Enter the Experiment name");
   29                         experiment.Name = Console.ReadLine();
   30                         Console.WriteLine("Enter the duration in seconds");
   31                         experiment.Duration = Convert.ToInt32(Console.ReadLine());
   32                         inventory.RecordData(experiment);
   33                         Console.WriteLine("Experiment added successfully");
   34                         break;
   35                     case 2:
   36                         double res = inventory.CalculateReactionRate(ExperimentalData);
   37                         Console.WriteLine("Reaction rate is " + res);
   38                         break;
   39                     case 3:
   40                         Console.WriteLine("Thank you");
   41                         break;
   42                 }	 	  	      	  	      			       	 	
   43             } while (choice != 3);
   44         }
   45     }
   46 }
ChemicalInventory.cs
    1 namespace ExperimentTracker   //DO NOT change the namespace name
    2 {
    3     public class ChemicalInventory    //DO NOT change the class name
    4     {
    5         //Implement your code here
    6         public bool RecordData(Experiment data)
    7         {
    8             if (data.Duration <= 0)
    9             {
   10                 return false;
   11             }
   12             else
   13             {
   14                 Program.ExperimentalData.Add(data);
   15                 return true;
   16             }
   17         }
   18 
   19         public double CalculateReactionRate(List<Experiment> experimentalData)
   20         {
   21             double duration = 0;
   22             int count = 0;
   23             foreach (var experiment in experimentalData)
   24             {
   25                 duration += experiment.Duration;
   26                 count++;
   27             }
   28             double res = count / duration;
   29             double roundedRes = Math.Round(res, 4);
   30 
   31             return roundedRes;
   32         }
   33 
   34     }
   35 }	 	  	      	  	      			       	 	
   36 
Experiment.cs
    1 namespace ExperimentTracker   //DO NOT change the namespace name
    2 {
    3     public class Experiment    //DO NOT change the class name
    4     {
    5         //DO NOT change the given code
    6         public string Name { get; set; }
    7         public int Duration { get; set; }
    8     }
    9 }
	
	
	
answer 3 
----------
Library.cs 
-------------
using System;
using System.Collections.Generic;

public class Library
{
    // This is the dictionary to hold genre and book titles
    public static Dictionary<string, LinkedList<string>> genreCatalog = new Dictionary<string, LinkedList<string>>();

    // Requirement 1: Add book title to genre
    public static void AddBookToGenre(string bookTitle, string genre)
    {
        if (genreCatalog.ContainsKey(genre))
        {
            genreCatalog[genre].AddLast(bookTitle);
        }
        else
        {
            LinkedList<string> books = new LinkedList<string>();
            books.AddLast(bookTitle);
            genreCatalog.Add(genre, books);
        }
    }

    // Requirement 2: Get books under a given genre
    public static LinkedList<string> GetBooksInGenre(string genre)
    {
        if (genreCatalog.ContainsKey(genre))
        {
            return genreCatalog[genre];
        }
        else
        {
            return new LinkedList<string>();
        }
    }
}


using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the number of books:");
        int n = int.Parse(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.WriteLine($"Enter the title of book {i}:");
            string title = Console.ReadLine();

            Console.WriteLine($"Enter the genre of book {i}:");
            string genre = Console.ReadLine();

            Library.AddBookToGenre(title, genre);
        }

        Console.WriteLine("Enter the genre to retrieve:");
        string searchGenre = Console.ReadLine();

        LinkedList<string> result = Library.GetBooksInGenre(searchGenre);

        if (result.Count == 0)
        {
            Console.WriteLine("No books found in the given genre");
        }
        else
        {
            Console.WriteLine($"Books under {searchGenre}:");
            foreach (var title in result)
            {
                Console.WriteLine(title);
            }
        }
    }
}


answer 4 
-----------
Program.cs
    1 namespace DockMasterHub   //DO NOT change the namespace name
    2 {
    3     public class Program    //DO NOT change the class name
    4     {
    5         public static void Main(string[] args) //DO NOT change the 'Main' method signature
    6         {
    7              //DO NOT change the given code
    8                 CargoDock dock = new CargoDock();
    9                 Console.WriteLine("Enter your account balance: ");
   10                 dock.AccountBalance= Convert.ToDouble(Console.ReadLine());
   11                 
   12                 Console.WriteLine("Enter the number of hours docked: ");
   13                 int hours = Convert.ToInt32(Console.ReadLine());
   14                 
   15                 Console.WriteLine("Enter the docking location (Commercial Dock, Industrial Dock, Fishing Dock): ");
   16                 dock.DockingLocation= Console.ReadLine();
   17                 
   18                 double charges = dock.CalculateDockingFee(hours);
   19                 if (charges == 0)
   20                 {
   21                     Console.WriteLine("Invalid hour or location");
   22                     return;
   23                 }
   24                 Console.WriteLine($"Docking charges: {charges}");
   25                 
   26                 Console.WriteLine("Do you want to proceed with the payment? (yes/no): ");
   27                 string choice = Console.ReadLine().ToLower();
   28                 
   29                 if (choice == "yes")
   30                 {
   31                     double remainingBalance = dock.ProcessPayment(charges);
   32                     if (remainingBalance == -1)
   33                     {
   34                         Console.WriteLine("Payment failed. Insufficient balance.");
   35                     }
   36                     else
   37                     {
   38                         Console.WriteLine($"Payment successful. Remaining account balance: {remainingBalance}");
   39                     }
   40                 }
   41                 else
   42                 {
   43                     Console.WriteLine("Transaction cancelled.");
   44                 }
   45           }
   46      }
   47 }
DockingSystem.cs
    1 namespace DockMasterHub   //DO NOT change the namespace name
    2 {
    3     public abstract class DockingSystem    //DO NOT change the class name
    4     {
    5         //DO NOT change the given code
    6         public double AccountBalance {  get; set; }
    7         public string DockingLocation {  get; set; }
    8         
    9         public abstract double CalculateDockingFee(int hours);
   10         public abstract double ProcessPayment(double amount);
   11     }
   12 }
CargoDock.cs
    1 namespace DockMasterHub   //DO NOT change the namespace name
    2 {
    3     public class CargoDock:DockingSystem  //DO NOT change the class name
    4     {
    5          
    6         //Implement your code here
    7         
    8          public override double CalculateDockingFee(int hours){
    9             double Dockingcharges=0;
   10             if(DockingLocation!= "Commercial Dock" && DockingLocation!= "Industrial Dock" && DockingLocation!= "Fishing Dock"){
   11                 return 0;
   12             }
   13             if(hours<=0){
   14                 return 0;
   15             }
   16             if(DockingLocation=="Commercial Dock"){
   17                 Dockingcharges=hours*200;
   18             }else if(DockingLocation=="Industrial Dock"){
   19                 Dockingcharges=hours*300;
   20             }else if(DockingLocation=="Fishing Dock"){
   21                 Dockingcharges=hours*100;
   22             }
   23             
   24             return Dockingcharges;
   25         }
   26         
   27         public override double ProcessPayment(double amount){
   28             // double Dockingcharges= CalculateDockingFee(Program.hours);
   29             double balance=0;
   30             Program P=new Program();
   31             if(amount<=AccountBalance){
   32                 balance=AccountBalance-amount;
   33                 return balance;
   34             }
   35             else{
   36                 return -1;
   37             }
   38         }
   39             
   40     }
   41 }
   
   
   answer 5
   ---------
   using System;

public class PlayerQualifier
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the number of rounds");
        int numRounds = Convert.ToInt32(Console.ReadLine());

        int[] points = new int[numRounds];

        for (int i = 0; i < numRounds; i++)
        {
            Console.WriteLine($"Enter the points for Round {i + 1}");
            points[i] = Convert.ToInt32(Console.ReadLine());
        }

        int maxSoFar = points[0];
        bool qualified = false;

        for (int i = 1; i < numRounds; i++)
        {
            if (points[i] > maxSoFar)
            {
                Console.WriteLine($"Congratulations! The player has qualified for the next round with a first maximum point of {points[i]} in Round {i + 1}");
                qualified = true;
                break;
            }
            else
            {
                maxSoFar = Math.Max(maxSoFar, points[i]);
            }
        }

        if (!qualified)
        {
            Console.WriteLine("No qualifying round found.");
        }
    }
}


 Explanation
Reads the number of rounds.

Collects points for each round.

Compares each round's point with all previous rounds.

Displays the first round where the player scores higher than any previous round.

If no such round exists, prints a message.
