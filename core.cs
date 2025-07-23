

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



-------------------start here ---------------------
 
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

new code
------------
In Models Folder 
---------------
using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFDEmo.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}



using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstEFDEmo.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Who Buyed")]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } 
    }
}

code added in EventContext class now like this 

-------------------------------------------------
public DbSet<Customer> Customers { get; set; }
public DbSet<Product> Products { get; set; }

Then gone in TransactionController 
-------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeFirstEFDEmo.Models;

namespace CodeFirstEFDEmo.Controllers
{
    public class TransactionController : Controller
    {
        private readonly EventContext _context;

        public TransactionController(EventContext context)
        {
            _context = context;
        }

        // ----------------- CUSTOMER CREATE -----------------

       [HttpGet]
 public IActionResult CreateCustomer()
 {
     return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]

 public IActionResult CreateCustomer(Customer customer)
 {
     // Example condition: remove CustomerName if it's just whitespace
         ModelState.Clear();
         ModelState.Remove(nameof(customer.CustomerID));
         // optionally assign a default value
         // customer.CustomerName = "Guest";
      

     if (ModelState.IsValid)
     {
         _context.Customers.Add(customer);
         _context.SaveChanges();
         return RedirectToAction("CreateProduct");
     }

     return View(customer);
 }


 [HttpGet]
 public IActionResult CreateProduct()
 {
     ViewBag.CustomerList = new SelectList(_context.Customers, "CustomerID", "CustomerName");
     return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public IActionResult CreateProduct(Product product)
 {
     ModelState.Clear();
     ModelState.Remove(nameof(product.ProductID));
     if (ModelState.IsValid)
     {
         _context.Products.Add(product);
         _context.SaveChanges();
         return RedirectToAction("CreateProduct");
     }

     ViewBag.CustomerList = 
         new SelectList(_context.Customers, "CustomerID", "CustomerName", product.CustomerID);
     return View(product);
 }
    }
}

Views created like this 
------------------------

@model CodeFirstEFDEmo.Models.Customer

<h2>Create Customer</h2>

<form asp-action="CreateCustomer" method="post">
    <div>
        <label asp-for="CustomerName"></label>
        <input asp-for="CustomerName" />
        <span asp-validation-for="CustomerName"></span>
    </div>
    <button type="submit">Save</button>
</form>



@model CodeFirstEFDEmo.Models.Product

<h2>Create Product</h2>

<form asp-action="CreateProduct" method="post">
    <div>
        <label asp-for="ProductName"></label>
        <input asp-for="ProductName" />
        <span asp-validation-for="ProductName"></span>
    </div>

    <div>
        <label asp-for="CustomerID">Who Buyed</label>
        <select asp-for="CustomerID" asp-items="ViewBag.CustomerList">
            <option value="">-- Select Customer --</option>
        </select>
        <span asp-validation-for="CustomerID"></span>
    </div>

    <button type="submit">Save</button>
</form>

For the above coding the value is not getting inserted in the table Customers in database what can be errror 
