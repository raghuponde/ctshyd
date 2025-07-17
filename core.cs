
	
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




