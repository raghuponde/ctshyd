
code1 lab 
---------------
EFCore/appsettings.json
    1 {
    2   "ConnectionStrings": {
    3     "DefaultConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True"
    4   }
    5 }
    6 
EFCore/DataContext/CarContext.cs
    1 using Microsoft.EntityFrameworkCore;
    2 using Microsoft.Extensions.Configuration;
    3 
    4 namespace CarProject  //DO NOT Change the namespace name
    5 {
    6     public class CarContext : DbContext //DO NOT Change the class name
    7     {
    8          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    9          {
   10              // implement the code for configuring connection using connection string name mentioned in appsettings.json file
   11               if (!optionsBuilder.IsConfigured)
   12             {
   13                 IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
   14                 
   15                 string connectionString = configuration.GetConnectionString("DefaultConnection");                
   16                 optionsBuilder.UseSqlServer(connectionString);                
   17             }
   18          }
   19        
   20         //Declare 'Cars' of type Dbset and add neccessary declaration code.
   21          public  DbSet<Car> Cars { get; set; }
   22     }
   23 }
EFCore/EFCore.csproj
    1 <Project Sdk="Microsoft.NET.Sdk">
    2 
    3   <PropertyGroup>
    4     <OutputType>Exe</OutputType>
    5     <TargetFramework>net6.0</TargetFramework>
    6     <ImplicitUsings>enable</ImplicitUsings>
    7     <Nullable>enable</Nullable>
    8     <GenerateProgramFile>false</GenerateProgramFile>
    9   </PropertyGroup>
   10 
   11   <ItemGroup>
   12     <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0-preview.4.23259.3" />
   13     <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0-preview.4.23259.3">
   14       <PrivateAssets>all</PrivateAssets>
   15       <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
   16     </PackageReference>
   17     <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0-preview.4.23259.5" />
   18     <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.0" />
   19     <PackageReference Include="NUnit" Version="3.13.3" />
   20     <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
   21   </ItemGroup>
   22 
   23    <ItemGroup>
   24 	  <None Update="appsettings.json">
   25 	    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
   26 	  </None>
   27 	</ItemGroup>
   28 
   29 </Project>
   30 	 	  	      	 		  	   	   	 	
   31 
EFCore/Models/Program.cs
    1 namespace CarProject //DO NOT Change the namespace name
    2 {
    3     public class Program //DO NOT Change the class name
    4     {
    5         public static void Main(string[] args)
    6         {
    7             //Implement the code here
    8             Car carObj= new Car();
    9            CarRepository repObj = new CarRepository(); 
   10            
   11             Console.WriteLine("Enter car id");
   12             carObj.Id = Convert.ToInt32(Console.ReadLine());
   13 
   14             Console.WriteLine("Enter car brand");
   15             carObj.Brand = Console.ReadLine();
   16 
   17             Console.WriteLine("Enter car model");
   18             carObj.Model = Console.ReadLine();
   19             
   20             Console.WriteLine("Enter car price");
   21             carObj.Price = Convert.ToDouble(Console.ReadLine());
   22 
   23             repObj.AddCar(carObj);
   24             Console.WriteLine("Details Added Successfully");
   25         }
   26     }
   27 }
   28 	 	  	    	 	      	  	 	
   29 
EFCore/Models/Car.cs
    1 using System.ComponentModel.DataAnnotations;
    2 using System.ComponentModel.DataAnnotations.Schema;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Car //DO NOT Change the class name
    7     {
    8         //Implement the code here
    9          [Key]
   10         [DatabaseGenerated(DatabaseGeneratedOption.None)]
   11         public int Id { get; set; }
   12         public string Brand { get; set; }
   13         public string Model { get; set; }
   14         public double Price { get; set; }
   15     }
   16 }
EFCore/Models/CarRepository.cs
    1 namespace CarProject  //DO NOT Change the namespace name--
    2 {
    3     public class CarRepository //DO NOT Change the class name
    4     {
    5         //Implement the code here
    6          CarContext context = new CarContext();
    7          
    8         public bool AddCar(Car car)
    9         {
   10             context.Cars.Add(car);
   11             context.SaveChanges();
   12             return true;
   13         }
   14     }
   15 }
   16 
   17 Lab 2 Demo
     -------------
     EFCore/appsettings.json
    1 {
    2   "ConnectionStrings": {
    3     "DefaultConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True"
    4   }
    5 }
    6 
EFCore/DataContext/CarContext.cs
    1 using Microsoft.EntityFrameworkCore;
    2 using Microsoft.Extensions.Configuration;
    3 
    4 namespace CarProject  //DO NOT Change the namespace name
    5 {
    6     public class CarContext : DbContext //DO NOT Change the class name
    7     {
    8          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    9          {
   10              // implement the code for configuring connection using connection string name mentioned in appsettings.json file
   11              if (!optionsBuilder.IsConfigured)
   12             {
   13                 IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
   14 
   15                 string connectionString = configuration.GetConnectionString("DefaultConnection");
   16                 optionsBuilder.UseSqlServer(connectionString);
   17             }
   18          }
   19        
   20         //Declare 'Cars' and 'Makes' of type Dbset and add neccessary declaration code.
   21         public DbSet<Car> Cars { get; set; }
   22         public DbSet<Make> Makes { get; set; }
   23     }
   24 }
EFCore/EFCore.csproj
    1 <Project Sdk="Microsoft.NET.Sdk">
    2 
    3   <PropertyGroup>
    4     <OutputType>Exe</OutputType>
    5     <TargetFramework>net6.0</TargetFramework>
    6     <ImplicitUsings>enable</ImplicitUsings>
    7     <Nullable>enable</Nullable>
    8     <GenerateProgramFile>false</GenerateProgramFile>
    9   </PropertyGroup>
   10 
   11   <ItemGroup>
   12     <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0-preview.4.23259.3" />
   13     <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0-preview.4.23259.3">
   14       <PrivateAssets>all</PrivateAssets>
   15       <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
   16     </PackageReference>
   17     <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0-preview.4.23259.5" />
   18     <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.0" />
   19     <PackageReference Include="NUnit" Version="3.13.3" />
   20     <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
   21   </ItemGroup>
   22 
   23    <ItemGroup>
   24 	  <None Update="appsettings.json">
   25 	    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
   26 	  </None>
   27 	</ItemGroup>
   28 
   29 </Project>
   30 	 	  	      	 		  	   	   	 	
   31 
EFCore/Models/Program.cs
    1 using Microsoft.EntityFrameworkCore;
    2 using System.Linq.Expressions;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Program //DO NOT Change the class name
    7     {
    8         public static void Main(string[] args)
    9         {
   10             //Implement the code here
   11              using (var dbContext = new CarContext())
   12             {
   13                 var cars = CarRepository.GetAllCarsWithMake(dbContext);
   14 
   15                 foreach (var car in cars)
   16                 {
   17                     Console.WriteLine($"Car Id: {car.Id}, Make: {car.Make.Name}, Model: {car.Model}, Year: {car.Year}");
   18                 }
   19             }
   20         }
   21         
   22         public static ParameterExpression variableExpr = Expression.Variable(typeof(IEnumerable<Car>), "sampleVar");
   23         public static Expression GetMyExpression(CarContext context)
   24         {
   25             Expression assignExpr = Expression.Assign(variableExpr, Expression.Constant(context.Cars.Include(c => c.Make)/**Copy and Paste the LINQ Query here **/));
   26             return assignExpr;
   27         }
   28     }
   29 }
   30 	 	  	    	 	      	  	 	
   31 
EFCore/Models/Car.cs
    1 using System.ComponentModel.DataAnnotations;
    2 using System.ComponentModel.DataAnnotations.Schema;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Car //DO NOT Change the class name
    7     {
    8         //Implement the code here
    9         [Key]
   10         [DatabaseGenerated(DatabaseGeneratedOption.None)]
   11         public int Id { get; set; }
   12         public string Model { get; set; }
   13         public int Year { get; set; }
   14         public int MakeId { get; set; }
   15         public Make Make { get; set; }
   16     }
   17 }
EFCore/Models/Make.cs
    1 using System.ComponentModel.DataAnnotations;
    2 using System.ComponentModel.DataAnnotations.Schema;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Make //DO NOT Change the class name
    7     {
    8         //Implement the code here
    9         [Key]
   10         [DatabaseGenerated(DatabaseGeneratedOption.None)]
   11         public int Id { get; set; }
   12         public string Name { get; set; }
   13     }
   14 }
EFCore/Models/CarRepository.cs
    1 using Microsoft.EntityFrameworkCore;
    2 
    3 namespace CarProject  //DO NOT Change the namespace name
    4 {
    5     public class CarRepository //DO NOT Change the class name
    6     {
    7         //Implement the code here
    8          public static IEnumerable<Car> GetAllCarsWithMake(CarContext context)
    9         {
   10             return context.Cars.Include(c => c.Make);
   11         }
   12     }
   13 }
   14 
   15 	 	  	    	 	      	  	 	
   16 Lab3 demo 
--------------
EFCore/appsettings.json
    1 {
    2   "ConnectionStrings": {
    3     "DefaultConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True"
    4   }
    5 }
    6 
EFCore/DataContext/CarContext.cs
    1 using Microsoft.EntityFrameworkCore;
    2 using Microsoft.Extensions.Configuration;
    3 
    4 namespace CarProject  //DO NOT Change the namespace name
    5 {
    6     public class CarContext : DbContext//DO NOT Change the class name
    7     {
    8          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    9          {
   10              // implement the code for configuring connection using connection string name mentioned in appsettings.json file
   11               if (!optionsBuilder.IsConfigured)
   12             {
   13                 IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
   14 
   15                 string connectionString = configuration.GetConnectionString("DefaultConnection");
   16                 optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
   17             }
   18          }
   19        
   20         //Declare 'Cars' and 'Makes' of type Dbset and add neccessary declaration code.
   21         public DbSet<Car> Cars { get; set; }
   22         public DbSet<Make> Makes { get; set; }
   23     }
   24 }
EFCore/EFCore.csproj
    1 <Project Sdk="Microsoft.NET.Sdk">
    2 
    3   <PropertyGroup>
    4     <OutputType>Exe</OutputType>
    5     <TargetFramework>net6.0</TargetFramework>
    6     <ImplicitUsings>enable</ImplicitUsings>
    7     <Nullable>enable</Nullable>
    8   <GenerateProgramFile>false</GenerateProgramFile>
    9   </PropertyGroup>
   10 
   11   <ItemGroup>
   12     <PackageReference Include="Microsoft.EntityFrameworkCore.Proxies" Version="8.0.0-preview.5.23280.1" />
   13     <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0-preview.5.23280.1" />
   14     <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0-preview.5.23280.1">
   15       <PrivateAssets>all</PrivateAssets>
   16       <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
   17     </PackageReference>
   18     <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0-preview.5.23280.8" />
   19     <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.0-preview.23280.1" />
   20     <PackageReference Include="NUnit" Version="3.13.3" />
   21     <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
   22   </ItemGroup>
   23 
   24 <ItemGroup>
   25 	  <None Update="appsettings.json">
   26 	    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
   27 	  </None>
   28 	</ItemGroup>
   29 
   30 </Project>
   31 
   32 
   33 	 	  	      	 		  	   	   	 	
   34 
EFCore/Models/Program.cs
    1 using Microsoft.EntityFrameworkCore;
    2 using System.Linq.Expressions;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Program //DO NOT Change the class name
    7     {
    8         public static void Main(string[] args)
    9         {
   10             //Implement the code here-
   11             using (var dbContext = new CarContext())
   12             {                
   13                var cars = CarRepository.GetAllCarsWithMake(dbContext);               
   14              
   15                 foreach (var car in cars)   
   16                 {
   17                     Console.WriteLine($"Car Id: {car.Id},Make: {car.Make.Name}, Model: {car.Model}, Year: {car.Year}");                    
   18                 }                
   19             }
   20         }
   21         
   22         public static ParameterExpression variableExpr = Expression.Variable(typeof(IEnumerable<Car>), "sampleVar");
   23         public static Expression GetMyExpression(CarContext context)
   24         {
   25             Expression assignExpr = Expression.Assign(variableExpr, Expression.Constant(context.Cars.ToList()/**Copy and Paste the LINQ Query here **/));
   26             return assignExpr;
   27         }
   28     }
   29 }
   30 	 	  	    	 	      	  	 	
   31 
EFCore/Models/Car.cs
    1 using System.ComponentModel.DataAnnotations;
    2 using System.ComponentModel.DataAnnotations.Schema;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Car //DO NOT Change the class name
    7     {
    8         //Implement the code here
    9          [Key]
   10         [DatabaseGenerated(DatabaseGeneratedOption.None)]
   11         public int Id { get; set; }
   12         public string Model { get; set; }
   13         public int Year { get; set; }
   14         public int MakeId { get; set; }
   15         public virtual Make Make { get; set; }
   16     }
   17 }
EFCore/Models/Make.cs
    1 using System.ComponentModel.DataAnnotations;
    2 using System.ComponentModel.DataAnnotations.Schema;
    3 
    4 namespace CarProject //DO NOT Change the namespace name
    5 {
    6     public class Make //DO NOT Change the class name
    7     {
    8         //Implement the code here
    9          [Key]
   10         [DatabaseGenerated(DatabaseGeneratedOption.None)]
   11         public int Id { get; set; }
   12         public string Name { get; set; }    
   13     }
   14 }
EFCore/Models/CarRepository.cs
    1 using Microsoft.EntityFrameworkCore;
    2 
    3 namespace CarProject  //DO NOT Change the namespace name
    4 {
    5     public class CarRepository //DO NOT Change the class name
    6     {
    7         //Implement the code here
    8         public static IEnumerable<Car> GetAllCarsWithMake(CarContext context)
    9         {
   10             return context.Cars.ToList();
   11         }
   12     }
   13 }
   14 
   15 	 	  	    	 	      	  	 	
   16 
   18 
