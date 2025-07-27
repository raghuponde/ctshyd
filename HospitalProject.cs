Hospital Project 
-------------------
1. Patient Table
CREATE TABLE Patient ( patientId INT PRIMARY KEY AUTO_INCREMENT, name VARCHAR(100), dateOfBirth DATE, gender VARCHAR(10), contactNumber
VARCHAR(15), address VARCHAR(255), medicalHistory TEXT );
2. Doctor Table
CREATE TABLE Doctor ( doctorId INT PRIMARY KEY AUTO_INCREMENT, name VARCHAR(100), specialization VARCHAR(100), contactNumber VARCHAR(15),
availabilitySchedule TEXT );
3. Appointment Table
CREATE TABLE Appointment ( appointmentId INT PRIMARY KEY AUTO_INCREMENT, patientId INT, doctorId INT, appointmentDate DATE, timeSlot
VARCHAR(20), status ENUM('CONFIRMED', 'CANCELLED'), FOREIGN KEY (patientId) REFERENCES Patient(patientId), FOREIGN KEY (doctorId)
REFERENCES Doctor(doctorId) );
4. Bill Table
CREATE TABLE Bill (
billId INT PRIMARY KEY AUTO_INCREMENT,
patientId INT,
totalAmount DECIMAL(10, 2),
paymentStatus ENUM('PAID', 'UNPAID'),
billDate DATE,
FOREIGN KEY (patientId) REFERENCES Patient(patientId)
);
5. User Table
CREATE TABLE User ( userId INT PRIMARY KEY AUTO_INCREMENT, username VARCHAR(50) UNIQUE, password VARCHAR(255), role ENUM('ADMIN',
'PATIENT', 'DOCTOR') );

for this description i want classes with annotatons  so that i can use it in code first entity framework to generate tables 



1. Patient.cs


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Patient
{
    [Key]
    public int PatientId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [MaxLength(10)]
    public string Gender { get; set; }

    [MaxLength(15)]
    public string ContactNumber { get; set; }

    [MaxLength(255)]
    public string Address { get; set; }

    public string MedicalHistory { get; set; }

    // Navigation properties
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Bill> Bills { get; set; }
}


2.Doctor.cs 

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Specialization { get; set; }

    [MaxLength(15)]
    public string ContactNumber { get; set; }

    public string AvailabilitySchedule { get; set; }

    // Navigation property
    public ICollection<Appointment> Appointments { get; set; }
}

3 Appointment

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [MaxLength(20)]
    public string TimeSlot { get; set; }

    [Required]
    public string Status { get; set; } // Use string or enum in code and convert to DB enum via Fluent API

    // Navigation properties
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }
}


4. Bill.cs 

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Bill
{
    [Key]
    public int BillId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    public string PaymentStatus { get; set; } // ENUM in DB - string in code

    [DataType(DataType.Date)]
    public DateTime BillDate { get; set; }

    // Navigation property
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
}


5.User.cs 
---------

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; } // ENUM in DB - string in code
}


Optional: Create an enum for Status and PaymentStatus
If you want to use enums in code and map them to strings or integers in the database using Fluent API:


public enum AppointmentStatus
{
    CONFIRMED,
    CANCELLED
}

public enum PaymentStatus
{
    PAID,
    UNPAID
}

Then update the Appointment and Bill class to use the enum type and configure mapping using Fluent API.


Great! Here's how you can configure the enums and relationships using Fluent API in Entity Framework Core. This allows you to map your enums to string-based database columns (matching your SQL ENUMs) and fine-tune your schema.

1. Patient.cs


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Patient
{
    [Key]
    public int PatientId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [MaxLength(10)]
    public string Gender { get; set; }

    [MaxLength(15)]
    public string ContactNumber { get; set; }

    [MaxLength(255)]
    public string Address { get; set; }

    public string MedicalHistory { get; set; }

    // Navigation properties
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Bill> Bills { get; set; }
}


2.Doctor.cs 

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Specialization { get; set; }

    [MaxLength(15)]
    public string ContactNumber { get; set; }

    public string AvailabilitySchedule { get; set; }

    // Navigation property
    public ICollection<Appointment> Appointments { get; set; }
}


3 Appointment

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [MaxLength(20)]
    public string TimeSlot { get; set; }

    [Required]
    public AppointmentStatus Status { get; set; } 
    // Navigation properties
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }
}

public enum AppointmentStatus
{
    CONFIRMED,
    CANCELLED
}


4. Bill.cs 

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Bill
{
    [Key]
    public int BillId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    [Required]
     public PaymentStatus PaymentStatus { get; set; } 
    [DataType(DataType.Date)]
    public DateTime BillDate { get; set; }

    // Navigation property
    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }
}

public enum PaymentStatus
{
    PAID,
    UNPAID
}


5.User.cs 
---------

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; } // ENUM in DB - string in code
}


HospitalContext 
----------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourNamespace.Models;

namespace YourNamespace.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion(new EnumToStringConverter<AppointmentStatus>());

            modelBuilder.Entity<Bill>()
                .Property(b => b.PaymentStatus)
                .HasConversion(new EnumToStringConverter<PaymentStatus>());
        }
    }
}



now for the above things how the tables will be generated in database using code first apprach i need the complete structure of tables now 


✅ 1. Patients Table
sql
Copy
Edit
CREATE TABLE Patients (
    PatientId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    ContactNumber NVARCHAR(15),
    Address NVARCHAR(255),
    MedicalHistory NVARCHAR(MAX)
);
✅ 2. Doctors Table
sql
Copy
Edit
CREATE TABLE Doctors (
    DoctorId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100),
    ContactNumber NVARCHAR(15),
    AvailabilitySchedule NVARCHAR(MAX)
);
✅ 3. Appointments Table
sql
Copy
Edit
CREATE TABLE Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATE NOT NULL,
    TimeSlot NVARCHAR(20),
    Status NVARCHAR(MAX) NOT NULL, -- Stored as CONFIRMED / CANCELLED

    CONSTRAINT FK_Appointments_Patients FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    CONSTRAINT FK_Appointments_Doctors FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);
✅ 4. Bills Table
sql
Copy
Edit
CREATE TABLE Bills (
    BillId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaymentStatus NVARCHAR(MAX) NOT NULL, -- Stored as PAID / UNPAID
    BillDate DATE NOT NULL,

    CONSTRAINT FK_Bills_Patients FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);
✅ 5. Users Table
sql
Copy
Edit
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(MAX) NOT NULL
);
📝 Notes
IDENTITY(1,1) means auto-increment starting from 1.

All string properties like Name, Role, etc. are translated into NVARCHAR.

Enum values like AppointmentStatus and PaymentStatus are saved as string values (e.g., 'CONFIRMED', 'PAID') due to the EnumToStringConverter.

Navigation properties like Patient and Doctor are not added as columns, but they define foreign key relationships.

✅ Relationships Summary
Appointments

FK to Patients on PatientId

FK to Doctors on DoctorId

Bills

FK to Patients on PatientId

so now i have to write web api controller 



3.1 Patient Management Module

Purpose: 
    Manages patient details and their medical history.

Controller:
    PatientController
        - addPatient(patientData)
        - updatePatient(patientId, patientData)
        - getPatientDetails(patientId)
        - deletePatient(patientId)

Service:
    PatientService
        - Validate patient data.
        - Interact with the database for CRUD operations.

Model:
    Entity: Patient
        Attributes:
            - patientId (PK)
            - name (VARCHAR)
            - dateOfBirth (DATE)
            - gender (VARCHAR)
            - contactNumber (VARCHAR)
            - address (VARCHAR)
            - medicalHistory (TEXT)
			
			
for this i want web api controller code and also need service also which will be used in controller of web api 





HospitalAPI/
├── Controllers/
│   └── PatientController.cs
├── Services/
│   └── IPatientService.cs
│   └── PatientService.cs
├── Models/
│   └── Patient.cs
├── Data/
│   └── HospitalContext.cs


 1. Interface: IPatientService.cs
csharp
Copy
Edit
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAPI.Models;

namespace HospitalAPI.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int patientId);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(int patientId, Patient updatedPatient);
        Task<bool> DeletePatientAsync(int patientId);
    }
}


 2. Service Implementation: PatientService.cs
csharp
Copy
Edit
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAPI.Data;
using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalContext _context;

        public PatientService(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int patientId)
        {
            return await _context.Patients.FindAsync(patientId);
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> UpdatePatientAsync(int patientId, Patient updatedPatient)
        {
            var existing = await _context.Patients.FindAsync(patientId);
            if (existing == null) return false;

            existing.Name = updatedPatient.Name;
            existing.DateOfBirth = updatedPatient.DateOfBirth;
            existing.Gender = updatedPatient.Gender;
            existing.ContactNumber = updatedPatient.ContactNumber;
            existing.Address = updatedPatient.Address;
            existing.MedicalHistory = updatedPatient.MedicalHistory;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


 3. Web API Controller: PatientController.cs
csharp
Copy
Edit
using HospitalAPI.Models;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Patient patientData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _patientService.AddPatientAsync(patientData);
            return CreatedAtAction(nameof(GetById), new { id = created.PatientId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Patient patientData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _patientService.UpdatePatientAsync(id, patientData);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
✅ 4. Register Service in Program.cs
Open Program.cs and register the service:


builder.Services.AddScoped<IPatientService, PatientService>();
🧪 Test via Swagger
Run the app and go to:


https://localhost:<port>/swagger
You can now test:

GET /api/patient

POST /api/patient

PUT /api/patient/{id}

DELETE /api/patient/{id}


