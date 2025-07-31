
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

Here i had written the code in web api controller i want it to put it in interface IEmployee and I want to implement EmployeeService and then i want to use it in web api controller where i will call a web api controller with read and write actions so provide me the three codes one is IEmployee and then EmployeeService implementing interface and finally give create a new EmployeeController of web api with read and write actions subsitting Employee service


✅ 1. IEmployee.cs (Interface)

using CodeFirstEFDEmo.Models;

namespace CodeFirstEFDEmo.Services
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee?> UpdateEmployeeAsync(Employee employee);
        Task<Employee?> DeleteEmployeeAsync(int id);
    }
}
✅ 2. EmployeeService.cs (Service Implementation)

using CodeFirstEFDEmo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFDEmo.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EventContext _context;

        public EmployeeService(EventContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee emp)
        {
            var existing = await _context.employees.FindAsync(emp.Id);
            if (existing == null) return null;

            existing.FirstName = emp.FirstName;
            existing.LastName = emp.LastName;
            existing.Email = emp.Email;
            existing.Age = emp.Age;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null) return null;

            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
✅ 3. EmployeeController.cs (Web API Controller using the service)

using CodeFirstEFDEmo.Models;
using CodeFirstEFDEmo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll()
        {
            return Ok(await _employeeService.GetAllEmployeesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound("Employee not found");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            var added = await _employeeService.AddEmployeeAsync(employee);
            return Ok(added);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> Update(Employee employee)
        {
            var updated = await _employeeService.UpdateEmployeeAsync(employee);
            if (updated == null)
                return NotFound("Employee not found");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if (deleted == null)
                return NotFound("Employee not found");
            return Ok(deleted);
        }
    }
}
✅ 4. Register Service in Startup.cs or Program.cs
If using ASP.NET Core 6 or later (Program.cs):


builder.Services.AddScoped<IEmployee, EmployeeService>();


so for the above program the model is like this so i need Image Upload of employee and pagination code and validation code as well to be added give me complete code for it 

To fully implement image upload, pagination, and validation in your ASP.NET Core Web API for the Employee model, I’ll provide a complete working example divided into:

✅ Updated Employee Model with ImagePath

✅ Interface IEmployee

✅ Service EmployeeService with pagination and image upload

✅ Controller EmployeeController with model validation and pagination

✅ Configure File Upload Folder

✅ Dependency Injection in Program.cs


✅ 1. Employee.cs Model (Add ImagePath)

using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFDEmo.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your firstname")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your lastname")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        [EmailAddress(ErrorMessage = "Please enter valid email id")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your age")]
        [Range(0, 100, ErrorMessage = "Age should be between 1 to 100")]
        public int Age { get; set; }

        public string? ImagePath { get; set; }
    }
}

✅ 2. Interface: IEmployee.cs

using CodeFirstEFDEmo.Models;

namespace CodeFirstEFDEmo.Services
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee, IFormFile image);
        Task<Employee?> UpdateEmployeeAsync(Employee employee, IFormFile? image);
        Task<Employee?> DeleteEmployeeAsync(int id);
    }
}


✅ 3. Service: EmployeeService.cs

using CodeFirstEFDEmo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFDEmo.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EventContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeService(EventContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(_env.WebRootPath, "uploads", imageName);

                Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
                using var stream = new FileStream(imagePath, FileMode.Create);
                await image.CopyToAsync(stream);

                employee.ImagePath = "/uploads/" + imageName;
            }

            _context.employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee emp, IFormFile? image)
        {
            var existing = await _context.employees.FindAsync(emp.Id);
            if (existing == null) return null;

            existing.FirstName = emp.FirstName;
            existing.LastName = emp.LastName;
            existing.Email = emp.Email;
            existing.Age = emp.Age;

            if (image != null && image.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(_env.WebRootPath, "uploads", imageName);

                Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
                using var stream = new FileStream(imagePath, FileMode.Create);
                await image.CopyToAsync(stream);

                existing.ImagePath = "/uploads/" + imageName;
            }

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var emp = await _context.employees.FindAsync(id);
            if (emp == null) return null;

            _context.employees.Remove(emp);
            await _context.SaveChangesAsync();
            return emp;
        }
    }
}

 4. EmployeeController.cs

using CodeFirstEFDEmo.Models;
using CodeFirstEFDEmo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _employeeService.GetAllEmployeesAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            if (emp == null)
                return NotFound("Employee not found");
            return Ok(emp);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create([FromForm] Employee emp, IFormFile image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.AddEmployeeAsync(emp, image);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Update(int id, [FromForm] Employee emp, IFormFile? image)
        {
            if (id != emp.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _employeeService.UpdateEmployeeAsync(emp, image);
            if (updated == null)
                return NotFound("Employee not found");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if (deleted == null)
                return NotFound("Employee not found");

            return Ok(deleted);
        }
    }
}


✅ 5. Enable Static File Serving and Folder
In Program.cs:


app.UseStaticFiles(); // Enable wwwroot for uploads

Create the folder:


wwwroot/uploads/

✅ 6. Register Services in Program.cs

builder.Services.AddScoped<IEmployee, EmployeeService>();


Example Form Data to Test in Postman:
Method: POST
URL: https://localhost:5001/api/Employee
Body: form-data

Key: FirstName → Value: John

Key: LastName → Value: Doe

Key: Email → Value: john@example.com

Key: Age → Value: 30

Key: image → Type: File → Select an image


Prompt : 
I am in asp.net core mvc application and in controller folder only i had added API controller Now how do i show the data from 
    Web Api to MVC Controller by using jquery ajax method for the above coding which i have done i want to use bootswatch Quartz theme here 

Go to layout of shared folder comment the normal available bootstrap css file and add the quartz css okay 

@*   <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" /> *@
  <link href="https://cdn.jsdelivr.net/npm/bootswatch@5.3.2/dist/quartz/bootstrap.min.css" rel="stylesheet">

Then in the HomeController add Index2 method like this remember when u are actually doing the project the the name u give it as EmpUIController which is consuming the web api 

 public IActionResult Index2()
 {
     return View();
 }

generate an empty view for the above method and remove the default design for it okay 

and paste below code 

@{
    ViewData["Title"] = "Employee Directory";
}

<div class="container mt-4">
    <div class="card shadow">
        <div class="card-header bg-primary text-white">
            <h4><i class="bi bi-people-fill"></i> Employee Directory</h4>
        </div>
        <div class="card-body">
            <table class="table table-hover table-bordered text-center" id="employeeTable">
                <thead class="table-dark">
                    <tr>
                        <th>Image</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>
</div>

@section Scripts {
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script>
        $(document).ready(function () {
            loadEmployees();
        });

        function loadEmployees() {
            $.ajax({
                url: 'https://localhost:7267/api/Demo',
                type: 'GET',
                success: function (data) {
                    var tbody = $("#employeeTable tbody");
                    tbody.empty();

                    $.each(data, function (i, emp) {
                        var row = `<tr>
                                    <td><img src="${emp.imageUrl}" class="img-thumbnail rounded-circle" width="60" height="60" /></td>
                                    <td>${emp.firstName}</td>
                                    <td>${emp.lastName}</td>
                                    <td><i class="bi bi-envelope-fill text-primary"></i> ${emp.email}</td>
                                </tr>`;
                        tbody.append(row);
                    });
                },
                error: function (xhr, status, error) {
                    alert("Failed to load data: " + error);
                }
            });
        }
    </script>
}

you see in this above code what i have done is url which i have given is of Demoweb api controller url whhere i had written the code directly and at that time image were not uploaded 
and demo controller normal crud operations are there here 

Now run the applicationn u can see the design so like this u have to consume  a web api using asp.core mvc controller okay .

so now above web api which is there in EmployeeController I am modfying further  to make it better okay and finally will go for design okay .


Prompt :
---------
Yes give me image delete logic also and return full image URLs so i want this update in backend 


 1. Update EmployeeService.cs

using CodeFirstEFDEmo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFDEmo.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EventContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(EventContext context, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        private string SaveImageToUploads(IFormFile image)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fullPath = Path.Combine(uploadPath, imageName);
            using var stream = new FileStream(fullPath, FileMode.Create);
            image.CopyTo(stream);

            return "/uploads/" + imageName;
        }

        private void DeleteImageFile(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var employees = await _context.employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            string baseUrl = GetBaseUrl();
            employees.ForEach(e =>
            {
                if (!string.IsNullOrEmpty(e.ImagePath))
                {
                    e.ImagePath = baseUrl + e.ImagePath;
                }
            });

            return employees;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            var emp = await _context.employees.FindAsync(id);
            if (emp != null && !string.IsNullOrEmpty(emp.ImagePath))
            {
                emp.ImagePath = GetBaseUrl() + emp.ImagePath;
            }

            return emp;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                employee.ImagePath = SaveImageToUploads(image);
            }

            _context.employees.Add(employee);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(employee.ImagePath))
                employee.ImagePath = GetBaseUrl() + employee.ImagePath;

            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee emp, IFormFile? image)
        {
            var existing = await _context.employees.FindAsync(emp.Id);
            if (existing == null) return null;

            existing.FirstName = emp.FirstName;
            existing.LastName = emp.LastName;
            existing.Email = emp.Email;
            existing.Age = emp.Age;

            if (image != null && image.Length > 0)
            {
                DeleteImageFile(existing.ImagePath);
                existing.ImagePath = SaveImageToUploads(image);
            }

            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(existing.ImagePath))
                existing.ImagePath = GetBaseUrl() + existing.ImagePath;

            return existing;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var emp = await _context.employees.FindAsync(id);
            if (emp == null) return null;

            DeleteImageFile(emp.ImagePath);
            _context.employees.Remove(emp);
            await _context.SaveChangesAsync();

            emp.ImagePath = null; // optional to avoid exposing deleted image URL
            return emp;
        }
    }
}

 2. IEmployee.cs (No change required, but for reference)
You can leave the interface as is, since implementation logic change doesn't affect method signatures.

🔧 3. Program.cs — Register IHttpContextAccessor
Make sure this is added:


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IEmployee, EmployeeService>();


✅ Final Result
Image is deleted from the server when the employee is deleted or updated with a new image.

All returned ImagePath fields are full URLs like:


https://localhost:5001/uploads/abc123.jpg


Let me know if you also want:

A separate endpoint to change just the image

A default image when none is uploaded

Integration with Angular or React front end for testing


A default image when none is uploaded ...that logic i want 


To implement a default image logic when none is uploaded for an Employee, you’ll need to:

Provide a default image in the wwwroot/uploads/ folder.

Ensure the service assigns the default image path if the uploaded image is missing.

Always return the full URL for the default image as well.

✅ Step-by-Step Implementation
📁 1. Add a Default Image File
Place a file named default.jpg inside:


wwwroot/uploads/default.jpg
This can be a generic avatar image.

✏️ 2. Update EmployeeService.cs — Use Default Image if None Provided
🔄 Update/Add the following logic inside AddEmployeeAsync method:

public async Task<Employee> AddEmployeeAsync(Employee employee, IFormFile? image)
{
    if (image != null && image.Length > 0)
    {
        employee.ImagePath = SaveImageToUploads(image);
    }
    else
    {
        employee.ImagePath = "/uploads/default.jpg";
    }

    _context.employees.Add(employee);
    await _context.SaveChangesAsync();

    employee.ImagePath = GetBaseUrl() + employee.ImagePath;
    return employee;
}
🧠 Also Ensure This Line is Present in GetAllEmployeesAsync and GetEmployeeByIdAsync
This ensures all returned images (default or uploaded) include the full URL:


e.ImagePath = string.IsNullOrEmpty(e.ImagePath)
    ? GetBaseUrl() + "/uploads/default.jpg"
    : GetBaseUrl() + e.ImagePath;
For example, in GetEmployeeByIdAsync:

public async Task<Employee?> GetEmployeeByIdAsync(int id)
{
    var emp = await _context.employees.FindAsync(id);
    if (emp != null)
    {
        emp.ImagePath = string.IsNullOrEmpty(emp.ImagePath)
            ? GetBaseUrl() + "/uploads/default.jpg"
            : GetBaseUrl() + emp.ImagePath;
    }
    return emp;
}

public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
{
    var employees = await _context.employees
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    string baseUrl = GetBaseUrl();

    foreach (var e in employees)
    {
        e.ImagePath = string.IsNullOrEmpty(e.ImagePath)
            ? baseUrl + "/uploads/default.jpg"
            : baseUrl + e.ImagePath;
    }

    return employees;
}

✅ Final Behavior
Scenario	Resulting ImagePath
User uploads an image	/uploads/guid-filename.jpg → Full URL returned
User does not upload an image	/uploads/default.jpg → Full URL returned
On update, no new image given	Keeps existing image or defaults if none existed
On deletion	Deletes actual image (not default.jpg)

🔒 Optional: Skip Deletion of Default Image
In DeleteImageFile, skip deletion if it’s the default:


private void DeleteImageFile(string? imagePath)
{
    if (string.IsNullOrEmpty(imagePath) || imagePath.Contains("default.jpg"))
        return;

    var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
    if (File.Exists(fullPath))
    {
        File.Delete(fullPath);
    }
}


Let me know if you want the same logic applied in an export/download endpoint or if you'd like to display only selected columns like name, email, image URL.

For both options give me the code 


Here’s the complete solution to your request, covering:

✅ GetAllEmployeesAsync returning all fields with full image URL or default.

✅ A second option: GetAllEmployeeBasicInfoAsync returning only selected fields (e.g., Name, Email, Image URL).

✅ Corresponding endpoint GetBasicEmployeeList in the controller.



    ✅ 1. Full Employee Records with Image URL – GetAllEmployeesAsync
This already exists (but included again for clarity):


public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
{
    var employees = await _context.employees
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    string baseUrl = GetBaseUrl();

    foreach (var e in employees)
    {
        e.ImagePath = string.IsNullOrEmpty(e.ImagePath)
            ? baseUrl + "/uploads/default.jpg"
            : baseUrl + e.ImagePath;
    }

    return employees;
}


✅ 2. Return Only Selected Fields – GetAllEmployeeBasicInfoAsync
Create a DTO class first:

📁 Models/EmployeeBasicDto.cs

namespace CodeFirstEFDEmo.Models
{
    public class EmployeeBasicDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
    }
}


Add Method in EmployeeService.cs

public async Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize)
{
    var employees = await _context.employees
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    string baseUrl = GetBaseUrl();

    var basicList = employees.Select(e => new EmployeeBasicDto
    {
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        ImageUrl = string.IsNullOrEmpty(e.ImagePath)
            ? baseUrl + "/uploads/default.jpg"
            : baseUrl + e.ImagePath
    }).ToList();

    return basicList;
}


 Add to Interface IEmployee.cs

Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize);

3. Add Endpoint in EmployeeController.cs

[HttpGet("basic")]
public async Task<ActionResult<List<EmployeeBasicDto>>> GetBasicEmployeeList(int page = 1, int pageSize = 5)
{
    var result = await _employeeService.GetAllEmployeeBasicInfoAsync(page, pageSize);
    return Ok(result);
}

✅ Example Output (JSON):
For /api/employee/basic?page=1&pageSize=3

[
  {
    "firstName": "Ravi",
    "lastName": "Kumar",
    "email": "ravi.kumar@example.com",
    "imageUrl": "https://localhost:5001/uploads/ravi.jpg"
  },
  {
    "firstName": "Meena",
    "lastName": "Sharma",
    "email": "meena@example.com",
    "imageUrl": "https://localhost:5001/uploads/default.jpg"
  }
]


Filter this data (e.g., by name/email)

Export as CSV or Excel

✅ 1. Filtering by Name or Email
✅ 2. Exporting to CSV or Excel file
✅ 1. Filtering Logic in EmployeeService.cs
📌 Update GetAllEmployeeBasicInfoAsync to accept a filter:
🔄 Modify Interface IEmployee.cs

Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize, string? searchTerm);

Update Method in EmployeeService.cs

public async Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize, string? searchTerm)
{
    var query = _context.employees.AsQueryable();

    if (!string.IsNullOrEmpty(searchTerm))
    {
        query = query.Where(e => e.FirstName!.Contains(searchTerm) || 
                                 e.LastName!.Contains(searchTerm) || 
                                 e.Email!.Contains(searchTerm));
    }

    var employees = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    string baseUrl = GetBaseUrl();

    var result = employees.Select(e => new EmployeeBasicDto
    {
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        ImageUrl = string.IsNullOrEmpty(e.ImagePath)
                    ? baseUrl + "/uploads/default.jpg"
                    : baseUrl + e.ImagePath
    }).ToList();

    return result;
}
✅ 2. Add Filtering Endpoint to EmployeeController.cs

[HttpGet("basic")]
public async Task<ActionResult<List<EmployeeBasicDto>>> GetBasicEmployeeList(
    int page = 1, int pageSize = 5, string? search = null)
{
    var result = await _employeeService.GetAllEmployeeBasicInfoAsync(page, pageSize, search);
    return Ok(result);
}
✅ 3. Export to CSV or Excel
✔️ Add NuGet Package:
For Excel, install:


Install-Package ClosedXML
Or via CLI:


dotnet add package ClosedXML
🔧 Add Export Method to Controller (EmployeeController.cs)
✅ Export to CSV

[HttpGet("export/csv")]
public async Task<IActionResult> ExportToCsv(string? search = null)
{
    var employees = await _employeeService.GetAllEmployeeBasicInfoAsync(1, int.MaxValue, search);

    var csv = new StringBuilder();
    csv.AppendLine("FirstName,LastName,Email,ImageUrl");

    foreach (var emp in employees)
    {
        csv.AppendLine($"\"{emp.FirstName}\",\"{emp.LastName}\",\"{emp.Email}\",\"{emp.ImageUrl}\"");
    }

    byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
    return File(buffer, "text/csv", "Employees.csv");
}
✅ Export to Excel

using ClosedXML.Excel;

[HttpGet("export/excel")]
public async Task<IActionResult> ExportToExcel(string? search = null)
{
    var employees = await _employeeService.GetAllEmployeeBasicInfoAsync(1, int.MaxValue, search);

    using var workbook = new XLWorkbook();
    var worksheet = workbook.Worksheets.Add("Employees");

    worksheet.Cell(1, 1).Value = "First Name";
    worksheet.Cell(1, 2).Value = "Last Name";
    worksheet.Cell(1, 3).Value = "Email";
    worksheet.Cell(1, 4).Value = "Image URL";

    int row = 2;
    foreach (var emp in employees)
    {
        worksheet.Cell(row, 1).Value = emp.FirstName;
        worksheet.Cell(row, 2).Value = emp.LastName;
        worksheet.Cell(row, 3).Value = emp.Email;
        worksheet.Cell(row, 4).Value = emp.ImageUrl;
        row++;
    }

    using var stream = new MemoryStream();
    workbook.SaveAs(stream);
    stream.Seek(0, SeekOrigin.Begin);

    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
}
✅ Final API Endpoints Summary:
HTTP Method	Route	Description
GET	/api/employee/basic	List paginated + search
GET	/api/employee/export/csv	Download filtered list as CSV
GET	/api/employee/export/excel	Download filtered list as Excel

Let me know if you want:


public async Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize, string? searchTerm)
{
    var query = _context.employees.AsQueryable();

    if (!string.IsNullOrEmpty(searchTerm))
    {
        query = query.Where(e => e.FirstName!.Contains(searchTerm) || 
                                 e.LastName!.Contains(searchTerm) || 
                                 e.Email!.Contains(searchTerm));
    }

    var employees = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    string baseUrl = GetBaseUrl();

    var result = employees.Select(e => new EmployeeBasicDto
    {
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        ImageUrl = string.IsNullOrEmpty(e.ImagePath)
                    ? baseUrl + "/uploads/default.jpg"
                    : baseUrl + e.ImagePath
    }).ToList();

    return result;
}








