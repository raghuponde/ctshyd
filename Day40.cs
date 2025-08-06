https://olympus1.mygreatlearning.com/online_session/recordings?access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJtZW50b3JlZF9zZXNzaW9uX2lkIjoxMzE2ODg2fQ.avLztTupOGifKxf4FgzGz4Eob_NA7251IWlQIGY6W_E

time 3:56 onwards check it 

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

    public class EventDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
        options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
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
    
here i am adding roles manully though migrations above here 

Step 4:
-----------
Now in program.cs do the following changes 

// For Entity Framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("ConnStr")));  //after this u have to add 
 // For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
// adding basic authentication
builder.Services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme =
JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})


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

Next add AuthenticationContrller 
-----------------------------------
using IdentityWithTokenDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityWithTokenDemo.Controllers
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

step 7 :
---------
once build the solution and then
Run the web api an insert one value of the user
Now i need to work on login method ..
Now first add Login Model class into the folder of models 


  public class LoginModel
  {
      [Required(ErrorMessage = "User Name is required")]
      public string? Username { get; set; }

      [Required(ErrorMessage = "Password is required")]
      public string? Password { get; set; }
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

step 8:
----------
Now further code of Authentication Controller 

using IdentityWithTokenDemo.Models.Auuthentication.SignUp;
using IdentityWithTokenDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityWithTokenDemo.Models.Auuthentication.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityWithTokenDemo.Controllers
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

using IdentityWithTokenDemo.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IdentityWithTokenDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // For Entity Framework
            var configuration = builder.Configuration;
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
            // For Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
           

            // Add services to the container.

            builder.Services.AddControllers();
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
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            }); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
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
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


Now i had created one controller like this

step 10 :
-----------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithTokenDemo.Controllers
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
next i will add [Authorize] attribute t above class then 401 error i will get it means that first you
have to login and then token will be created and u have to send the token whenever u you are
calling the method which has that [Authorize] attribute

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithTokenDemo.Controllers
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithTokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
