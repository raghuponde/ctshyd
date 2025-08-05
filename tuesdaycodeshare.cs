for this model 

Employee.cs
-------------

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

interface IEmployee is defined like this 


using CodeFirstEFDEmo.Models;

namespace CodeFirstEFDEmo
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee, IFormFile image);
        Task<Employee?> UpdateEmployeeAsync(Employee employee, IFormFile? image);
        Task<Employee?> DeleteEmployeeAsync(int id);

        Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize);

        Task<List<EmployeeBasicDto>> GetAllEmployeeBasicInfoAsync(int pageNumber, int pageSize, string? searchTerm);


    }
}


and EmployeeService is defined like this 

using CodeFirstEFDEmo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFDEmo
{
    public class EmployeeService :IEmployee
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
            if (string.IsNullOrEmpty(imagePath) || imagePath.Contains("default.jpg"))
                return;

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

            foreach (var e in employees)
            {
                e.ImagePath = string.IsNullOrEmpty(e.ImagePath)
                    ? baseUrl + "/uploads/default.jpg"
                    : baseUrl + e.ImagePath;
            }

            return employees;
        }

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
    }
}

and web api controller EmployeeController is defined like this 


using ClosedXML.Excel;
using CodeFirstEFDEmo.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<Employee>> Create([FromForm] Employee emp, IFormFile? image)
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

        //[HttpGet("basic")]
        //public async Task<ActionResult<List<EmployeeBasicDto>>> GetBasicEmployeeList(int page = 1, int pageSize = 5)
        //{
        //    var result = await _employeeService.GetAllEmployeeBasicInfoAsync(page, pageSize);
        //    return Ok(result);
        //}

        [HttpGet("basic")]
        public async Task<ActionResult<List<EmployeeBasicDto>>> GetBasicEmployeeList(
    int page = 1, int pageSize = 5, string? search = null)
        {
            var result = await _employeeService.GetAllEmployeeBasicInfoAsync(page, pageSize, search);
            return Ok(result);
        }

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
    }
}


Now  I am expecting a MVC Controller with the name EmployeeUIController which will consume the above web api methods 
and i want to use jquery ajax methods for this so in index view i want to call web api GetBasicEmployeeList and there only 
a header with serach text box and button to filter the data i want 

and with each data i want details link ,Edit link and delete link and when i click the links the details should be shown in the form of card like how i had done for DogMgtApp means in card header i want to see name and in card boy image of 300 pixels and other details u can provide same i want it in update view and delete view as well .

 
For the current project i am using quartz/bootstrap as u can see i had kept that in layout file now 

<link href="https://cdn.jsdelivr.net/npm/bootswatch@5.3.2/dist/quartz/bootstrap.min.css" rel="stylesheet">

Now i want header and footer and left side  i want navigation so and in betweeen i want child page data to be displayed 

Take the below code and paste it in .html file and see how it looks 

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>_MyLayout</title>
</head>
<body>
    <style type="text/css">
        .auto-style1 {
            width: 373px;
        }
    </style>
   
    <table style="width:100%;">
        <tr>
            <td colspan="2" style="background-color: #66FFCC">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" style="background-color: #FF3399">
                <br />
                <br />
               
             
                    <ul>
                        <li><a href="Emp/Index">EMployeeData </a></li>
                        <li><a href="Dept/Index">DepartmentData </a></li>
                    </ul>

            
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
            <td>@RenderBody()</td>
        </tr>
        <tr>
            <td colspan="2" style="background-color:yellow">&nbsp;</td>
        </tr>
    </table>
</body>
</html>


so i want this type of layout for my child pages but it should be of quartz/bootstrap so at the place of links 
i want accordian controls when i click accordian1 i should see index page of Employee with its own header where only
heading should be there Employee data and in header i want search filter for the employee 


1. _Layout.cshtml
-------------------
Replace your existing layout file with this improved Quartz-themed layout:

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
    <header class="bg-primary text-white p-3">
        <h2 class="text-center">Employee Management Dashboard</h2>
    </header>

    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar Navigation -->
            <div class="col-md-3 bg-light p-3">
                <div class="accordion" id="sidebarAccordion">
                    <div class="accordion-item">
                        <h2 class="accordion-header" id="headingEmp">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseEmp" aria-expanded="true">
                             <span style="color:black;font-weight:500">  Employee Management </span>
                            </button>
                        </h2>
                        <div id="collapseEmp" class="accordion-collapse collapse show" data-bs-parent="#sidebarAccordion">
                            <div class="accordion-body">
                                <a href="/EmployeeUI/Index" style="color:black" class="d-block mb-2">Employee Data</a>
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
</body>
</html>

2. MVC Controller: EmployeeUIController.cs
----------------------------------------------

using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    public class EmployeeUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}


3. View: Index.cshtml (for search, listing, and navigation)
-------------------------------------------------------------
@{
    ViewBag.Title = "Employee Data";
}

<h3 class="mb-3">Employee Data</h3>

<div class="mb-3 d-flex">
    <input type="text" id="searchText" class="form-control me-2" placeholder="Search by name/email" />
    <button id="btnSearch" class="btn btn-primary">Search</button>
</div>

<table class="table table-bordered table-striped">
    <thead class="table-dark">
        <tr>
            <th>Image</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody id="employeeTableBody">
    </tbody>
</table>

<script>
    function loadEmployees(search = "") {
        $.ajax({
            url: `/api/Employee/basic?search=${search}`,
            type: "GET",
            success: function (data) {
                var rows = "";
                $.each(data, function (i, emp) {
                    rows += `
                    <tr>
                        <td><img src="${emp.imageUrl}" width="50" height="50" /></td>
                        <td>${emp.firstName}</td>
                        <td>${emp.lastName}</td>
                        <td>${emp.email}</td>
                        <td>
                            <a href="/EmployeeUI/Details?id=${emp.id}" class="btn btn-sm btn-info">Details</a>
                            <a href="/EmployeeUI/Edit?id=${emp.id}" class="btn btn-sm btn-warning">Edit</a>
                            <a href="/EmployeeUI/Delete?id=${emp.id}" class="btn btn-sm btn-danger">Delete</a>
                        </td>
                    </tr>`;
                });
                $("#employeeTableBody").html(rows);
            },
            error: function () {
                alert("Failed to load employee data.");
            }
        });
    }

    $(document).ready(function () {
        loadEmployees();

        $("#btnSearch").click(function () {
            const search = $("#searchText").val();
            loadEmployees(search);
        });
    });
</script>

Now go to EmployeeUIController details view and delete view and edit view added it using razor only dont use any template but use layout 


Details view 
---------------
@{
    ViewBag.Title = "Employee Details";
}

<div class="card mx-auto" style="width: 400px;">
    <div class="card-header bg-info text-white" id="empName">Employee Name</div>
    <img id="empImg" class="card-img-top" src="" alt="Employee Image" style="height:300px; object-fit:cover;">
    <div class="card-body">
        <p><strong>Email:</strong> <span id="empEmail"></span></p>
        <p><strong>Age:</strong> <span id="empAge"></span></p>
        <a href="/EmployeeUI/Index" class="btn btn-secondary">Back</a>
    </div>
</div>

<script>
    $(document).ready(function () {
        const params = new URLSearchParams(window.location.search);
        const id = params.get("id");

        $.get(`/api/Employee/${id}`, function (emp) {
            $("#empName").text(emp.firstName + " " + emp.lastName);
            $("#empEmail").text(emp.email);
            $("#empAge").text(emp.age);
            $("#empImg").attr("src", emp.imagePath);
        });
    });
</script>



Edit viww 
------------
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
                    alert("Error: " + xhr.responseText);
                }
            });
        });
    });
</script>


Delete view 
---------------------


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
                    success: function () {
                        alert("Employee deleted successfully.");
                        window.location.href = "/EmployeeUI/Index";
                    },
                    error: function (xhr) {
                        alert("Error: " + xhr.responseText);
                    }
                });
            }
        });
    });
</script>


Updated index view with seachg and pagination 
----------------------------------------------
@{
    ViewBag.Title = "Employee Data";
}

<h3 class="mb-3">Employee Data</h3>

<div class="mb-3 d-flex">
    <input type="text" id="searchText" class="form-control me-2" placeholder="Search by name/email" />
    <button id="btnSearch" class="btn btn-primary">Search</button>
</div>

<table class="table table-bordered table-striped">
    <thead class="table-dark">
        <tr>
            <th>Image</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody id="employeeTableBody"></tbody>
</table>

<!-- Pagination Controls -->
<div class="d-flex justify-content-between">
    <button id="prevPage" class="btn btn-outline-primary">Previous</button>
    <span id="currentPageLabel" class="align-self-center">Page 1</span>
    <button id="nextPage" class="btn btn-outline-primary">Next</button>
</div>

<script>
    let currentPage = 1;
    const pageSize = 5;

    function loadEmployees(search = "") {
        $.ajax({
            url: `/api/Employee/basic?page=${currentPage}&pageSize=${pageSize}&search=${search}`,
            type: "GET",
            success: function (data) {
                let rows = "";
                if (data.length === 0) {
                    rows = "<tr><td colspan='5' class='text-center'>No records found</td></tr>";
                    $("#nextPage").prop("disabled", true);
                } else {
                    $.each(data, function (i, emp) {
                        rows += `
                            <tr>
                                <td><img src="${emp.imageUrl}" width="50" height="50" /></td>
                                <td>${emp.firstName}</td>
                                <td>${emp.lastName}</td>
                                <td>${emp.email}</td>
                                <td>
                                    <a href="/EmployeeUI/Details?id=${emp.id}" class="btn btn-sm btn-info">Details</a>
                                    <a href="/EmployeeUI/Edit?id=${emp.id}" class="btn btn-sm btn-warning">Edit</a>
                                    <a href="/EmployeeUI/Delete?id=${emp.id}" class="btn btn-sm btn-danger">Delete</a>
                                </td>
                            </tr>`;
                    });

                    // Enable/disable Next button depending on result count
                    $("#nextPage").prop("disabled", data.length < pageSize);
                }

                $("#employeeTableBody").html(rows);
                $("#currentPageLabel").text("Page " + currentPage);
            },
            error: function () {
                alert("Failed to load data.");
            }
        });
    }

    function reload() {
        const search = $("#searchText").val();
        loadEmployees(search);
    }

    $(document).ready(function () {
        reload();

        $("#btnSearch").click(function () {
            currentPage = 1;
            reload();
        });

        $("#prevPage").click(function () {
            if (currentPage > 1) {
                currentPage--;
                reload();
            }
        });

        $("#nextPage").click(function () {
            currentPage++;
            reload();
        });
    });
</script>



as details image not showing 


namespace CodeFirstEFDEmo.Models
{
    public class EmployeeBasicDto
    {

        public int Id { get; set; }  add this 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
    }
}

add one link on index.cshtml

------------------------------
<a href="/EmployeeUI/Create" class="btn btn-success mb-3">Add new Employee</a>

add Create view in EmplyeeUI contorller 
----------------------------------------
 public IActionResult Create()
 {
     return View();
 }
 
 and generate razor view with default templtare anmd paste below code 


create view 
------------


@{
    ViewBag.Title = "Add New Employee";
}

<div class="card mx-auto" style="width: 450px;">
    <div class="card-header bg-primary text-white">
        Add New Employee
    </div>
    <form id="createForm" enctype="multipart/form-data">
        <div class="card-body">

            <div class="mb-2">
                <label>First Name</label>
                <input type="text" name="FirstName" class="form-control" required />
            </div>

            <div class="mb-2">
                <label>Last Name</label>
                <input type="text" name="LastName" class="form-control" required />
            </div>

            <div class="mb-2">
                <label>Email</label>
                <input type="email" name="Email" class="form-control" required />
            </div>

            <div class="mb-2">
                <label>Age</label>
                <input type="number" name="Age" class="form-control" min="1" max="100" required />
            </div>

            <div class="mb-3">
                <label>Upload Image</label>
                <input type="file" name="image" class="form-control" />
            </div>

            <button type="submit" class="btn btn-success">Create</button>
            <a href="/EmployeeUI/Index" class="btn btn-secondary">Cancel</a>
        </div>
    </form>
</div>

<script>
    $(document).ready(function () {
        $("#createForm").submit(function (e) {
            e.preventDefault();
            var formData = new FormData(this);

            $.ajax({
                url: "/api/Employee",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function () {
                    alert("Employee created successfully!");
                    window.location.href = "/EmployeeUI/Index";
                },
                error: function (xhr) {
                    alert("Error: " + xhr.responseText);
                }
            });
        });
    });
</script>


In the layout 
----------------
add one link to export the data like this in accordian body only 

  <div class="accordion-body">
      <a href="/EmployeeUI/Index" class="d-block mb-2" style="color:black">Employee Data</a>
  </div>
  <div class="accordion-body">
      <a href="/EmployeeUI/Export" class="d-block mb-2" style="color:black">Employee Export</a>
  </div>
  
  
  
add action method export in emplolyee ui 
----------------------------------
 public IActionResult Export ()
 {
     return View();
 }
 
 
 add a view for export and overrite the below code in Export view 
 ----------------------------------------------------------------
 
 @{
    ViewBag.Title = "Export Employee Data";
}

<h3>Export Employee Data</h3>

<div class="mb-3">
    <input type="text" id="searchText" class="form-control" placeholder="Search term (optional)" />
</div>
<button class="btn btn-success" id="btnExport">Export to Excel</button>

<script>
    $(document).ready(function () {
        $("#btnExport").click(function () {
            let search = $("#searchText").val();

            // Create dynamic link and click to download
            let link = document.createElement('a');
            link.href = `/api/Employee/export/excel?search=${encodeURIComponent(search)}`;
            link.download = "Employees.xlsx";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });
    });
</script>
 
 
 
 
 
 
 

