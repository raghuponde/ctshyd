Lab 1 
*********


Controllers/InterrogationController.cs
    1 using Microsoft.AspNetCore.Mvc;
    2 
    3 namespace ASPNET6_Template.Controllers
    4 {
    5     public class InterrogationController : Controller
    6     {
    7         //Implement your code here
    8         public ActionResult MemberName(int registrationNumber, string name)
    9         {
   10             ViewBag.Details = registrationNumber + " " + name;
   11             return View();
   12         }
   13 
   14         public ActionResult Problem(string problem)
   15         {
   16             ViewData["Problem"] = problem;
   17             return View();
   18         }
   19 
   20         public ActionResult Solution(string solution)
   21         {
   22             TempData["Solution"] = solution;
   23             return RedirectToAction("FinalSolution");
   24         }
   25 
   26         public ActionResult FinalSolution()
   27         {
   28             String final = TempData["Solution"] as String;
   29             return View();  
   30         }
   31     }
   32 }
   33 	 	  	  		    	  	   	    	 	
   34 
Views/Interrogation/FinalSolution.cshtml
    1 <!-- Implement your code here -->
    2 <h1>
    3 The Final Solution is : @TempData["Solution"]
    4 </h1>
    5 
    6 
Views/Interrogation/MemberName.cshtml
    1 <!-- Implement your code here -->
    2 @{
    3     <p>@ViewBag.Details</p>
    4 }
Views/Interrogation/Problem.cshtml
    1 <!-- Implement your code here -->
    2 @{
    3     <p>@ViewData["Problem"]</p>
    4 }
Program.cs
    1 var builder = WebApplication.CreateBuilder(args);
    2 
    3 // Add services to the container.
    4 builder.Services.AddControllersWithViews();
    5 
    6 var app = builder.Build();
    7 
    8 // Configure the HTTP request pipeline.
    9 if (!app.Environment.IsDevelopment())
   10 {
   11     app.UseExceptionHandler("/Home/Error");
   12     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   13     app.UseHsts();
   14 }
   15 
   16 app.UseHttpsRedirection();
   17 app.UseStaticFiles();
   18 
   19 app.UseRouting();
   20 
   21 app.UseAuthorization();
   22 
   23 app.MapControllerRoute(
   24     name: "default",
   25     pattern: "{controller=Interrogation}/{action=MemberName}/{id?}");
   26 
   27 app.Run();
   28 	 	  	  		    	  	   	    	 	
   29 Lab 2 
     *********
     Controllers/InterrogationController.cs
    1 using Microsoft.AspNetCore.Mvc;
    2 
    3 namespace ASPNET6_Template.Controllers
    4 {
    5     public class InterrogationController : Controller
    6     {
    7         //Implement your code here
    8          public IActionResult GuideMe()
    9         {
   10             return View();
   11         }
   12 
   13         // GET : InterrogationController/Form
   14         public ActionResult Form()
   15         {
   16             return View();
   17         }
   18 
   19         // POST : InterrogationController/Form
   20         [HttpPost]
   21         [ValidateAntiForgeryToken]
   22         public ActionResult Form(IFormCollection collection)
   23         {
   24             try
   25             {
   26                 return RedirectToAction(nameof(GuideMe));
   27             }
   28             catch
   29             {
   30                 return View();
   31             }
   32         }
   33     }
   34 }
   35 
   36 	 	  	  		    	  	   	    	 	
   37 
Models/Problem.cs
    1 namespace ASPNET6_Template.Models
    2 {
    3     public class Problem
    4     {
    5         //Implement your code here-
    6         public int Id { get; set; }
    7         public string InterrogatorsProblem { get; set; }
    8         public string Choice1 { get; set; }
    9         public string Choice2 { get; set; }
   10         public string Choice3 { get; set; }
   11         public string Choice4 { get; set; }
   12         public string Solution { get; set; }
   13     }
   14 }
   15 
   16 
Views/Interrogation/Form.cshtml
    1 <!-- Implement your code here -->
    2 @model ASPNET6_Template.Models.Problem
    3 
    4 @{
    5     ViewData["Title"] = "Form";
    6 }
    7 
    8 <h1>Form</h1>
    9 
   10 <h4>Problem</h4>
   11 <hr />
   12 <div class="row">
   13     <div class="col-md-4">
   14         <form asp-action="Form">
   15             <div asp-validation-summary="ModelOnly" class="text-danger"></div>
   16             <div class="form-group">
   17                 <label asp-for="Id" class="control-label"></label>
   18                 <input asp-for="Id" class="form-control" />
   19                 <span asp-validation-for="Id" class="text-danger"></span>
   20             </div>
   21             <div class="form-group">
   22                 <label asp-for="InterrogatorsProblem" class="control-label"></label>
   23                 <input asp-for="InterrogatorsProblem" class="form-control" />
   24                 <span asp-validation-for="InterrogatorsProblem" class="text-danger"></span>
   25             </div>
   26             <div class="form-group">
   27                 <label asp-for="Choice1" class="control-label"></label>
   28                 <input asp-for="Choice1" class="form-control" />
   29                 <span asp-validation-for="Choice1" class="text-danger"></span>
   30             </div>
   31             <div class="form-group">
   32                 <label asp-for="Choice2" class="control-label"></label>
   33                 <input asp-for="Choice2" class="form-control" />
   34                 <span asp-validation-for="Choice2" class="text-danger"></span>
   35             </div>
   36             <div class="form-group">
   37                 <label asp-for="Choice3" class="control-label"></label>
   38                 <input asp-for="Choice3" class="form-control" />
   39                 <span asp-validation-for="Choice3" class="text-danger"></span>
   40             </div>
   41             <div class="form-group">
   42                 <label asp-for="Choice4" class="control-label"></label>
   43                 <input asp-for="Choice4" class="form-control" />
   44                 <span asp-validation-for="Choice4" class="text-danger"></span>
   45             </div>
   46             <div class="form-group">
   47                 <label asp-for="Solution" class="control-label"></label>
   48                 <input asp-for="Solution" class="form-control" />
   49                 <span asp-validation-for="Solution" class="text-danger"></span>
   50             </div>
   51             <div class="form-group">
   52                 <input type="submit" value="Create" class="btn btn-primary" />
   53             </div>
   54         </form>
   55     </div>
   56 </div>
   57 
   58 <div>
   59     <a asp-action="Index">Back to List</a>
   60 </div>
   61 
   62 	 	  	  		    	  	   	    	 	
   63 
Views/Interrogation/GuideMe.cshtml
    1 <!-- Implement your code here -->
    2 
    3 @{
    4     ViewData["Title"] = "GuideMe";
    5 }
    6 
    7 <h1>
    8 Welcome to Interrogation Panel!!!
    9 </h1>
   10 
   11 <partial name="_ReferenceButton" />
   12 
   13 
Views/Shared/_ReferenceButton.cshtml
    1 <!-- Implement your code here -->
    2 <p>
    3     <a asp-action="Form">Create a new problem</a>
    4 </p>
    5 
    6 
    7 
Program.cs
    1 var builder = WebApplication.CreateBuilder(args);
    2 
    3 // Add services to the container.
    4 builder.Services.AddControllersWithViews();
    5 
    6 var app = builder.Build();
    7 
    8 // Configure the HTTP request pipeline.
    9 if (!app.Environment.IsDevelopment())
   10 {
   11     app.UseExceptionHandler("/Home/Error");
   12     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   13     app.UseHsts();
   14 }
   15 
   16 app.UseHttpsRedirection();
   17 app.UseStaticFiles();
   18 
   19 app.UseRouting();
   20 
   21 app.UseAuthorization();
   22 
   23 app.MapControllerRoute(
   24     name: "default",
   25     pattern: "{controller=Interrogation}/{action=GuideMe}/{id?}");
   26 
   27 app.Run();
   28 	 	  	  		    	  	   	    	 	
   
     
     Lab 3:
    *********

     ASPNET6_Template/appsettings.Development.json
    1 {
    2   "Logging": {
    3     "LogLevel": {
    4       "Default": "Information",
    5       "Microsoft.AspNetCore": "Warning"
    6     }
    7   }
    8 }
    9 
ASPNET6_Template/appsettings.json
    1 {
    2   "Logging": {
    3     "LogLevel": {
    4       "Default": "Information",
    5       "Microsoft.AspNetCore": "Warning"
    6     }
    7   },
    8   "AllowedHosts": "*",
    9   "ConnectionStrings": {
   10     "DBConnection": "server=(LocalDB)\\MSSQLLocalDB;database=InterrogationPanelDB;Trusted_Connection=true;"
   11   }
   12 }
   13 
ASPNET6_Template/ASPNET6_Template.csproj
    1 <Project Sdk="Microsoft.NET.Sdk.Web">
    2 
    3   <PropertyGroup>
    4     <TargetFramework>net6.0</TargetFramework>
    5     <Nullable>enable</Nullable>
    6     <ImplicitUsings>enable</ImplicitUsings>
    7   </PropertyGroup>
    8 
    9 	<ItemGroup>
   10 		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.5" />
   11 		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.5" />
   12 		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.5">
   13 			<PrivateAssets>all</PrivateAssets>
   14 			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
   15 		</PackageReference>
   16 	</ItemGroup>
   17 
   18 </Project>
   19 
ASPNET6_Template/Controllers/InterrogationController.cs
    1 using ASPNET6_Template.Data;
    2 using ASPNET6_Template.Models;
    3 using Microsoft.AspNetCore.Mvc;
    4 
    5 namespace ASPNET6_Template.Controllers
    6 {
    7     public class InterrogationController : Controller
    8     {
    9         // Implement code here
   10         InterrogationDbContext db;
   11 
   12         public InterrogationController(InterrogationDbContext _db)
   13         {
   14             db = _db;
   15         }
   16 
   17         [HttpGet]
   18         public ActionResult Craft()
   19         {
   20             return View();
   21         }
   22 
   23         [HttpPost]
   24         [ValidateAntiForgeryToken]
   25         public ActionResult Craft(Problem model)
   26         {
   27             try
   28             {
   29                 db.Problems.Add(model);
   30                 db.SaveChanges();
   31                 return RedirectToAction("Catalog", "Interrogation");
   32             }
   33             catch
   34             {
   35                 return RedirectToAction("Catalog", "Interrogation");
   36             }
   37         }	 	  	  		    	  	   	    	 	
   38         public ActionResult Catalog()
   39         {
   40             return View(db.Problems.ToList());
   41         }
   42     }
   43 }
   44 
ASPNET6_Template/Data/InterrogationDbContext.cs
    1 using ASPNET6_Template.Models;
    2 using Microsoft.EntityFrameworkCore;
    3 
    4 namespace ASPNET6_Template.Data
    5 {
    6     public class InterrogationDbContext : DbContext
    7     {
    8         public InterrogationDbContext(DbContextOptions<InterrogationDbContext> options) : base(options)
    9         {
   10 
   11         }
   12 
   13         // Implement code here
   14         public DbSet<Problem> Problems {set;get;}
   15     }
   16 }
   17 
ASPNET6_Template/Models/ErrorViewModel.cs
    1 namespace ASPNET6_Template.Models
    2 {
    3     public class ErrorViewModel
    4     {
    5         public string? RequestId { get; set; }
    6 
    7         public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    8     }
    9 }
ASPNET6_Template/Models/Problem.cs
    1 using System.ComponentModel.DataAnnotations;
    2 
    3 namespace ASPNET6_Template.Models
    4 {
    5     public class Problem
    6     {
    7         // Implement code here
    8         [Key]
    9         public int Id { get; set; }
   10         [Required(ErrorMessage = "Please provide Problem")]
   11         public string InterrogatorsProblem { get; set; }
   12         [Required(ErrorMessage = "Please provide Choice1")]
   13         public string Choice1 { get; set; }
   14         [Required(ErrorMessage = "Please provide Choice2")]
   15         public string Choice2 { get; set; }
   16         [Required(ErrorMessage = "Please provide Choice3")]
   17         public string Choice3 { get; set; }
   18         [Required(ErrorMessage = "Please provide Choice4")]
   19         public string Choice4 { get; set; }
   20         [Required(ErrorMessage = "Please provide Solution")]
   21         public string Solution { get; set; }
   22     }
   23 }
   24 
   25 
ASPNET6_Template/Program.cs
    1 using ASPNET6_Template.Data;
    2 using Microsoft.EntityFrameworkCore;
    3 
    4 var builder = WebApplication.CreateBuilder(args);
    5 
    6 // Add services to the container.
    7 builder.Services.AddControllersWithViews();
    8 builder.Services.AddDbContext<InterrogationDbContext>
    9     (options => options.UseSqlServer
   10     (builder.Configuration.GetConnectionString("DBConnection")));
   11 
   12 var app = builder.Build();
   13 
   14 // Configure the HTTP request pipeline.
   15 if (!app.Environment.IsDevelopment())
   16 {
   17     app.UseExceptionHandler("/Home/Error");
   18     // The default HSTS value is 30 days.
   19     // You may want to change this for production scenarios,
   20     // see https://aka.ms/aspnetcore-hsts.
   21     app.UseHsts();
   22 }
   23 
   24 app.UseHttpsRedirection();
   25 app.UseStaticFiles();
   26 
   27 app.UseRouting();
   28 
   29 app.UseAuthorization();
   30 
   31 app.MapControllerRoute(
   32     name: "default",
   33     pattern: "{controller=Interrogation}/{action=Catalog}/{id?}");
   34 
   35 app.Run();
   36 	 	  	  		    	  	   	    	 	
   37 
ASPNET6_Template/Properties/launchSettings.json
    1 {
    2   "iisSettings": {
    3     "windowsAuthentication": false,
    4     "anonymousAuthentication": true,
    5     "iisExpress": {
    6       "applicationUrl": "http://localhost:31672",
    7       "sslPort": 44347
    8     }
    9   },
   10   "profiles": {
   11     "ASPNET6_Template": {
   12       "commandName": "Project",
   13       "dotnetRunMessages": true,
   14       "launchBrowser": true,
   15       "applicationUrl": "https://localhost:7142;http://localhost:5142",
   16       "environmentVariables": {
   17         "ASPNETCORE_ENVIRONMENT": "Development"
   18       }
   19     },
   20     "IIS Express": {
   21       "commandName": "IISExpress",
   22       "launchBrowser": true,
   23       "environmentVariables": {
   24         "ASPNETCORE_ENVIRONMENT": "Development"
   25       }
   26     }
   27   }
   28 }
   29 	 	  	  		    	  	   	    	 	
   30 
ASPNET6_Template/Views/Interrogation/Catalog.cshtml
    1 <!-- Implement code here -->
    2 @model IEnumerable<ASPNET6_Template.Models.Problem>
    3 
    4 @{
    5     ViewData["Title"] = "Catalog";
    6 }
    7 
    8 <h1>Catalog</h1>
    9 
   10 <p>
   11     <a asp-action="Craft">Create New</a>
   12 </p>
   13 <table class="table">
   14     <thead>
   15         <tr>
   16             <th>
   17                 @Html.DisplayNameFor(model => model.InterrogatorsProblem)
   18             </th>
   19             <th>
   20                 @Html.DisplayNameFor(model => model.Choice1)
   21             </th>
   22             <th>
   23                 @Html.DisplayNameFor(model => model.Choice2)
   24             </th>
   25             <th>
   26                 @Html.DisplayNameFor(model => model.Choice3)
   27             </th>
   28             <th>
   29                 @Html.DisplayNameFor(model => model.Choice4)
   30             </th>
   31             <th>
   32                 @Html.DisplayNameFor(model => model.Solution)
   33             </th>
   34             <th></th>
   35         </tr>
   36     </thead>
   37     <tbody>
   38         @foreach (var item in Model) 
   39         {	 	  	  		    	  	   	    	 	
   40         <tr>
   41             <td>
   42                 @Html.DisplayFor(modelItem => item.InterrogatorsProblem)
   43             </td>
   44             <td>
   45                 @Html.DisplayFor(modelItem => item.Choice1)
   46             </td>
   47             <td>
   48                 @Html.DisplayFor(modelItem => item.Choice2)
   49             </td>
   50             <td>
   51                 @Html.DisplayFor(modelItem => item.Choice3)
   52             </td>
   53             <td>
   54                 @Html.DisplayFor(modelItem => item.Choice4)
   55             </td>
   56             <td>
   57                 @Html.DisplayFor(modelItem => item.Solution)
   58             </td>
   59         </tr>
   60         }
   61     </tbody>
   62 </table>
   63 
   64 	 	  	  		    	  	   	    	 	
   65 
ASPNET6_Template/Views/Interrogation/Craft.cshtml
    1 <!-- Implement code here -->
    2 @model ASPNET6_Template.Models.Problem
    3 
    4 @{
    5     ViewData["Title"] = "Craft";
    6 }
    7 
    8 <h1>Craft</h1>
    9 
   10 <h4>Problem</h4>
   11 <hr />
   12 <div class="row">
   13     <div class="col-md-4">
   14         <form asp-action="Craft">
   15             <div asp-validation-summary="ModelOnly" class="text-danger"></div>
   16             <div class="form-group">
   17                 <label asp-for="InterrogatorsProblem" class="control-label"></label>
   18                 <input asp-for="InterrogatorsProblem" class="form-control" />
   19                 <span asp-validation-for="InterrogatorsProblem" class="text-danger"></span>
   20             </div>
   21             <div class="form-group">
   22                 <label asp-for="Choice1" class="control-label"></label>
   23                 <input asp-for="Choice1" class="form-control" />
   24                 <span asp-validation-for="Choice1" class="text-danger"></span>
   25             </div>
   26             <div class="form-group">
   27                 <label asp-for="Choice2" class="control-label"></label>
   28                 <input asp-for="Choice2" class="form-control" />
   29                 <span asp-validation-for="Choice2" class="text-danger"></span>
   30             </div>
   31             <div class="form-group">
   32                 <label asp-for="Choice3" class="control-label"></label>
   33                 <input asp-for="Choice3" class="form-control" />
   34                 <span asp-validation-for="Choice3" class="text-danger"></span>
   35             </div>
   36             <div class="form-group">
   37                 <label asp-for="Choice4" class="control-label"></label>
   38                 <input asp-for="Choice4" class="form-control" />
   39                 <span asp-validation-for="Choice4" class="text-danger"></span>
   40             </div>
   41             <div class="form-group">
   42                 <label asp-for="Solution" class="control-label"></label>
   43                 <input asp-for="Solution" class="form-control" />
   44                 <span asp-validation-for="Solution" class="text-danger"></span>
   45             </div>
   46             <div class="form-group">
   47                 <input type="submit" id="btnCraft" value="Create" class="btn btn-primary" />
   48             </div>
   49         </form>
   50     </div>
   51 </div>
   52 
   53 <div>
   54     <a asp-action="Catalog">Back to List</a>
   55 </div>
   56 
   57 	 	  	  		    	  	   	    	 	
   58 
ASPNET6_Template/Views/Shared/Error.cshtml
    1 @model ErrorViewModel
    2 @{
    3     ViewData["Title"] = "Error";
    4 }
    5 
    6 <h1 class="text-danger">Error.</h1>
    7 <h2 class="text-danger">An error occurred while processing your request.</h2>
    8 
    9 @if (Model.ShowRequestId)
   10 {
   11     <p>
   12         <strong>Request ID:</strong> <code>@Model.RequestId</code>
   13     </p>
   14 }
   15 
   16 <h3>Development Mode</h3>
   17 <p>
   18     Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
   19 </p>
   20 <p>
   21     <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
   22     It can result in displaying sensitive information from exceptions to end users.
   23     For local debugging, enable the <strong>Development</strong> environment by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>
   24     and restarting the app.
   25 </p>
   26 	 	  	  		    	  	   	    	 	
   27 
ASPNET6_Template/Views/Shared/_Layout.cshtml
    1 <!DOCTYPE html>
    2 <html lang="en">
    3 <head>
    4     <meta charset="utf-8" />
    5     <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    6     <title>@ViewData["Title"] - ASPNET6_Template</title>
    7     <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    8     <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    9     <link rel="stylesheet" href="~/ASPNET6_Template.styles.css" asp-append-version="true" />
   10 </head>
   11 <body>
   12     <header>
   13         <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
   14             <div class="container-fluid">
   15                 <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">ASPNET6_Template</a>
   16                 <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
   17                         aria-expanded="false" aria-label="Toggle navigation">
   18                     <span class="navbar-toggler-icon"></span>
   19                 </button>
   20                 <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
   21                     <ul class="navbar-nav flex-grow-1">
   22                         <li class="nav-item">
   23                             <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
   24                         </li>
   25                         <li class="nav-item">
   26                             <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
   27                         </li>
   28                     </ul>
   29                 </div>
   30             </div>
   31         </nav>
   32     </header>
   33     <div class="container">
   34         <main role="main" class="pb-3">
   35             @RenderBody()
   36         </main>
   37     </div>
   38 
   39     <footer class="border-top footer text-muted">
   40         <div class="container">
   41             &copy; 2024 - ASPNET6_Template - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
   42         </div>
   43     </footer>
   44     <script src="~/lib/jquery/dist/jquery.min.js"></script>
   45     <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
   46     <script src="~/js/site.js" asp-append-version="true"></script>
   47     @await RenderSectionAsync("Scripts", required: false)
   48 </body>
   49 </html>
   50 	 	  	  		    	  	   	    	 	
   51 
ASPNET6_Template/Views/Shared/_ValidationScriptsPartial.cshtml
    1 <script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
    2 <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
    3 
ASPNET6_Template/Views/_ViewImports.cshtml
    1 @using ASPNET6_Template
    2 @using ASPNET6_Template.Models
    3 @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
    4 
ASPNET6_Template/Views/_ViewStart.cshtml
    1 @{
    2     Layout = "_Layout";
    3 }
    4 
Interrogation Panel - Create and List - Razor Page.sln
    1 
    2 Microsoft Visual Studio Solution File, Format Version 12.00
    3 # Visual Studio Version 17
    4 VisualStudioVersion = 17.2.32616.157
    5 MinimumVisualStudioVersion = 10.0.40219.1
    6 Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ASPNET6_Template", "ASPNET6_Template\ASPNET6_Template.csproj", "{5048CDDC-C8D1-4FF8-A83F-CC4E70683E2C}"
    7 EndProject
    8 Global
    9 	GlobalSection(SolutionConfigurationPlatforms) = preSolution
   10 		Debug|Any CPU = Debug|Any CPU
   11 		Release|Any CPU = Release|Any CPU
   12 	EndGlobalSection
   13 	GlobalSection(ProjectConfigurationPlatforms) = postSolution
   14 		{5048CDDC-C8D1-4FF8-A83F-CC4E70683E2C}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
   15 		{5048CDDC-C8D1-4FF8-A83F-CC4E70683E2C}.Debug|Any CPU.Build.0 = Debug|Any CPU
   16 		{5048CDDC-C8D1-4FF8-A83F-CC4E70683E2C}.Release|Any CPU.ActiveCfg = Release|Any CPU
   17 		{5048CDDC-C8D1-4FF8-A83F-CC4E70683E2C}.Release|Any CPU.Build.0 = Release|Any CPU
   18 	EndGlobalSection
   19 	GlobalSection(SolutionProperties) = preSolution
   20 		HideSolutionNode = FALSE
   21 	EndGlobalSection
   22 	GlobalSection(ExtensibilityGlobals) = postSolution
   23 		SolutionGuid = {55B08CE3-D946-42A1-A50D-E0389EFBE736}
   24 	EndGlobalSection
   25 EndGlobal
   26 	 	  	  		    	  	   	    	 	
   27 
      Lab4:
      *******
      ASPNET6_Template/appsettings.Development.json
    1 {
    2   "Logging": {
    3     "LogLevel": {
    4       "Default": "Information",
    5       "Microsoft.AspNetCore": "Warning"
    6     }
    7   }
    8 }
    9 
ASPNET6_Template/appsettings.json
    1 {
    2   "Logging": {
    3     "LogLevel": {
    4       "Default": "Information",
    5       "Microsoft.AspNetCore": "Warning"
    6     }
    7   },
    8   "AllowedHosts": "*",
    9   "ConnectionStrings": {
   10     "DBConnection": "server=(LocalDB)\\MSSQLLocalDB;database=DataDB;Trusted_Connection=true;"
   11   }
   12 }
   13 
ASPNET6_Template/ASPNET6_Template.csproj
    1 <Project Sdk="Microsoft.NET.Sdk.Web">
    2 
    3   <PropertyGroup>
    4     <TargetFramework>net6.0</TargetFramework>
    5     <Nullable>enable</Nullable>
    6     <ImplicitUsings>enable</ImplicitUsings>
    7   </PropertyGroup>
    8 
    9 	<ItemGroup>
   10 		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.5" />
   11 		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.5" />
   12 		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.5">
   13 			<PrivateAssets>all</PrivateAssets>
   14 			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
   15 		</PackageReference>
   16 	</ItemGroup>
   17 
   18 </Project>
   19 
ASPNET6_Template/Data/DbIntializer.cs
    1 using ASPNET6_Template.Models;
    2 using Microsoft.EntityFrameworkCore;
    3 
    4 namespace ASPNET6_Template.Data
    5 {
    6     public class DbIntializer : IDbIntializer
    7     {
    8         // Implement code here - Inject QuizDbContext
    9         
   10         // public void Initialize()
   11         // {
   12         //   // Implement code here - Default data insertion logics
   13         // }
   14         
   15         private readonly InterrogationDbContext _context;
   16 
   17         public DbIntializer(InterrogationDbContext context)
   18         {
   19             _context = context;
   20         }
   21         public void Initialize()
   22         {
   23             Console.WriteLine("---------11--------------");
   24             try
   25             {
   26                 if (_context.Database.GetPendingMigrations().Count() > 0)
   27                 {
   28                     _context.Database.Migrate();
   29                 }
   30                 Console.WriteLine("---------12--------------");
   31             }
   32             catch (Exception ex) { }
   33 Console.WriteLine("---------13--------------");
   34             if (!_context.Problems.Any())
   35             {
   36                 Console.WriteLine("---------14--------------");
   37                 var questions = new List<Problem>()
   38                 {	 	  	  		    	  	   	    	 	
   39                         new Problem()
   40                         {
   41                             InterrogatorsProblem = "The first metal used by the man was",
   42                             Choice1 = "Iron",
   43                             Choice2 = "Copper",
   44                             Choice3 = "Aluminium",
   45                             Choice4 = "Gold",
   46                             Solution ="Copper"
   47                         },
   48                         new Problem()
   49                         {
   50                             InterrogatorsProblem = "Which of the following is a balanced fertiliser for plants ?",
   51                             Choice1 = "Urea",
   52                             Choice2 = "Ammonia sulphate",
   53                             Choice3 = "Nitrates",
   54                             Choice4 = "Compost",
   55                             Solution ="Compost"
   56                         }
   57                 };
   58 Console.WriteLine("---------15--------------");
   59                 _context.Problems.AddRange(questions);
   60                 _context.SaveChanges();
   61 Console.WriteLine("---------16--------------");
   62             }
   63         }
   64         
   65     }
   66 }
   67 	 	  	  		    	  	   	    	 	
   68 
ASPNET6_Template/Data/IDbIntializer.cs
    1 namespace ASPNET6_Template.Data
    2 {
    3     public interface IDbIntializer
    4     {
    5         void Initialize();
    6     }
    7 }
    8 
ASPNET6_Template/Data/InterrogationDbContext.cs
    1 using ASPNET6_Template.Models;
    2 using Microsoft.EntityFrameworkCore;
    3 
    4 namespace ASPNET6_Template.Data
    5 {
    6     public class InterrogationDbContext : DbContext
    7     {
    8         public InterrogationDbContext(DbContextOptions<InterrogationDbContext> options) : base(options)
    9         {
   10 
   11         }
   12 
   13         public DbSet<Problem> Problems { set; get; }
   14     }
   15 }
   16 
ASPNET6_Template/Models/ErrorViewModel.cs
    1 namespace ASPNET6_Template.Models
    2 {
    3     public class ErrorViewModel
    4     {
    5         public string? RequestId { get; set; }
    6 
    7         public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    8     }
    9 }
ASPNET6_Template/Models/Problem.cs
    1 using System.ComponentModel.DataAnnotations;
    2 
    3 namespace ASPNET6_Template.Models
    4 {
    5     public class Problem
    6     {
    7         // Implement code here
    8         [Key]
    9         public int Id { get; set; }
   10         [Required(ErrorMessage = "Please provide Problem")]
   11         public string InterrogatorsProblem { get; set; }
   12         [Required(ErrorMessage = "Please provide Choice1")]
   13         public string Choice1 { get; set; }
   14         [Required(ErrorMessage = "Please provide Choice2")]
   15         public string Choice2 { get; set; }
   16         [Required(ErrorMessage = "Please provide Choice3")]
   17         public string Choice3 { get; set; }
   18         [Required(ErrorMessage = "Please provide Choice4")]
   19         public string Choice4 { get; set; }
   20         [Required(ErrorMessage = "Please provide Solution")]
   21         public string Solution { get; set; }
   22     }
   23 }
   24 
ASPNET6_Template/Program.cs
    1 using ASPNET6_Template.Data;
    2 using Microsoft.EntityFrameworkCore;
    3 
    4 var builder = WebApplication.CreateBuilder(args);
    5 
    6 // Add services to the container.
    7 builder.Services.AddControllersWithViews();
    8 builder.Services.AddDbContext<InterrogationDbContext>
    9     (options => options.UseSqlServer
   10     (builder.Configuration.GetConnectionString("DBConnection")));
   11 builder.Services.AddScoped<IDbIntializer, DbIntializer>();
   12 
   13 var app = builder.Build();
   14 
   15 // Configure the HTTP request pipeline.
   16 if (!app.Environment.IsDevelopment())
   17 {
   18     app.UseExceptionHandler("/Home/Error");
   19     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   20     app.UseHsts();
   21 }
   22 
   23 app.UseHttpsRedirection();
   24 app.UseStaticFiles();
   25 
   26 app.UseRouting();
   27 
   28 SeedDatabase();
   29 
   30 app.UseAuthorization();
   31 
   32 app.MapControllerRoute(
   33     name: "default",
   34     pattern: "{controller=Home}/{action=Index}/{id?}");
   35 
   36 app.Run();
   37 
   38 void SeedDatabase()
   39 {	 	  	  		    	  	   	    	 	
   40     using (var scope = app.Services.CreateScope())
   41     {
   42         var dbintializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
   43         dbintializer.Initialize();
   44     }
   45 }
ASPNET6_Template/Properties/launchSettings.json
    1 {
    2   "iisSettings": {
    3     "windowsAuthentication": false,
    4     "anonymousAuthentication": true,
    5     "iisExpress": {
    6       "applicationUrl": "http://localhost:19595",
    7       "sslPort": 44328
    8     }
    9   },
   10   "profiles": {
   11     "ASPNET6_Template": {
   12       "commandName": "Project",
   13       "dotnetRunMessages": true,
   14       "launchBrowser": true,
   15       "applicationUrl": "https://localhost:7086;http://localhost:5086",
   16       "environmentVariables": {
   17         "ASPNETCORE_ENVIRONMENT": "Development"
   18       }
   19     },
   20     "IIS Express": {
   21       "commandName": "IISExpress",
   22       "launchBrowser": true,
   23       "environmentVariables": {
   24         "ASPNETCORE_ENVIRONMENT": "Development"
   25       }
   26     }
   27   }
   28 }
   29 	 	  	  		    	  	   	    	 	
   30 
ASPNET6_Template/Views/Shared/Error.cshtml
    1 @model ErrorViewModel
    2 @{
    3     ViewData["Title"] = "Error";
    4 }
    5 
    6 <h1 class="text-danger">Error.</h1>
    7 <h2 class="text-danger">An error occurred while processing your request.</h2>
    8 
    9 @if (Model.ShowRequestId)
   10 {
   11     <p>
   12         <strong>Request ID:</strong> <code>@Model.RequestId</code>
   13     </p>
   14 }
   15 
   16 <h3>Development Mode</h3>
   17 <p>
   18     Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
   19 </p>
   20 <p>
   21     <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
   22     It can result in displaying sensitive information from exceptions to end users.
   23     For local debugging, enable the <strong>Development</strong> environment by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>
   24     and restarting the app.
   25 </p>
   26 	 	  	  		    	  	   	    	 	
   27 
ASPNET6_Template/Views/Shared/_Layout.cshtml
    1 <!DOCTYPE html>
    2 <html lang="en">
    3 <head>
    4     <meta charset="utf-8" />
    5     <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    6     <title>@ViewData["Title"] - ASPNET6_Template</title>
    7     <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    8     <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    9     <link rel="stylesheet" href="~/ASPNET6_Template.styles.css" asp-append-version="true" />
   10 </head>
   11 <body>
   12     <header>
   13         <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
   14             <div class="container-fluid">
   15                 <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">ASPNET6_Template</a>
   16                 <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
   17                         aria-expanded="false" aria-label="Toggle navigation">
   18                     <span class="navbar-toggler-icon"></span>
   19                 </button>
   20                 <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
   21                     <ul class="navbar-nav flex-grow-1">
   22                         <li class="nav-item">
   23                             <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
   24                         </li>
   25                         <li class="nav-item">
   26                             <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
   27                         </li>
   28                     </ul>
   29                 </div>
   30             </div>
   31         </nav>
   32     </header>
   33     <div class="container">
   34         <main role="main" class="pb-3">
   35             @RenderBody()
   36         </main>
   37     </div>
   38 
   39     <footer class="border-top footer text-muted">
   40         <div class="container">
   41             &copy; 2024 - ASPNET6_Template - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
   42         </div>
   43     </footer>
   44     <script src="~/lib/jquery/dist/jquery.min.js"></script>
   45     <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
   46     <script src="~/js/site.js" asp-append-version="true"></script>
   47     @await RenderSectionAsync("Scripts", required: false)
   48 </body>
   49 </html>
   50 	 	  	  		    	  	   	    	 	
   51 
ASPNET6_Template/Views/Shared/_ValidationScriptsPartial.cshtml
    1 <script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
    2 <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
    3 
ASPNET6_Template/Views/_ViewImports.cshtml
    1 @using ASPNET6_Template
    2 @using ASPNET6_Template.Models
    3 @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
    4 
ASPNET6_Template/Views/_ViewStart.cshtml
    1 @{
    2     Layout = "_Layout";
    3 }
    4 
Interrogation Panel - Data Seed.sln
    1 
    2 Microsoft Visual Studio Solution File, Format Version 12.00
    3 # Visual Studio Version 17
    4 VisualStudioVersion = 17.2.32616.157
    5 MinimumVisualStudioVersion = 10.0.40219.1
    6 Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ASPNET6_Template", "ASPNET6_Template\ASPNET6_Template.csproj", "{C56ED2A5-8C81-4751-9FBC-32C17861409C}"
    7 EndProject
    8 Global
    9 	GlobalSection(SolutionConfigurationPlatforms) = preSolution
   10 		Debug|Any CPU = Debug|Any CPU
   11 		Release|Any CPU = Release|Any CPU
   12 	EndGlobalSection
   13 	GlobalSection(ProjectConfigurationPlatforms) = postSolution
   14 		{C56ED2A5-8C81-4751-9FBC-32C17861409C}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
   15 		{C56ED2A5-8C81-4751-9FBC-32C17861409C}.Debug|Any CPU.Build.0 = Debug|Any CPU
   16 		{C56ED2A5-8C81-4751-9FBC-32C17861409C}.Release|Any CPU.ActiveCfg = Release|Any CPU
   17 		{C56ED2A5-8C81-4751-9FBC-32C17861409C}.Release|Any CPU.Build.0 = Release|Any CPU
   18 	EndGlobalSection
   19 	GlobalSection(SolutionProperties) = preSolution
   20 		HideSolutionNode = FALSE
   21 	EndGlobalSection
   22 	GlobalSection(ExtensibilityGlobals) = postSolution
   23 		SolutionGuid = {2FA85765-CD99-43AD-B0C3-FC275B503932}
   24 	EndGlobalSection
   25 EndGlobal
   26 	 	  	  		    	  	   	    	 	
   27 
