In C#, a virtual method is a method that can be overridden in a derived class. When a method is declared as virtual in a base class, it allows a derived class to provide its own implementation of the method.

The virtual keyword is useful in modifying a method, property, indexer, or event. When you have a function defined in a class that you want to be implemented in an inherited class(es), you use virtual functions. The virtual functions could be implemented differently in different inherited class and the call to these functions will be decided at runtime.

The following is a virtual function

public virtual int area() { }

virtual functions 
---------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtualfunctions
{
    class BaseClass
    {
        public void display()
        {
            Console.WriteLine("Base class display...");
        }
    }
    class SubClass:BaseClass
    {
        public void display()
        {
            Console.WriteLine("Sub class display...");
        }
    }
     class Program
    {
        static void Main(string[] args)
        {
            BaseClass bobj;
            bobj = new SubClass();
            bobj.display();
            Console.ReadLine();

        }
    }
}


as per above logic sub clas code should come but base class code is shown as it base class is hiding the funcitonlity of sub class so base class function make it virtual 



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtualfunctions
{
    class BaseClass
    {
        public virtual  void display()
        {
            Console.WriteLine("Base class display...");
        }
    }
    class SubClass:BaseClass
    {
        public override void display()
        {
            Console.WriteLine("Sub class display...");
        }
    }
     class Program
    {
        static void Main(string[] args)
        {
            BaseClass bobj;
            bobj = new SubClass();
            bobj.display();
            Console.ReadLine();

        }
    }
}

But what is the differnce between abstract class and virtual functions both casses i am overrding can you spot the difference 

in abstarct class sub class should compulsory override it otherwsie errro will come but in virtual functions you may override or you may not

error will not come example below code 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtualfunctions
{
    class BaseClass
    {
        public virtual  void display()
        {
            Console.WriteLine("Base class display...");
        }
    }
    class SubClass:BaseClass
    {
        //public override void display()
        //{
        //    Console.WriteLine("Sub class display...");
        //}
    }
     class Program
    {
        static void Main(string[] args)
        {
            BaseClass bobj;
            bobj = new SubClass();
            bobj.display();
            Console.ReadLine();

        }
    }
}

above i commneted the code i am not getting error same thing you will get error in abstract class  okay means u have to implment it 


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












static is a modifier means how i am using that function it tells or how i am using that varibale it tells 

static mean it will not change 

we have static varibles and we have static functions 

so one static variable one program and on static function one program we will see 

Static :
---------
A static variable will remeber its value .It will not referesh for every call.

Inside a class if there are static variables and static methods then all the objects 
of that class will share those static variables and methods .

A static memeber of a class can be called directly using a classname

Inisde a static function by default all memebers are static but if u want to use 
a non static function inside a static function u need to declare an object of a class and use it .

You can call all static methods and varibles directly by using a classname.varibale or function if u are outsid of the class 




A static function is called implictly .

Below is program on static variable .

here non static methods are nothing but instance methods okay ..


static variable demo
-----------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaticvariableDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class JointAccount
        {
           static int balance = 10000;

            public void withdraw(int amt)
            {
                balance = balance - amt;
                MessageBox.Show("The Current baalnce is " + balance);
;            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            JointAccount account = new JointAccount();
            account.withdraw(Convert.ToInt32(textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JointAccount account = new JointAccount();
            account.withdraw(Convert.ToInt32(textBox1.Text));

        }
    }
}

static functions 
----------------

static function demo (console app )(basic template) 
-----------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        int a = 1;
        public void count()
        {
            a = a + 1;
            Console.WriteLine($"The value of a : {a}");
        }
    }
     class Program
    {
        static void Main(string[] args)
        {



        }
    }
}

bassic program above so now what i will do now 

Non static to static  outside the class declare the object and use it declaring object is like taking permission 
------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        int a = 1;
        public void count()
        {
            a = a + 1;
            Console.WriteLine($"The value of a : {a}");
        }
    }
     class Program
    {
        static void Main(string[] args)
        {

            abcd obj = new abcd();
            obj.count();
            Console.ReadLine();

        }
    }
}


Non static to static inside the class we have to declare the object and use it like earleir 
----------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        int a = 1;
        public void count()
        {
            a = a + 1;
            Console.WriteLine($"The value of a : {a}");
        }
        static void Main(string[] args)
        {

            abcd obj = new abcd();
            obj.count();
            Console.ReadLine();

        }
    }
     class Program
    {
       
    }
}


static to static within the class directly i can use function 
---------------------------------------------------------------
// but u can see that non static variable is used in static function count by declaring object okay means taking permission 
// as non static to static even in varibale we have to declare and use it means taking permisiion is declaring object 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        int a = 1;
        public static void count()
        {
            abcd obj = new abcd();// here again non static variable is used in static function 
            obj.a = obj.a + 1;
            Console.WriteLine($"The value of a : {obj.a}");
        }
        static void Main(string[] args)
        {

           
           count();// drectly i can call 
            Console.ReadLine();

        }
    }
     class Program
    {
       
    }
}


static to static outisde the class we will class name 
-------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        int a = 1;
        public static void count()
        {
            abcd obj = new abcd();// here again non static variable is used in static function 
            obj.a = obj.a + 1;
            Console.WriteLine($"The value of a : {obj.a}");
        }
      
    }
     class Program
    {
        static void Main(string[] args)
        {


            abcd.count();// static to static outside the class use classname
            Console.ReadLine();

        }
    }
}

finally all static outside  class 
----------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        static int a = 1;
        public static void count()
        {
           
            a = a + 1;
            Console.WriteLine($"The value of a : {a}");
        }
      
    }
     class Program
    {
        static void Main(string[] args)
        {


            abcd.count();// static to static outside the class use classname
            Console.ReadLine();

        }
    }
}


finally all static  inside  class 
----------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staticfunctiondemo
{
    class abcd
    {
        static int a = 1;
        public static void count()
        {
           
            a = a + 1;
            Console.WriteLine($"The value of a : {a}");
        }
        static void Main(string[] args)
        {


            count();// directy can be called 
            Console.ReadLine();

        }

    }
     class Program
    {
       
    }
}


Delegates :
------------
delegates are function pointers .delegate means getting work done .
so in order to make you explain lambda expression i have to explain delegates so 

public void add(int x, int y) is function 

syntax:
-------
public delegate retuntypeoffunction delegatename (parameters in function) 

public delegate void mydelegate(int x, int y) is the delegate for function add 


here name of delegate is mydelegate and it is having same return type of function add 
and same parameters .



Delegates are function pointers and delegate means getting work done (just telling meaning) 

In delegates while creating return type of function should match with the function and no of paramters in side the 
function should match with  the numbers of parameters in the function then only it can point to that function 

so same deleagte pointing to multiple functions we call it as multi cast delegate 

delegate demo 1
-----------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegatedemo1
{
    internal class Program
    {
        public static void add(int x, int y)
        {
            Console.WriteLine($"The sum is :{x + y}");
        }

        public static int substract(int x, int y)
        {
            return (x - y);
        }

        public static int multiply(int x, int y)
        {
            return (x * y);
        }
        static void Main(string[] args)
        {
        }
    }
}

after creating delegate for the function add code is like this 

  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegatedemo1
{
    internal class Program
    {
        public static void add(int x, int y)
        {
            Console.WriteLine($"The sum is :{x + y}");
        }

        public static int substract(int x, int y)
        {
            return (x - y);
        }

        public static int multiply(int x, int y)
        {
            return (x * y);
        }

        // public delegate retuntypeoffunction delegatename(parameters in function)
        public delegate void mydelegate1(int x , int y);
        static void Main(string[] args)
        {
            mydelegate1 m1 = add;
            m1(12, 4);// running the delegate here 
            m1.Invoke(13, 6);// like this also okay 
            Console.ReadLine();

        }
    }
}

now another delegate i want to create for substract and multiply 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegatedemo1
{
    internal class Program
    {
        public static void add(int x, int y)
        {
            Console.WriteLine($"The sum is :{x + y}");
        }

        public static int substract(int x, int y)
        {
            return (x - y);
        }

        public static int multiply(int x, int y)
        {
            return (x * y);
        }

        // public delegate retuntypeoffunction delegatename(parameters in function)
        public delegate void mydelegate1(int x , int y);
        public delegate int mydelegate2(int x, int y);
        static void Main(string[] args)
        {
            mydelegate1 m1 = add;
            m1(12, 4);// running the delegate here 
            m1.Invoke(13, 6);// like this also okay 
                             //    mydelegate1 m2=substract// not pointing becasue return type is different 
                             // so create another delegate 
            mydelegate2 m2 = substract;
          Console.WriteLine($"substration is :{m2(12, 4)}");
            //now multiply is also having same return type i can do multicasting of delegate
            m2 += multiply;// multicasting means same delegate pointing to multiple functions

            Console.WriteLine($"Multiplication is :{m2(12, 9)}");

            mydelegate2 m3 = substract;
            Console.WriteLine($"{m3(15, 5)}");
            m3 += multiply;
          //  m3 -= multiply;if i add this it will not do multiple but for the same number as i am calling again it will just do substraction 
            Console.WriteLine($"{m3(15, 5)}");
            Console.ReadLine();

        }
    }
}

//Lambda expression : 
//-------------------------
//some times u want to execute the function in a single line when 
//	u think that inside that function lot of code is not there then i will
//	make that function run using lambda expression and this is very much used
//			   in MVC programming and linq programming


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// check versions in code file for this program 
namespace delegatedemo1
{
    internal class Program
    {
        //public static void add(int x, int y)
        //{
        //    Console.WriteLine($"The sum is :{x + y}");
        //    Console.WriteLine("hai");
        //}

        //publi/c static int substract(int x, int y)
        //{
        //    return (x - y);
        //}

        //public static int multiply(int x, int y)
        //{
        //    return (x * y);
        //}

        // public delegate retuntypeoffunction delegatename(parameters in function)
        public delegate void mydelegate1(int x , int y);
        public delegate int mydelegate2(int x, int y);
        static void Main(string[] args)
        {

            mydelegate1 m1 = (x, y) => { Console.WriteLine($"The sum is :{x + y}"); Console.WriteLine("hai"); };
            m1(12, 5);
            mydelegate2 m2 = (x, y) => { return (x - y); };
            Console.WriteLine($"The substaction:{m2(12, 4)}");
            m2 += (x, y) => { return (x * y); };
            Console.WriteLine($"The multiplication:{m2(12, 4)}");
            
            Console.ReadLine();

        }
    }
}



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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linqdemo1
{
     class Program
    {
        public static void  vowelscountinstring(string input)
        {
            var vowels = input.Where(x => "aeiou".Contains(x));
            var vowelscount = vowels.Count();
            Console.WriteLine("The string {0} is having {1} no of vowels ", input, vowelscount);
        }
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 12, 34, 56, 1, 3, 4, 6, 100, 123, 51, 39, 45 };
            string[] names = new string[] { "Kiran", "Kishore", "Mahesh", "Suresh", "Sita" };
            //give me numbers which are greater than 20 

            var query1 = from number in numbers where number > 20 select number;//query syntax 
            var query2 = numbers.Where(x => x > 20);// method syntax 

            Console.WriteLine("\n The numbers greater than 20 are:");
            foreach(int num in query1)// here query2 also u can put same output u will get 
            {
                Console.Write("{0,5}", num);
            }
            // give me all odd numbers 

            var query3 = from number in numbers where number % 2 != 0 select number;
            var query4 = numbers.Where(x => x % 2 != 0);
            Console.WriteLine("\n The odd numbers are : ");
            foreach(int num in query4)
            {
                Console.Write("{0,5}", num);
            }
            // give the all the names starting from capital K 

            var query5 = from name in names where name.StartsWith("K") select name;// query syntax
            var query6 = names.Where(x => x.StartsWith("K"));
            Console.WriteLine("\n Names starting with K :");
            foreach(string name1 in query6)
            {
                Console.WriteLine("{0}", name1);
            }
            // give me number of character count in each name 
            var query7 = from name in names select name;
            var query8 = names;
            Console.WriteLine("\n no  of characters count in each name is :");
            foreach(string name2 in query7)
            {
                Console.WriteLine("{0}--{1}", name2, name2.Length);
            }
            Console.WriteLine("\n enter a input string to count no of vowels in that string..");
            string input = Console.ReadLine();
            vowelscountinstring(input);
            Console.ReadLine();



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{

   public  class Customer
    {
        public int CustomerID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string City { set; get; }


    }

    public class CustomerRepository
    {

        public static List<Customer> Retrive()
        {
            List<Customer> custlist = new List<Customer>();

            custlist.Add(new Customer { CustomerID = 101, FirstName = "suresh", LastName = "babu", City = "Hyderabad" });
            custlist.Add(new Customer { CustomerID = 102, FirstName = "Mahesh", LastName = "naidu", City = "Mysore" });
            custlist.Add(new Customer { CustomerID = 103, FirstName = "Kranthi", LastName = "kumari", City = "Bangalore" });
            custlist.Add(new Customer { CustomerID = 104, FirstName = "Narendra", LastName = "Jha", City = "Delhi" });
            custlist.Add(new Customer { CustomerID = 105, FirstName = "Vithal", LastName = "Kumar", City = "Hyderabad" });

            return custlist;

            
        }



    }
    class Program
    {
        static void Main(string[] args)
        {


            var custlist = CustomerRepository.Retrive();
            var concatnames = from cust in custlist select cust.FirstName + " " + cust.LastName;
            Console.WriteLine("The complete name of customers :");
            Console.WriteLine("*************************************");
            foreach(var element in concatnames)
            {
                Console.WriteLine("{0,10}", element);

            }

            Console.WriteLine("Check customer presence enter id of customer ");
            int id = Convert.ToInt16(Console.ReadLine());
            
            var checkuser = from cust in custlist
                            where cust.custid == id
                            select cust;
            Customer foundcustomer = checkuser.FirstOrDefault();
            if (foundcustomer!=null )
            {
                Console.WriteLine("the person with this id is there and name is {0}",
                    foundcustomer.FirstName);
            }
            else
            {
                Console.WriteLine("the person is not available ...");
            }
            Console.ReadLine();
        }
    }
}

some more code ..


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CustomerTypeID { get; set; }
        public string EmailAdress { get; set; }
    }


    public class CustomerRepository
    {
        public static List<Customer> retrive()
        {
            List<Customer> custlist = new List<Customer>()
            {
                new Customer {
                                CustomerID =1,FirstName ="Raghavendra", LastName ="Ponde",
                                 EmailAdress="Raghuponde@yahoo.com" ,CustomerTypeID =1},

                              new Customer {CustomerID =2,FirstName ="Naresh" , LastName ="Ponde" ,
                              EmailAdress="naresh.1107@reddiffmail.com" ,CustomerTypeID =null},

                              new Customer {CustomerID =3,FirstName ="Gowri", LastName ="rani" ,
                              EmailAdress="gowri22@gmail.com",CustomerTypeID =1},

                              new Customer {CustomerID =4,FirstName ="gopinatha ", LastName ="rao",
                              EmailAdress="gopi#45@yahoo.com",CustomerTypeID =2}


                             };

            return custlist;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var custlist = CustomerRepository.retrive();
            // usage of ananomous types in two ways 
            var query = from cust in custlist where cust.CustomerTypeID == 1 select new
            {
                Name = cust.FirstName + " " + cust.LastName,

                Email=cust.EmailAdress
            };
            var query2 = custlist.Where(c => c.CustomerTypeID == 1).Select(c => new {Name=c.FirstName + " "+c.LastName,Email=c.EmailAdress });
            foreach(var item in query)
            {

                Console.WriteLine("Full Name is {0} and Email id is {1}", item.Name, item.Email);
            }
            foreach (var item in query2)
            {

                Console.WriteLine("Full Name is {0} and Email id is {1}", item.Name, item.Email);
            }
            Console.WriteLine("enter the customer id");
            int id = Convert.ToInt16(Console.ReadLine());
            Customer custfound = (from cust in custlist where cust.CustomerID == id select cust).FirstOrDefault();
            Customer custfound2 = custlist.Where(a => a.CustomerID == id).FirstOrDefault();
            Customer custfound3 = custlist.FirstOrDefault(c => c.CustomerID == id);
            Customer custfound4 = custlist.FirstOrDefault(c => { Console.WriteLine("hai");return c.CustomerID == id; });
            //for multiline statements we are using it ...

            Customer custfound5 = custlist.Where(a => a.CustomerTypeID == id).Skip(1).FirstOrDefault();
            // this will print me second value as matching value with that id first i will skip and next 
            // matching value i will put okay .

            if(custfound!=null)
            {
                Console.WriteLine("the customer  with Id= {0} is there and name is {1} {2} ",
                    id,custfound.FirstName,custfound.LastName);
            }
            else
            {

                Console.WriteLine("the person with this {0} is not there",id);
                   
            }

            if (custfound2 != null)
            {
                Console.WriteLine("the customer  with Id= {0} is there and name is {1} {2} ",
                    id, custfound2.FirstName, custfound2.LastName);
            }
            else
            {

                Console.WriteLine("the person with this {0} is not there", id);

            }

            if (custfound3 != null)
            {
                Console.WriteLine("the customer  with Id= {0} is there and name is {1} {2} ",
                    id, custfound3.FirstName, custfound3.LastName);
            }
            else
            {

                Console.WriteLine("the person with this {0} is not there", id);

            }

            if (custfound4 != null)
            {
                Console.WriteLine("the customer  with Id= {0} is there and name is {1} {2} ",
                    id, custfound4.FirstName, custfound4.LastName);
            }
            else
            {

                Console.WriteLine("the person with this {0} is not there", id);

            }

            if (custfound5 != null)
            {
                Console.WriteLine("the customer  with Id= {0} is there and name is {1} {2} ",
                    id, custfound5.FirstName, custfound5.LastName);
            }
            else
            {

                Console.WriteLine("the person with this {0} is not there", id);

            }

            Console.WriteLine("sorting diferent ways ");
            var sorbyname = custlist.OrderBy(c => c.FirstName);
            //u can check it from notes different functions are there just subsitite here directly and check it 
            Console.WriteLine("sort by firstname");
            Console.WriteLine("******************");
            foreach(var item in sorbyname)
            {
                Console.WriteLine(item.FirstName);
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

