Introduction to Unit Testing in C#:
-----------------------------------------
Unit testing is the process of testing individual units or components of an application in isolation
to ensure that they function as expected. In C#, 
  unit tests are typically written using frameworks like NUnit, MSTest, or XUnit.
  Here, I'll provide a basic understanding of unit testing using NUnit
  as an example.

create one project of class libary means C#   ,All Platforms and library  here select core 
libray project not of .net framework  and while 
	creating name give project name as PassWordStrength and solution name as 
TestingProjects and create a project 

now in the class library project write this code like this so class1 name has to be changed 
PasswordStrengthMeter and 
if  u want u can change the file name as to same 


namespace PassWordStrength
{
    public class PasswordStrengthMeter
    {
        public static int GetPassWordStrength(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return 0;
            }
            int result = 0;
            if(password.Length > 8)
            {
                result = result + 1;
            }
            if(password.Any(char.IsUpper))
            {
                result = result + 1;
            }
            if (password.Any(char.IsLower))
            {
                result = result + 1;
            }
            if(password.Any(char.IsDigit))
            {
                result = result + 1;
            }
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            foreach (char ch in specialChArray)
            {
                if (password.Contains(ch))

                    result = result + 1;
            }

            return result;
        }

    }
}

Now build the solution u will get the DLL file for this project alone as main method is not there 

then in TestingProjects add another project C# ,All platforms and directly select here NUnit one okay 

in this testing project u need to add the DLL reference and provide the namespace as well okay 

remove the default class which u are getting and add this class like this 

using PassWordStrength;
namespace TestProject2
{
   
    [TestFixture]
    public class Tests 
    {
        [Test]
        public void Test1()
        {
            // arrange 
            string password = "Rajesh#123";
            int expected = 6;
            //act 
            int actual = PasswordStrengthMeter.
                GetPassWordStrength(password);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}

add another libary project and give name as MockExample

namespace MockExample
{
    public class  CheckEmployee
    {
        public virtual Boolean checkemp()
        {
            throw new NotImplementedException();
        }
    }
    public class ProcessEmployee
    {
        public bool insertEmployee(CheckEmployee emp)

        {
            emp.checkemp();
            // logic for implemting inseting employee using ado.net 


           return true;

            

        }
    }
}


Now build the solution again and agin add MockExample dll into current  testing project 

includ namespace in test project as using MockExample;

now add Mock package from nugget package manager 

go to test project dependecies right clik and say Manage nugget packages

browse and in search type Moq 4.18.4 and install it and add another namespace using Moq;


using MockExample;
using Moq;
using NUnit.Framework;
using PassWordStrength;
namespace TestProject2
{

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // arrange 
            string password = "Rajesh#123";
            int expected = 5;// if kept 6 it will fail
            //act 
            int actual = PasswordStrengthMeter.
                GetPassWordStrength(password);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class Mocktesting
    {
        [Test]
        public void TestMethod1()
        {
            Mock<CheckEmployee> chk = new Mock<CheckEmployee>();
            chk.Setup(x => x.checkemp()).Returns(true);


            ProcessEmployee objprocess = new ProcessEmployee();
            Assert.AreEqual(objprocess.insertEmployee(chk.Object), true);





        }
    }
}

Lab1 
-------
StringConcatenation/Program.cs

//THIS IS FOR REFERENCE ONLY. YOU NEED NOT MAKE ANY CHANGES HERE
namespace StringConcatenation
{
    public class Program
    {
        public static string? Message(string input1, string input2)
        {
            return input1 + " " + input2;
        }
    }
}

StringConcatenation/StringConcatenation.csproj


<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.8.0" />
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.2" />
		<PackageReference Include="NUnit" Version="3.13.3" />
		<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
	</ItemGroup>

</Project>

StringConcatenation/UnitTest.cs


using System.Text;
using NUnit.Framework;

namespace StringConcatenation
{
    //Add required NUnit test attribute
    [TestFixture]
    class UnitTest
    {
        //Implement code here
        [Test]
        public void Test_Message_String()
        {
            Assert.AreEqual("Hello World", Program.Message("Hello", "World"));
        }
    }
}

String Concatenation.sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.4.33110.190
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "StringConcatenation", "StringConcatenation\StringConcatenation.csproj", "{91830A06-7064-4E37-9F4E-0C6CEAE070EA}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {07B8A7E4-880F-4E96-919B-5735FD6949AA}
	EndGlobalSection
EndGlobal

Lab 2
------
VowelChecker/Program.cs


//THIS IS FOR REFERENCE ONLY. YOU NEED NOT MAKE ANY CHANGES HERE
namespace VowelChecker
{
    public class Program
    {
        public static bool? CheckVowels(char letter)
        {
            if (letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u' ||
                letter == 'A' || letter == 'E' || letter == 'I' || letter == 'O' || letter == 'U')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


VowelChecker/UnitTest.cs

using System.Text;
using NUnit.Framework;

namespace VowelChecker
{
    //Add required NUnit test attribute
    [TestFixture]
    class UnitTest 
    {
        //Implement code here
        [Test]
        public void Test_CheckVowels_Valid()
        {
            Assert.AreEqual(true, Program.CheckVowels('A'));
        }
        [Test]
        public void Test_CheckVowels_Invalid()
        {
            Assert.AreEqual(false, Program.CheckVowels('b'));
        }
    }
}


VowelChecker/VowelChecker.csproj


<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.8.0" />
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.2" />
		<PackageReference Include="NUnit" Version="3.13.3" />
		<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
	</ItemGroup>

</Project>

Vowel Checker.sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.4.33110.190
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "VowelChecker", "VowelChecker\VowelChecker.csproj", "{91830A06-7064-4E37-9F4E-0C6CEAE070EA}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {07B8A7E4-880F-4E96-919B-5735FD6949AA}
	EndGlobalSection
EndGlobal

Lab 3 
--------
ArraySearch/ArraySearch.csproj

<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.8.0" />
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.2" />
		<PackageReference Include="NUnit" Version="3.13.3" />
		<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
	</ItemGroup>

</Project>

ArraySearch/Program.cs

//THIS IS FOR REFERENCE ONLY. YOU NEED NOT MAKE ANY CHANGES HERE
namespace ArraySearch
{
    public class Program
    {
        public static int[] a1 = new int[10];

        public static int? AddValues(int a)
        {
            int j = 0;
            for (int i = j; i < 10; i++)
            {
                a1[j] = i;
                j++;
            }
            j = 0;
            for (int i = j; i < 10; i++)
            {
                if (a1[i] == a)
                {
                    return a1[i];
                }
            }

            return 0;
        }
    }
}

ArraySearch/UnitTest.cs

using System.Text;
using NUnit.Framework;

namespace ArraySearch
{
    //Add required NUnit test attribute
    [TestFixture]
    class UnitTest
    {
        //Implement code here
        [Test]
        public void Test_AddValues_RightValue()
        {
            int inputValue = 3;
            int? result = Program.AddValues(inputValue);

            Assert.AreEqual(inputValue, result);
        }

        [Test]
        public void Test_AddValues_ValueNotFound()
        {
            int inputValue = 11;
            int? result = Program.AddValues(inputValue);

            Assert.AreEqual(0, result);
        }
    }
}


Array Search.sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.4.33110.190
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ArraySearch", "ArraySearch\ArraySearch.csproj", "{91830A06-7064-4E37-9F4E-0C6CEAE070EA}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {07B8A7E4-880F-4E96-919B-5735FD6949AA}
	EndGlobalSection
EndGlobal

Lab 4 
-----
ArrayMultiplication/ArrayMultiplication.csproj

<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.8.0" />
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.2" />
		<PackageReference Include="NUnit" Version="3.13.3" />
		<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
	</ItemGroup>

</Project>

ArrayMultiplication/Program.cs
//THIS IS FOR REFERENCE ONLY. YOU NEED NOT MAKE ANY CHANGES HERE
namespace ArrayMultiplication
{
    public class Program
    {
        public static int[] a1 = new int[10];
        
        public static int[]? Multiply(int a)
        {
            for (int i = 1; i < 10; i++)
            {
                a1[i] = a * i;
            }
            return a1;
        }
    }
}


ArrayMultiplication/UnitTest.cs

using System.Text;
using NUnit.Framework;

namespace ArrayMultiplication
{
    //Add required NUnit test attribute
    [TestFixture]
    class UnitTest
    {
        //Implement code here
        [Test]
        public void Test_Multiply_NotReturningNull()
        {
            int[] result = Program.Multiply(5);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_Multiply_RightValue()
        {
            int[] expected = { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45 };

            int[] result = Program.Multiply(5);
            Assert.AreEqual(expected, result);
        }
    }
}

Array Multiplication.sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.4.33110.190
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ArrayMultiplication", "ArrayMultiplication\ArrayMultiplication.csproj", "{91830A06-7064-4E37-9F4E-0C6CEAE070EA}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {07B8A7E4-880F-4E96-919B-5735FD6949AA}
	EndGlobalSection
EndGlobal

Lab 5 
--------
DemoAppCore/DemoAppCore.csproj

<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<OutputType>Exe</OutputType>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.8.0" />
		<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.2" />
		<PackageReference Include="NUnit" Version="3.13.3" />
		<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
	</ItemGroup>

</Project>

DemoAppCore/Program.cs
//THIS IS FOR REFERENCE ONLY. YOU NEED NOT MAKE ANY CHANGES HERE
namespace DemoAppCore
{
    public class Program
    {
        public static List<Student> details = null;

        public static List<Student>? FinalList()
        {
            details = new List<Student>();
            details.Add(new Student { Id = 150, Name = "Suresh" });
            details.Add(new Student { Id = 151, Name = "Karthick" });
            details.Add(new Student { Id = 152, Name = "Prem" });
            return details;
        }
    }

    public class Student
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}

DemoAppCore/UnitTest.cs

using System.Text;
using NUnit.Framework;

namespace DemoAppCore
{
    //Add required NUnit test attribute
    [TestFixture]
    class UnitTest
    {
        //Implement code here
        [Test]
        public void Test_FinalList_UniqueValues()
        {
            List<Student> result = Program.FinalList();

            Assert.That(result, Is.Unique);
        }

        [Test]
        public void Test_FinalList_AddingValuesToList()
        {
            List<Student> result = Program.FinalList();
            Assert.AreEqual(3, result.Count);
            // List<Student> test = new List<Student>
            // { 
            //     new Student { Id = 150, Name = "Suresh" },
            //     new Student { Id = 151, Name = "Karthick" },
            //     new Student { Id = 152, Name = "Prem" }
            // };
            // List<Student> result = Program.FinalList();
            // CollectionAssert.AreEqual(test, result);
        }
    }
}

DemoAppCore.sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.4.33110.190
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "DemoAppCore", "DemoAppCore\DemoAppCore.csproj", "{91830A06-7064-4E37-9F4E-0C6CEAE070EA}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{91830A06-7064-4E37-9F4E-0C6CEAE070EA}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {07B8A7E4-880F-4E96-919B-5735FD6949AA}
	EndGlobalSection
EndGlobal
