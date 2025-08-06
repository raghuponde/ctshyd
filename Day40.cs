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

step 3:
----------
In Models folder add RegisterUser class like this with annotations 

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
    
here i am adding roles manully though migrations 


