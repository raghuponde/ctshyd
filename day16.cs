
so object orientation
-----------------------
means set of ideas supported by many languages okay .so why i need this
object orientation what are the drwbacks in procedural oreineted programs
like c or cobol that we have changed to object oriented programming let
us disscuss now below

Object Orienatation :
-------------------------------
OOPs object oreiented programming concepts

1) Abstraction : showing the essentials details of the object we call it as abstraction eg: we say what is abstract of the project ,what is abstract of the story etc 
How i will implement it in program if i am creating a class then i am implementing abstraction 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopsDemo1
{
	class Employee
	{
		int empid;
		string empame;
		int sal;
		int bonus;
   
         public void tsal(int sal, int bonus)
		{

			Console.WriteLine("The total sal is " + (sal + bonus));
		}

		public void display()
		{

		}

	}
	class Program
	{
		static void Main(string[] args)
		{

			Employee ee = new Employee();
			ee.tsal(12000, 450);
			
			Console.ReadLine();
		}
	}
}

eg : washing mahine I want to work with washing machine for that buttons are given .

2) Encapsulation : Hiding the unessential details of the object .If u are creating the object of the class then u are implemting encapsulation .

when u declare an object all features of class in kept inside the object 

eg: how the washing machine is working u will break and see all interternal charatetics how it works is hidden 
eg: I am taking a tablet i have stomacache that tablet is an object interanlly it reduces my pain 

NOte : if u are using access modiifers like private ,public ,protected ,internal and procted internal in your program then u are implementing 
encapsulation which is nothing but data hiding which part of the program u want to show and which u dont want to show for that purpose .


3) Inheirtance : Deriving from one class you inherit from one class when we inherit then who is inherting is a child so 
here we will have base class and child class



eg: check slide 

     simple inheirtance 
     multtiple inheritance 
    multilevel inheirtance 
    hiearchichical inheritance 


Here C# does not support multiple inheritance because ambuiguty will be there 

eg: father having blue eyes and mother also blue eyes and son will have blue eyese whoes blues is this father or mother ???

     in programming same functions defined in both parent class then which one to implement it gets confused 
	 
	 
	 
inheritnce demo (multilevel)
----------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inheritancedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //multi level inheritance
        class Father
        {
            public void maruthicar()
            {
                MessageBox.Show("Maruthi car ...");
            }
        }
        class Son : Father
        {
            public void MBCar()
            {
                MessageBox.Show("Mercedes benz car...");
            }
        }
        class GrandSon:Son
        {
            public void BMWCar()
            {
                MessageBox.Show("BMW car....");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GrandSon grandSon = new GrandSon();
            grandSon.maruthicar();
            grandSon.MBCar();
            grandSon.BMWCar();

        }
    }
}

now hiearchical inheritance 
-----------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inheritancedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //multi level inheritance
        class Father
        {
            public void maruthicar()
            {
                MessageBox.Show("Maruthi car ...");
            }
        }
        class Son : Father
        {
            public void MBCar()
            {
                MessageBox.Show("Mercedes benz car...");
            }
        }
        class GrandSon:Father
        {
            public void BMWCar()
            {
                MessageBox.Show("BMW car....");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GrandSon grandSon = new GrandSon();
            grandSon.maruthicar();
            grandSon.BMWCar();
            Son son = new Son();
            son.maruthicar();
            son.MBCar();

        }
    }
}


next multiple inheritance is not possible and using base class obje sub class function is not called 
------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inheritancedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        class Father
        {
            public void maruthicar()
            {
                MessageBox.Show("Maruthi car ...");
            }
        }
        class Son : Father
        {
            public void MBCar()
            {
                MessageBox.Show("Mercedes benz car...");
            }
        }
        class GrandSon:Son//,Father //not possible multiple inheirtance 
        {
            public void BMWCar()
            {
                MessageBox.Show("BMW car....");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GrandSon grandSon = new GrandSon();
            Father fobj = new Father();
            // remove comments and check 
            //fobj.MBCar() // error not possible
            //fobj.BMWCar() // error not possible
            Son sobj = new Son();
            //sobj.BMWCar(); //errro not possible
        }
    }
}


I don't want to overrdie the sub class or i dont want to inherit i dont want other classes to inheirt me then i will make that class as sealed class 
---------------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inheritancedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //multi level inheritance
       sealed class Father
        {
            public void maruthicar()
            {
                MessageBox.Show("Maruthi car ...");
            }
        }
     sealed   class Son : Father
        {
            public void MBCar()
            {
                MessageBox.Show("Mercedes benz car...");
            }
        }
        class GrandSon:Son//,Father not possible 
        {
            public void BMWCar()
            {
                MessageBox.Show("BMW car....");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GrandSon grandSon = new GrandSon();
            Father fobj = new Father();
            // remove comments and check 
            //fobj.MBCar() // error not possible
            //fobj.BMWCar() // error not possible
            Son sobj = new Son();
            //sobj.BMWCar(); //errro not possible
        }
    }
}


you can see error coming in program 


Two Limitations in inheritance :
----------------------------------------
1) Multiple inheritance is not supported to Overcome this limiation i will use Interfaces 
2) using base class object i cannot call sub class function To overcome this limitaation in inheritance I will use abstract classses 

              in the button click 

	                                Father fobj = new Father();
			fobj.MaruthiCar();
			fobj.MBCar();// error 
			fobj.BMWCar();// error 


 





Inheritance :
-------------
The process of deriving from one class to another is called inheritance.we go for inheritance for reusability purpose.
when the class cannot satisfy complete information then also we go for inheritance .sometimes we may inherit wrong things also so we need to inheritance carefully .The subclass is heavier than the base class .

1)single inheritance:
This is having only two classes one class is base class and another is child class,here only one class is inheriting from base class.
2)multiple inheritance
when a class is derived more than one class we call it as multiple inheritance so here child classes will be having multiple base classes and C#.net doesnt support multiple inheritance 
as ambiguity problem arises in this .


3)multi path inheritance:
In this we will be having multiple classes but having only one base class directly attached to child class and there is no limit for the level of inheritance means one base class can have only one child class and that can act as base class for another class and which may be a base class of another class...like that it can continue ....


4)hiearchical inheritance:
In this one base class will be having collection of child classes ,in this we need to create instance variables of all child classes so this is also possible in C#.net 


5)hybrid inheritance:It is a combination of all types of inheritances .as we already know we cannot implement multiple so it creates ambuiguity so hybrid inheritance also we cannot implement .




In this .net wont support multiple inheritance due to ambiguity problem.
another limitation  of inheritance is that using the base class object i cant call the sub class function.
objectofbaseclass.functionofsubclass  not possible.

so to overcome first limitation means using base class object i want to call sub class function i will go for abstract class 
and to overcome multiple inheritance which is not possibe i will use interfaces 

so code will be written slowly in abstract class and interface considernig some  scenarios 



when  u dont want to inherit the class make it sealed class no one can inherit then okay 


Abstract classes and interfaces :
----------------------------------
Using the base class object i cannot call sub class function so to overcome that abstract classes are used.
In order to implement some generalized concept we go for abstract classes .
An abstract class is an class which willl have atleast one function as abstract function .
so an abstract function is nothing but a function which is having one header and no body .

when classes are interrealated like traingle ,rectangle etc belong to same category polygon then we go for abstract classes here 


eg: add(int x,int y);

syntax:
--------
public abstract class class_name
{
 public  abstract void function_name(parameters list);
 public void function_name( )
  {

  }

}


Interface :
-----------
A pure abstract class is called interface means all the functions inside an interface are by default abstract .we go for interfaces mainly to increase security in programs.

syntax:
-------
interface Interface_name
{
 void function_name1(parmeters_list);
 void function_name2(parmeters_list);

}

Abstract class demo 
--------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abstractclassandinterfacedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public abstract class Polygon
        {
            public void testfunction()
            {
                MessageBox.Show("**************************");
            }
            public abstract void area(int a, int b);// abstarct method 
        }
      class Triangle:Polygon
      {

      }
      class Rectangle:Polygon
      {

      }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}


try to override the functionlity graphically by putting cursor and then put your own code for the same concpet of area


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abstractclassandinterfacedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public abstract class Polygon
        {
            public void testfunction()
            {
                MessageBox.Show("**************************");
            }
            public abstract void area(int a, int b);// abstarct method 
        }

        class Triangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of Triangle is :" + 0.5 * a * b);
            }
        }
        class Rectangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of rectangle is :" + (a * b));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // use the base class obj sub classfucntion 
            //  Polygon obj = new Polygon();// error i cannot create an object of abstract class 
            // becuse abstarct class is having partial method means incomplete methoods 
            // i cannot create an object by i can create object reference means

            Polygon obj;// it is a car without a petrol 
            obj = new Triangle();// allocating memory of trianngle class in base class only putting petrol
            obj.area(12, 3);
            obj=new Rectangle();// it can allocate memeory of rectanglealso 
            obj.area(12, 4);
            obj.testfunction();
            // now i can use base class refercne object and i can call sub class function which 
            //is happening here first limitiation of inheritnace is overcome 

        }
    }
}

now another class comes square he is asking me to implement area() in polygon class so for that class area 
the logic is different so triangle and rectangle though they dont want it they have to override it and implement it 
and have to implment so for squre we have to use seperate abstract class 


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abstractclassandinterfacedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public abstract class Polygon
        {
            public void testfunction()
            {
                MessageBox.Show("**************************");
            }
            public abstract void area(int a, int b);// abstarct method 
            public abstract void area(int s);
        }

        class Triangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of Triangle is :" + 0.5 * a * b);
            }
        }
        class Rectangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of rectangle is :" + (a * b));
            }
        }

        class square
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // use the base class obj sub classfucntion 
            //  Polygon obj = new Polygon();// error i cannot create an object of abstract class 
            // becuse abstarct class is having partial method means incomplete methoods 
            // i cannot create an object by i can create object reference means

            Polygon obj;// it is a car without a petrol 
            obj = new Triangle();// allocating memory of trianngle class in base class only putting petrol
            obj.area(12, 3);
            obj=new Rectangle();// it can allocate memeory of rectanglealso 
            obj.area(12, 4);
            obj.testfunction();
            // now i can use base class refercne object and i can call sub class function which 
            //is happening here first limitiation of inheritnace is overcome 

        }
    }
}

so in visual studio  if i do this i will get error in triangle or rectangle not error they have to override another area which is not of 
theri interest so i am asking square class to use another polygon base class of abstract to override it 



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abstractclassandinterfacedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public abstract class Polygon2
        {
            public abstract void area(int s);
        }
        public abstract class Polygon
        {
            public void testfunction()
            {
                MessageBox.Show("**************************");
            }
            public abstract void area(int a, int b);// abstarct method 
          //  public abstract void area(int s);
        }

        class Triangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of Triangle is :" + 0.5 * a * b);
            }
        }
        class Rectangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of rectangle is :" + (a * b));
            }
        }

        class square : Polygon2
        {
            public override void area(int s)
            {
                MessageBox.Show("The area of square is :"+(s*s));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // use the base class obj sub classfucntion 
            //  Polygon obj = new Polygon();// error i cannot create an object of abstract class 
            // becuse abstarct class is having partial method means incomplete methoods 
            // i cannot create an object by i can create object reference means

            Polygon obj;// it is a car without a petrol 
            obj = new Triangle();// allocating memory of trianngle class in base class only putting petrol
            obj.area(12, 3);
            obj=new Rectangle();// it can allocate memeory of rectanglealso 
            obj.area(12, 4);
            obj.testfunction();
            // now i can use base class refercne object and i can call sub class function which 
            //is happening here first limitiation of inheritnace is overcome 
            Polygon2 obj2;
            obj2 = new square();
            obj2.area(12);

        }
    }
}

now a new shape will come he is telling that he wants to implment area of rectangle also
and square also include those mehtods he is telling new shape 

so i will try like this 

 class NewShape:Polygon,Polygon2
 {

 }
 
 I have to do like this because both functionlities are present in different class i have to do 
 multiple inheitance which is not possible there red line is there i can override one functionlity 
 but red line will still show because multiple inheitance is not supported in C#
 
  class NewShape : Polygon, Polygon2
 {
     public override void area(int a, int b)
     {
         throw new NotImplementedException();
     }
 }
 
 still red line is showing as multiple inheritance is not supported 



so what to do ????
use interface 

Interface :
-----------
A pure abstract class is called interface means all the functions inside an interface are by default abstract .we go for interfaces mainly to increase security in programs.

syntax:
-------
interface Interface_name
{
 void function_name1(parmeters_list);
 void function_name2(parmeters_list);

}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abstractclassandinterfacedemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public abstract class Polygon2
        {
            public abstract void area(int s);
        }
        public abstract class Polygon
        {
            public void testfunction()
            {
                MessageBox.Show("**************************");
            }
            public abstract void area(int a, int b);// abstarct method 
          //  public abstract void area(int s);
        }

        class Triangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of Triangle is :" + 0.5 * a * b);
            }
        }
        class Rectangle : Polygon
        {
            public override void area(int a, int b)
            {
                MessageBox.Show("The are of rectangle is :" + (a * b));
            }
        }

        class square : Polygon2
        {
            public override void area(int s)
            {
                MessageBox.Show("The area of square is :" + (s * s));
            }
        }

        //class NewShape : Polygon, Polygon2
        //{
        //    public override void area(int a, int b)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        interface A
        {
            // never use public in interface as by default it is public only 
           // int a;//cannot declare varibale 
           //  int a1 { set; get; }// can declare proepties 
            //public void testfunction()
            //{
            //    MessageBox.Show("**************************");
            //}
            void area(int a, int b);

        }

        interface B
        {
            void area(int s);
        }
        class NewShape : A, B
        {
           
            public void area(int a, int b)
            {
                MessageBox.Show("The are of rectangle is :" + (a * b));
            }

            public void area(int s)
            {
                MessageBox.Show("The area of square is :" + (s * s));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // use the base class obj sub classfucntion 
            //  Polygon obj = new Polygon();// error i cannot create an object of abstract class 
            // becuse abstarct class is having partial method means incomplete methoods 
            // i cannot create an object by i can create object reference means

            Polygon obj;// it is a car without a petrol 
            obj = new Triangle();// allocating memory of trianngle class in base class only putting petrol
            obj.area(12, 3);
            obj=new Rectangle();// it can allocate memeory of rectanglealso 
            obj.area(12, 4);
            obj.testfunction();
            // now i can use base class refercne object and i can call sub class function which 
            //is happening here first limitiation of inheritnace is overcome 
            Polygon2 obj2;
            obj2 = new square();
            obj2.area(12);

            A aobj;
            aobj = new NewShape();
            aobj.area(12, 34);
            B bobj;
            bobj = new NewShape();
            bobj.area(5);

        }
    }
}


Final Note is that you can at a time can inherit with one class that can be abstarct class but you can implment many  interfaces so this is golden line 
This is also possible that one abstarct class and one interface u can use it but both abstarct class u cannot use it 

polymorhism : 
-------------------
Poly means many morh forms 

I am person to my mother i am his son and to my daughter i am his father and with my wife i am husband 


water is there 3 forms : solid liquid gases 

 Function overloading
---------------------
 functions having same name but no of arguments ,type of arguments ,order of arguments differs 
and retrn tye will also change

void     add (int x, int y)
 int   add (int x, float f,double kk) 
double    add (int x, decimal kk )



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionoverloading_compiletimepoly
{

    class abcd
    {
        public void add(int x, int y)
        {
            Console.WriteLine($"The sum is {x + y}");
        }
        // not a overloaded method below 
        //public int add(int y, int k)
        //{
        //    return (x + k);
        //}

        public double add(int k,decimal ss,double jj)
        {
            return (k + Convert.ToDouble(ss) + jj);
        }

        public decimal add(float k, decimal ss, double jj)
        {
            return (Convert.ToDecimal(k) + ss+Convert.ToDecimal(jj));
        }
    }

    class cde :abcd
    {
        public void add(int kk,char ch)
        {
            Console.WriteLine($"The sum is :{kk+ch} ");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            cde cde = new cde();
            cde.add(12, 34);
            cde.add(12, 'A');
            Console.WriteLine($"The sum is {cde.add(12.34F, 123.34M, 345.567)}");
            Console.WriteLine($"The sum is {cde.add(123, 345.56M, 345.567)}");
            Console.ReadLine();
            
        }
    }
}


what is referecne types and what is value types 


reference type means classes and objects are refercne types 

primitive data types like int ,float ,double are value types 

int x=10;

int y=20;

int sum=x+y;

30 i will get the result 

for value types u will get the result now i want to add objects and addtion should happen that will not happen 

i need to overload a + operator for that 

class A 
{

int a;

}

code :
---------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operatoroverlaoding
{
	class ABCD
	{
		public int a;
		public ABCD()//default constructor
		{

		}
		public ABCD(int k)//paramamatized constructor
		{
			this.a = k;
		}
		public static ABCD operator +(ABCD x, ABCD y)
		{
			ABCD third = new ABCD();
			third.a = x.a + y.a;
			return third;

		}
	}

	class Program
	{
		public static void Main()
		{
			ABCD obj1 = new ABCD(10);
			ABCD obj2 = new ABCD(20);
			ABCD obj3 = new ABCD();

			obj3 = obj1 + obj2;

			Console.WriteLine("{0}", obj3.a);
			Console.ReadLine();
			
		}
	}
}





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









