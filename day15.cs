Cursors 
-------
In SQL Server, a cursor is a database object used to retrieve and manipulate data row-by-row from a result set. While it's often more efficient to use set-based operations, cursors are useful when row-wise operations are necessary.

✅ 1. How to Declare a Cursor

DECLARE cursor_name CURSOR 
FOR 
SELECT column1, column2 
FROM your_table 
WHERE condition;

✅ 2. How to Open a Cursor

OPEN cursor_name;

✅ 3. How to Fetch from a Cursor

FETCH NEXT FROM cursor_name INTO @var1, @var2;
You declare variables beforehand to store each row's column values.


DECLARE @var1 INT, @var2 NVARCHAR(100);
You usually use this in a loop:


WHILE @@FETCH_STATUS = 0
BEGIN
    -- process each row
    PRINT @var1;

    -- fetch next row
    FETCH NEXT FROM cursor_name INTO @var1, @var2;
END
✅ 4. How to Close and Deallocate a Cursor

CLOSE cursor_name;
DEALLOCATE cursor_name;


Print all OrderID and OrderDate values from the Orders table using a cursor.

-- Step 1: Declare variables
DECLARE @OrderID INT;
DECLARE @OrderDate DATETIME;

-- Step 2: Declare the cursor
DECLARE order_cursor CURSOR FOR
SELECT OrderID, OrderDate
FROM Orders;

-- Step 3: Open the cursor
OPEN order_cursor;

-- Step 4: Fetch the first row
FETCH NEXT FROM order_cursor INTO @OrderID, @OrderDate;

-- Step 5: Loop through the result set
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Print current row data
    PRINT 'Order ID: ' + CAST(@OrderID AS NVARCHAR) + ' | Order Date: ' + CAST(@OrderDate AS NVARCHAR);

    -- Fetch next row
    FETCH NEXT FROM order_cursor INTO @OrderID, @OrderDate;
END

-- Step 6: Close and deallocate the cursor
CLOSE order_cursor;
DEALLOCATE order_cursor;


Explanation:
----------------
Step	Purpose
DECLARE @OrderID, @OrderDate	Variables to hold data from each row
DECLARE order_cursor CURSOR FOR...	Creates cursor over the Orders table
OPEN	Starts the cursor
FETCH NEXT INTO	Moves row-by-row into variables
WHILE @@FETCH_STATUS = 0	Continues until no rows left
PRINT	Outputs current row data
CLOSE and DEALLOCATE	Frees memory and resources


Using the Products table in Northwind, we'll:

Find all products where UnitsInStock is less than 10

Add 10 units to those products using a cursor

code :
--------
-- Step 1: Declare variables to hold column values
DECLARE @ProductID INT;
DECLARE @UnitsInStock SMALLINT;

-- Step 2: Declare the cursor
DECLARE product_cursor CURSOR FOR
SELECT ProductID, UnitsInStock
FROM Products
WHERE UnitsInStock < 10;

-- Step 3: Open the cursor
OPEN product_cursor;

-- Step 4: Fetch the first row
FETCH NEXT FROM product_cursor INTO @ProductID, @UnitsInStock;

-- Step 5: Loop through each product and update stock
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Update the stock by adding 10
    UPDATE Products
    SET UnitsInStock = @UnitsInStock + 10
    WHERE ProductID = @ProductID;

    PRINT 'Updated Product ID: ' + CAST(@ProductID AS NVARCHAR) + 
          ' | New UnitsInStock: ' + CAST(@UnitsInStock + 10 AS NVARCHAR);

    -- Fetch next row
    FETCH NEXT FROM product_cursor INTO @ProductID, @UnitsInStock;
END

-- Step 6: Close and deallocate the cursor
CLOSE product_cursor;
DEALLOCATE product_cursor;


 What This Does:
Action	Description
SELECT ProductID, UnitsInStock WHERE UnitsInStock < 10	Filters low stock products
UPDATE Products SET UnitsInStock = ...	Increases stock by 10
PRINT	Confirms the update for each product
CLOSE, DEALLOCATE	Properly clean up resources


Warning:
Running this code will modify data. Use it only in a test environment or after making a backup.


We’ll use a SCROLL cursor on the Employees table to:

Fetch the first, last, next, and prior rows

Show how to move in both directions

Full SCROLL Cursor Example with Employees Table:


-- Step 1: Declare variables
DECLARE @EmpID INT, @FirstName NVARCHAR(50), @LastName NVARCHAR(50);

-- Step 2: Declare a SCROLL cursor
DECLARE emp_cursor SCROLL CURSOR FOR
SELECT EmployeeID, FirstName, LastName
FROM Employees
ORDER BY EmployeeID;

-- Step 3: Open the cursor
OPEN emp_cursor;

-- Step 4: Fetch the FIRST row
FETCH FIRST FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'FIRST ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 5: Fetch the NEXT row
FETCH NEXT FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'NEXT ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 6: Fetch the LAST row
FETCH LAST FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'LAST ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 7: Fetch the PRIOR row
FETCH PRIOR FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'PRIOR ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 8: Close and deallocate the cursor
CLOSE emp_cursor;
DEALLOCATE emp_cursor;


Output Example (based on your data):

FIRST ROW: 1 - Nancy Davolio
NEXT ROW: 2 - Andrew Fuller
LAST ROW: 9 - Anne Dodsworth
PRIOR ROW: 8 - Laura Callahan


SCROLL Cursor Options:
Keyword	Behavior
FIRST	Moves to first row
NEXT	Moves to next row
PRIOR	Moves to previous row
LAST	Moves to last row
ABSOLUTE n	Moves to the nth row
RELATIVE n	Moves n rows from current position


Let's extend the SCROLL cursor example on the Northwind database to include:

ABSOLUTE n: jump to the n-th row

RELATIVE n: move n rows forward or backward from the current position

We will continue using the Employees table.


Full SCROLL Cursor Example with ABSOLUTE and RELATIVE:


-- Step 1: Declare variables
DECLARE @EmpID INT, @FirstName NVARCHAR(50), @LastName NVARCHAR(50);

-- Step 2: Declare the SCROLL cursor
DECLARE emp_cursor SCROLL CURSOR FOR
SELECT EmployeeID, FirstName, LastName
FROM Employees
ORDER BY EmployeeID;

-- Step 3: Open the cursor
OPEN emp_cursor;

-- Step 4: Fetch ABSOLUTE 3rd row
FETCH ABSOLUTE 3 FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'ABSOLUTE 3rd ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 5: Fetch RELATIVE +2 rows (move 2 rows forward)
FETCH RELATIVE 2 FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'RELATIVE +2 ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 6: Fetch RELATIVE -1 row (move 1 row backward)
FETCH RELATIVE -1 FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'RELATIVE -1 ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 7: Fetch ABSOLUTE -1 (last row)
FETCH ABSOLUTE -1 FROM emp_cursor INTO @EmpID, @FirstName, @LastName;
PRINT 'ABSOLUTE -1 (LAST) ROW: ' + CAST(@EmpID AS NVARCHAR) + ' - ' + @FirstName + ' ' + @LastName;

-- Step 8: Close and deallocate the cursor
CLOSE emp_cursor;
DEALLOCATE emp_cursor;


Explanation of SCROLL Options Used:
Statement	What it Does
FETCH ABSOLUTE 3	Goes to the 3rd row from the top
FETCH RELATIVE 2	Moves 2 rows forward from current position
FETCH RELATIVE -1	Moves 1 row backward from current position
FETCH ABSOLUTE -1	Goes to the last row in the result set


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


Reusable Features from C#.net :(Assemblies  ) 
_________________________________________________________________________________________
whatver program we are writing we are writing  in some namesapce when we build them then assembly is created for the program 
if in my progam entry point function main method or button click is there then when we compile it or build it  exe will be created executble file but when i build a class or when my program is not having main method then its dll will be created which is called as dynamic link library .

when u wan to create dll file means assembly without an main method then u have to use class library template for it 

There are two types of assemblies one is shared assembly given by .net and they are stored in GAC (Global assemby cache)

and another is provate asssembly the assembly which we are creating 

namespaces are logical when we build them they become physical parts which are assemblies 



uisng class library template 

assembly created here dll 
-----------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeFinding
{
    public class FindCube
    {

        public int Cube(int x)
		{
            return (x * x * x);
		}
    }
}

check the demos of console application i had integrated this dll here 

Access Modifiers or specifiers :
---------------------------------
private ,public ,protected ,internal and protected internal 

so this comes under encapsulation data hiding which part of the program i want to show 
and which i want to hide is decided by these access specifiers in the program .


private : directly cannot access this u need set and get methods (Be defualt it is private if u dont give any access specifier )

public : can be accessible outside the class 

protected : only sub class can access the variable defined as protected in base class 

internal : you can access within the assembly or within the namespace only .

protected internal : you can access across assembly but it should be inherited .

when u have an entry point function like main .exe file will be created and when 
you have normal class a dll file will be created . so you cannot execute dll file it can be used as an component in some other program or in an exe.

so in this what u do 	create an or add a project of new as clas library and inside that class create a varibale as internal and try to access it using adding refernce and including namespace also then also it will not highlight and same thing making it protected internal and inheriting in main progam the class then 	it will highlight eventhough u are from seperate assembly .


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifiers
{
    class abcd
    {
        private int a;
        public int b=2;
        protected int c=3;

        public void seta(int k)
        {
            this.a = k;
        }

        public int geta()
        {
            return this.a;
        }

    }
     class Program:abcd
    {
        static void Main(string[] args)
        {
            abcd abcdobj = new abcd();
            Program pp = new Program();
            //and see which can be accessed which cannot 
            // i can touch and display b using both class objects as it is public 
            Console.WriteLine($"b i can touch using base class obj and display {abcdobj.b}");
            Console.WriteLine($"b i can touch using sub class obj and display {pp.b}");
            // i cannot do this 
            //abcdobj.a  // error private...
            //pp.a // error private
            // abcdobj.c // error only sub class can use even it is there in main class 
            Console.WriteLine($"c i can touch using sub class obj and display {pp.c}");
           // Console.WriteLine($"{abcd.c}"); //error

            pp.seta(1);
            Console.WriteLine($"{pp.geta()}");
            Console.ReadLine();

        }
    }
}


from the above program i can understand which part of the program i can touch and which i cannot touch usng base class i can touch only b 
but using sub class i can touch b and c but not a as a is private once privat always private 
then how i can touch a by writing set and get methods within class 


now add a new project of class library and code fro that is like this 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
       internal int d = 4;
    }
}


now from earier program i want to touch and print d variable which is internal 
for that i will build and add referecne and will try to touch and print d variable 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
namespace AccessModifiers
{
    class abcd
    {
        private int a;
        public int b=2;
        protected int c=3;

        public void seta(int k)
        {
            this.a = k;
        }

        public int geta()
        {
            return this.a;
        }

    }
     class Program:abcd
    {
        static void Main(string[] args)
        {
            abcd abcdobj = new abcd();
            Program pp = new Program();
            //and see which can be accessed which cannot 
            // i can touch and display b using both class objects as it is public 
            Console.WriteLine($"b i can touch using base class obj and display {abcdobj.b}");
            Console.WriteLine($"b i can touch using sub class obj and display {pp.b}");
            // i cannot do this 
            //abcdobj.a  // error private...
            //pp.a // error private
            // abcdobj.c // error only sub class can use even it is there in main class 
            Console.WriteLine($"c i can touch using sub class obj and display {pp.c}");
           // Console.WriteLine($"{abcd.c}"); //error

            pp.seta(1);
            Console.WriteLine($"{pp.geta()}");
            // even after adding referecne of dll i cannot touch and print d varibale

            //pp.d;
            //abcdobj.d;

            Console.ReadLine();

        }
    }
}


Now i want to access d now if u inherit then u can access and that variable should be protected internal 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
    {
       protected internal int d = 4;
    }
}


again build once class library as change in code has happened 

now let us go to acces modider code


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
namespace AccessModifiers
{
    class abcd
    {
        private int a;
        public int b=2;
        protected int c=3;

        public void seta(int k)
        {
            this.a = k;
        }

        public int geta()
        {
            return this.a;
        }

    }
     class Program:Class1 //abcd
    {
        static void Main(string[] args)
        {
            abcd abcdobj = new abcd();
            Program pp = new Program();
            //and see which can be accessed which cannot 
            // i can touch and display b using both class objects as it is public 
            Console.WriteLine($"b i can touch using base class obj and display {abcdobj.b}");
            //  Console.WriteLine($"b i can touch using sub class obj and display {pp.b}");
            // i cannot do this 
            //abcdobj.a  // error private...
            //pp.a // error private
            // abcdobj.c // error only sub class can use even it is there in main class 
            //   Console.WriteLine($"c i can touch using sub class obj and display {pp.c}");
            // Console.WriteLine($"{abcd.c}"); //error

            //  pp.seta(1);
            // Console.WriteLine($"{pp.geta()}");
            // even after adding referecne of dll i cannot touch and print d varibale


            //abcdobj.d;

            Console.WriteLine($"Now i can touch and print {pp.d}");

            Console.ReadLine();

        }
    }
}


if pp.d is not working what u do delete dll build once again and add it refercne again 


Next come to the concept of static now ....

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


