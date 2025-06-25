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
