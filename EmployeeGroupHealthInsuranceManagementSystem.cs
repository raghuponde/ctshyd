1. Employee Table
CREATE TABLE Employee ( employeeId INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(100), email VARCHAR(100), phone VARCHAR(15), address
TEXT, designation VARCHAR(50), organizationId INT );
2 Policy Table
CREATE TABLE Policy (
policyId INT AUTO_INCREMENT PRIMARY KEY,
policyName VARCHAR(100),
coverageAmount DECIMAL(10, 2),
premiumAmount DECIMAL(10, 2),
policyType ENUM('INDIVIDUAL', 'FAMILY')
);
-- 3. Enrollment Table
CREATE TABLE Enrollment (
enrollmentId INT AUTO_INCREMENT PRIMARY KEY,
employeeId INT,
policyId INT,
enrollmentDate DATE,
status ENUM('ACTIVE', 'CANCELLED'),
FOREIGN KEY (employeeId) REFERENCES Employee(employeeId),
FOREIGN KEY (policyId) REFERENCES Policy(policyId)
);
-- 4. Claim Table
CREATE TABLE Claim (
claimId INT AUTO_INCREMENT PRIMARY KEY,
enrollmentId INT,
claimAmount DECIMAL(10, 2),
claimReason TEXT,
claimDate DATE,
claimStatus ENUM('SUBMITTED', 'APPROVED', 'REJECTED'),
FOREIGN KEY (enrollmentId) REFERENCES Enrollment(enrollmentId)
);
-- 5. Organization Table
CREATE TABLE Organization (
organizationId INT AUTO_INCREMENT PRIMARY KEY,
organizationName VARCHAR(100),
contactPerson VARCHAR(100),
contactEmail VARCHAR(100)
);

for this description i want classes with annotations  so that i can use it in code first entity framework to generate tables 

1. Organization.cs

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Organization
{
    [Key]
    public int OrganizationId { get; set; }

    [Required]
    [StringLength(100)]
    public string OrganizationName { get; set; }

    [Required]
    [StringLength(100)]
    public string ContactPerson { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string ContactEmail { get; set; }

    public ICollection<Employee> Employees { get; set; }
}
2. Employee.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(15)]
    public string Phone { get; set; }

    public string Address { get; set; }

    [StringLength(50)]
    public string Designation { get; set; }

    [ForeignKey("Organization")]
    public int OrganizationId { get; set; }

    public Organization Organization { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}
3. Policy.cs

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public enum PolicyType
{
    INDIVIDUAL,
    FAMILY
}

public class Policy
{
    [Key]
    public int PolicyId { get; set; }

    [Required]
    [StringLength(100)]
    public string PolicyName { get; set; }

    public decimal CoverageAmount { get; set; }

    public decimal PremiumAmount { get; set; }

    [Required]
    public PolicyType PolicyType { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}
4. Enrollment.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

public enum EnrollmentStatus
{
    ACTIVE,
    CANCELLED
}

public class Enrollment
{
    [Key]
    public int EnrollmentId { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }

    [ForeignKey("Policy")]
    public int PolicyId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    [Required]
    public EnrollmentStatus Status { get; set; }

    public Employee Employee { get; set; }
    public Policy Policy { get; set; }
    public ICollection<Claim> Claims { get; set; }
}
5. Claim.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

public enum ClaimStatus
{
    SUBMITTED,
    APPROVED,
    REJECTED
}

public class Claim
{
    [Key]
    public int ClaimId { get; set; }

    [ForeignKey("Enrollment")]
    public int EnrollmentId { get; set; }

    public decimal ClaimAmount { get; set; }

    public string ClaimReason { get; set; }

    public DateTime ClaimDate { get; set; }

    [Required]
    public ClaimStatus ClaimStatus { get; set; }

    public Enrollment Enrollment { get; set; }
}


I want context class for this say InsuranceContext and in that include fluent api for enum conversion 


✅ InsuranceContext.cs

using Microsoft.EntityFrameworkCore;

public class InsuranceContext : DbContext
{
    public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Claim> Claims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Enum to string conversion for PolicyType
        modelBuilder.Entity<Policy>()
            .Property(p => p.PolicyType)
            .HasConversion<string>();

        // Enum to string conversion for EnrollmentStatus
        modelBuilder.Entity<Enrollment>()
            .Property(e => e.Status)
            .HasConversion<string>();

        // Enum to string conversion for ClaimStatus
        modelBuilder.Entity<Claim>()
            .Property(c => c.ClaimStatus)
            .HasConversion<string>();

        // Optional: Set relationships and cascade rules explicitly if needed
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Employee)
            .WithMany(emp => emp.Enrollments)
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Policy)
            .WithMany(p => p.Enrollments)
            .HasForeignKey(e => e.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Claim>()
            .HasOne(c => c.Enrollment)
            .WithMany(e => e.Claims)
            .HasForeignKey(c => c.EnrollmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


now for the above things how the tables will be generated in database using code first apprach i need the complete structure of tables now 




📘 1. Organization Table

CREATE TABLE [Organizations] (
    [OrganizationId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [OrganizationName] NVARCHAR(100) NOT NULL,
    [ContactPerson] NVARCHAR(100) NOT NULL,
    [ContactEmail] NVARCHAR(100) NOT NULL
);
📘 2. Employee Table

CREATE TABLE [Employees] (
    [EmployeeId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100),
    [Phone] NVARCHAR(15),
    [Address] NVARCHAR(MAX),
    [Designation] NVARCHAR(50),
    [OrganizationId] INT NOT NULL,
    CONSTRAINT [FK_Employees_Organizations] FOREIGN KEY ([OrganizationId]) 
        REFERENCES [Organizations]([OrganizationId]) ON DELETE CASCADE
);
📘 3. Policy Table

CREATE TABLE [Policies] (
    [PolicyId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PolicyName] NVARCHAR(100) NOT NULL,
    [CoverageAmount] DECIMAL(18, 2) NOT NULL,
    [PremiumAmount] DECIMAL(18, 2) NOT NULL,
    [PolicyType] NVARCHAR(MAX) NOT NULL
    -- values will be 'INDIVIDUAL' or 'FAMILY'
);
📘 4. Enrollment Table

CREATE TABLE [Enrollments] (
    [EnrollmentId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [EmployeeId] INT NOT NULL,
    [PolicyId] INT NOT NULL,
    [EnrollmentDate] DATETIME2 NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL
    -- values will be 'ACTIVE' or 'CANCELLED',
    CONSTRAINT [FK_Enrollments_Employees] FOREIGN KEY ([EmployeeId]) 
        REFERENCES [Employees]([EmployeeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_Policies] FOREIGN KEY ([PolicyId]) 
        REFERENCES [Policies]([PolicyId]) ON DELETE CASCADE
);
📘 5. Claim Table

CREATE TABLE [Claims] (
    [ClaimId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [EnrollmentId] INT NOT NULL,
    [ClaimAmount] DECIMAL(18, 2) NOT NULL,
    [ClaimReason] NVARCHAR(MAX),
    [ClaimDate] DATETIME2 NOT NULL,
    [ClaimStatus] NVARCHAR(MAX) NOT NULL
    -- values will be 'SUBMITTED', 'APPROVED', 'REJECTED',
    CONSTRAINT [FK_Claims_Enrollments] FOREIGN KEY ([EnrollmentId]) 
        REFERENCES [Enrollments]([EnrollmentId]) ON DELETE CASCADE
);



3.1 Employee Management Module

Purpose: Handles employee registration and profile management.

• Controller:
    • EmployeeController
        • registerEmployee()
        • getEmployeeDetails()
        • updateEmployeeProfile()
        • listAllEmployees()

• Service:
    • EmployeeService
        • Business logic for employee profile validation and updates.

• Model:
    • Employee Entity
        • Attributes:
            • employeeId (PK)
            • name
            • email
            • phone
            • address
            • designation
            • organizationId (FK)


for this i want web api controller code and also need service also which will be used in controller of web api 
I want it in the form of interface where All methods will go and service class should implement that interface 
and in EmployeeController i should use it 


✅ Step-by-step Breakdown
📁 Folder Structure

Controllers/
    EmployeeController.cs
Models/
    Employee.cs
Services/
    IEmployeeService.cs
    EmployeeService.cs
Data/
    InsuranceContext.cs
✅ 1. Model: Employee.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(15)]
    public string Phone { get; set; }

    public string Address { get; set; }

    [StringLength(50)]
    public string Designation { get; set; }

    [ForeignKey("Organization")]
    public int OrganizationId { get; set; }

    public Organization Organization { get; set; }
}
✅ 2. DbContext: InsuranceContext.cs

using Microsoft.EntityFrameworkCore;

public class InsuranceContext : DbContext
{
    public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // additional configurations...
    }
}
✅ 3. Interface: IEmployeeService.cs

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeeService
{
    Task<Employee> RegisterEmployeeAsync(Employee employee);
    Task<Employee> GetEmployeeDetailsAsync(int id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> UpdateEmployeeProfileAsync(int id, Employee employee);
}
✅ 4. Service Implementation: EmployeeService.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly InsuranceContext _context;

    public EmployeeService(InsuranceContext context)
    {
        _context = context;
    }

    public async Task<Employee> RegisterEmployeeAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> GetEmployeeDetailsAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> UpdateEmployeeProfileAsync(int id, Employee employee)
    {
        var existing = await _context.Employees.FindAsync(id);
        if (existing == null) return null;

        existing.Name = employee.Name;
        existing.Email = employee.Email;
        existing.Phone = employee.Phone;
        existing.Address = employee.Address;
        existing.Designation = employee.Designation;
        existing.OrganizationId = employee.OrganizationId;

        await _context.SaveChangesAsync();
        return existing;
    }
}
✅ 5. Controller: EmployeeController.cs

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterEmployee([FromBody] Employee employee)
    {
        var created = await _employeeService.RegisterEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeDetails), new { id = created.EmployeeId }, created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeDetails(int id)
    {
        var employee = await _employeeService.GetEmployeeDetailsAsync(id);
        if (employee == null)
            return NotFound();
        return Ok(employee);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> ListAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeProfile(int id, [FromBody] Employee employee)
    {
        var updated = await _employeeService.UpdateEmployeeProfileAsync(id, employee);
        if (updated == null)
            return NotFound();
        return Ok(updated);
    }
}



