Rank Finder 
--------------
    1 namespace TheRankFinder //DO NOT change the namespace name
    2 {
    3     public class Program //DO NOT change the class name
    4     {
    5         //Implement the methods here
    6          public int[] FindStudentsRank(int[,] stdMarks)
    7         {
    8             int numStudents = stdMarks.GetLength(0);
    9             int numSubjects = stdMarks.GetLength(1);
   10 
   11             int[] totalMarks = new int[numStudents];
   12             for (int i = 0; i < numStudents; i++)
   13             {
   14                 for (int j = 0; j < numSubjects; j++)
   15                 {
   16                     totalMarks[i] += stdMarks[i, j];
   17                 }
   18             }
   19 
   20             int[] ranks = new int[numStudents];
   21             for (int i = 0; i < numStudents; i++)
   22             {
   23                 int rank = 1;
   24                 for (int j = 0; j < numStudents; j++)
   25                 {
   26                     if (totalMarks[j] > totalMarks[i])
   27                     {
   28                         rank++;
   29                     }
   30                 }
   31                 ranks[i] = rank;
   32             }
   33             return ranks;
   34         }
   35 
   36         
   37         public static void Main(string[] args)  //DO NOT change the method signature
   38         {	 	  	      	  	      			       	 	
   39             //Implement your code here
   40             Program obj = new Program();
   41 
   42             Console.WriteLine("Enter the number of students");
   43             int numStudents = Convert.ToInt32(Console.ReadLine());
   44 
   45             int numSubjects = 5;
   46             int[,] marks = new int[numStudents, numSubjects];
   47 
   48             for (int i = 0; i < numStudents; i++)
   49             {
   50                 Console.WriteLine("Enter marks for Student " + (i + 1));
   51                 for (int j = 0; j < numSubjects; j++)
   52                 {
   53                     marks[i, j] = Convert.ToInt32(Console.ReadLine());
   54                 }
   55             }
   56 
   57             int[] result = obj.FindStudentsRank(marks);
   58 
   59             for (int i = 0; i < result.Length; i++)
   60             {
   61                 Console.WriteLine("Rank of student " + (i + 1) + " is " + result[i]);
   62             }            
   63         }
   64     }
   65 }
   66 	 	  	      	  	      			       	 	
   67 
   
   
  Max Points 
  ----------
    1 namespace JaggedArray //DO NOT change the namespace name
    2 {
    3     public class Program //DO NOT change the class name
    4     {
    5         //Implement the methods here
    6          public string FindWhoGotMaximumPoints(int[][] points)
    7         {
    8             int maxPoints = int.MinValue;
    9             string[] studentsWithMaxPoints = new string[points.Length];
   10             int count = 0;
   11 
   12             for (int i = 0; i < points.Length; i++)
   13             {
   14                 int totalPoints = 0;
   15                 for (int j = 0; j < points[i].Length; j++)
   16                 {
   17                     totalPoints += points[i][j];
   18                 }
   19 
   20                 if (totalPoints > maxPoints)
   21                 {
   22                     maxPoints = totalPoints;
   23                     studentsWithMaxPoints = new string[points.Length];
   24                     studentsWithMaxPoints[count++] = $"Student {i + 1}";
   25                 }
   26             }
   27             string result = string.Join("", studentsWithMaxPoints, 0, count);
   28             return result + " got maximum points";
   29         }
   30         
   31         public static void Main(string[] args)  //DO NOT change the method signature
   32         {
   33             //Implement your code here
   34              Program obj = new Program();
   35             Console.WriteLine($"Enter the number of students");
   36             int number = Convert.ToInt32(Console.ReadLine());
   37 
   38             int[][] data = new int[number][];
   39 
   40             for (int i = 0; i < data.Length; i++)
   41             {	 	  	      	  	      			       	 	
   42                 Console.WriteLine($"Enter the number of competitions attended by student {i + 1} ");
   43                 int dataCount = int.Parse(Console.ReadLine());
   44 
   45                 data[i] = new int[dataCount];
   46                 Console.WriteLine($"Enter the student {i + 1} points");
   47 
   48                 for (int j = 0; j < data[i].Length; j++)
   49                 {
   50                     data[i][j] = int.Parse(Console.ReadLine());
   51                 }
   52             }
   53 
   54             string result = obj.FindWhoGotMaximumPoints(data);
   55             Console.WriteLine(result);
   56         }
   57     }
   58 }
   59 
   
     LateComers detention 
	 ----------------------
   
    1 namespace Looping 
    2 {
    3     public class Program
    4     {
    5         public static void Main(string[] args)
    6         {
    7             // Implement your code here
    8              Console.WriteLine("Number of times:");
    9             int times = Convert.ToInt32(Console.ReadLine());
   10 
   11             Console.WriteLine("Enter the message:");
   12             string message = Console.ReadLine();
   13 
   14             for (int i = 0; i < times; i++)
   15             {
   16                 Console.WriteLine(message);
   17             }
   18         }
   19     }
   20 }
   
   
     Total Findings
	 ---------------
	 1 namespace TotalFinding 
    2 {
    3     public class Program
    4     {
    5         public static void Main(string[] args)
    6         {
    7             // Implement your code here
    8             Console.WriteLine("Enter the first number");
    9             int first=Convert.ToInt32(Console.ReadLine());
   10             Console.WriteLine("Enter the last number");
   11             int last = Convert.ToInt32(Console.ReadLine());
   12 
   13             int sum=0;            
   14             
   15             if(first !=last)
   16             {
   17                 for (int i = first; i <= last; i++)
   18                 {
   19                     sum += i;
   20                 }
   21             }
   22             else
   23             {
   24                 sum = first;
   25             }
   26             Console.WriteLine(sum);   
   27         }
   28     }
   29 }	 	  	      	  	      			       	 	
   30 
   
   conTVCompany 
   -----------------
   1 namespace DateEx2 //DO NOT change the namespace name
    2 {
    3     public class Program //DO NOT change the class name
    4     {
    5         //Implement the methods here
    6         public string CustomerFeedback(string  expectedDate, string installedDate)  
    7         {
    8             DateTime ed = DateTime.Parse(expectedDate);
    9             DateTime id = DateTime.Parse(installedDate);
   10             if(id<ed)
   11             {
   12                 return "VeryGood";
   13             }
   14             else if(id==ed)
   15             {
   16                 return "Good";
   17             }
   18             else if(id.AddDays(-3)<=ed)
   19             {
   20                 return "Average";
   21             }else if(id.AddDays(3)>=ed)
   22             {
   23                 return "Poor";
   24             }
   25             return null;
   26         }
   27         
   28         public string FindTheInstalledDay(string installedDate)
   29         {
   30             DateTime id = DateTime.Parse(installedDate);
   31              string day = id.DayOfWeek.ToString();
   32              return day;
   33         }
   34         
   35         public static void Main(string[] args)  //DO NOT change the method signature
   36         {
   37             //Implement your code here
   38             Console.WriteLine("Enter the expected date");
   39             string expectedDate = Console.ReadLine();
   40             Console.WriteLine("Enter the installed date");
   41             string installedDate = Console.ReadLine();
   42             Program obj = new Program();
   43             string res = obj.CustomerFeedback(expectedDate,installedDate);
   44             string day = obj.FindTheInstalledDay(installedDate);
   45             Console.WriteLine("Installed day : "+day);
   46             Console.WriteLine("Customer feedBack : "+res);
   47         }	 	  	      	  	  	 	    	  	 	
   48     }
   49 }
   50 
   
   Case File Management 
   ------------------------
   
   Program.cs
    1 namespace ListClass //DO NOT change the namespace name
    2 {
    3     public class Program //DO NOT change the class name
    4     {
    5         public static List<Case> CaseFile = new List<Case>();
    6         //Implement the methods here
    7         public bool AddToList(Case caseObj)
    8         {
    9             try
   10             {
   11                 CaseFile.Add(caseObj);
   12                 return true;
   13             }
   14             catch
   15             {
   16                 return false;
   17             }
   18         }
   19         public bool DeleteFromList(int caseNo)
   20         {
   21             Case caseToRemove = CaseFile.Find(c => c.CaseNo == caseNo);
   22             if (caseToRemove != null)
   23             {
   24                 CaseFile.Remove(caseToRemove);
   25                 return true;
   26             }
   27             return false;
   28         }
   29 
   30         public static void Main(string[] args)  //DO NOT change the method signature
   31         {
   32             //Implement your code here
   33             Program obj = new Program();
   34  
   35             Console.WriteLine("1. Add to the list");
   36             Console.WriteLine("2. Delete from the list");
   37             Console.WriteLine("Enter your choice");
   38             int choice = int.Parse(Console.ReadLine());
   39             if (choice == 1)
   40             {
   41                 Console.WriteLine("Enter the case no");
   42                 int caseNo = int.Parse(Console.ReadLine());
   43                 Console.WriteLine("Enter the case code");
   44                 string caseCode = Console.ReadLine();
   45                 Console.WriteLine("Enter the case content");
   46                 string caseContent = Console.ReadLine();
   47                 Console.WriteLine("Enter the date:");
   48                 DateTime date = DateTime.Parse(Console.ReadLine());
   49                 Case newCase = new Case
   50                 {
   51                     CaseNo = caseNo,
   52                     CaseCode = caseCode,
   53                     CaseContent = caseContent,
   54                     Date = date
   55                 };
   56                 bool result = obj.AddToList(newCase);
   57                 Console.WriteLine(result ? "Added successfully" : "Adding failed");
   58             }
   59             else if (choice == 2)
   60             {
   61                 Console.WriteLine("Enter the case no");
   62                 int caseNo = int.Parse(Console.ReadLine());
   63                 bool result = obj.DeleteFromList(caseNo);
   64                 Console.WriteLine(result ? "Deleted successfully" : "Deleting failed");
   65             }
   66             else
   67             {
   68                 Console.WriteLine("Invalid choice");
   69             }
   70         }
   71     }
   72 }
Case.cs
    1 namespace ListClass //DO NOT change the namespace name
    2 {
    3     public class Case //DO NOT change the class name
    4     {
    5         //Implement your code here
    6         public int CaseNo{get;set;}
    7         public string CaseCode{get;set;}
    8         public string CaseContent{get;set;}
    9         public DateTime Date{get;set;}
   10     }
   11 }
   12 
   
   course details 
   -------------------
   
   Program.cs
    1 namespace CourseDetails //DO NOT change the namespace name
    2 {
    3     public class Program //DO NOT change the class name    
    4     {
    5         //Implement the property here
    6         public static Dictionary<string, int> CourseDetails { get; set; } = new Dictionary<string, int>();
    7         
    8         public static void Main(string[] args)  //DO NOT change the method signature
    9         {
   10             //Implement your code here
   11             Course course = new Course();
   12             int choice = 0;
   13             
   14             do
   15             {
   16                 Console.WriteLine("1. Add Course Details");
   17                 Console.WriteLine("2. Remove Course Details");
   18                 Console.WriteLine("3. Sort Course By Fee");
   19                 Console.WriteLine("4. Exit");
   20                 Console.WriteLine("Enter your choice");
   21                 
   22                 choice = Convert.ToInt32(Console.ReadLine());
   23                 
   24                 switch (choice)
   25                 {
   26                     case 1:
   27                         Console.WriteLine("Enter the course name");
   28                         string name = Console.ReadLine();
   29                         
   30                         Console.WriteLine("Enter the course fee");
   31                         int fee = Convert.ToInt32(Console.ReadLine());
   32                         
   33                         course.AddCourseDetails(name, fee);
   34                         Console.WriteLine("Course details added successfully");
   35                         break;
   36                         
   37                     case 2:
   38                         Console.WriteLine("Enter the course name");
   39                         string removeName = Console.ReadLine();
   40                         
   41                         course.RemoveCourseDetails(removeName);
   42                         Console.WriteLine("Course details removed successfully");
   43                         break;
   44                         
   45                     case 3:
   46                         Dictionary<string, int> sortedCourses = course.SortCourseByFee();
   47                         
   48                         Console.WriteLine("Course Name");
   49                         Console.WriteLine("Fee");
   50                         foreach (var item in sortedCourses)
   51                         {
   52                             Console.WriteLine(item.Key);
   53                             Console.WriteLine(item.Value);
   54                         }
   55                         break;
   56                         
   57                     case 4:
   58                         break;
   59                         
   60                     default:
   61                         Console.WriteLine("Invalid choice");
   62                         break;
   63                 }
   64                 
   65             } while (choice != 4);
   66         }
   67     }
   68 }
   69 
Course.cs
    1 namespace CourseDetails //DO NOT change the namespace name
    2 {
    3     public class Course //DO NOT change the class name
    4     {
    5         //Implement your code here
    6         public void AddCourseDetails(string name, int fee)
    7         {
    8             Program.CourseDetails[name] = fee;
    9         }
   10         
   11         public void RemoveCourseDetails(string name)
   12         {
   13             if (Program.CourseDetails.ContainsKey(name))
   14             {
   15                 Program.CourseDetails.Remove(name);
   16             }
   17         }
   18         
   19         
   20         public Dictionary<string, int> SortCourseByFee()
   21         {
   22             return Program.CourseDetails.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
   23         }
   24         
   25     }
   26     
   27     
   28 }
   29 
