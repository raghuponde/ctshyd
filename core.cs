
Exception :
----------
An abnormal event that disrupts the normal flow of programming is called as an exception 
we can say run time error 


eg: you are trying to open a file which is not present an exception will come 


where there is a possiblity of error that code is kept in try block

and for to catch error we use catch block where we write user defined message in catch .

for one try block there can be multiple catch blocks based upon type of exceptions occur.

whether exception comes or not comes i want my code to be executed i use finally block 


try -->catch-->finally is okay 
try -->catch is okay 
try-->finally 

in between try and catch nothing should be used except comments // 

we can create our own exceptions and throw in .net there is no throws only throw is there in .net

Base class of all exception is Exception class .

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class ICICIBankException : ApplicationException
        {
            public ICICIBankException(string message) : base(message)
            {

            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(textBox1.Text);
                int b = Convert.ToInt32(textBox2.Text);
                int c = a / b;
                textBox3.Text = c.ToString();
            }

            //comments
            catch (DivideByZeroException ee)
            {
                MessageBox.Show("dont enter second value  as zero..." + ee.Message);

            }
            catch (FormatException ee)
            {
                MessageBox.Show("dont enter charcters or special symbols.." + ee.Message);
            }

            catch (Exception ee)
            {
                MessageBox.Show("Some general errror came chk it.." + ee.Message);
            }
            finally
            {
                MessageBox.Show(" I am still alive..");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int age = Convert.ToInt32(textBox4.Text);
                if (age < 18)
                {
                    ICICIBankException obj = new ICICIBankException("ICICIBankException : Age above 18 to open accoutn");
                    throw obj;
                }
                else
                {
                    MessageBox.Show("click this link to open an account :\"+\"https://www.icicibank.com/\"");
                }
            }
            catch (ICICIBankException ice)
            {
                MessageBox.Show(ice.Message);
            }
        }
    }
}

Now go to collectionofobjects.cs view and add there two links like this one is achcor link  and another is tag helper link as per latest trend okay 


@model IEnumerable<mvcwebapp1.Models.Employee>
@{
    ViewData["Title"] = "collectionofobjectpassing";
}

<h1>collectionofobjectpassing</h1>

<style>
	table {
		border-collapse: collapse;
		width: 50%;
	}

	table, th, td {
		border: 2px solid black;
	}

	th, td {
		padding: 8px;
		text-align: left;
	}
</style>

<table>
	<thead>
		<tr>
			<th>EmployeeID</th>
			<th>EmpName</th>
			<th>Salary</th>
			<th> Image </th>
		</tr>
	</thead>
	<tbody>
		@foreach (var emp in Model)
		{
			<tr>

				<td> @emp.EmployeeID</td>
				<td>@emp.EmpName</td>
				<td>@emp.Salary</td>
				<td>


					<img src="@emp.ImageUrl"
						 alt="@emp.EmpName"
						 width="100" height="100" />

				</td>
				<td>
					<a href="/Home/Details/@emp.EmployeeID">Details </a> |
					<a asp-controller="Home" asp-action="Details" asp-route-id="@emp.EmployeeID">Details </a>
				</td>

			</tr>
		}

	</tbody>
</table>

I am telling here that in home controller there is details method so let us add taht method now 

	public IActionResult Details(int id)
	{
		

		var employee = emplist.FirstOrDefault(e => e.EmployeeID == id);
		if (employee == null)
		{
			return NotFound();
		}

		return View(employee);
	}

and details view will be like tis 
------------------------------------
@model mvcwebapp1.Models.Employee
@{
    ViewData["Title"] = "Details";
}

<h1>Details</h1>

<h1>@Model.EmpName</h1>

<div>
    <img src="@Model.ImageUrl" alt="@Model.EmpName" width="300" height="300" />
</div>

<div>
  
    <p><strong>Salary:</strong> @Model.Salary</p>
</div>

<a asp-action="Index">Back to List</a>

Now let us try view model now 

add one department class like this 

namespace MvcFirstExample.Models
{
    public class Department
    {

        public int DeptId { set; get; }
        public string? DeptName { set; get; }
    }
}



 List<Department> deptlist = new List<Department>()
     {
         new Department{DeptId=10,DeptName="Sales"},
         new Department{DeptId=20,DeptName="HR"},
         new Department{DeptId=30,DeptName="Software"}
     };


public ActionResult viewmodeldemo(int empid)
{
	var query1 = deptlist.ToList();

	Employee emp=emplist.Where(x=>x.EmployeeID==empid).FirstOrDefault();
	var query2 = emp;

	EmpDeptViewModel obj = new EmpDeptViewModel()
	{
		deptlist = query1,
		emp = query2,
		date = DateTime.Now
	};

	return View(obj);
}

Employeeviewmodel view 
-------------------------
@model MvcFirstExample.Models.EmpDeptViewModel
@{
    ViewData["Title"] = "viewmodeldemo";
}

<h1>viewmodeldemo</h1>

<h2> Date : @Model.date</h2>

<h3>dept details </h3>
<table border="1" cellpadding="1" cellspacing="1">
    <tr>
        <th>DeptID</th>
        <th>Deptname</th>
    

    @foreach (Department dept in Model.deptlist)
    {
            <tr>
                <td>@dept.DeptId</td>
                <td>@dept.DeptName</td>
            </tr>
    }


</table>
<h3> Search emp details</h3>
<table border="1" cellpadding="0" cellspacing="0">
        <tr>
            <th>EmployeeID</th>
            <th>Empname</th>
            <th>EmpSalary</th>
        </tr>
        @if(Model.emp!=null)
    {
        <tr>
            <td>@Model.emp.EmployeeID</td>
            <td>@Model.emp.EmpName</td>
            <td>@Model.emp.Salary</td>
        </tr>
    }
    else
    {
        <tr>
            <td>There is no employee with this id</td>
        </tr>
    }

 </table>

Search Employeee 
------------------
public ActionResult searchemp(int empid)
{
	Employee emp = (from e1 in emplist where e1.EmployeeID == empid select e1).FirstOrDefault();
	return View(emp);
}

search emp view 
------------------
@model MvcFirstExample.Models.Employee
@{
    ViewData["Title"] = "searchemp";
}

<h1>searchemp</h1>

  <table border="1" cellpadding="0" cellspacing="0">
        <tr>
            <th>EmployeeID</th>
            <th>Empname</th>
            <th>EmpSalary</th>
        </tr>
        <tr>
            <td>@Model.EmployeeID</td>
            <td>@Model.EmpName</td>
            <td>@Model.Salary</td>
        </tr>
    </table>

	
CRUD operation using a Model :
-----------------------------------------
Means for a model means for a class any class i want to define insert,update ,delete and read etc methods in Controller function means function for insert and function for update and all i want to write 
here i will use get and post methods and all i will validate the class means business rules also i will apply 

earlier manually i had gone into the view and written html code and embedded the model object s into it 
but now i will ask the visual studio to genrate the code for me in the view that thing is called as scafffolding.

so check program dog on this example ..

Get and Post methods are there for only insert ,update and delete functionalities


﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MvcFirstExample.Models;
namespace MvcFirstExample.Controllers
{
    public class DogController : Controller
    {

        static private List<Dog> dogs = new List<Dog>();

        // GET: DogController
        public ActionResult Index()
        {
            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog d = new Dog();
            foreach(Dog dog in dogs)
            {
                if (dog.ID ==id)
                {
                    d.ID = dog.ID;
                    d.Name = dog.Name;
                    d.Age = dog.Age;
                }

            }
            return View(d);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog d)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    dogs.Add(d);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Create", d);
                }
            }
            catch(Exception ex) 
            {
                return View("Create", d);
            }
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dog d)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View("Edit", d);
                }
                else
                {
                    foreach(Dog dog in dogs)
                    {
                        if(dog.ID==d.ID)
                        {
                            dog.Name = d.Name;
                            dog.Age = d.Age;
                        }

                    }
                    return RedirectToAction("Index");

                }
            }
            catch(Exception ex) 
            {
                return View("Edit", d);
            }
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {

            Dog d = new Dog();
            foreach (Dog dog in dogs)
            {
                if (dog.ID == id)
                {
                    d.ID = dog.ID;
                    d.Name = dog.Name;
                    d.Age = dog.Age;
                }

            }
            return View(d);
        }
        [HttpGet]
        public ActionResult DirectDelete(int id)
        {
            Dog d = new Dog();
           foreach (Dog dog in dogs)
            {
                if (dog.ID ==id)
                {
                    dogs.Remove(dog);
                    break;
                }
            }
            return RedirectToAction("Index");
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Dog d)
        {
            try
            {
                foreach(Dog dog in dogs)
                {
                    if(dog.ID==d.ID)
                    {
                        dogs.Remove(dog);
                        break;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MvcFirstExample.Models
{
    public class Dog
    {

        [Required(ErrorMessage = "ID is required")]
        public int ID { set; get; }

        [Required(ErrorMessage = "Name is required"), MaxLength(222)]
        public string? Name { set; get; }

        [Required(ErrorMessage = "Age is required"),
            Range(0, 20, ErrorMessage = "Age should be between 0 to 20 only")]
        public int Age { set; get; }




    }
}


For designing first learn bootstrap from this link 

https://www.w3schools.com/bootstrap4/

as in MVC they are using some bootstrap file 

and after learning go to this link 

https://getbootstrap.com/docs/4.6/getting-started/introduction/

and in this in search type what u want like forms ,dropdowns etc 

some code will come try to analize it and replace that code with  your desing code which u need it 


bootswatch.com is another site for taking code into desing 

Go through html helpers or stongly typed html helpers from the internet and chatGPT what are they learn it

for strongly typed create a class with properties and then write a action method of create and right click it 
and graphically create view saying add view there add model and template which u want to genrate like here create template 

and see the code of automatic generated and check it with what bootsrap which u have learned ...so there you will find lot of bootstrap code 


Best video to refer for strongly typed html helpers 


https://www.youtube.com/watch?v=yLTKwy0KZRw&t=1910s

