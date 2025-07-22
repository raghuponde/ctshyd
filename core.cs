
	
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

Now i had Already taught you bootstrap now just refer the links which I have told to you below and
	
For designing first learn bootstrap from this link 

https://www.w3schools.com/bootstrap4/

as in MVC they are using some bootstrap file 

and after learning go to this link 

https://getbootstrap.com/docs/4.6/getting-started/introduction/

and in this in search type what u want like forms ,dropdowns etc 

some code will come try to analize it and replace that code with  your desing code which u need it 


bootswatch.com is another site for taking code into desing 

and  continue with the below notes and develop an app to enhance the above dog
application which we created in normal one we will do it same using bootswatch where different themes of bootstrap are there which u can use it okay .

prompt for enhancing above dog app
------------------------------------
so for the above coding all things i want to implement it using bootswatch.com and i want to use https://bootswatch.com/quartz/

theme here so i want to add one image for the dog using file and also description about the dog so two more properties i want to add into the model class and after adding all dogs i want  list of dogs i want to show in table i want to use quartz tables for displaying 

table data along with small image of 50 by 50 pix display 

In navbar  of quartz i want to provide hyperlinks for create a dog form where i can enter details and image of dog and insert it into 

the tables again and while displaying about dog all details i want to show in 


In create form i want file control to browse image from computer and when added it should go to images folder ..


I want  complete source code   along with earlier code for each module 


Now steps for implementing the above code 
-------------------------------------------
1.Updated Dog model with Description and ImagePath.

using System.ComponentModel.DataAnnotations;

namespace DogMgtAPP.Models
{
    public class Dog
    {

        [Required(ErrorMessage = "ID is required")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required"), MaxLength(222)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Age is required"), Range(0, 20, ErrorMessage = "Age should be between 0 to 20 only")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Description is required"), MaxLength(500)]
        public string? Description { get; set; }

        public string? ImagePath { get; set; } // Path to saved image
    }
}


2.Ensure you’ve added Quartz CSS from CDN in your _Layout.cshtml:

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>@ViewData["Title"] - Dog App</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootswatch@5.3.0/dist/quartz/bootstrap.min.css" />
</head>
<body>
    <nav class="navbar navbar-expand-lg bg-primary navbar-dark">
        <div class="container-fluid">
            <a class="navbar-brand" asp-controller="Dog" asp-action="Index">DogApp</a>
            <div class="collapse navbar-collapse">
                <ul class="navbar-nav me-auto">
                    <li class="nav-item">
                        <a class="nav-link" asp-controller="Dog" asp-action="Create">Create Dog</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container mt-4">
        @RenderBody()
    </div>
</body>
</html>


so this code you have to replace in your current layout page so in my layout page i had replaced it like this 

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - DogMgtAPP</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/DogMgtAPP.styles.css" asp-append-version="true" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootswatch@5.3.0/dist/quartz/bootstrap.min.css" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container-fluid">
                <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">DogMgtAPP</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <nav class="navbar navbar-expand-lg bg-primary navbar-dark">
                    <div class="container-fluid">
                        <a class="navbar-brand" asp-controller="Dog" asp-action="Index">DogApp</a>
                        <div class="collapse navbar-collapse">
                            <ul class="navbar-nav me-auto">
                                <li class="nav-item">
                                    <a class="nav-link" asp-controller="Dog" asp-action="Create">Create Dog</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <form class="d-flex" asp-controller="Dog" asp-action="Index" method="get">
                        <input class="form-control me-2" type="search" name="search" placeholder="Search by name" aria-label="Search" />
                        <button class="btn btn-outline-light" type="submit">Search</button>
                    </form>
                </nav>
            </div>
        </nav>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2025 - DogMgtAPP - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </div>
    </footer>
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>

3. Dog Controller complete code 
--------------------------------
using DogMgtAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogMgtAPP.Controllers
{
    public class DogController : Controller
    {
        private static List<Dog> dogs = new List<Dog>();
        private readonly IWebHostEnvironment _environment;

        public DogController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index(string? search)
        {
            var filtered = string.IsNullOrEmpty(search)
                ? dogs
                : dogs.Where(d => d.Name != null && d.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return View(filtered);
        }
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dog d, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(_environment.WebRootPath, "images", imageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    d.ImagePath = "/images/" + imageName;
                }

                dogs.Add(d);
                return RedirectToAction("Index");
            }

            return View(d);
        }

        public IActionResult Edit(int id)
        {
            var dog = dogs.FirstOrDefault(d => d.ID == id);
            return View(dog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dog d)
        {
            if (ModelState.IsValid)
            {
                var existing = dogs.FirstOrDefault(x => x.ID == d.ID);
                if (existing != null)
                {
                    existing.Name = d.Name;
                    existing.Age = d.Age;
                    existing.Description = d.Description;
                }
                return RedirectToAction("Index");
            }

            return View(d);
        }

        public IActionResult Details(int id) => View(dogs.FirstOrDefault(d => d.ID == id));

        public IActionResult Delete(int id) => View(dogs.FirstOrDefault(d => d.ID == id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Dog d)
        {
            var dog = dogs.FirstOrDefault(x => x.ID == d.ID);
            if (dog != null)
                dogs.Remove(dog);

            return RedirectToAction("Index");
        }
    }
}

4.Create one folder in wwwroot with the name images and download some images of dog and keep it on desktop or any folder so you have to copy the images from desktop to images folder 
from the app when u run okay 

5.Views of Dog App 

  Create View 
  -----------
  @model DogMgtAPP.Models.Dog
@{
    ViewData["Title"] = "Create";
}

<h2>Create New Dog</h2>

<form asp-action="Create" enctype="multipart/form-data">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>

    <div class="form-group">
        <label asp-for="ID" class="form-label"></label>
        <input asp-for="ID" class="form-control" />
        <span asp-validation-for="ID" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Name" class="form-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Age" class="form-label"></label>
        <input asp-for="Age" class="form-control" />
        <span asp-validation-for="Age" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Description" class="form-label"></label>
        <textarea asp-for="Description" class="form-control"></textarea>
        <span asp-validation-for="Description" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label>Dog Image</label>
        <input type="file" name="imageFile" class="form-control" />
    </div>

    <button type="submit" class="btn btn-success mt-2">Create</button>
</form>

Delete View 
-------------
@model DogMgtAPP.Models.Dog
@{
    ViewData["Title"] = "Delete";
}

<h2>Delete Dog</h2>

<h4>Are you sure you want to delete this?</h4>
<hr />
<dl class="row">
    <dt class="col-sm-2">ID</dt>
    <dd class="col-sm-10">@Model.ID</dd>

    <dt class="col-sm-2">Name</dt>
    <dd class="col-sm-10">@Model.Name</dd>

    <dt class="col-sm-2">Age</dt>
    <dd class="col-sm-10">@Model.Age</dd>

    <dt class="col-sm-2">Description</dt>
    <dd class="col-sm-10">@Model.Description</dd>

    <dt class="col-sm-2">Image</dt>
    <dd class="col-sm-10">
        @if (!string.IsNullOrEmpty(Model.ImagePath))
        {
            <img src="@Model.ImagePath" width="100" height="100" />
        }
    </dd>
</dl>

<form asp-action="Delete">
    <input type="hidden" asp-for="ID" />
    <button type="submit" class="btn btn-danger">Delete</button> |
    <a asp-action="Index" class="btn btn-secondary">Cancel</a>
</form>

Details View 
--------------
@model DogMgtAPP.Models.Dog

@{
    ViewData["Title"] = "Dog Details";
}

<div class="card shadow-lg mb-4">
    <div class="card-header bg-primary text-white">
        Dog ID: @Model.ID
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-5 text-center">
                @if (!string.IsNullOrEmpty(Model.ImagePath))
                {
                    <img src="@Model.ImagePath" alt="Dog Image" class="img-fluid rounded border" style="max-height: 300px;" />
                }
                else
                {
                    <span class="text-muted">No Image Available</span>
                }
            </div>
            <div class="col-md-7">
                <h4 class="card-title">@Model.Name</h4>
                <p><strong>Age:</strong> @Model.Age</p>
                <p><strong>Description:</strong></p>
                <p class="text-muted">@Model.Description</p>

                <div class="mt-4">
                    <a asp-action="Edit" asp-route-id="@Model.ID" class="btn btn-warning">Edit</a>
                    <a asp-action="Index" class="btn btn-secondary">Back to List</a>
                </div>
            </div>
        </div>
    </div>
</div>

Edit View 
-----------
@model DogMgtAPP.Models.Dog
@{
    ViewData["Title"] = "Edit";
}

<h2>Edit Dog</h2>

<form asp-action="Edit">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>

    <div class="form-group">
        <label asp-for="ID" class="form-label"></label>
        <input asp-for="ID" class="form-control" readonly />
    </div>

    <div class="form-group">
        <label asp-for="Name" class="form-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Age" class="form-label"></label>
        <input asp-for="Age" class="form-control" />
        <span asp-validation-for="Age" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Description" class="form-label"></label>
        <textarea asp-for="Description" class="form-control"></textarea>
        <span asp-validation-for="Description" class="text-danger"></span>
    </div>

    @if (!string.IsNullOrEmpty(Model.ImagePath))
    {
        <div class="form-group">
            <label>Current Image</label><br />
            <img src="@Model.ImagePath" width="100" height="100" />
        </div>
    }

    <button type="submit" class="btn btn-primary mt-2">Update</button>
</form>

<div class="mt-2">
    <a asp-action="Index">Back to List</a>
</div>

Index View 
---------
@model IEnumerable<DogMgtAPP.Models.Dog>
@{
    ViewData["Title"] = "Dog List";
}

<h2>Dog List</h2>

@{
    var searchQuery = Context.Request.Query["search"].ToString();
}

@if (!string.IsNullOrEmpty(searchQuery))
{
    <div class="alert alert-info">
        Showing results for: <strong>@searchQuery</strong>
    </div>
}


<table class="table table-hover table-bordered">
    <thead class="table-light">
        <tr>
            <th>ID</th>
            <th>Image</th>
            <th>Name</th>
            <th>Age</th>
            <th>Description</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        @if (Model.Any())
        {
            foreach (var dog in Model)
            {
                <tr>
                    <td>@dog.ID</td>
                    <td>
                        @if (!string.IsNullOrEmpty(dog.ImagePath))
                        {
                            <img src="@dog.ImagePath" width="50" height="50" class="rounded border" />
                        }
                        else
                        {
                            <span>No Image</span>
                        }
                    </td>
                    <td>@dog.Name</td>
                    <td>@dog.Age</td>
                    <td>@dog.Description</td>
                    <td>
                        <a asp-action="Edit" asp-route-id="@dog.ID" class="btn btn-sm btn-primary">Edit</a>
                        <a asp-action="Details" asp-route-id="@dog.ID" class="btn btn-sm btn-info">Details</a>
                        <a asp-action="Delete" asp-route-id="@dog.ID" class="btn btn-sm btn-danger">Delete</a>
                    </td>
                </tr>
            }
        }
        else
        {
            <tr>
                <td colspan="6" class="text-center text-muted">No dogs found.</td>
            </tr>
        }
    </tbody>
</table>

<div class="mt-3">
    <a asp-action="Create" class="btn btn-success">Add New Dog</a>
</div>

so this is all about the app code is there i had kept in day 30 folder u can check it okay .

Tag Helpers let you use 
	    server-side C# code in 
     Razor views using HTML-like syntax. Unlike HTML Helpers (@Html.TextBoxFor(...)), Tag Helpers are more readable and align with HTML semantics

creating model 
---------------
// Models/Employee.cs
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    [Range(18, 60)]
    public int Age { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Department { get; set; }

    public bool IsPermanent { get; set; }

    public string Gender { get; set; }
}

creating controller 
---------------------
// Controllers/EmployeeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EmployeeController : Controller
{
    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(new[] { "HR", "IT", "Finance" });
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee emp)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Success");
        }

        ViewBag.Departments = new SelectList(new[] { "HR", "IT", "Finance" });
        return View(emp);
    }

    public IActionResult Success() => View();
}
Razor view with tag helpers (create view)
-----------------------------------------
@model Employee

@{
    ViewData["Title"] = "Register Employee";
}

<h2>@ViewData["Title"]</h2>

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="Name" class="control-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Age"></label>
        <input asp-for="Age" class="form-control" />
        <span asp-validation-for="Age" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Email"></label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Department"></label>
        <select asp-for="Department" asp-items="ViewBag.Departments" class="form-control"></select>
        <span asp-validation-for="Department" class="text-danger"></span>
    </div>

    <div class="form-check">
        <input asp-for="IsPermanent" class="form-check-input" />
        <label asp-for="IsPermanent" class="form-check-label"></label>
    </div>

    <div class="form-group mt-2">
        Gender:
        <input type="radio" asp-for="Gender" value="Male" /> Male
        <input type="radio" asp-for="Gender" value="Female" /> Female
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
now sucesspage.cshtml
----------------------
@{
    ViewData["Title"] = "Success";
}

<h3>Employee Registered Successfully!</h3>
<a asp-action="Create" class="btn btn-success">Add Another</a>


syntax code explanation 
-------------------------
| Concept                        | Razor Syntax                                   | Description                        |
| ------------------------------ | ---------------------------------------------- | ---------------------------------- |
| `asp-for`                      | `asp-for="Name"`                               | Binds to model property            |
| `asp-action`, `asp-controller` | `asp-action="Create"`                          | Routes form to action              |
| `asp-items`                    | `asp-items="ViewBag.Departments"`              | Binds to a list for dropdown       |
| Validation                     | `asp-validation-for="Email"`                   | Shows field-specific error         |
| Partial View                   | `<partial name="_ValidationScriptsPartial" />` | For client-side validation         |
| Layout & Section               | `_Layout.cshtml`, `@section Scripts {}`        | Structure and scripts              |
| Anchor Helpers                 | `<a asp-action="Create">`                      | Generates anchor tags with routing |
| Conditional Logic              | `@if`, `@foreach`                              | Server-side logic in view          |
| Custom Tag Helper              | Create a class implementing `TagHelper`        | Extend HTML elements behavior      |


summary of tags 
----------------
Great! Let’s dive into **Tag Helpers** in **ASP.NET Core 8.0** with a full demo covering **all key Razor concepts**, from basic to advanced.

---

## ✅ What are Tag Helpers?

**Tag Helpers** let you use server-side C# code in Razor views using **HTML-like syntax**. Unlike HTML Helpers (`@Html.TextBoxFor(...)`), Tag Helpers are more readable and align with HTML semantics.

---

## 🧰 Key Razor Concepts Covered:

1. Basic Input Tag Helpers (`asp-for`)
2. Form Tag Helper (`asp-action`, `asp-controller`)
3. Label, Validation, and Select Tag Helpers
4. Conditional rendering (`@if`, `@foreach`)
5. Partial Views and `asp-fragment`
6. Custom Tag Helpers
7. Anchor tag helpers (`asp-route-*`)
8. Layouts, Sections, and `_ViewStart.cshtml`
9. Scripts and styles with `asp-src`, `asp-append-version`

---

## 🔧 Complete Demo: **Employee Registration App**

---

### 🔹 Step 1: Create Model

```csharp
// Models/Employee.cs
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    [Range(18, 60)]
    public int Age { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Department { get; set; }

    public bool IsPermanent { get; set; }

    public string Gender { get; set; }
}
```

---

### 🔹 Step 2: Create Controller

```csharp
// Controllers/EmployeeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class EmployeeController : Controller
{
    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(new[] { "HR", "IT", "Finance" });
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee emp)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Success");
        }

        ViewBag.Departments = new SelectList(new[] { "HR", "IT", "Finance" });
        return View(emp);
    }

    public IActionResult Success() => View();
}
```

---

### 🔹 Step 3: Razor View with Tag Helpers (`Create.cshtml`)

```razor
@model Employee

@{
    ViewData["Title"] = "Register Employee";
}

<h2>@ViewData["Title"]</h2>

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="Name" class="control-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Age"></label>
        <input asp-for="Age" class="form-control" />
        <span asp-validation-for="Age" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Email"></label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Department"></label>
        <select asp-for="Department" asp-items="ViewBag.Departments" class="form-control"></select>
        <span asp-validation-for="Department" class="text-danger"></span>
    </div>

    <div class="form-check">
        <input asp-for="IsPermanent" class="form-check-input" />
        <label asp-for="IsPermanent" class="form-check-label"></label>
    </div>

    <div class="form-group mt-2">
        Gender:
        <input type="radio" asp-for="Gender" value="Male" /> Male
        <input type="radio" asp-for="Gender" value="Female" /> Female
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

### 🔹 Step 4: Success Page (`Success.cshtml`)

```razor
@{
    ViewData["Title"] = "Success";
}

<h3>Employee Registered Successfully!</h3>
<a asp-action="Create" class="btn btn-success">Add Another</a>
```

---

## 🔹 Razor Concepts Explained

| Concept                        | Razor Syntax                                   | Description                        |
| ------------------------------ | ---------------------------------------------- | ---------------------------------- |
| `asp-for`                      | `asp-for="Name"`                               | Binds to model property            |
| `asp-action`, `asp-controller` | `asp-action="Create"`                          | Routes form to action              |
| `asp-items`                    | `asp-items="ViewBag.Departments"`              | Binds to a list for dropdown       |
| Validation                     | `asp-validation-for="Email"`                   | Shows field-specific error         |
| Partial View                   | `<partial name="_ValidationScriptsPartial" />` | For client-side validation         |
| Layout & Section               | `_Layout.cshtml`, `@section Scripts {}`        | Structure and scripts              |
| Anchor Helpers                 | `<a asp-action="Create">`                      | Generates anchor tags with routing |
| Conditional Logic              | `@if`, `@foreach`                              | Server-side logic in view          |
| Custom Tag Helper              | Create a class implementing `TagHelper`        | Extend HTML elements behavior      |

---

## 🔧 BONUS: Create Custom Tag Helper

```csharp
// TagHelpers/UpperCaseTagHelper.cs
using Microsoft.AspNetCore.Razor.TagHelpers;

[HtmlTargetElement("input", Attributes = "asp-uppercase")]
public class UpperCaseTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.SetAttribute("style", "text-transform: uppercase");
    }
}
```

Use it in view:

```razor
<input asp-for="Name" asp-uppercase="true" />
```

Enable in `_ViewImports.cshtml`:

```razor
@addTagHelper *, YourAppName
```

---

## 📌 Summary of Razor Tag Helper Concepts

| Category            | Tag Helper Example                                                       |
| ------------------- | ------------------------------------------------------------------------ |
| Input               | `<input asp-for="Name" />`                                               |
| Label               | `<label asp-for="Name"></label>`                                         |
| Select/Dropdown     | `<select asp-for="Department" asp-items="ViewBag.Departments"></select>` |
| Checkbox            | `<input asp-for="IsPermanent" />`                                        |
| Validation          | `<span asp-validation-for="Email"></span>`                               |
| Anchor/Link         | `<a asp-action="Success"></a>`                                           |
| Form Routing        | `<form asp-action="Create">`                                             |
| Partial View        | `<partial name="_ValidationScriptsPartial" />`                           |
| Layout and Sections | `@RenderBody()`, `@section Scripts {}`                                   |
| Custom Tag Helpers  | `asp-uppercase`, etc.                                                    |

---

Would you like me to generate the full downloadable project or a Bootstrap-styled advanced form as next step?
now i am having going with bootstrap code 

in the layout page (add reference )	
------------------
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />

so you can put this link in create view also
and also layout anywhere u can put 

update the create form with bootstrap 
-----------------------------------
@model Employee

@{
    ViewData["Title"] = "Register Employee";
}

<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />

<div class="container mt-4">
    <h2 class="mb-4">@ViewData["Title"]</h2>

    <form asp-action="Create" method="post">
        <div class="mb-3">
            <label asp-for="Name" class="form-label"></label>
            <input asp-for="Name" class="form-control" asp-uppercase="true" />
            <span asp-validation-for="Name" class="text-danger"></span>
        </div>

        <div class="mb-3">
            <label asp-for="Age" class="form-label"></label>
            <input asp-for="Age" class="form-control" />
            <span asp-validation-for="Age" class="text-danger"></span>
        </div>

        <div class="mb-3">
            <label asp-for="Email" class="form-label"></label>
            <input asp-for="Email" class="form-control" />
            <span asp-validation-for="Email" class="text-danger"></span>
        </div>

        <div class="mb-3">
            <label asp-for="Department" class="form-label"></label>
            <select asp-for="Department" asp-items="ViewBag.Departments" class="form-select"></select>
            <span asp-validation-for="Department" class="text-danger"></span>
        </div>

        <div class="form-check mb-3">
            <input asp-for="IsPermanent" class="form-check-input" />
            <label asp-for="IsPermanent" class="form-check-label"></label>
        </div>

        <div class="mb-3">
            <label class="form-label">Gender:</label><br />
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" asp-for="Gender" value="Male" />
                <label class="form-check-label">Male</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" asp-for="Gender" value="Female" />
                <label class="form-check-label">Female</label>
            </div>
        </div>

        <button type="submit" class="btn btn-primary">Submit</button>
    </form>

    @section Scripts {
        <partial name="_ValidationScriptsPartial" />
    }
</div>

now sucess page updated code 
------------------------
<!-- Views/Employee/Success.cshtml -->
@{
    ViewData["Title"] = "Success";
}
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />

<div class="container mt-5">
    <div class="alert alert-success">
        <h4>Employee Registered Successfully!</h4>
    </div>
    <a asp-action="Create" class="btn btn-success">Add Another</a>
</div>
final code 
-----------
Absolutely! Here's the **step-by-step code demo** of using **Bootstrap styling with Tag Helpers in ASP.NET Core 8.0**. This builds upon the previous Tag Helpers demo by incorporating **Bootstrap 5.3** classes to make the form UI modern and responsive.

---

## ✅ Bootstrap-Styled Tag Helpers Demo in Steps

---

### 🔹 Step 1: Reference Bootstrap CSS

Add this inside your `Create.cshtml`:

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
```

Place it at the top of the view or in `_Layout.cshtml` for global styling.

---

### 🔹 Step 2: Update the Form with Bootstrap Classes

Here’s the updated view: `Views/Employee/Create.cshtml`

```razor
@model Employee

@{
    ViewData["Title"] = "Register Employee";
}

<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />

<div class="container mt-4">
    <h2 class="mb-4">@ViewData["Title"]</h2>

    <form asp-action="Create" method="post">
        <div class="mb-3">
            <label asp-for="Name" class="form-label"></label>
            <input asp-for="Name" class="form-control" asp-uppercase="true" />
            <span asp-validation-for="Name" class="text-danger"></span>
        </div>

        <div class="mb-3">
            <label asp-for="Age" class="form-label"></label>
            <input asp-for="Age" class="form-control" />
            <span asp-validation-for="Age" class="text-danger"></span>
        </div>

        <div class="mb-3">
            <label asp-for="Email" class="form-label"></label>
            <input asp-for="Email" class="form-control" />
            <span asp-validation-for="Email" class="text-danger"></span>
        </div>

        <div class="mb-3">
            <label asp-for="Department" class="form-label"></label>
            <select asp-for="Department" asp-items="ViewBag.Departments" class="form-select"></select>
            <span asp-validation-for="Department" class="text-danger"></span>
        </div>

        <div class="form-check mb-3">
            <input asp-for="IsPermanent" class="form-check-input" />
            <label asp-for="IsPermanent" class="form-check-label"></label>
        </div>

        <div class="mb-3">
            <label class="form-label">Gender:</label><br />
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" asp-for="Gender" value="Male" />
                <label class="form-check-label">Male</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" asp-for="Gender" value="Female" />
                <label class="form-check-label">Female</label>
            </div>
        </div>

        <button type="submit" class="btn btn-primary">Submit</button>
    </form>

    @section Scripts {
        <partial name="_ValidationScriptsPartial" />
    }
</div>
```

---

### 🔹 Step 3: Success Page with Bootstrap

```razor
<!-- Views/Employee/Success.cshtml -->
@{
    ViewData["Title"] = "Success";
}
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />

<div class="container mt-5">
    <div class="alert alert-success">
        <h4>Employee Registered Successfully!</h4>
    </div>
    <a asp-action="Create" class="btn btn-success">Add Another</a>
</div>
```

---

### ✅ Bootstrap Classes Used

| Tag Helper Field   | Bootstrap Classes Used                 |
| ------------------ | -------------------------------------- |
| Text fields        | `form-control`                         |
| Label              | `form-label`                           |
| Dropdown           | `form-select`                          |
| Checkbox           | `form-check-input`, `form-check-label` |
| Radio buttons      | `form-check`, `form-check-inline`      |
| Validation errors  | `text-danger`                          |
| Button             | `btn`, `btn-primary`, `btn-success`    |
| Container & margin | `container`, `mb-3`, `mt-4`            |
| Alert box          | `alert alert-success`                  |

---

### 🧠 Tip: For consistent styling

Instead of adding Bootstrap per view, place this in `_Layout.cshtml`:

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
```

And use `@RenderBody()` for layout content.

---

Let me know if you want:

* A full Bootstrap form with cards and modal popups
* Responsive layout using Bootstrap grid
* Error handling using Bootstrap alerts

Would you like a Razor Pages version next?

-------------------start here ---------------------
Now i want to add some more classes into the same program but with annotations which will provide valdiation to me according constraints will be imposed on the table from 
 the classe and I also want to impose fleunt api to provide realtionship .
 
 so some classes i will be adding and again same commands i will be using 
 
 so now add classes Author1 ,Course1 ,Employee ,UserDetail into the models folder 
 
 
 namespace CodeFirstEntityFrameworkDemo.Models
{
    public class Author1
    {

        public int Id { set; get; }
        public string Name { set; get; }
        public IList<Course1> Courses { set; get; }
    }
}



 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class Course1
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }// not an identity column 

        [Required]
        [Column("Stitle", TypeName = "varchar")]
        public string Title { set; get; }

        [Required]
        [MaxLength(220)]
        public string Description { set; get; }


        public float fullprice { set; get; }


        [ForeignKey("Author")]
        public int AuthorId { set; get; }

        public Author1 Author { set; get; }

    }
}

 
 using System.ComponentModel.DataAnnotations;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class Employee
    {
         public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your firstname")]
        public string? FirstName { set; get; }

        [Required(ErrorMessage = "Please enter your lastname")]
        public string? LastName { set; get; }

        [Required(ErrorMessage = "Please enter email id")]
        [EmailAddress(ErrorMessage = "Please enter valid email id")]
        public string? Email { set; get; }

        [Required(ErrorMessage = "Please enter your age")]
        [Range(0, 100, ErrorMessage = "Please enter your age betwen 1 to 100 only ")]
        
        public int Age { set; get; }
    }
}



 
 
 using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class UserDetail
    {
         public int Id { get; set; } 
    
        [Required(ErrorMessage = "User Name is Required")]
        [StringLength(15, ErrorMessage = "User Name cannot be more than 15 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(11, MinimumLength = 5, ErrorMessage = "Minimum Length of Password is 5 letters or Max Length is of 11 letters..")]
        [DataType("password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Date Of Birth is Required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Please enter valid Email Id")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Postal Code is Required")]
        [Range(100, 1000, ErrorMessage = "Must be between 100 and 1000")]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [DisplayName("Phone Number")]
        public int PhoneNo { get; set; }

        [Required(ErrorMessage = "Profile is Required")]
        [DataType(DataType.MultilineText)]
        public string Profile { get; set; }

    }
}

 
updte in eventcontext 

-------------------------
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class EventContext:DbContext
    {

        public EventContext()
        {
                
        }

        public EventContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Author> authors { get; set; }

        public DbSet<Course> courses { get; set; }

        public DbSet<Student> students { get; set; }

        public DbSet<UserDetail> userdetails { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<Author1> authors1 { get; set; }

        public DbSet<Course1> courses1 { get; set; }    
    }
}

 Now i want to use fluent api and do the work which was done by data annotations and also i need to seed the data into some table by default
 by usinng fluent api so let us add some tables and provide relationship and include annotaions using fluent api 
 
 
 
 Now add 3 classes liek this Author2 ,Course2 and UserDetail2 and in that add previous proepties only but remove all annotoations from top of proeprties 
 
 namespace CodeFirstEntityFrameworkDemo.Models
{
    public class Author2
    {

        public int Id { set; get; }
        public string Name { set; get; }
        public IList<Course2> Courses { set; get; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class Course2
    {

        public int Id { set; get; }// not an identity column 

        
        public string Title { set; get; }

       
        public string Description { set; get; }


        public float fullprice { set; get; }


        public int Author2Id { set; get; }

        public Author2 Author { set; get; }
    }
}


 using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class UserDetail2
    {

        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? NewPassword { get; set; }

        
        public string? ConfirmPassword { get; set; }

        
        public DateTime DateOfBirth { get; set; }

        
        public string? Email { get; set; }

        
        public int PostalCode { get; set; }

        
        public int PhoneNo { get; set; }

        
        public string Profile { get; set; }


    }
}

 using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstEntityFrameworkDemo.Models
{
    public class EventContext:DbContext
    {

        public EventContext()
        {
                
        }

        public EventContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Author> authors { get; set; }

        public DbSet<Course> courses { get; set; }

        public DbSet<Student> students { get; set; }

        public DbSet<UserDetail> userdetails { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<Author1> authors1 { get; set; }

        public DbSet<Course1> courses1 { get; set; }  
        
        public DbSet<Course2> courses2 { get; set; }

        public DbSet<Author2 > authors2 { get; set; }   

        public DbSet<UserDetail2> userdetails2 { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           

            // Fluent API for Author2
            modelBuilder.Entity<Author2>(entity =>
            {

               

                entity.HasKey(a => a.Id); // Primary Key
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);

                // Relationship with Course2
                entity.HasMany(a => a.Courses)
                      .WithOne(c =>c.Author)
                      .HasForeignKey(c => c.Author2Id)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete
            });

            // Fluent API for Course2
            modelBuilder.Entity<Course2>(entity =>
            {
                entity.HasKey(c => c.Id); // Primary Key

                entity.Property(c => c.Id)
                      .ValueGeneratedNever(); // Not an identity column

                entity.Property(c => c.Title)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("Stitle")
                      .HasColumnType("varchar");

                entity.Property(c => c.Description)
                      .IsRequired()
                      .HasMaxLength(220);

                entity.Property(c => c.fullprice)
                      .HasColumnType("float")
                      .IsRequired();

                // Foreign Key to Author1
                entity.HasOne(c => c.Author)
                      .WithMany(a => a.Courses)
                      .HasForeignKey(c => c.Author2Id);
            });


            // Fluent API for UserDetail
            modelBuilder.Entity<UserDetail2>(entity =>
            {
                entity.HasKey(u => u.Id); // Primary Key

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(u => u.NewPassword)
                      .IsRequired()
                      .HasMaxLength(11);

                entity.Property(u => u.DateOfBirth)
                      .IsRequired()
                      .HasColumnType("date");

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnType("varchar");

                entity.Property(u => u.PostalCode)
                      .IsRequired()
                      .HasColumnType("int");

                entity.Property(u => u.PhoneNo)
                      .IsRequired();

                entity.Property(u => u.Profile)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");
            });

            // Seed data for Author1 and Course1
            modelBuilder.Entity<Author2>().HasData(
                new Author2 { Id = 1, Name = "Author One" },
                new Author2 { Id = 2, Name = "Author Two" }
            );

            modelBuilder.Entity<Course2>().HasData(
                new Course2 { Id = 1, Title = "Course A", Description = "Description A", fullprice = 100, Author2Id = 1 },
                new Course2 { Id = 2, Title = "Course B", Description = "Description B", fullprice = 200, Author2Id = 2 }
            );
        }
    }
}


 
CRUD using code first and also will use repository pattern 
--------------------------------------------------------------
here one interface we define methods and and one class will implment that interface and that class will be used by a contoller which is nothing but repsoitory pattern 


Add one class Post like this on Models folder 

public class Post
{

	public int Id { set; get; }
	public DateTime DatePublished { set; get; }
	public string Title { set; get; }
	public string Body { set; get; }
}

add in EventContext the the DBSet 

public DbSet<Post> posts { get; set; }

build the solution 

add migrations 

see the table in db it will be there 

create one folder Repositories 

in that add one inetface IPost and and one class PostRepository


using CodeFirstEntityFrameworkDemo.Models;

namespace CodeFirstEntityFrameworkDemo.Repositories
{
    public interface IPost
    {

        List<Post> GetPosts();

        Post GetPostByID(int postid);

        void InsertPost(Post post);

        void DeletePost(int postid);

        void UpdatePost(Post post);

        void save();
    }
}



using CodeFirstEntityFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEntityFrameworkDemo.Repositories
{
    public class PostRepository : IPost
    {
        private EventContext context;

        public PostRepository(EventContext cnt)
        {
            this.context = cnt;
        }
        public void DeletePost(int postid)
        {
            Post post = context.posts.Find(postid);
            context.posts.Remove(post);
        }
        public Post GetPostByID(int postid)
        {
            return context.posts.Find(postid);

        }
        public List<Post> GetPosts()
        {
            return context.posts.ToList();
        }

        public void InsertPost(Post post)
        {
            context.posts.Add(post);
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;


        }
    }
}

register this in Program.cs file 
----------------------------------
after the EventContext of buider u add this below line 

builder.Services.AddScoped<IPost,PostRepository>();  


Post Contoller 
-------------
using CodeFirstEntityFrameworkDemo.Models;
using CodeFirstEntityFrameworkDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEntityFrameworkDemo.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postRepository;

        public PostController(IPost postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Post
        public IActionResult Index()
        {
            var posts = _postRepository.GetPosts();
            return View(posts);
        }

        // GET: Post/Details/5
        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPostByID(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _postRepository.InsertPost(post);
                _postRepository.save();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Edit/5
        public IActionResult Edit(int id)
        {
            var post = _postRepository.GetPostByID(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _postRepository.UpdatePost(post);
                    _postRepository.save();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public IActionResult Delete(int id)
        {
            var post = _postRepository.GetPostByID(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _postRepository.DeletePost(id);
            _postRepository.save();
            return RedirectToAction(nameof(Index));
        }
    }
}


