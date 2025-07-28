1. Policy Table

CREATE TABLE Policy (
    policyId INT PRIMARY KEY AUTO_INCREMENT,
    policyType VARCHAR(100),
    coverageAmount DECIMAL(10, 2),
    premiumAmount DECIMAL(10, 2),
    validityStartDate DATE,
    validityEndDate DATE
);

2. Claim Table

CREATE TABLE Claim (
    claimId INT PRIMARY KEY AUTO_INCREMENT,
    policyId INT,
    claimAmount DECIMAL(10, 2),
    claimStatus ENUM('PENDING', 'APPROVED', 'REJECTED'),
    submissionDate DATE,
    settlementDate DATE,
    FOREIGN KEY (policyId) REFERENCES Policy(policyId)
);

3. Customer Table

CREATE TABLE Customer (
    customerId INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    email VARCHAR(100),
    phone VARCHAR(15),
    address TEXT
);

4. PremiumCalculation Table

CREATE TABLE PremiumCalculation (
    calculationId INT PRIMARY KEY AUTO_INCREMENT,
    policyId INT,
    customerId INT,
    basePremium DECIMAL(10, 2),
    adjustedPremium DECIMAL(10, 2),
    FOREIGN KEY (policyId) REFERENCES Policy(policyId),
    FOREIGN KEY (customerId) REFERENCES Customer(customerId)
);

5. User Table

CREATE TABLE User (
    userId INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) UNIQUE,
    password VARCHAR(255),
    role ENUM('ADMIN', 'USER')
);

for this description i want classes with annotations  so that i can use it in code first entity framework to generate tables 


✅ 1. Policy Class

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Policy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PolicyId { get; set; }

    [Required]
    [MaxLength(100)]
    public string PolicyType { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal CoverageAmount { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PremiumAmount { get; set; }

    public DateTime ValidityStartDate { get; set; }
    public DateTime ValidityEndDate { get; set; }
}
✅ 2. Claim Class

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Claim
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClaimId { get; set; }

    [ForeignKey("Policy")]
    public int PolicyId { get; set; }

    public Policy Policy { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ClaimAmount { get; set; }

    [Required]
    [EnumDataType(typeof(ClaimStatus))]
    public ClaimStatus ClaimStatus { get; set; }

    public DateTime SubmissionDate { get; set; }
    public DateTime SettlementDate { get; set; }
}

public enum ClaimStatus
{
    PENDING,
    APPROVED,
    REJECTED
}
✅ 3. Customer Class

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    public string Address { get; set; }
}
✅ 4. PremiumCalculation Class

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PremiumCalculation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CalculationId { get; set; }

    [ForeignKey("Policy")]
    public int PolicyId { get; set; }

    public Policy Policy { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal BasePremium { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal AdjustedPremium { get; set; }
}
✅ 5. User Class

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    [Required]
    [EnumDataType(typeof(UserRole))]
    public UserRole Role { get; set; }
}

public enum UserRole
{
    ADMIN,
    USER
}

I want context class for this say InsurancePolicyContext and in that include fluent api for enum conversion 

InsurancePolicyContext.cs

using Microsoft.EntityFrameworkCore;

public class InsurancePolicyContext : DbContext
{
    public InsurancePolicyContext(DbContextOptions<InsurancePolicyContext> options)
        : base(options)
    {
    }

    public DbSet<Policy> Policies { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PremiumCalculation> PremiumCalculations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API for ClaimStatus enum (store as string)
        modelBuilder.Entity<Claim>()
            .Property(c => c.ClaimStatus)
            .HasConversion<string>();

        // Fluent API for UserRole enum (store as string)
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        // Optional: Define column types explicitly if needed
        modelBuilder.Entity<Policy>()
            .Property(p => p.CoverageAmount)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Policy>()
            .Property(p => p.PremiumAmount)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Claim>()
            .Property(c => c.ClaimAmount)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<PremiumCalculation>()
            .Property(p => p.BasePremium)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<PremiumCalculation>()
            .Property(p => p.AdjustedPremium)
            .HasColumnType("decimal(10,2)");
    }
}

now for the above things how the tables will be generated in database using code first apprach i need the complete structure of tables now 

✅ 1. Policies Table

CREATE TABLE Policies (
    PolicyId INT PRIMARY KEY IDENTITY(1,1),
    PolicyType NVARCHAR(100) NOT NULL,
    CoverageAmount DECIMAL(10,2) NOT NULL,
    PremiumAmount DECIMAL(10,2) NOT NULL,
    ValidityStartDate DATE NOT NULL,
    ValidityEndDate DATE NOT NULL
);
✅ 2. Claims Table

CREATE TABLE Claims (
    ClaimId INT PRIMARY KEY IDENTITY(1,1),
    PolicyId INT NOT NULL,
    ClaimAmount DECIMAL(10,2) NOT NULL,
    ClaimStatus NVARCHAR(MAX) NOT NULL, -- stored as string because of HasConversion<string>()
    SubmissionDate DATE NOT NULL,
    SettlementDate DATE NOT NULL,
    FOREIGN KEY (PolicyId) REFERENCES Policies(PolicyId)
);
✅ 3. Customers Table

CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15),
    Address NVARCHAR(MAX)
);
✅ 4. PremiumCalculations Table

CREATE TABLE PremiumCalculations (
    CalculationId INT PRIMARY KEY IDENTITY(1,1),
    PolicyId INT NOT NULL,
    CustomerId INT NOT NULL,
    BasePremium DECIMAL(10,2) NOT NULL,
    AdjustedPremium DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (PolicyId) REFERENCES Policies(PolicyId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
✅ 5. Users Table

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(MAX) NOT NULL -- stored as string because of HasConversion<string>()
);

3.1 Policy Management Module

Purpose: Handles operations for policy lifecycle management.

• Controller:
    • PolicyController
        - createPolicy(policyData)
        - updatePolicy(policyId, policyData)
        - getPolicyDetails(policyId)
        - deletePolicy(policyId)

• Service:
    • PolicyService
        - Validate policy data.
        - Enforce business rules.

• Model:
    • Entity: Policy
        • Attributes:
            - policyId (PK)
            - policyType (VARCHAR)
            - coverageAmount (DECIMAL)
            - premiumAmount (DECIMAL)
            - validityStartDate (DATE)
            - validityEndDate (DATE)

for this i want web api controller code and also need service also which will be used in controller of web api 
I want it in the form of interface where All methods will go and service class should implement that interface 
and in PolicyController i should use it 

✅ 1. IPolicyService.cs (Interface)

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPolicyService
{
    Task<Policy> CreatePolicyAsync(Policy policy);
    Task<Policy> UpdatePolicyAsync(int policyId, Policy updatedPolicy);
    Task<Policy> GetPolicyDetailsAsync(int policyId);
    Task<bool> DeletePolicyAsync(int policyId);
    Task<IEnumerable<Policy>> GetAllPoliciesAsync();
}
✅ 2. PolicyService.cs (Service Implementation)

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PolicyService : IPolicyService
{
    private readonly InsurancePolicyContext _context;

    public PolicyService(InsurancePolicyContext context)
    {
        _context = context;
    }

    public async Task<Policy> CreatePolicyAsync(Policy policy)
    {
        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();
        return policy;
    }

    public async Task<Policy> UpdatePolicyAsync(int policyId, Policy updatedPolicy)
    {
        var existing = await _context.Policies.FindAsync(policyId);
        if (existing == null) return null;

        existing.PolicyType = updatedPolicy.PolicyType;
        existing.CoverageAmount = updatedPolicy.CoverageAmount;
        existing.PremiumAmount = updatedPolicy.PremiumAmount;
        existing.ValidityStartDate = updatedPolicy.ValidityStartDate;
        existing.ValidityEndDate = updatedPolicy.ValidityEndDate;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Policy> GetPolicyDetailsAsync(int policyId)
    {
        return await _context.Policies.FindAsync(policyId);
    }

    public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
    {
        return await _context.Policies.ToListAsync();
    }

    public async Task<bool> DeletePolicyAsync(int policyId)
    {
        var policy = await _context.Policies.FindAsync(policyId);
        if (policy == null) return false;

        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync();
        return true;
    }
}
✅ 3. PolicyController.cs (Web API Controller)

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PolicyController : ControllerBase
{
    private readonly IPolicyService _policyService;

    public PolicyController(IPolicyService policyService)
    {
        _policyService = policyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePolicy([FromBody] Policy policy)
    {
        var result = await _policyService.CreatePolicyAsync(policy);
        return CreatedAtAction(nameof(GetPolicyDetails), new { policyId = result.PolicyId }, result);
    }

    [HttpPut("{policyId}")]
    public async Task<IActionResult> UpdatePolicy(int policyId, [FromBody] Policy policy)
    {
        var result = await _policyService.UpdatePolicyAsync(policyId, policy);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{policyId}")]
    public async Task<IActionResult> GetPolicyDetails(int policyId)
    {
        var result = await _policyService.GetPolicyDetailsAsync(policyId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{policyId}")]
    public async Task<IActionResult> DeletePolicy(int policyId)
    {
        var success = await _policyService.DeletePolicyAsync(policyId);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPolicies()
    {
        var result = await _policyService.GetAllPoliciesAsync();
        return Ok(result);
    }
}
