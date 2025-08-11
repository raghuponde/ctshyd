Prompt :
----------
for the above EmployeeController and for the above coding I had added AuthenticationController like this 
 
    
using CodeFirstEFDEmo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            //Check User Exist 
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!" });
            }

            //Add the User in the database
            IdentityUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username

            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "User Failed to Create" });
                }
                //Add role to the user....

                await _userManager.AddToRoleAsync(user, role);





                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User created SuccessFully" });

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "This Role Doesnot Exist." });
            }


        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddYears(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }


                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
                //returning the token...

            }
            return Unauthorized();


        }



    }
}

and the models involved are kept in Models folder like this 

using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFDEmo.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFDEmo.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}

namespace CodeFirstEFDEmo.Models
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }

    }
}

I want to create AuthenticationUIController of MVC and want to create Register and Login action methods and want to generate views for  it and in view i want to use jquery 
 ajax method and also i want to use Model Popup for Login view and Register view in Login View only Register link should be there so i want to consume Authentication web 
 api controller In AuthenticationController of web api add logic for logout also and same add it in AuthenticationUIController also jump me to login if i logout 
My Roles are User,Admin and HR so i need drop down in register view for that
Now for this above prompt i got some code so now let us go with it 

Step 1 :
---------
Add the below method in AuthenticationController of web api which i forgot to include 

[HttpPost]
[Route("logout")]
public IActionResult Logout()
{
    return Ok(new Response { Status = "Success", Message = "Logged out successfully" });
}


Step 2:
---------
Create an AuthenticationUIController of  empty MVC type in Controllers folder and write the below code into it 

using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    public class AuthenticationUIController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session/token
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}


Step 3:
--------
Add views for all action methods razor view only as i need the layout and dont select any template just add view add and add the code which i had given below 
Note that for logout view is not needed dont generate it i will add one link in Layout  view of  when u click that u will jump to Login Page view only so 
Logout view dont generate 

Next I want to make the popups movable in screen 



Login View 
----------
@{
    ViewBag.Title = "Login";
}

<!-- Modal Login -->
<div class="modal show d-block" tabindex="-1">
    <div class="modal-dialog" id="modalDialog">
        <div class="modal-content">
            <form id="loginForm">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Login</h5>
                </div>
                <div class="modal-body">
                    <div class="mb-2">
                        <label>Username</label>
                        <input type="text" name="username" class="form-control" required />
                    </div>
                    <div class="mb-2">
                        <label>Password</label>
                        <input type="password" name="password" class="form-control" required />
                    </div>
                    <p>Don't have an account? <a href="/AuthenticationUI/Register">Register</a></p>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success">Login</button>
                </div>
            </form>
        </div>
    </div>
</div>

<script>
    $("#loginForm").submit(function (e) {
        e.preventDefault();
        const data = {
            username: $("input[name='username']").val(),
            password: $("input[name='password']").val()
        };

        $.ajax({
            url: "/api/Authentication/login",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (res) {
                sessionStorage.setItem("jwt", res.token);
                alert("Login successful");
                window.location.href = "/EmployeeUI/Index";
            },
            error: function () {
                alert("Login failed");
            }
        });
    });
</script>

<script>
    $(function () {
        $("#modalDialog").draggable({
            handle: ".modal-header"  // Only drag using header
        });
    });
</script>
Register View 
--------------
@{
    ViewBag.Title = "Register";
}

<!-- Modal Register -->
<div class="modal show d-block" tabindex="-1">
    <div class="modal-dialog" id="modalDialog">  // added id and down added script to make it scrollable in jquery
        <div class="modal-content">
            <form id="registerForm">
                <div class="modal-header bg-warning text-white">
                    <h5 class="modal-title">Register</h5>
                </div>
                <div class="modal-body">
                    <div class="mb-2">
                        <label>Username</label>
                        <input type="text" name="username" class="form-control" required />
                    </div>
                    <div class="mb-2">
                        <label>Email</label>
                        <input type="email" name="email" class="form-control" required />
                    </div>
                    <div class="mb-2">
                        <label>Password</label>
                        <input type="password" name="password" class="form-control" required />
                    </div>
                    <div class="mb-2">
                        <label>Role</label>
                        <select id="roleSelect" class="form-select" required>
                            <option value="" disabled selected>-- Select Role --</option>
                            <option value="Admin">Admin</option>
                            <option value="HR">HR</option>
                            <option value="User">User</option>
                        </select>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success">Register</button>
                </div>
            </form>
        </div>
    </div>
</div>

<script>
    $("#registerForm").submit(function (e) {
        e.preventDefault();

        const user = {
            username: $("input[name='username']").val(),
            email: $("input[name='email']").val(),
            password: $("input[name='password']").val()
        };

        const role = $("#roleSelect").val();

        if (!role) {
            alert("Please select a role.");
            return;
        }

        $.ajax({
            url: `/api/Authentication?role=${role}`,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(user),
            success: function (res) {
                alert(res.message);
                window.location.href = "/AuthenticationUI/Login";
            },
            error: function (xhr) {
                alert("Registration failed: " + xhr.responseText);
            }
        });
    });
</script>
<script>
    $(function () {
        $("#modalDialog").draggable({
            handle: ".modal-header"  // Only drag using header
        });
    });
</script>


once check in login coding that once  are in session means token created when u login that token  is saved in session means untill you dont click logout button u are there in sesssion 
and expiry date of token i had configured till 2 years i guess once u need to check now in layout i want welcome the user name and logout button so in prompt i had given them my layout 
and asked for welcome user name and logout button functionality 
step 4:
---------
In layout check what code i had written for this 

updated Layout 
-------------
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title - Employee UI</title>
    <link href="https://cdn.jsdelivr.net/npm/bootswatch@5.3.2/dist/quartz/bootstrap.min.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
</head>
<body>
    <!-- Header with Welcome and Logout -->
    <header class="bg-primary text-white p-3 d-flex justify-content-between align-items-center">
        <h2 class="mb-0">Employee Management Dashboard</h2>
        <div>
            @if (Context.Session.GetString("username") != null)
            {
                <span class="me-3">Welcome, <strong>@Context.Session.GetString("username")</strong></span>
                <a href="/AuthenticationUI/Logout" class="btn btn-light btn-sm">Logout</a>
            }
        </div>
    </header>

    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar Navigation -->
            <div class="col-md-3 bg-light p-3">
                <div class="accordion" id="sidebarAccordion">
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingEmp">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseEmp" aria-expanded="true">
                                Employee Management
                            </button>
                        </h2>
                        <div id="collapseEmp" class="accordion-collapse collapse show" data-bs-parent="#sidebarAccordion">
                            <div class="accordion-body">
                                <a href="/EmployeeUI/Index" class="d-block mb-2" style="color:black">Employee Data</a>
                            </div>
                            <div class="accordion-body">
                                <a href="/EmployeeUI/Export" class="d-block mb-2" style="color:black">Employee Export</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Main Content -->
            <div class="col-md-9 mt-3">
                @RenderBody()
            </div>
        </div>
    </div>

    <footer class="bg-secondary text-white text-center p-2 mt-5">
        <p>&copy; 2025 Employee UI App</p>
    </footer>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <!-- jQuery UI for draggable modals -->
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
</body>
</html>

  so u can see there u have to add only code from 411 header with welcome and layout 
  
Store Username in Session after Login
In your Login View's AJAX success callback, store the username via the backend.


Step 5:
------------
Update your AuthenticationUIController to support this:
-------------------------------------------------------

[HttpPost]
public IActionResult SaveSession(string username)
{
    HttpContext.Session.SetString("username", username);
    return Ok();
}


In Login.cshtml (after successful login):
---------------------------------------

$.ajax({
    url: "/api/Authentication/login",
    type: "POST",
    contentType: "application/json",
    data: JSON.stringify(data),
    success: function (res) {
        sessionStorage.setItem("jwt", res.token);
        alert("Login Successful");
        // Save username in server-side session
        $.post("/AuthenticationUI/SaveSession", { username: data.username }, function () {
            window.location.href = "/EmployeeUI/Index";
        });
    },
    error: function () {
        alert("Login failed");
    }
});

Step 6:
--------
Enable Session in Startup.cs or Program.cs
Make sure you have this in your Program.cs 


builder.Services.AddSession();
app.UseSession();


check here down where i had added in layout page for your reference 

using CodeFirstEFDEmo.Models;
using CodeFirstEFDEmo.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CodeFirstEFDEmo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IPost, PostRepository>();
            builder.Services.AddScoped<IEmployee, EmployeeService>();


            builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));

            // For Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<EventContext>()
            .AddDefaultTokenProviders();


            // adding basic authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience =builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer =builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            }); ;


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            

            builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });



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
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AuthenticationUI}/{action=Login}/{id?}");

            app.Run();
        }
    }
}

Now on EmployeeContoller u put [Authorize] filter and see how you can implement Authentication and Authorization which u have done to AdminController earlier okay 

so now i had changed in EmployeeController like this 

using ClosedXML.Excel;
using CodeFirstEFDEmo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Employee>>> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _employeeService.GetAllEmployeesAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HR,User")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            if (emp == null)
                return NotFound("Employee not found");
            return Ok(emp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Employee>> Create([FromForm] Employee emp, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _employeeService.AddEmployeeAsync(emp, image);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if (deleted == null)
                return NotFound("Employee not found");

            return Ok(deleted);
        }

        //[HttpGet("basic")]
        //public async Task<ActionResult<List<EmployeeBasicDto>>> GetBasicEmployeeList(int page = 1, int pageSize = 5)
        //{
        //    var result = await _employeeService.GetAllEmployeeBasicInfoAsync(page, pageSize);
        //    return Ok(result);
        //}

        [HttpGet("basic")]
        [Authorize(Roles = "Admin,HR,User")]
        public async Task<ActionResult<List<EmployeeBasicDto>>> GetBasicEmployeeList(
    int page = 1, int pageSize = 5, string? search = null)
        {
            var result = await _employeeService.GetAllEmployeeBasicInfoAsync(page, pageSize, search);
            return Ok(result);
        }

        [HttpGet("export/excel")]
        [Authorize(Roles = "Admin")]
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
    }
}

as i had kept Authorize on top of the controller every time i have to send token so add this in _Layout last section add it 


updated _Layout code 
----------------------

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewBag.Title - Employee UI</title>
    <link href="https://cdn.jsdelivr.net/npm/bootswatch@5.3.2/dist/quartz/bootstrap.min.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
</head>
<body>
    <!-- Header with Welcome and Logout -->
    <header class="bg-primary text-white p-3 d-flex justify-content-between align-items-center">
        <h2 class="mb-0">Employee Management Dashboard</h2>
        <div>
            @if (Context.Session.GetString("username") != null)
            {
                <span class="me-3">Welcome, <strong>@Context.Session.GetString("username")</strong></span>
                <a href="/AuthenticationUI/Logout" class="btn btn-light btn-sm">Logout</a>
            }
        </div>
    </header>

    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar Navigation -->
            <div class="col-md-3 bg-light p-3">
                <div class="accordion" id="sidebarAccordion">
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingEmp">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseEmp" aria-expanded="true">
                                Employee Management
                            </button>
                        </h2>
                        <div id="collapseEmp" class="accordion-collapse collapse show" data-bs-parent="#sidebarAccordion">
                            <div class="accordion-body">
                                <a href="/EmployeeUI/Index" class="d-block mb-2" style="color:black">Employee Data</a>
                            </div>
                            <div class="accordion-body">
                                <a href="/EmployeeUI/Export" class="d-block mb-2" style="color:black">Employee Export</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Main Content -->
            <div class="col-md-9 mt-3">
                @RenderBody()
            </div>
        </div>
    </div>

    <footer class="bg-secondary text-white text-center p-2 mt-5">
        <p>&copy; 2025 Employee UI App</p>
    </footer>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <!-- jQuery UI for draggable modals -->
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

    <script>
        // attach Bearer token to all jQuery AJAX calls
        $.ajaxSetup({
            beforeSend: function (xhr) {
                const token = sessionStorage.getItem("jwt");
                if (token) xhr.setRequestHeader("Authorization", "Bearer " + token);
            }
        });
    </script>
</body>
</html>

A plain <a href="/api/Employee/export/excel"> cannot add headers, so switch to a JS download using fetch with the token and then create a Blob:
udpate Export.cshtml
--------------------
@{
    ViewBag.Title = "Export Employee Data";
}

<h3>Export Employee Data</h3>

<div class="mb-3">
    <input type="text" id="searchText" class="form-control" placeholder="Search term (optional)" />
</div>
<button id="btnExport" class="btn btn-success">Export to Excel</button>
<script>
    $("#btnExport").click(async function () {
        const token = sessionStorage.getItem("jwt");
        const search = $("#searchText").val() || "";

        const res = await fetch(`/api/Employee/export/excel?search=${encodeURIComponent(search)}`, {
            method: "GET",
            headers: { "Authorization": "Bearer " + token }
        });

        if (!res.ok) { alert("Export failed: " + res.status); return; }

        const blob = await res.blob();
        const url = URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = "Employees.xlsx";
        document.body.appendChild(a);
        a.click();
        a.remove();
        URL.revokeObjectURL(url);
    });
</script>

Note : Also add [AllowAnonymous]  to AuthenticationController okay 


so you can see last i add added script that much only add it in your code now failed to load data error i will not get 
  but when u are not logged in as Admin then if u try to do update it will throw in javascript jquery only 
simply saying some error but as admin u can update and do all things okay 
now what i want is I want a Message that as u are not admin u cannot chnage this 


updated Edit.cshtml  (only ajax method i chnage)
----------------------
@{
    ViewBag.Title = "Edit Employee";
}

<div class="card mx-auto" style="width: 450px;">
    <div class="card-header bg-warning text-white">
        Edit Employee
    </div>
    <form id="editForm" enctype="multipart/form-data">
        <img id="empImg" class="card-img-top" src="" alt="Employee Image" style="height: 300px; object-fit: cover;" />
        <div class="card-body">
            <input type="hidden" id="empId" name="Id" />

            <div class="mb-2">
                <label>First Name</label>
                <input type="text" class="form-control" id="firstName" name="FirstName" required />
            </div>

            <div class="mb-2">
                <label>Last Name</label>
                <input type="text" class="form-control" id="lastName" name="LastName" required />
            </div>

            <div class="mb-2">
                <label>Email</label>
                <input type="email" class="form-control" id="email" name="Email" required />
            </div>

            <div class="mb-2">
                <label>Age</label>
                <input type="number" class="form-control" id="age" name="Age" min="1" max="100" required />
            </div>

            <div class="mb-3">
                <label>Change Image</label>
                <input type="file" class="form-control" name="image" id="imageInput" />
            </div>

            <button type="submit" class="btn btn-success">Update</button>
            <a href="/EmployeeUI/Index" class="btn btn-secondary">Cancel</a>
        </div>
    </form>
</div>

<script>
    $(document).ready(function () {
        const params = new URLSearchParams(window.location.search);
        const id = params.get("id");

        $.get(`/api/Employee/${id}`, function (emp) {
            $("#empId").val(emp.id);
            $("#firstName").val(emp.firstName);
            $("#lastName").val(emp.lastName);
            $("#email").val(emp.email);
            $("#age").val(emp.age);
            $("#empImg").attr("src", emp.imagePath);
        });

        $("#editForm").submit(function (e) {
            e.preventDefault();
            var formData = new FormData(this);
            const id = $("#empId").val();

            $.ajax({
                url: `/api/Employee/${id}`,
                type: "PUT",
                data: formData,
                contentType: false,
                processData: false,
                success: function () {
                    alert("Employee updated successfully!");
                    window.location.href = "/EmployeeUI/Index";
                },
                error: function (xhr) {
                    if (xhr.status === 403) {
                        const msg = (xhr.responseJSON && xhr.responseJSON.message) || "You are not an Admin.";
                        alert(msg);
                    } else if (xhr.status === 401) {
                        alert("Please log in first.");
                        window.location.href = "/AuthenticationUI/Login";
                    } else {
                        alert("Error: " + (xhr.responseText || xhr.statusText));
                    }
                }
            });
        });
    });
</script>


updated Delete.cshtml  (here also just ajax method chnaged) 
--------------------
@{
    ViewBag.Title = "Delete Employee";
}

<div class="card mx-auto" style="width: 450px;">
    <div class="card-header bg-danger text-white">
        Delete Confirmation
    </div>
    <div class="card-body text-center">
        <img id="empImg" src="" alt="Employee Image" class="img-fluid mb-3" style="height: 300px; object-fit: cover;" />
        <h4 id="empName"></h4>
        <p id="empEmail"></p>
        <p id="empAge"></p>

        <button class="btn btn-danger" id="btnDelete">Confirm Delete</button>
        <a href="/EmployeeUI/Index" class="btn btn-secondary">Cancel</a>
    </div>
</div>

<script>
    $(document).ready(function () {
        const params = new URLSearchParams(window.location.search);
        const id = params.get("id");

        $.get(`/api/Employee/${id}`, function (emp) {
            $("#empImg").attr("src", emp.imagePath);
            $("#empName").text(emp.firstName + " " + emp.lastName);
            $("#empEmail").text(emp.email);
            $("#empAge").text("Age: " + emp.age);
        });

        $("#btnDelete").click(function () {
            if (confirm("Are you sure you want to delete this employee?")) {
                $.ajax({
                    url: `/api/Employee/${id}`,
                    type: "DELETE",
                    success: function () { /* ... */ },
                    error: function (xhr) {
                        if (xhr.status === 403) alert("You are not an Admin.");
                        else if (xhr.status === 401) window.location.href = "/AuthenticationUI/Login";
                        else alert("Error: " + (xhr.responseText || xhr.statusText));
                    }
                });
            }
        });
    });
</script>

updated Export.cshtml (just added two status code ) 
---------------------
@{
    ViewBag.Title = "Export Employee Data";
}

<h3>Export Employee Data</h3>

<div class="mb-3">
    <input type="text" id="searchText" class="form-control" placeholder="Search term (optional)" />
</div>
<button id="btnExport" class="btn btn-success">Export to Excel</button>
<script>
    $("#btnExport").click(async function () {
        const token = sessionStorage.getItem("jwt");
        const search = $("#searchText").val() || "";

        const res = await fetch(`/api/Employee/export/excel?search=${encodeURIComponent(search)}`, {
            method: "GET",
            headers: { "Authorization": "Bearer " + token }
        });
        if (res.status === 403) {
            const body = await res.json().catch(() => ({}));
            alert(body.message || "You are not an Admin.");
            return;
        }
        if (res.status === 401) { window.location.href = "/AuthenticationUI/Login"; return; }

        if (!res.ok) { alert("Export failed: " + res.status); return; }

        const blob = await res.blob();
        const url = URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = "Employees.xlsx";
        document.body.appendChild(a);
        a.click();
        a.remove();
        URL.revokeObjectURL(url);
    });
</script>


