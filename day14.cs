Arrays are static and when u want to store in a single variable lot of values or multiple values of same data type then arrays is used
positon will start from 0 and goes upto n-1 means if i delare a[5] it means postions or subscript will start from 0 and ends at 4 

array.Length will give me number of elements i can store in array 

if i declare a[5] and then a.Length will give me 5 elements i can store 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Arraysdemo
{
     class Program
    {
        static void PrintArray(int[] arr)
        {
            Console.WriteLine("\n prininting the array ..");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}  ");
            }
        }
        static void Main(string[] args)
        {
            // inifinite array here i am declaring also and intilizing also 
            int[] aa1 = new int[] { 12, 34, 55, 66, 71, 23 };
            int[] aa2 = new int[3] { 12, 34, 89 };//normal array where declaration and inilization 
            int[] aa3 = new int[4] ;// this is just declaring array is empty here 
            string[] names = new string[3] { "ravi", "mahesh", "sudha" };
            char[] chars = new char[3] { 'a', 'b', 'c', };

            int[] a = new int[5];//declared not initialized
            int i, j, sum = 0;
            Console.WriteLine("Enter values in arrray..");
            for(i=0;i<a.Length;i++)
            {
                Console.Write($"Enter element {i + 1}:");
                a[i]=Convert.ToInt32(Console.ReadLine());
            }
            PrintArray(a);
            PrintArray(aa1);
            Console.WriteLine("\n Sum of elements in array finding ");
            for (i = 0; i< a.Length;i++)
            {
                sum = sum + a[i];
            }
            Console.WriteLine($"\nThe sum of elements in array is {sum}");
            Console.ReadLine();
        }
    }
}


I can use another loop to print the array which is foreach loop which is read only loop means i cannot modify the array if i am using for each loop but using for loop i can modify the array also let us see by puttin some code in above programming whic i am doing 

 Console.WriteLine("\nprinting the array using for each loop");
 foreach (int ele in a)
 {
     Console.Write($"{ele}  ");
 }
 Console.WriteLine("\n modiifying array using for loop..");
 for(i=0; i< a.Length; i++)
 {
     a[i] = a[i] * 3;
 }
 PrintArray(a);
 
 Searching the ele in array 
 -------------------------
  Console.WriteLine("\n enter the element to be searched in arrray ");
 int ele1=Convert.ToInt32(Console.ReadLine());
 int flag = 0;
 for (i = 0; i< a.Length;i++)
 {
     if (a[i] == ele1)
     {
         Console.WriteLine($"The element {ele1} is found at psosition {i + 1}");
         flag = 1;
         break;
     }
 }
 if(flag==0)
 {
     Console.WriteLine($"The element {ele1} is not there in array ");
 }
 
 sorting the array 
 ------------------
 Console.WriteLine("\n Sorting an array..using buble sort.");
 
 for(i=0;i<a.Length-1;i++)
 {
     for (j = i + 1; j < a.Length; j++)
     {
         if (a[i] > a[j])
         {
             int temp;
             temp = a[i];
             a[i] = a[j];
             a[j] = temp;
         }
     }
 }
 Console.WriteLine("\nAfer sorting printing array ....");
 PrintArray(a);
 Console.WriteLine("\n uisng in built function of Array to do the task like revese,sort");
 Array.Reverse(a);
 PrintArray(a);
 
 using user defined class and puttng final code 
  ----------------------------------------------
  using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Arraysdemo
{
   public  class Customer
    {
        public  int customerid ;
        
        public string customername;
    }
     class Program
    {
        static void PrintArray(int[] arr)
        {
            Console.WriteLine("\n prininting the array ..");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}  ");
            }
        }
        static void Main(string[] args)
        {
          
          
            // inifinite array here i am declaring also and intilizing also 
            int[] aa1 = new int[] { 12, 34, 55, 66, 71, 23 };
            int[] aa2 = new int[3] { 12, 34, 89 };//normal array where declaration and inilization 
            int[] aa3 = new int[4] ;// this is just declaring array is empty here 
            string[] names = new string[3] { "ravi", "mahesh", "sudha" };
            char[] chars = new char[3] { 'a', 'b', 'c', };

            int[] a = new int[5];//declared not initialized
            int i, j, sum = 0;
            Console.WriteLine("Enter values in arrray..");
            for(i=0;i<a.Length;i++)
            {
                Console.Write($"Enter element {i + 1}:");
                a[i]=Convert.ToInt32(Console.ReadLine());
            }
            PrintArray(a);
            PrintArray(aa1);
            Console.WriteLine("\n Sum of elements in array finding ");
            for (i = 0; i< a.Length;i++)
            {
                sum = sum + a[i];
            }
            Console.WriteLine($"\nThe sum of elements in array is {sum}");
            Console.WriteLine("\nprinting the array using for each loop");
            foreach (int ele in a)
            {
             //  ele= ele + 1; //modiying not possible
                Console.Write($"{ele}  ");
            }
            Console.WriteLine("\n modiifying array using for loop..");
            for(i=0; i< a.Length; i++)
            {
                a[i] = a[i] * 3;
            }
            
            PrintArray(a);
            Console.WriteLine("\n enter the element to be searched in arrray ");
            int ele1=Convert.ToInt32(Console.ReadLine());
            bool flag = true;
            for (i = 0; i< a.Length;i++)
            {
                if (a[i] == ele1)
                {
                    Console.WriteLine($"The element {ele1} is found at psosition {i + 1}");
                    flag = false;
                    break;
                }
                
            }
            if (flag)
            {
                Console.WriteLine($"The element {ele1} is not there in array ");
            }
            Console.WriteLine("\n Sorting an array..using buble sort.");
            
            for(i=0;i<a.Length-1;i++)
            {
                for (j = i + 1; j < a.Length; j++)
                {
                    if (a[i] > a[j])
                    {
                        int temp;
                        temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
            Console.WriteLine("\nAfer sorting printing array ....");
            PrintArray(a);
            Console.WriteLine("\n uisng in built function of Array to do the task like revese,sort");
            Array.Reverse(a);
            PrintArray(a);
          
            Customer[] custlist = new Customer[3];
            Console.WriteLine("\nEnter Customers..");
            for(i=0;i<custlist.Length;i++)
            {
                Customer c = new Customer();
                Console.WriteLine($"Enter Customer {i + 1} details");
                Console.WriteLine("Enter customer id :");
                c.customerid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter customer name:");
                c.customername = Console.ReadLine(); 
                custlist[i] = c;
            }

            Console.WriteLine("\nPrinting customer array ...");
            foreach(Customer cust in custlist)
            {
                Console.WriteLine($"{cust.customerid}--{cust.customername}");
            }
            Console.ReadLine();
        }
    }
}

2darraydemos
-------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2darraydemos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i, j, sum = 0;
            Console.WriteLine("----------------------------");
            for(i=1;i<=5;i++)
            {
                for(j=1;j<=i; j++)
                {
                    Console.Write($"{j}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------");
            int[,] a = new int[3,3];//declaring 2d array 
           
            int[,,] aa = new int[3, 3, 3];// 3d array where height is also there 

            Console.WriteLine("enter elements in 2d array..");
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.Write($"enter [{i + 1},{j + 1}] element:");
                    a[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("\nPrinting an array");
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.Write($"{a[i, j]}  ");

                }
                Console.Write("\n");
            }

            Console.WriteLine("\n sum of elelemts in matrix");

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {

                    sum = sum + a[i, j];
                }

            }
            Console.WriteLine($"\nn the sum is {sum}");
            // if u dont know the length of 2 d aaary 

            Console.WriteLine("enter array elements in 2d array ");
            for (i = 0; i < a.GetLength(0); i++)
            {
                for (j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write($"enter [{i + 1},{j + 1}] element :  ");
                    a[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.ReadLine();
        }
    }
}

Jagged Array
----------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaggedArraydemo
{
     class Program
    {
        static void Main(string[] args)
        {
            int i, j;
            int[][] jgArray = new int[4][];
            jgArray[0] = new int[1] { 100 };
            jgArray[1] = new int[3] { 12, 34, 65 };
            jgArray[2] = new int[2] { 120, 45 };
            jgArray[3] = new int[5] { 12, 33, 44, 55, 66 };

            Console.WriteLine("\n Displaying jagged array");
            for(i=0;i<jgArray.Length;i++)
            {
                Console.WriteLine($"I am in row :{i + 1} and having {jgArray[i].Length} elelemts ");
                for(j=0;j<jgArray[i].Length;j++)
                {
                    Console.WriteLine($"{jgArray[i][j]}");
                }
            }
            Console.ReadLine();

        }
    }
}
-------if u want to read this jagged array also then this program-------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaggedarray
{
     class Program
    {
        static void Main(string[] args)
        {
            int[][] jgArray=new int[4][];
            int i, j;

            //jgArray[0] = new int[2] { 12, 34 };
            //jgArray[1] = new int[1] { 1000 };
            //jgArray[2] = new int[4] { 20,78,14,76 };
            //jgArray[3] = new int[3] { 11,33,51};
            Console.WriteLine("read from the user ..");
            for (i = 0; i < jgArray.Length; i++)
            {
                Console.WriteLine($"\n I am in row :{i + 1} asking u to enter  ");
                   
                Console.WriteLine("\nenter columns or elemnts in the row ");
                int colsize = Convert.ToInt32(Console.ReadLine());
                jgArray[i] = new int[colsize];
                for (j = 0; j < jgArray[i].Length; j++)
                {
                    Console.WriteLine($"\nenter element at [{i+1},{j+1}]");
                    jgArray[i][j] = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine();
            }
            //displayng jagged array 
            Console.WriteLine("\n displaying jagged array ");
            for(i=0;i<jgArray.Length;i++)
            {
                Console.WriteLine($"I am in row : {i + 1} an having {jgArray[i].Length}");
                for(j=0;j<jgArray[i].Length;j++)
                {
                    Console.WriteLine($"{jgArray[i][j]}  ");
                }
            }

            Console.ReadLine();
        }
    }
}

C#.net parameters :
-------------------
1)value type parameters
2)reference type parameters


value type parameters:
----------------------
Any change in formal paramters is not reflected in actual parameters is called value type parameters.

reference type parameters:
--------------------------
any change in formal parameters is reflected in actual parameters is called as reference type parameters.


All primitive data types like int,double,float,char are call by value only  

but classess and its object ,delegates,strings are referecne types they refer to some address in memeory they will point something



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace callbyvalueandreferences
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //global section
        class swapdemo
        {
            public void swapexample1(int x, int y)
            {
                int temp;
                temp = x;
                x= y;
                y= temp;
            }

            public void swapexample2(ref int x,ref int y)
            {
                int temp;
                temp = x;
                x = y;
                y = temp;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int a, b;
            a = 10;
            b = 20;
            MessageBox.Show("Before swapping ...");
            MessageBox.Show("A=" + a + "\nB=" + b);
            swapdemo obj = new swapdemo();
            obj.swapexample1(a, b);
            MessageBox.Show("After swapping ...");
            MessageBox.Show("A=" + a + "\nB=" + b);
         
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a, b;
            a = 10;
            b = 20;
            MessageBox.Show("Before swapping ...");
            MessageBox.Show("A=" + a + "\nB=" + b);
            swapdemo obj = new swapdemo();
            obj.swapexample2(ref a, ref b);
            MessageBox.Show("After swapping ...");
            MessageBox.Show("A=" + a + "\nB=" + b);
        }
    }
}

Boxing and UnBoxing 
-----------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingandUnboxing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //value type is going inside ref type then it is called boxing 
            //while displaying ref type which is storing value type it is called 
            //unboxing 
            // now this process of boxing and unboxing should happen for same type of
            // data types compatibity should match 
            // means int x=45 i cannot put it in string eventhough string is reference type 

            object objone;
            int numberone;
            numberone = 77;
            objone = numberone;//boxing

            Console.WriteLine($"objectone is {objone}");// internally it will unbox means while displaying
                                                        // object will unwrap the data and display which is unboxin we call it as unboxing 

            // doing it with string data type 
            // Boxing: Converting a string value to an object type
            string originalString = "Hello, Boxing and Unboxing!";
            object boxedString = originalString;

            Console.WriteLine($"Boxed string:  {boxedString}");//unboxing

            // here i didnt faced the problem of typecasting above  becasue it is going into base 
            //data type object 
            // below is also doing unboxing but i am going from larger house to smaller house so typecastng
            // needed 
            int unboxint =(int) objone;// unboxing only but using typecasting
            string unboxedString = (string)boxedString;


            Console.WriteLine($"{unboxint}-- {unboxedString}");

            Console.ReadLine();

        }
    }
}


StringInBuiltFunctions 
--------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringInBuiltFunctions
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //String s1;
            //string s2;
            // here if i say s1. same methods i will se what i will see in s2. 
            // also if i say String. or string. same methods are there so both are same only 
            // dont get confuse ...so strings are referecne type only ...

            // how string we will learn later now let us see just some in built functions of 
            // string ..
            // one slide is stringlist there all methods and properties of strings are there 
            // which i will implent now 
            string str = "    Hello World      ";
            string str2 = "Hello";
            string str3 = "world";
            //properties and fields

            string sample = "";
            string sample2=string.Empty;// like this also i can use 

            //chars proeprty means index prerties in each string or any string i want to 
            // know in this position which character is there then i will use chars means 
            // i will not write chars using [] this thing is chars only 
            Console.WriteLine($"chars means : 6th chacatr in str:{str[6]}--first character in str2:{str2[0]}");
            Console.WriteLine($"Length :{str.Length}");

            // Modifying
            Console.WriteLine("\nModifying:");
            Console.WriteLine($"Insert: {str.Insert(6,"Beautiful ")}");
            Console.WriteLine($"Remove: {str.Remove(7,4)}");
            Console.WriteLine($"Replace: {str.Replace("World", "C#")}");
            Console.WriteLine($"Trim: '{str.Trim()}'");
            Console.WriteLine($"TrimStart: '{str.TrimStart()}'");
            Console.WriteLine($"TrimEnd: '{str.TrimEnd()}'");
            Console.WriteLine($"PadLeft: '{str2.PadLeft(15, '*')}'");
            Console.WriteLine($"PadRight: '{str2.PadRight(10, '-')}'");
            Console.WriteLine($"ToUpper: {str3.ToUpper()}");
            Console.WriteLine($"ToLower: {str2.ToLower()}");

            // extracting 
            Console.WriteLine("\n Extracting ...");
            string name = "Raghavendra";
            string name2 = "raghavendra kumar singh";
            Console.WriteLine($"Subsstring :{name.Substring(0,6)}");
            Console.WriteLine($"split :{string.Join(",", name2.Split(' '))}");
            Console.WriteLine($"Fullname : {string.Join("-", "ravi", "kumar")}");

            // Formatting
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\nFormatting:");
            double number = 12345.6789;
            Console.WriteLine($"just tostring will convert it into string : {number.ToString()}");//just string 
            Console.WriteLine($"US Currency: {number.ToString("C", new CultureInfo("en-US"))}");
            Console.WriteLine($"Indian Currency: {number.ToString("C", new CultureInfo("en-IN"))}");
            Console.WriteLine($"Euro Currency: {number.ToString("C", new CultureInfo("fr-FR"))}");

            Console.WriteLine($"Exponential: {number.ToString("E")}");
            Console.WriteLine($"General: {number.ToString("G")}");
            Console.WriteLine($"Percentage: {number.ToString("P")}");
            DateTime date = DateTime.Now;
            Console.WriteLine($"Short Date: {date.ToString("d")}");
            Console.WriteLine($"Long Date: {date.ToString("D")}");
            Console.WriteLine($"Short Time: {date.ToString("t")}");
            Console.WriteLine($"Long Time: {date.ToString("T")}");

            // Searching
            Console.WriteLine("\nSearching:");
            string samplename = "Kiran Hello!";
            Console.WriteLine($"StartsWith 'K': {samplename.StartsWith("K")}");
            Console.WriteLine($"EndsWith '!': {samplename.EndsWith("!")}");
            Console.WriteLine($"IndexOf 'Hello': {samplename.IndexOf("Hello")}");
            Console.WriteLine($"Contains 'ran': {samplename.Contains("ran")}");

            // comparing 

            Console.WriteLine("\n Comparing");
            string ss1 = "daghu";
            string ss2 = "raghu";

            string kk = "solve";
            string kk2 = "solve";
            Console.WriteLine($"compare :{string.Compare(ss1, ss2)}");   //<0 means ss1 is smaller than ss2 
            Console.WriteLine($"compare :{string.Compare(ss2, ss1)}");// > 0 means ss2 is largrr comes after ss1 
            Console.WriteLine($"compare :{ss1.CompareTo(ss2)}");   //<0 means ss1 is smaller than ss2 
           
            Console.WriteLine($"Equals :{kk2.Equals(kk)}");//checks the content inside string 
           
            Console.ReadLine();
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StringDemo1
{
    // array of chars we call it as string it can be used as 
    // data type or as a class also two things important 
    // strings are immutable means i cannot change i can override 
    // the same string by putting updated value but i cannot change
    // the string and string are referecne type ..
    //but string builder is not immutable it is changable 
    // we should not declare strings simply as it occupies memory 
    // so use string builder one time and in that object only append 
    // values ...
    // put debugger by clicking the side bar 
    // F11 means line by line 
    //F10 means skips function 
    // shift + f11 means come out of the function 
    class Program
    {
        public static void concat1(string s1)
        {
            string st = "world";
            s1 = st + s1;
        }

        public static void concat2(StringBuilder s2)
        {
            s2.Append("everyone");
        }

        public static void equalsexample()
        {

            string s1 = "hello";
            string s2 = "hello";
            string s3 = "kkkk";
            Console.WriteLine("{0}", s1.Equals(s2));
            s2 = s3;
            Console.WriteLine("{0}", s1.Equals(s2));
            Console.WriteLine("{0}", (s1==s2));
            Console.WriteLine("{0}", (s2==s3));



        }
        static void Main(string[] args)
        {
            //string str;
            //str = "Hello every body welcome to .net ";
            //Console.WriteLine("{0}", str);
            //string firstname;
            //string middlename;
            //string lastname;
            //Console.WriteLine("enter firstname :");
            //firstname = Console.ReadLine();
            //Console.WriteLine("enter middle name ");
            //middlename = Console.ReadLine();
            //Console.WriteLine("enter last name ");
            //lastname = Console.ReadLine();
            //string fullname = firstname + " " + middlename + " " + lastname;
            //string fullname2 = string.Concat(firstname, " ", middlename, " ", lastname);
            //Console.WriteLine("{0}", fullname.ToUpper());
            //Console.WriteLine("{0}", fullname2.ToLower());
            //Console.WriteLine("enter new first name to replace ...");
            //string newfirstname = Console.ReadLine();
            //Console.WriteLine("The new fullname is " + fullname.Replace(firstname, newfirstname));
            //// extract middle name 
            //Console.WriteLine("The midde name is " + fullname.Substring(5, 5));
            // strings are immutable 

            string s1 = "hello";
            StringBuilder s2 = new StringBuilder("hai");
            concat1(s1);
            concat2(s2);
            Console.WriteLine("{0}--{1}", s1, s2);
            equalsexample();

            string[] weekdays = {"Monday","Tuesday","wednesday","Thursday","Friday",
                "Saturday"};
            // i want like this Monday ,Tuesday,Wednesday,
            // Thursday,Friday and Stautday 

            StringBuilder sb = new StringBuilder();
           for(int i=0;i<weekdays.Length;i++)
            {
                sb.Append(weekdays[i]);
                if(i<weekdays.Length-2)
                {
                    sb.Append(',');
                }
                else if(i==weekdays.Length-2)
                {
                    sb.Append(" and ");
                }
            }
           Console.WriteLine(sb.ToString());
            Console.ReadLine();

            
        }
    }
}


Nullable Types in C#
----------------------
What are Nullable Types?
In C#, value types (like int, double, bool, etc.) cannot be null by default. However, you can use Nullable<T> or the shorthand T? to allow value types to store null

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //What are Nullable Types?
            //In C#, value types (like int, double, bool, etc.) cannot be
            //null by default. However, you can use Nullable<T> or the shorthand T?
            //to allow value types to store null

            //  int number = null;// not allowing ..as it is not possible 

            // Nullable integer
            int? number = null;

            // Nullable boolean
            bool? isAvailable = null;

            // Nullable double
            double? price = 99.99;// here i can put null also and also number optional

            // Checking if value exists
            if (number.HasValue)
            {
                Console.WriteLine("Number: " + number.Value);
            }
            else
            {
                Console.WriteLine("Number is null.");
            }

            Console.WriteLine("Another way of declaring nullable types ..");
            // Using Nullable<T>
            Nullable<int> number1 = null;

            // Alternative shorthand syntax (equivalent to Nullable<int>)
            int? anotherNumber = 20;

            // Checking if value exists
            if (number1.HasValue)
            {
                Console.WriteLine("Number: " + number1.Value);
            }
            else
            {
                Console.WriteLine("Number1 is null.");
            }

            Console.WriteLine("Another Number: " + anotherNumber);

            // Using GetValueOrDefault()
            Console.WriteLine($"Number (default 100 if null):  {number1.GetValueOrDefault(100)}");
            Console.ReadLine();

        }
    }
}

Collections:
------------
Collection means means collection of objects they are dynamic and they 
are actually classes and have lot of inbuilt methods defined in it 
we have two types of collections 

one is genric type : type safety is included you will use <T> kind of symbols 
to make the concept genric means general 

another is non generic type : anything can be added into the list like 
int ,double ,class obj ,char anything it can take and it will store in the form 
of objects

eg :Array list is non generic type when u are using non generic u have to use 
     var keyword why because while displaying different data types conflict will  occur 

   foreach( var ele in NonGenriccollection )
    {



   }
   
An ArrayList in C# is a non-generic collection that can store elements of any data type, unlike the strongly-typed List<T> generic collection. ArrayList is found in the System.Collections namespace. It automatically resizes as you add more elements, making it very flexible for situations where you do not know the size of the collection in advance or need to store different types of data.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NonGeneric_ArrayList
{
     class Program
    {
        static void Main(string[] args)
        {
            ArrayList obj = new ArrayList();
            obj.Add(1);
            obj.Add(true);
            obj.Add(DateTime.Now);
            obj.Add("Raghvendra");
            obj.Add(123.46);

            Console.WriteLine($"No of elements :{obj.Count}");
            Console.WriteLine($"\n capacity: {obj.Capacity}\n\n");
            foreach(var ele in obj)// here i had used var which means variant type what
                                   //it stores it becomes that data type
            {
                Console.WriteLine($"{ele}  ");
;            }
            int[] fourmore = new int[] { 10, 20, 30, 40 };
            obj.AddRange(fourmore);
            Console.WriteLine($"\nNo of elements :{obj.Count}");
            Console.WriteLine($"\n capacity: {obj.Capacity}\n\n");
            foreach (var ele in obj)// here i had used var which means variant type what
                                    //it stores it becomes that data type
            {
                Console.WriteLine($"{ele}  ");
                ;
            }

            obj.Insert(0, "first");
            obj.RemoveAt(3);
            int[] threemore = new int[] { 100, 200, 300, 40 };
            obj.InsertRange(4, threemore);
            Console.WriteLine($"\nNo of elements :{obj.Count}");
            Console.WriteLine($"\n capacity: {obj.Capacity}\n\n");
            foreach (var ele in obj)// here i had used var which means variant type what
                                    //it stores it becomes that data type
            {
                Console.WriteLine($"{ele}  ");
                ;
            }
            Console.ReadLine();

        }
    }
}


A Hashtable in C# is a collection that stores key-value pairs. It is similar to a dictionary but is not type-safe, meaning it can store any object as both key and value. The Hashtable class is found in the System.Collections namespace.

Let's create a simple example to demonstrate how to use a Hashtable in C#. This example will cover adding, removing, and retrieving items, as well as iterating through the Hashtable.

Scenario: Storing and Managing User Information
In this example, we will create a Hashtable to store user information, where the user ID (an integer) is the key, and the user name (a string) is the value. We will demonstrate basic operations like adding, accessing, modifying, and deleting items in the Hashtable.

using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        // Initialize a new Hashtable
        Hashtable users = new Hashtable();

        // Adding key-value pairs to the Hashtable
        users.Add(1, "Alice");
        users.Add(2, "Bob");
        users.Add(3, "Charlie");

        // Display all key-value pairs in the Hashtable
        Console.WriteLine("All users in the Hashtable:");
        foreach (DictionaryEntry entry in users)
        {
            Console.WriteLine($"User ID: {entry.Key}, User Name: {entry.Value}");
        }

        // Accessing an item using a key
        Console.WriteLine("\nAccessing user with ID 2:");
        if (users.ContainsKey(2))
        {
            Console.WriteLine($"User ID: 2, User Name: {users[2]}");
        }
        else
        {
            Console.WriteLine("User ID 2 not found.");
        }

        // Modifying an item in the Hashtable
        Console.WriteLine("\nModifying user with ID 3:");
        if (users.ContainsKey(3))
        {
            users[3] = "Chuck";
            Console.WriteLine($"User ID: 3, New User Name: {users[3]}");
        }

        // Removing an item from the Hashtable
        Console.WriteLine("\nRemoving user with ID 1:");
        if (users.ContainsKey(1))
        {
            users.Remove(1);
            Console.WriteLine("User ID 1 removed.");
        }

        // Display all key-value pairs in the Hashtable after removal
        Console.WriteLine("\nAll users in the Hashtable after removal:");
        foreach (DictionaryEntry entry in users)
        {
            Console.WriteLine($"User ID: {entry.Key}, User Name: {entry.Value}");
        }

        Console.ReadLine();
    }
}


All users in the Hashtable:
User ID: 1, User Name: Alice
User ID: 2, User Name: Bob
User ID: 3, User Name: Charlie

Accessing user with ID 2:
User ID: 2, User Name: Bob

Modifying user with ID 3:
User ID: 3, New User Name: Chuck

Removing user with ID 1:
User ID 1 removed.

All users in the Hashtable after removal:
User ID: 2, User Name: Bob
User ID: 3, User Name: Chuck



A SortedList in C# is a collection of key-value pairs that are sorted by the keys. It combines the features of a Hashtable and a SortedDictionary, where the elements are accessible by both key and index and are automatically sorted by the key. SortedList is part of the System.Collections namespace.

Let's create a simple example to demonstrate how to use a SortedList in C#. This example will cover adding elements, accessing elements, modifying elements, removing elements, and iterating through the SortedList.

Scenario: Managing a Sorted List of Students
In this example, we will create a SortedList to store students' information where the key is the student ID (integer) and the value is the student name (string). The list will automatically sort students by their IDs.

using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        // Initialize a new SortedList
        SortedList students = new SortedList();

        // Adding elements to the SortedList
        students.Add(102, "Alice");
        students.Add(101, "Bob");
        students.Add(104, "Charlie");
        students.Add(103, "David");

        // Display all elements in the SortedList
        Console.WriteLine("All students in the SortedList:");
        foreach (DictionaryEntry entry in students)
        {
            Console.WriteLine($"Student ID: {entry.Key}, Student Name: {entry.Value}");
        }

        // Accessing elements by key
        Console.WriteLine("\nAccessing student with ID 101:");
        if (students.ContainsKey(101))
        {
            Console.WriteLine($"Student ID: 101, Student Name: {students[101]}");
        }
        else
        {
            Console.WriteLine("Student ID 101 not found.");
        }

        // Modifying an element
        Console.WriteLine("\nModifying student with ID 104:");
        if (students.ContainsKey(104))
        {
            students[104] = "Chuck";
            Console.WriteLine($"Student ID: 104, New Student Name: {students[104]}");
        }

        // Removing an element
        Console.WriteLine("\nRemoving student with ID 102:");
        if (students.ContainsKey(102))
        {
            students.Remove(102);
            Console.WriteLine("Student ID 102 removed.");
        }

        // Display all elements in the SortedList after removal
        Console.WriteLine("\nAll students in the SortedList after removal:");
        foreach (DictionaryEntry entry in students)
        {
            Console.WriteLine($"Student ID: {entry.Key}, Student Name: {entry.Value}");
        }

        // Accessing elements by index
        Console.WriteLine("\nAccessing student by index 0:");
        Console.WriteLine($"Student ID: {students.GetKey(0)}, Student Name: {students.GetByIndex(0)}");

        Console.ReadLine();
    }
}

All students in the SortedList:
Student ID: 101, Student Name: Bob
Student ID: 102, Student Name: Alice
Student ID: 103, Student Name: David
Student ID: 104, Student Name: Charlie

Accessing student with ID 101:
Student ID: 101, Student Name: Bob

Modifying student with ID 104:
Student ID: 104, New Student Name: Chuck

Removing student with ID 102:
Student ID 102 removed.

All students in the SortedList after removal:
Student ID: 101, Student Name: Bob
Student ID: 103, Student Name: David
Student ID: 104, Student Name: Chuck

Accessing student by index 0:
Student ID: 101, Student Name: Bob

so there are some collections which are purely non generic and some are purely generic and some are both like stack ,queue etc 


Now let us go with the concept of generics 
------------------------------------------

Generics:
---------
Generics means code reuse dont write the same code repeteadely put the code inside a method and call the method repeatedly which is called as generic method 
and in the same manner dont write the same method repeteadly put the methods inside a class and use the class repeteadly called as Generic class 
when you are using generics type safety is ensured 

first version of program without genric method 
------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{
    internal class Program
    {
        public static void swap(ref int x, ref int y)
        {
            int temp;
            temp = x;
            x = y;
            y = temp;
        }
        public static void swap(ref DateTime x, ref DateTime y)
        {
            DateTime temp;
            temp = x;
            x = y;
            y = temp;
        }

        public static void swap(ref string st1, ref string st2)
        {
            string temp;
            temp = st1;
            st1 = st2;
            st2 = temp;
        }
        static void Main(string[] args)
        {

            int x = 10;
            int y = 20;
            Console.WriteLine("\n Before swapping integers");
            Console.WriteLine($"x={x}\n y={y}");
            swap(ref x, ref y);
            Console.WriteLine("\n after swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(2);
            Console.WriteLine("\n Before swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");
            swap(ref date1, ref date2);
            Console.WriteLine("\n after swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");
            Console.ReadLine();
        }
    }
}


Now just observe above program 

for 3 types i had written 3 methods if 10 types are there ten methods i cannot write so i will write a genric method for this one 

add one class with the name Helper.cs in the project and add one generic method like this ...here T means any type is allowed 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{
    class Helper
    {
        public static void swap<T>(ref T x, ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }

    }
}

then comment the methods in program.cs 

and use Helper clas method for all three functions 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{
    internal class Program
    {
        //public static void swap(ref int x, ref int y)
        //{
        //    int temp;
        //    temp = x;
        //    x = y;
        //    y = temp;
        //}
        //public static void swap(ref DateTime x, ref DateTime y)
        //{
        //    DateTime temp;
        //    temp = x;
        //    x = y;
        //    y = temp;
        //}

        //public static void swap(ref string st1, ref string st2)
        //{
        //    string temp;
        //    temp = st1;
        //    st1 = st2;
        //    st2 = temp;
        //}
        static void Main(string[] args)
        {

            int x = 10;
            int y = 20;
            Console.WriteLine("\n Before swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

            Helper.swap<int>(ref x, ref y);
            Console.WriteLine("\n after swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(2);
            Console.WriteLine("\n Before swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");
           Helper.swap<DateTime>(ref date1, ref date2);
            Console.WriteLine("\n after swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");
            Console.ReadLine();
        }
    }
}

Now i want to add another method of genric to add any kind of numers so whenn try that i will get some errors so at that i will use a
keyword which is dynamic to solve my requirement 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{
    class Helper
    {
        public static void swap<T>(ref T x, ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }

        public static T add<T>(T x, T y)
        {
            dynamic x1 = x;
            dynamic y1 = y;
            T sum;
            sum = x1 + y1;
            return sum;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{

    
    internal class Program
    {
        //public static void swap(ref int x, ref int y)
        //{
        //    int temp;
        //    temp = x;
        //    x = y;
        //    y = temp;
        //}
        //public static void swap(ref DateTime x, ref DateTime y)
        //{
        //    DateTime temp;
        //    temp = x;
        //    x = y;
        //    y = temp;
        //}

        //public static void swap(ref string st1, ref string st2)
        //{
        //    string temp;
        //    temp = st1;
        //    st1 = st2;
        //    st2 = temp;
        //}
        static void Main(string[] args)
        {

            int x = 10;
            int y = 20;
            Console.WriteLine("\n Before swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

            Helper.swap<int>(ref x, ref y);
            Console.WriteLine("\n after swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(2);
            Console.WriteLine("\n Before swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");
           Helper.swap<DateTime>(ref date1, ref date2);
            Console.WriteLine("\n after swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");

            double dd1 = 123.45;
            double dd2 = 234.567;
           Console.WriteLine($"The sum of double types :{Helper.add<double>(dd1, dd2)}");

            Console.WriteLine($"The sum of ints types :{Helper.add<int>(x,y)}");

            Console.ReadLine();
        }
    }
}

Now i want to go with genric class means <T> i will shift to class level it means all methods in that class should 
folow the type provided by the class so add another class Helper2 in the same code 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{
    class Helper
    {
        public static void swap<T>(ref T x, ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }

        public static T add<T>(T x, T y)
        {
            
            dynamic x1 = x;
            dynamic y1 = y;
            T sum;
            sum = x1 + y1;
            return sum;
        }

    }

    class Helper2<T>
    {
        public static void swap(ref T x, ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }

        public static T add(T x, T y)
        {

            dynamic x1 = x;
            dynamic y1 = y;
            T sum;
            sum = x1 + y1;
            return sum;
        }
    }
}

and main method code will chenage like this observer comments see helper2 i had used it here 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDemoonMethodandclass
{

    
    internal class Program
    {
        //public static void swap(ref int x, ref int y)
        //{
        //    int temp;
        //    temp = x;
        //    x = y;
        //    y = temp;
        //}
        //public static void swap(ref DateTime x, ref DateTime y)
        //{
        //    DateTime temp;
        //    temp = x;
        //    x = y;
        //    y = temp;
        //}

        //public static void swap(ref string st1, ref string st2)
        //{
        //    string temp;
        //    temp = st1;
        //    st1 = st2;
        //    st2 = temp;
        //}
        static void Main(string[] args)
        {

            int x = 10;
            int y = 20;
            Console.WriteLine("\n Before swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

           // Helper.swap<int>(ref x, ref y);
             Helper2<int>.swap(ref x, ref y);
            Console.WriteLine("\n after swapping integers");
            Console.WriteLine($"x={x}\n y={y}");

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(2);
            Console.WriteLine("\n Before swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");
            //  Helper.swap<DateTime>(ref date1, ref date2);
            Helper2<DateTime>.swap(ref date1, ref date2);
            Console.WriteLine("\n after swapping dates");
            Console.WriteLine($"date1={date1}\n date2={date2}");

            double dd1 = 123.45;
            double dd2 = 234.567;
         //  Console.WriteLine($"The sum of double types :{Helper.add<double>(dd1, dd2)}");

          //  Console.WriteLine($"The sum of ints types :{Helper.add<int>(x,y)}");

            Console.WriteLine($"The sum of double types :{Helper2<double>.add(dd1, dd2)}");

            Console.WriteLine($"The sum of ints types :{Helper2<int>.add(x, y)}");


            Console.ReadLine();
        }
    }
}





Now let us go with some generic collections 
---------------------------------------------

Generic List 
----------------
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_ListDemo
{
    class Customer
    {
        public int CustomerID { set; get;}
        public string CustomerName { set; get; }

        public static List<Customer> retrive()
        {
            List<Customer> list = new List<Customer>()
            {
              new Customer{CustomerID=101,CustomerName="sita"},
              new Customer{CustomerID=102,CustomerName="suresh"},
              new Customer{CustomerID=103,CustomerName="mahesh"}

            };
            return list;
        }
        public static void PrintCustomer(List<Customer> clist)
        {
            Console.WriteLine("\ndisplaying cusomters");
            foreach (Customer cust in clist)
            {
                Console.WriteLine($"{cust.CustomerID}--{cust.CustomerName}");
            }
        }

        public static void insertcustomer(Customer customer,List<Customer> clist)
        {
            clist.Add(customer);
        }

        public static Customer findcustomer(int custid1,List<Customer> clist) 
        {
            Customer customerfound = null;
            foreach (Customer c in clist)
            {
                if (c.CustomerID == custid1)
                {
                    customerfound = c;
                    break;
                }
                
            }
            return customerfound;   
        
        }

        public static void updatecustomer(int cid,List<Customer> clist)
        {
            for (int i = 0; i < clist.Count; i++)
            {
                if(clist[i].CustomerID == cid)
                {
                    Console.WriteLine("enter new name");
                    string newname=Console.ReadLine();
                    clist[i].CustomerName = newname;
                }
            }
        }

        public static void  deletecustomer(int cid, List<Customer> clist)
        {
            for (int i = 0; i < clist.Count; i++)
            {
                if (clist[i].CustomerID == cid)
                {
                    clist.RemoveAt(i);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 12, 45, 67, 89, 99, 192 };
            List<int> numbers2 = new List<int>();
            numbers2.Add(12);
            numbers2.AddRange(new int[] { 12, 45, 67, 89, 100 });
            Console.WriteLine("\n displaying numbers ");
            foreach (int number in numbers)
            {
               
                    Console.Write($"{number}  ");
                
            }
             Console.WriteLine("\n displaying numbers2");
                foreach (int num in numbers2)
                {

                    Console.Write($"{num}  ");
                }

            List<string> boysnames = new List<string>() { "kiran", "karthik", "mahesh", "suresh" };
            var  girlnames = new List<string>();
            girlnames.Add("sudha");
            girlnames.AddRange(new string[] { "sita", "shanthi", "priya", "suman" });

            Console.WriteLine("\n\n displaying boys ");
            foreach (string boy in boysnames)
            {

                Console.WriteLine($"{boy}");

            }
            Console.WriteLine("\n displaying girls");
            foreach (string girl in girlnames)
            {

                Console.WriteLine($"{girl}");
            }

            int[] aa = new int[] { 12, 33, 44, 5, 89, 56, 71, 90, 12, 44 };
            //remove  duplicate elements from the array and give it to me 

            List<int> result = new List<int>();

             foreach (int number in aa)
            {
                bool found = false;
                foreach(int resultitem in result)
                {
                    if (resultitem == number)
                    {
                        found = true;
                    }
                }
                if(!found)
                {
                    result.Add(number);
                }
            }
            Console.WriteLine("\n after removing duplicate elements ..");
            foreach (int k in result)
            {
                Console.Write($"{k}  ");
            }
            List<Customer>  custlist = Customer.retrive();
            Customer.PrintCustomer(custlist);
            Console.WriteLine("\n enter the customer to be added in the list");
            Customer c4 = new Customer()
            {
                CustomerID=104,
                CustomerName="rajesh"
            };
            Customer.insertcustomer(c4, custlist);
            Customer.PrintCustomer(custlist);
            Console.WriteLine("\n enter the customer id to find");
            int custid2=Convert.ToInt32(Console.ReadLine());
          Customer customergot=  Customer.findcustomer(custid2,custlist);
            if(customergot != null)
            {
                Console.WriteLine($"The cusotmer wit id {customergot.CustomerID} is having name {customergot.CustomerName}");
            }
            else
            {
                Console.WriteLine("\n the customer is not found ");
            }

            Console.WriteLine("\n enter the id of customer whos name u want to change ");
            int customerid2= Convert.ToInt32(Console.ReadLine());
            Customer.updatecustomer(customerid2,custlist);
            Customer.PrintCustomer(custlist);
            Console.WriteLine("\n enter the id of customer whos name u want to delete ");
            int customerid3 = Convert.ToInt32(Console.ReadLine());
            Customer.deletecustomer(customerid2, custlist);
            Customer.PrintCustomer(custlist);
            Console.ReadLine();
        }
    }
}





