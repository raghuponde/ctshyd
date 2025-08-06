

Now open the project which we were doing yesterday codefirstdemo and add packages from nugget manager 
step 1 :
-----------

Microsoft.AspNetCore.Identity.EntityFrameworkCore  of version 8.0.18 

Microsoft.AspNetCore.Authentication.JwtBearer  of version 8.0.18

step 2 :
--------
after installing this once build the project once

step 3 :
---------
add one class in Models folder like this with annotations 
    
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


step 4 :
--------
now go to EventDbContext chnage the code like this 

    using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFDEmo.Models
{
    public class EventContext:IdentityDbContext<IdentityUser>
    {
        public EventContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Author> authors { get; set; }

        public DbSet<Course> courses { get; set; }

        public DbSet<Student> students { get; set; }

        public DbSet<UserDetail> userdetails { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<Author1> authors1 { get; set; }

        public DbSet<Course1> courses1 { get; set; }


        public DbSet<Course2> courses2 { get; set; }

        public DbSet<Author2> authors2 { get; set; }

        public DbSet<UserDetail2> userdetails2 { get; set; }



        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Emp> emps { get; set; }

        public DbSet<Post> posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);


            // Fluent API for Author2
            modelBuilder.Entity<Author2>(entity =>
            {



                entity.HasKey(a => a.Id); // Primary Key
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);

                // Relationship with Course2
                entity.HasMany(a => a.Courses)
                      .WithOne(c => c.Author)
                      .HasForeignKey(c => c.Author2Id)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete
            });

            // Fluent API for Course2
            modelBuilder.Entity<Course2>(entity =>
            {
                entity.HasKey(c => c.Id); // Primary Key

                entity.Property(c => c.Id)
                      .ValueGeneratedNever(); // Not an identity column

                entity.Property(c => c.Title)
                      .IsRequired()
                      .HasMaxLength(255)
                      .HasColumnName("Stitle")
                      .HasColumnType("varchar");

                entity.Property(c => c.Description)
                      .IsRequired()
                      .HasMaxLength(220);

                entity.Property(c => c.fullprice)
                      .HasColumnType("float")
                      .IsRequired();

                // Foreign Key to Author1
                entity.HasOne(c => c.Author)
                      .WithMany(a => a.Courses)
                      .HasForeignKey(c => c.Author2Id);
            });


            // Fluent API for UserDetail
            modelBuilder.Entity<UserDetail2>(entity =>
            {
                entity.HasKey(u => u.Id); // Primary Key

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(u => u.NewPassword)
                      .IsRequired()
                      .HasMaxLength(11);

                entity.Property(u => u.DateOfBirth)
                      .IsRequired()
                      .HasColumnType("date");

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnType("varchar");

                entity.Property(u => u.PostalCode)
                      .IsRequired()
                      .HasColumnType("int");

                entity.Property(u => u.PhoneNo)
                      .IsRequired();

                entity.Property(u => u.Profile)
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");
            });

            // Seed data for Author1 and Course1
            modelBuilder.Entity<Author2>().HasData(
                new Author2 { Id = 1, Name = "Author One" },
                new Author2 { Id = 2, Name = "Author Two" }
            );

            modelBuilder.Entity<Course2>().HasData(
                new Course2 { Id = 1, Title = "Course A", Description = "Description A", fullprice = 100, Author2Id = 1 },
                new Course2 { Id = 2, Title = "Course B", Description = "Description B", fullprice = 200, Author2Id = 2 }
            );
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
            new IdentityRole()
            {
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "Admin"
            },
            new IdentityRole()
            {
                Name = "User",
                ConcurrencyStamp = "2",
                NormalizedName = "User"
            },
            new IdentityRole()
            {
                Name = "HR",
                ConcurrencyStamp = "3",
                NormalizedName = "HR"
            }
            );
        }

    }
}

here i am adding roles manully though migrations above here 

Step 4:
-----------
Now in program.cs do the following changes 

// For Entity Framework
var configuration = builder.Configuration;
    builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring"))); //after this add it okay 
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
});


step 5 :
---------
---> Now run the migrations commands 

       add-migration InitialCreate
       update-database


--->Now go to dabatabse and analaize the tables once okay these tables will be empty only okay only roles table will be filled 

step 6:
---------
add another class Response in Models folder okay ...
    
public class Response
{
public string? Status { get; set; }
public string? Message { get; set; }
}

I will use above class properties to define status and message 

Next add AuthenticationContrller of web api type and it should be empty one 
-------------------------------------------------------------------------------
using CodeFirstEFDEmo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    }
}

once build the solution and then
Run the web api an insert one value of the user

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
  FROM [EventsDB].[dbo].[AspNetUsers]

step 7 :
---------
Now i need to work on login method ..
    
Now first add LoginModel class into the folder of models 

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

and then go to app settings and write the JWT ..code here


{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConnStr": "server=LAPTOP-4G8BHPK9\\SQLEXPRESS;database=TaokenDB;Trusted_Connection=true"
  },  
  "JWT": {
    "ValidAudience": "https://localhost:3000",
    "ValidIssuer": "https://localhost:7113",
    "Secret": "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyrsss"
  }
}

so here 3000 is external frontend of react which is trying to access our web api ..and 7264 is
the URL used in profiles of launch settings https one only use it of properties but here in our case both urls are same only 


so changing it like this 

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "constring": "Data Source=LAPTOP-4G8BHPK9\\SQLEXPRESS;initial catalog=EventsDB;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;"
  },
  "JWT": {
    "ValidAudience": "https://localhost:7267",
    "ValidIssuer": "https://localhost:7267",
    "Secret": "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyrsss"
  }
}



step 8:
----------
Now further code of Authentication Controller 

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

step 9 :
-----------
Now Program.cs file further jwt code is written like this and for authorize button in swagger i am
writing some code in the middleware because in swagger authorize button is not there like
postman so explicitly u have to add code and finally down u have to add one method which is
app.UseAuthentication();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

Now i had created one controller like this

step 10 :
-----------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        [HttpGet("employees")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "santosh", "Ali", "sita" };
        }

    }
}

Here all in built methods will be there and now run the login method then we will see that a
token will be created .
next i will add [Authorize] attribute Here [Authorize] is one kind of filter which gets executed before u call means its logic will be executed 
Above class then 401 error i will get it means that first you
have to login and then token will be created and u have to send the token whenever u you are
calling the method which has that [Authorize] attribute

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {

        [HttpGet("employees")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "santosh", "Ali", "sita" };
        }

    }
}
so you can see in program.cs file to create a authorize button i had added this much amount of
code
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

one token will be created after login and on top one button is there Authorize take this token and click that button once
and submit the token

step 11 :
----------
    now again i have changed it to Admin Controller like this with role okay
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEFDEmo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")
    public class AdminController : ControllerBase
    {

        [HttpGet("employees")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "santosh", "Ali", "sita" };
        }

    }
}
now again if try to use means create a user without admin role okay and then try to access this
method with that user just now error 403 forbidden will come okay means u will login with other
than admin user and will try to use this method then 403 error will come okay so using admin
token only i have to give then i can access above method
