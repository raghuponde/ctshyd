-- 1. Policy Table
CREATE TABLE Policy (
    policyId INT PRIMARY KEY AUTO_INCREMENT,
    policyNumber VARCHAR(50),
    policyholderId INT,
    coverageAmount DECIMAL(10, 2),
    policyStatus ENUM('ACTIVE', 'INACTIVE', 'CANCELLED'),
    createdDate DATE,
    FOREIGN KEY (policyholderId) REFERENCES User(userId)
);

-- 2. Claim Table
CREATE TABLE Claim (
    claimId INT PRIMARY KEY AUTO_INCREMENT,
    policyId INT,
    claimAmount DECIMAL(10, 2),
    claimDate DATE,
    claimStatus ENUM('PENDING', 'APPROVED', 'REJECTED'),
    adjusterId INT,
    FOREIGN KEY (policyId) REFERENCES Policy(policyId),
    FOREIGN KEY (adjusterId) REFERENCES User(userId)
);

-- 3. SupportTicket Table
CREATE TABLE SupportTicket (
    ticketId INT PRIMARY KEY AUTO_INCREMENT,
    userId INT,
    issueDescription TEXT,
    ticketStatus ENUM('OPEN', 'RESOLVED'),
    createdDate DATE,
    FOREIGN KEY (userId) REFERENCES User(userId)
);

-- 4. Document Table
CREATE TABLE Document (
    documentId INT PRIMARY KEY AUTO_INCREMENT,
    claimId INT,
    documentName VARCHAR(100),
    documentPath VARCHAR(255),
    documentType ENUM('PDF', 'JPG', 'PNG', 'DOC'),
    FOREIGN KEY (claimId) REFERENCES Claim(claimId)
);

-- 5. User Table
CREATE TABLE User (
    userId INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) UNIQUE,
    password VARCHAR(255),
    role ENUM('ADMIN', 'AGENT', 'CLAIM_ADJUSTER', 'POLICYHOLDER'),
    email VARCHAR(100)
);
for this description i want classes with annotations  so that i can use it in code first entity framework to generate tables 

✅ 1. User Class

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required, MaxLength(255)]
    public string Password { get; set; }

    [Required]
    public Role Role { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    // Navigation properties
    public ICollection<Policy> Policies { get; set; }
    public ICollection<Claim> AdjustedClaims { get; set; }
    public ICollection<SupportTicket> SupportTickets { get; set; }
}

public enum Role
{
    ADMIN,
    AGENT,
    CLAIM_ADJUSTER,
    POLICYHOLDER
}
✅ 2. Policy Class

public class Policy
{
    [Key]
    public int PolicyId { get; set; }

    [Required, MaxLength(50)]
    public string PolicyNumber { get; set; }

    [ForeignKey("User")]
    public int PolicyholderId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal CoverageAmount { get; set; }

    [Required]
    public PolicyStatus PolicyStatus { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation
    public User User { get; set; }
    public ICollection<Claim> Claims { get; set; }
}

public enum PolicyStatus
{
    ACTIVE,
    INACTIVE,
    CANCELLED
}
✅ 3. Claim Class

public class Claim
{
    [Key]
    public int ClaimId { get; set; }

    [ForeignKey("Policy")]
    public int PolicyId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ClaimAmount { get; set; }

    public DateTime ClaimDate { get; set; }

    [Required]
    public ClaimStatus ClaimStatus { get; set; }

    [ForeignKey("Adjuster")]
    public int AdjusterId { get; set; }

    // Navigation
    public Policy Policy { get; set; }
    public User Adjuster { get; set; }
    public ICollection<Document> Documents { get; set; }
}

public enum ClaimStatus
{
    PENDING,
    APPROVED,
    REJECTED
}
✅ 4. SupportTicket Class

public class SupportTicket
{
    [Key]
    public int TicketId { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public string IssueDescription { get; set; }

    [Required]
    public TicketStatus TicketStatus { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation
    public User User { get; set; }
}

public enum TicketStatus
{
    OPEN,
    RESOLVED
}
✅ 5. Document Class

public class Document
{
    [Key]
    public int DocumentId { get; set; }

    [ForeignKey("Claim")]
    public int ClaimId { get; set; }

    [Required, MaxLength(100)]
    public string DocumentName { get; set; }

    [Required, MaxLength(255)]
    public string DocumentPath { get; set; }

    [Required]
    public DocumentType DocumentType { get; set; }

    // Navigation
    public Claim Claim { get; set; }
}

public enum DocumentType
{
    PDF,
    JPG,
    PNG,
    DOC
}

I want context class for this say PolicyContext and in that include fluent api for enum conversion 


PolicyContext.cs

using Microsoft.EntityFrameworkCore;

public class PolicyContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<SupportTicket> SupportTickets { get; set; }
    public DbSet<Document> Documents { get; set; }

    public PolicyContext(DbContextOptions<PolicyContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Enum to string conversions

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Policy>()
            .Property(p => p.PolicyStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Claim>()
            .Property(c => c.ClaimStatus)
            .HasConversion<string>();

        modelBuilder.Entity<SupportTicket>()
            .Property(t => t.TicketStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Document>()
            .Property(d => d.DocumentType)
            .HasConversion<string>();

        // Optional: configure relationships and constraints if needed
        base.OnModelCreating(modelBuilder);
    }
}

now for the above things how the tables will be generated in database using code first apprach i need the complete structure of tables now 





1. User Table

CREATE TABLE [Users] (
    [UserId] INT PRIMARY KEY IDENTITY,
    [Username] NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(MAX) NOT NULL, -- Stored as string: 'ADMIN', 'AGENT', etc.
    [Email] NVARCHAR(100) NULL
);
2. Policy Table

CREATE TABLE [Policies] (
    [PolicyId] INT PRIMARY KEY IDENTITY,
    [PolicyNumber] NVARCHAR(50) NOT NULL,
    [PolicyholderId] INT NOT NULL,
    [CoverageAmount] DECIMAL(10,2) NOT NULL,
    [PolicyStatus] NVARCHAR(MAX) NOT NULL, -- Enum: 'ACTIVE', etc.
    [CreatedDate] DATE NOT NULL,
    CONSTRAINT [FK_Policies_Users] FOREIGN KEY ([PolicyholderId]) REFERENCES [Users]([UserId])
);
3. Claim Table

CREATE TABLE [Claims] (
    [ClaimId] INT PRIMARY KEY IDENTITY,
    [PolicyId] INT NOT NULL,
    [ClaimAmount] DECIMAL(10,2) NOT NULL,
    [ClaimDate] DATE NOT NULL,
    [ClaimStatus] NVARCHAR(MAX) NOT NULL, -- Enum: 'PENDING', etc.
    [AdjusterId] INT NOT NULL,
    CONSTRAINT [FK_Claims_Policies] FOREIGN KEY ([PolicyId]) REFERENCES [Policies]([PolicyId]),
    CONSTRAINT [FK_Claims_Users] FOREIGN KEY ([AdjusterId]) REFERENCES [Users]([UserId])
);
4. SupportTicket Table

CREATE TABLE [SupportTickets] (
    [TicketId] INT PRIMARY KEY IDENTITY,
    [UserId] INT NOT NULL,
    [IssueDescription] NVARCHAR(MAX) NULL,
    [TicketStatus] NVARCHAR(MAX) NOT NULL, -- Enum: 'OPEN', 'RESOLVED'
    [CreatedDate] DATE NOT NULL,
    CONSTRAINT [FK_SupportTickets_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
);
5. Document Table

CREATE TABLE [Documents] (
    [DocumentId] INT PRIMARY KEY IDENTITY,
    [ClaimId] INT NOT NULL,
    [DocumentName] NVARCHAR(100) NOT NULL,
    [DocumentPath] NVARCHAR(255) NOT NULL,
    [DocumentType] NVARCHAR(MAX) NOT NULL, -- Enum: 'PDF', 'JPG', etc.
    CONSTRAINT [FK_Documents_Claims] FOREIGN KEY ([ClaimId]) REFERENCES [Claims]([ClaimId])
);


3.1 Policy Management Module

Purpose: Manages the creation, update, and tracking of health insurance policies.

• Controller:
    • PolicyController
        - createPolicy(policyData)
        - updatePolicy(policyId, policyData)
        - getPolicyDetails(policyId)
        - getAllPolicies()
        - deletePolicy(policyId)

Service:
    PolicyService

Handles the business logic for creating, updating, and retrieving policies.

Model:

Entity: Policy

Attributes:
    policyId (PK)
    policyNumber (VARCHAR)
    policyholderId (FK)
    coverageAmount (DECIMAL)
    policyStatus (ENUM : ACTIVE, INACTIVE, CANCELLED)
    createdDate (DATE)


for this i want web api controller code and also need service also which will be used in controller of web api 
I want it in the form of interface where All methods will go and service class should implement that interface 
and in PolicyController i should use it 



✅ Step 1: IPolicyService (Interface)

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPolicyService
{
    Task<Policy> CreatePolicyAsync(Policy policy);
    Task<Policy> UpdatePolicyAsync(int policyId, Policy policy);
    Task<Policy> GetPolicyDetailsAsync(int policyId);
    Task<IEnumerable<Policy>> GetAllPoliciesAsync();
    Task<bool> DeletePolicyAsync(int policyId);
}
✅ Step 2: PolicyService (Service Class)

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class PolicyService : IPolicyService
{
    private readonly PolicyContext _context;

    public PolicyService(PolicyContext context)
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

        existing.PolicyNumber = updatedPolicy.PolicyNumber;
        existing.PolicyholderId = updatedPolicy.PolicyholderId;
        existing.CoverageAmount = updatedPolicy.CoverageAmount;
        existing.PolicyStatus = updatedPolicy.PolicyStatus;
        existing.CreatedDate = updatedPolicy.CreatedDate;

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
✅ Step 3: PolicyController (Web API Controller)

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        var created = await _policyService.CreatePolicyAsync(policy);
        return CreatedAtAction(nameof(GetPolicyDetails), new { policyId = created.PolicyId }, created);
    }

    [HttpPut("{policyId}")]
    public async Task<IActionResult> UpdatePolicy(int policyId, [FromBody] Policy policy)
    {
        var updated = await _policyService.UpdatePolicyAsync(policyId, policy);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpGet("{policyId}")]
    public async Task<IActionResult> GetPolicyDetails(int policyId)
    {
        var policy = await _policyService.GetPolicyDetailsAsync(policyId);
        if (policy == null) return NotFound();
        return Ok(policy);
    }

    [HttpGet]
    public async Task<IEnumerable<Policy>> GetAllPolicies()
    {
        return await _policyService.GetAllPoliciesAsync();
    }

    [HttpDelete("{policyId}")]
    public async Task<IActionResult> DeletePolicy(int policyId)
    {
        var deleted = await _policyService.DeletePolicyAsync(policyId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}

