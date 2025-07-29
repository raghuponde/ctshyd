
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



