
	
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

Now u have to generate the views graphically but still i am putting generated code view of my programs done earlier 


dog create view 
-------------------
@model MvcFirstExample.Models.Dog

@{
    ViewData["Title"] = "Create";
}

<h1>Create</h1>

<h4>Dog</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Create">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="ID" class="control-label"></label>
                <input asp-for="ID" class="form-control" />
                <span asp-validation-for="ID" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Name" class="control-label"></label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Age" class="control-label"></label>
                <input asp-for="Age" class="form-control" />
                <span asp-validation-for="Age" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Create" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

Dog Index view 
--------------
@model IEnumerable<MvcFirstExample.Models.Dog>

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.ID)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Age)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.ID)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Name)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Age)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", new {  id=item.ID  }) |
                @Html.ActionLink("Details", "Details", new { id=item.ID }) |
                @Html.ActionLink("Delete", "Delete", new {  id=item.ID })|
                @Html.ActionLink("DirectDelete", "DirectDelete", new {  id=item.ID })
                
            </td>
        </tr>
}
    </tbody>
</table>

Dog Edit view 
---------------
@model MvcFirstExample.Models.Dog

@{
    ViewData["Title"] = "Edit";
}

<h1>Edit</h1>

<h4>Dog</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Edit">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="ID" class="control-label"></label>
                <input asp-for="ID" class="form-control" />
                <span asp-validation-for="ID" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Name" class="control-label"></label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Age" class="control-label"></label>
                <input asp-for="Age" class="form-control" />
                <span asp-validation-for="Age" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Save" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

Dog Details view 
---------------
@model MvcFirstExample.Models.Dog

@{
    ViewData["Title"] = "Details";
}

<h1>Details</h1>

<div>
    <h4>Dog</h4>
    <hr />
    <dl class="row">
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.ID)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.ID)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Name)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Name)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Age)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Age)
        </dd>
    </dl>
</div>
<div>
    @Html.ActionLink("Edit", "Edit", new { /* id = Model.PrimaryKey */ }) |
    <a asp-action="Index">Back to List</a>
</div>

Dog delete view 
------------------
@model MvcFirstExample.Models.Dog

@{
    ViewData["Title"] = "Delete";
}

<h1>Delete</h1>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Dog</h4>
    <hr />
    <dl class="row">
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.ID)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.ID)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Name)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Name)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Age)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Age)
        </dd>
    </dl>
    
    <form asp-action="Delete">
        <input type="submit" value="Delete" class="btn btn-danger" /> |
        <a asp-action="Index">Back to List</a>
    </form>
</div>


Now i want to execute the same code in BootStrap for better look 


Now as i want to make a good bootstrap usage of above code i kept the prompt like this 

prompt for enhancing above dog app
------------------------------------
so for the above coding all things i want to implement it using bootswatch.com and i want to use https://bootswatch.com/quartz/

theme here so i want to add one image for the dog using file and also description about the dog so two more properties i want to add into the model class and after adding all dogs i want  list of dogs i want to show in table i want to use quartz tables for displaying 

table data along with small image of 50 by 50 pix display 

In navbar  of quartz i want to provide hyperlinks for create a dog form where i can enter details and image of dog and insert it into 

the tables again and while displaying about dog all details i want to show in 


In create form i want file control to browse image from computer and when added it should go to images folder ..


I want  complete source code   along with earlier code for each module 


now you can refer in mobile the chat gpt generated code for the above prompt and and it is  Dog Management with Image okay and as i had developed the application also so i am keeping that 
full source code in Day30  folder so you can download that folder in class and by checking prompt response from chat gpt develop the application for the student okay 



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

