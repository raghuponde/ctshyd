1. LoanApplication Table

CREATE TABLE LoanApplication (
    applicationId INT AUTO_INCREMENT PRIMARY KEY,
    companyName VARCHAR(100),
    loanType VARCHAR(50),
    loanAmount DECIMAL(18, 2),
    status ENUM('Pending', 'Approved', 'Rejected'),
    submissionDate DATE
);

2. CreditEvaluation Table

CREATE TABLE CreditEvaluation (
    evaluationId INT AUTO_INCREMENT PRIMARY KEY,
    applicationId INT,
    riskScore DECIMAL(5, 2),
    creditScore INT,
    evaluationDate DATE,
    FOREIGN KEY (applicationId) REFERENCES LoanApplication(applicationId)
);

3. Document Table

CREATE TABLE Document (
    documentId INT AUTO_INCREMENT PRIMARY KEY,
    applicationId INT,
    documentType VARCHAR(50),
    filePath VARCHAR(255),
    uploadDate DATE,
    isValid BOOLEAN,
    FOREIGN KEY (applicationId) REFERENCES LoanApplication(applicationId)
);

4. Approval Table

CREATE TABLE Approval (
    approvalId INT AUTO_INCREMENT PRIMARY KEY,
    applicationId INT,
    approverId INT,
    approvalLevel INT,
    approvalStatus ENUM('Pending', 'Approved', 'Rejected'),
    comments TEXT,
    approvalDate DATE,
    FOREIGN KEY (applicationId) REFERENCES LoanApplication(applicationId)
);

5. Disbursement Table

CREATE TABLE Disbursement (
    disbursementId INT AUTO_INCREMENT PRIMARY KEY,
    applicationId INT,
    disbursedAmount DECIMAL(18, 2),
    disbursementDate DATE,
    repaymentSchedule TEXT,
    FOREIGN KEY (applicationId) REFERENCES LoanApplication(applicationId)
);


for this description i want classes with annotations  so that i can use it in code first entity framework to generate tables 


✅ 1. LoanApplication Class

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class LoanApplication
{
    [Key]
    public int ApplicationId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LoanType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LoanAmount { get; set; }

    [Required]
    [EnumDataType(typeof(Status))]
    public Status Status { get; set; }

    public DateTime SubmissionDate { get; set; }

    public ICollection<CreditEvaluation> CreditEvaluations { get; set; }
    public ICollection<Document> Documents { get; set; }
    public ICollection<Approval> Approvals { get; set; }
    public ICollection<Disbursement> Disbursements { get; set; }
}

public enum Status
{
    Pending,
    Approved,
    Rejected
}
✅ 2. CreditEvaluation Class

public class CreditEvaluation
{
    [Key]
    public int EvaluationId { get; set; }

    [ForeignKey("LoanApplication")]
    public int ApplicationId { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal RiskScore { get; set; }

    public int CreditScore { get; set; }

    public DateTime EvaluationDate { get; set; }

    public LoanApplication LoanApplication { get; set; }
}
✅ 3. Document Class

public class Document
{
    [Key]
    public int DocumentId { get; set; }

    [ForeignKey("LoanApplication")]
    public int ApplicationId { get; set; }

    [MaxLength(50)]
    public string DocumentType { get; set; }

    [MaxLength(255)]
    public string FilePath { get; set; }

    public DateTime UploadDate { get; set; }

    public bool IsValid { get; set; }

    public LoanApplication LoanApplication { get; set; }
}
✅ 4. Approval Class

public class Approval
{
    [Key]
    public int ApprovalId { get; set; }

    [ForeignKey("LoanApplication")]
    public int ApplicationId { get; set; }

    public int ApproverId { get; set; }

    public int ApprovalLevel { get; set; }

    [EnumDataType(typeof(Status))]
    public Status ApprovalStatus { get; set; }

    public string Comments { get; set; }

    public DateTime ApprovalDate { get; set; }

    public LoanApplication LoanApplication { get; set; }
}
✅ 5. Disbursement Class

public class Disbursement
{
    [Key]
    public int DisbursementId { get; set; }

    [ForeignKey("LoanApplication")]
    public int ApplicationId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DisbursedAmount { get; set; }

    public DateTime DisbursementDate { get; set; }

    public string RepaymentSchedule { get; set; }

    public LoanApplication LoanApplication { get; set; }
}


I want DbContext class for this say LoanContext

using Microsoft.EntityFrameworkCore;

public class LoanContext : DbContext
{
    public LoanContext(DbContextOptions<LoanContext> options)
        : base(options)
    {
    }

    public DbSet<LoanApplication> LoanApplications { get; set; }
    public DbSet<CreditEvaluation> CreditEvaluations { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Approval> Approvals { get; set; }
    public DbSet<Disbursement> Disbursements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set enum to be stored as string
        modelBuilder
            .Entity<LoanApplication>()
            .Property(l => l.Status)
            .HasConversion<string>();

        modelBuilder
            .Entity<Approval>()
            .Property(a => a.ApprovalStatus)
            .HasConversion<string>();

        // Decimal precision setup
        modelBuilder
            .Entity<LoanApplication>()
            .Property(l => l.LoanAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder
            .Entity<CreditEvaluation>()
            .Property(c => c.RiskScore)
            .HasColumnType("decimal(5,2)");

        modelBuilder
            .Entity<Disbursement>()
            .Property(d => d.DisbursedAmount)
            .HasColumnType("decimal(18,2)");
    }
}


now for the above things how the tables will be generated in database using code first apprach i need the complete structure of tables now 


✅ 1. LoanApplication Table

CREATE TABLE LoanApplication (
    ApplicationId INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(100) NOT NULL,
    LoanType NVARCHAR(50) NOT NULL,
    LoanAmount DECIMAL(18, 2) NOT NULL,
    Status NVARCHAR(MAX) NOT NULL,
    SubmissionDate DATETIME2 NOT NULL
);
✅ 2. CreditEvaluation Table

CREATE TABLE CreditEvaluation (
    EvaluationId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicationId INT NOT NULL,
    RiskScore DECIMAL(5, 2) NOT NULL,
    CreditScore INT NOT NULL,
    EvaluationDate DATETIME2 NOT NULL,
    FOREIGN KEY (ApplicationId) REFERENCES LoanApplication(ApplicationId)
);
✅ 3. Document Table

CREATE TABLE Document (
    DocumentId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicationId INT NOT NULL,
    DocumentType NVARCHAR(50),
    FilePath NVARCHAR(255),
    UploadDate DATETIME2 NOT NULL,
    IsValid BIT NOT NULL,
    FOREIGN KEY (ApplicationId) REFERENCES LoanApplication(ApplicationId)
);
✅ 4. Approval Table

CREATE TABLE Approval (
    ApprovalId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicationId INT NOT NULL,
    ApproverId INT NOT NULL,
    ApprovalLevel INT NOT NULL,
    ApprovalStatus NVARCHAR(MAX) NOT NULL,
    Comments NVARCHAR(MAX),
    ApprovalDate DATETIME2 NOT NULL,
    FOREIGN KEY (ApplicationId) REFERENCES LoanApplication(ApplicationId)
);
✅ 5. Disbursement Table

CREATE TABLE Disbursement (
    DisbursementId INT IDENTITY(1,1) PRIMARY KEY,
    ApplicationId INT NOT NULL,
    DisbursedAmount DECIMAL(18, 2) NOT NULL,
    DisbursementDate DATETIME2 NOT NULL,
    RepaymentSchedule NVARCHAR(MAX),
    FOREIGN KEY (ApplicationId) REFERENCES LoanApplication(ApplicationId)
);


3.1 Loan Application Management Module

Purpose:
    Enables corporate clients to initiate and track loan applications.

Controller:
    - LoanApplicationController
        • submitApplication()
        • trackApplicationStatus()
        • viewApplicationDetails()

Service:
    - LoanApplicationService
        • Validates and saves loan applications to the database.
        • Retrieves application status and details.

Model:
    - LoanApplication Entity
        Attributes:
            • applicationId (PK)
            • companyName
            • loanType
            • loanAmount
            • status
            • submissionDate


for this i want web api controller code and also need service also which will be used in controller of web api 
I want it in the form of interface where All methods will go and service class should implement that interface 
and in LoanApplicationController i shoudl use it 


✅ 1. LoanApplication Entity (Model)

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LoanApplication
{
    [Key]
    public int ApplicationId { get; set; }

    [Required]
    public string CompanyName { get; set; }

    public string LoanType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LoanAmount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime SubmissionDate { get; set; } = DateTime.Now;
}
✅ 2. LoanContext (DbContext)

using Microsoft.EntityFrameworkCore;

public class LoanContext : DbContext
{
    public LoanContext(DbContextOptions<LoanContext> options) : base(options) { }

    public DbSet<LoanApplication> LoanApplications { get; set; }
}

✅ 3. ILoanApplicationService Interface

using System.Threading.Tasks;

public interface ILoanApplicationService
{
    Task<LoanApplication> SubmitApplicationAsync(LoanApplication application);
    Task<LoanApplication> GetApplicationStatusAsync(int applicationId);
    Task<LoanApplication> GetApplicationDetailsAsync(int applicationId);
}
✅ 4. LoanApplicationService Implementation

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LoanApplicationService : ILoanApplicationService
{
    private readonly LoanContext _context;

    public LoanApplicationService(LoanContext context)
    {
        _context = context;
    }

    public async Task<LoanApplication> SubmitApplicationAsync(LoanApplication application)
    {
        _context.LoanApplications.Add(application);
        await _context.SaveChangesAsync();
        return application;
    }

    public async Task<LoanApplication> GetApplicationStatusAsync(int applicationId)
    {
        return await _context.LoanApplications
            .FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
    }

    public async Task<LoanApplication> GetApplicationDetailsAsync(int applicationId)
    {
        return await _context.LoanApplications
            .FirstOrDefaultAsync(x => x.ApplicationId == applicationId);
    }
}
✅ 5. LoanApplicationController (Web API Controller)

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LoanApplicationController : ControllerBase
{
    private readonly ILoanApplicationService _loanService;

    public LoanApplicationController(ILoanApplicationService loanService)
    {
        _loanService = loanService;
    }

    // POST: api/LoanApplication/submit
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitApplication([FromBody] LoanApplication application)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _loanService.SubmitApplicationAsync(application);
        return CreatedAtAction(nameof(ViewApplicationDetails), new { id = result.ApplicationId }, result);
    }

    // GET: api/LoanApplication/status/5
    [HttpGet("status/{id}")]
    public async Task<IActionResult> TrackApplicationStatus(int id)
    {
        var result = await _loanService.GetApplicationStatusAsync(id);
        if (result == null)
            return NotFound();

        return Ok(new { result.ApplicationId, result.Status });
    }

    // GET: api/LoanApplication/details/5
    [HttpGet("details/{id}")]
    public async Task<IActionResult> ViewApplicationDetails(int id)
    {
        var result = await _loanService.GetApplicationDetailsAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
✅ 6. Program.cs (Dependency Injection Setup)

builder.Services.AddDbContext<LoanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
✅ Summary
Layer	File Name	Purpose
Model	LoanApplication.cs	Entity definition
DbContext	LoanContext.cs	EF Core DbContext
Interface	ILoanApplicationService.cs	Contract for service layer
Service	LoanApplicationService.cs	Implements business logic
Controller	LoanApplicationController.cs	Handles HTTP API requests


