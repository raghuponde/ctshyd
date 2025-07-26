
SearchCustomer 
---------------

In db :
-----------
select CustomerID,ContactTitle,CompanyName from [dbo].[Customers] where 
ContactName='Yang Wang'


in front end 
---------------

public IActionResult SearchCustomer(string contactname)
{
    var searchcustomers = from cust in cnt.Customers
                          where
                        cust.ContactName == contactname
                          select new Customer
                          {
                              CustomerId = cust.CustomerId,
                              ContactTitle = cust.ContactTitle,
                              CompanyName = cust.CompanyName
                          };

    var query1 = searchcustomers.Single();

    return View(query1);
}

view 
--------
@model EntityFrameWorkDemo1.Models.Customer

@{
    ViewData["Title"] = "SearchCustomer";
}

<h1>SearchCustomer</h1>

<div>
    <h4>Customer</h4>
    <hr />
    <dl class="row">
        <dt class="col-sm-2">
            @Html.DisplayNameFor(model => model.CustomerId)
        </dt>
        <dd class="col-sm-10">
            @Html.DisplayFor(model => model.CustomerId)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.CompanyName)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.CompanyName)
        </dd>
        
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.ContactTitle)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.ContactTitle)
        </dd>
        
    </dl>
</div>
<div>
    <a asp-action="Edit" asp-route-id="@Model?.CustomerId">Edit</a> |
    <a asp-action="Index">Back to List</a>
</div>



In db :
----------
select * from categories 
select * from products

select p1.ProductName ,c1.CategoryName from Products p1 join 
Categories c1 on p1.CategoryID=c1.CategoryID where c1.CategoryName='Beverages'


In front end using ling 

------------------------
add one class Prodcat in Models folder it should like this which will take one column from prodcut one column from categoroy 
namespace EntityFrameWorkDemo1.Models
{
    public class ProdCat
    {
        public string prodname { set; get; }
        public string catname { set; get; }
    }
}

then write this action method as belwo 

earlie theere was one model for a view now multiple models are inolved for a sinle view and i am doing join operation also 

go to Product Table in models and add this navugation proeprty to it if it is not theer 

 public virtual Category? Category { get; set; }
 
 

 public IActionResult ProductsInCategory(string categoryname)
 {
     var productsincategory = from prod in cnt.Products
                              where
                            prod.Category.CategoryName == categoryname
                              select new ProdCat
                              {
                                  prodname= prod.ProductName,
                                  catname = prod.Category.CategoryName
                              };

     return View(productsincategory);
 }


view 
------
@model IEnumerable<EntityFrameWorkDemo1.Models.ProdCat>

@{

    ViewData["Title"] = "ProductsInCategory";
}

<h1>ProductsInCategory</h1>


    <div class="row">
        <div class="col-lg-9">
            <h3>Products In Category:</h3>
        </div>
        <form method="get" action="/NorthWind/ProductsInCategory">
            <!--not jumping to post in this page only display -->
            <div class="col-lg-2">
                <input type="text" id="Category" name="categoryname" class="form-control " />
            </div>
            <div class="col-lg-1">
                <input type="submit" id="searchproducts" class="btn btn-primary" value="search" />
            </div>
        </form>
    </div>

@if (Model.Count() == 0)
{
    <table>
        <tr>
            <td></td>
        </tr>
    </table>

}
else
{
    <div class="row">


        <div class="col-lg-12">

            <table id="prodlist" width="80%" cellpadding="7" cellspacing="7" class="table-striped table-hover">

                <thead>
                    <tr>
                        <th style="padding:15px">ProdName</th>
                        <th style="padding:15px">Category</th>

                    </tr>

                </thead>
                <tbody>
                    @foreach (var prod in Model)
                    {


                        <tr>
                            <td style="padding:15px">@prod.prodname</td>
                            <td style="padding:15px">@prod.catname</td>


                        </tr>


                    }
                </tbody>
            </table>

        </div>
    </div>
}

-- give me all the customers who have orders count more than 10 

select CustomerID, count(OrderID)  from Orders group by CustomerID having count(OrderID) > 10


first add one class in Models folder 

namespace EntityFrameWorkDemo1.Models
{
    public class CustomerRange
    {
        public string CustomerId { get; set; }
        public string ContactName { get; set; } = string.Empty;

        public int? orderscount { get; set; }
    }
}

check in product class of model do u have collection navigation proeprty for orders if not add it or leave it if it is there 

 public virtual ICollection<Order> Orders { get; set; }
 

and action method is like this using that above model 

 public IActionResult OrderRange(string range)
 {
     int range1 = Convert.ToInt32(range);
     var custorderscount = from cust in cnt.Customers
                           where cust.Orders.Count > range1
                           select new CustomerRange
                           {
                               CustomerId = cust.CustomerId,
                               ContactName = cust.ContactName,
                               orderscount=cust.Orders.Count,
                               
                           };

     return View(custorderscount);
 }
 
 view 
 ----
 @model IEnumerable<EntityFrameWorkDemo1.Models.CustomerRange>

@{
    ViewData["Title"] = "OrderRange";
}
<div class="row">
    <div class="col-lg-9">
        <h3>Find Customers more than :</h3>
    </div>
    <form method="get" action="/NorthWind/OrderRange">
        <div class="col-lg-2">
            <select name="range" id="orderrange">
                <option selected value="">Select Range</option>
                <option value="5">Five</option>
                <option value="10">Ten</option>
                <option value="15">Fifteen</option>
                <option value="20">Twenty</option>
            </select>
        </div>
        <div class="col-lg-1">
            <br />
            <input type="submit" id="findcustomers"
                   value="searchcustomerorders" class="btn btn-primary" />
        </div>
    </form>
</div>
@if (Model.Count() == 0)
{
    <table>
        <tr>
            <td></td>
        </tr>
    </table>
}
else
{
    <div class="row">
        <div class="col-lg-12">
            <table id="orderlist" width="80%" cellpadding="7" cellspacing="7"
                   class="table-striped table-hover">
                <thead>
                    <tr>
                        <th>Customer Id </th>
                        <th>Contact Name </th>
                        <th>No of orders </th>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var cust in Model)
                    {
                        <tr>
                            <td style="padding:15px">@cust.CustomerId </td>
                            <td style="padding:15px">@cust.ContactName</td>
                            <td style="padding:15px">@cust.orderscount</td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
    </div>

}

Now i want to start web api concept okay 

First explain about web api by giving bank example and xml examples okay and now instead of xml we are using now json format and
then show google docs soa architecture and all so explain all ...

so if google docs not opening in cts then I had kept in drive u can check it so after after explaining all this 


Tell that web api will not have views they are just methods the differecnne you explain 

then add one web api controller empty one in the Controller folder only and write a normal method like this and call it like this 

see the syntax also it is extending from controlbase okay and some defualt url it is giving in controller we were following ControllerName/ActionMethod but here we have to 
follow url okay 

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeFirstEFDEmo.Models;
namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly EventContext _context;
        public DemoController(EventContext context)
        {
            _context = context;
        }
        
        public List<Employee> getemployees()
        {
          return   _context.employees.ToList();
        }
    }
}

Now call the method like this 

https://localhost:7267/api/Demo

so there u can see raw data also and json data so this is what i was talking about theory concepts here okay ...

now imagine i am having another method like this 

 
  public List<Employee> getemployees2()
        {
          return   _context.employees.ToList();
        }

then i will get this error as below 

An unhandled exception occurred while processing the request.

AmbiguousMatchException: The request matched multiple endpoints. Matches:
CodeFirstEFDEmo.Controllers.DemoController.getemployees (CodeFirstEFDEmo)
CodeFirstEFDEmo.Controllers.DemoController.getemployees2 (CodeFirstEFDEmo)

Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ReportAmbiguity(Span<CandidateState> candidateState)
Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ProcessFinalCandidates(HttpContext httpContext, Span<CandidateState> candidateState)
Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.Select(HttpContext httpContext, Span<CandidateState> candidateState)
Microsoft.AspNetCore.Routing.Matching.DfaMatcher.MatchAsync(HttpContext httpContext)
Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware.Invoke(HttpContext httpContext)
Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware.Invoke(HttpContext context)
Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware.Invoke(HttpContext context)
Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)



so i had changed the coding like this in 

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeFirstEFDEmo.Models;
namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly EventContext _context;
        public DemoController(EventContext context)
        {
            _context = context;
        }
        
        public List<Employee> getemployees()
        {
          return   _context.employees.ToList();
        }
        [Route("two")]
        public List<Employee> getemployees2()
        {
            return _context.employees.ToList();
        }
    }
}



https://localhost:7267/api/Demo/two

now  i will get the things so this depends on routing 

......................................................................................................
.                             API Response - Employee List                                           .
.                            URL: https://localhost:7267/api/Demo/two                                .
......................................................................................................
.  ID  .   First Name   .   Last Name   .        Email Address          .  Age  .
......................................................................................................
.  1   .     Kiran       .     Shukla    .       kiran@gmail.com         .  34   .
......................................................................................................
.  2   .     Mahesh      .      Babu     .      Mahesh@gmail.com         .  39   .
......................................................................................................
.  3   .     Sita        .    Dinakar    .     dinakar@yahoo.com         .  32   .
......................................................................................................


so this application or function i want to check or test using swagger tool or postman tool .

so I will install from packge manage console this command 

Install-Package Swashbuckle.AspNetCore

and do changes in program.cs like this 


Program.cs 
------------
using CodeFirstEFDEmo.Models;
using CodeFirstEFDEmo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFDEmo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IPost, PostRepository>();


            builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
            builder.Services.AddSwaggerGen();           // Adds Swagger support

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // ✅ Add this line
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

so u can see i added on top [HttpGet] if u write like this then only swagger will work here okay .

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeFirstEFDEmo.Models;
namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly EventContext _context;
        public DemoController(EventContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Employee> getemployees()
        {
          return   _context.employees.ToList();
        }
        [HttpGet]
        [Route("two")]
        public List<Employee> getemployees2()
        {
            return _context.employees.ToList();
        }
    }
}

    
https://localhost:7267/swagger/index.html


so you can see the methods there and u can try out the methods over there okay .
so both will give me same results u know as they are having same body funcitonlity okay .

Now i want to make the method 
 [HttpGet]
 public async Task< List<Employee>> getemployees()
 {
     return  await _context.employees.ToListAsync();
 }
Now again i want to return status code also and the object i want to see in swagger so 

[HttpGet]
public async Task<ActionResult< List<Employee>>> getemployees()
{
    return Ok( await _context.employees.ToListAsync());
}

so all other methods of web api u have to write like this only okay 


remember here no get and post will come just 4 method get ,post,put and delete here put means update and ModelState.IsValid is automatically vaidated here mostly we dont write like 
we write in Controller 

so complete code is like this below 


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeFirstEFDEmo.Models;
using Microsoft.EntityFrameworkCore;
namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly EventContext _context;
        public DemoController(EventContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("two")]
        public List<Employee> getemployees2()
        {
            return _context.employees.ToList();
        }
        [HttpGet]
        public async Task<ActionResult< List<Employee>>> getemployees()
        {
            return Ok( await _context.employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return BadRequest("Employee not found");
            }

            return Ok(employee);

        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee emp)
        {
            _context.employees.Add(emp);
            await _context.SaveChangesAsync();

            return Ok(await _context.employees.ToListAsync());
            //emps.Add(emp);
            //  return await Task.FromResult(Ok(emps));

        }
        [HttpPost("emp_Post2")]
        public async Task<ActionResult<Employee>> AddEmployee2(Employee employee)
        {
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();
            // emps.Add(employee);
            return Ok(employee);
        }


        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee emp)
        {

            var employee = await _context.employees.FindAsync(emp.Id);
            if (employee == null)
            {
                return BadRequest("Employee not found");
            }

            employee.FirstName = emp.FirstName;
            employee.LastName = emp.LastName;
            employee.Email = emp.Email;
            employee.Age = emp.Age;


            await _context.SaveChangesAsync();
            return Ok(await _context.employees.ToListAsync());

        }
        [HttpPut("put2")]
        public async Task<ActionResult<Employee>> UpdateEmployee2(Employee employee)
        {
            // var employee1 = emps.Find(x => x.Id == employee.Id);
            var employee1 = await _context.employees.FindAsync(employee.Id);
            if (employee1 == null)
            {
                return BadRequest("Employee Not Found");
            }
            employee1.FirstName = employee.FirstName;
            employee1.LastName = employee.LastName;
            employee1.Email = employee.Email;
            employee1.Age = employee.Age;
            await _context.SaveChangesAsync();
            return Ok(employee1);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return BadRequest("Employee not found");
            }
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok( employee);

        }

        [HttpDelete("del2/{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee2(int id)
        {
            //  var employee1 = emps.Find(x => x.Id == id);
            var employee1 = await _context.employees.FindAsync(id);
            if (employee1 == null)
            {
                return BadRequest("Employee Not Found");
            }
            _context.employees.Remove(employee1);
            await _context.SaveChangesAsync();

            return Ok(await _context.employees.ToListAsync());
        }




    }
}



