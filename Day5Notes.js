<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Document</title>
</head>

<body>
    <p>click the button to display todays day</p>
    <button onclick="myfunction()">Get todays day</button>
    <p id="demo"></p>
    <script>
        function myfunction() {
            var x;
            var d = new Date().getDay();
            switch (d) {
                case 0:
                    x = "Today is sunday";
                    break;
                case 1:
                    x = "Today is Monday";
                    break;
                case 2:
                    x = "Today is Tuesday";
                    break;
                case 3:
                    x = "Today is wednesday";
                    break;
                case 4:
                    x = "Today is Thursday";
                    break;
                case 5:
                    x = "Today is Friday";
                    break;
                case 6:
                    x = "Today is Saturday";
                    break;
            }
            document.getElementById("demo").innerHTML=x;
        }
    </script>
</body>




 Table 1: Appointment Time Fixing Rules (If Time Preference Given)
---------------------------------------------------------------
Current Time (24hr clock).....Current Time (24hr clock).....Time Preference.....Appointment Time
----------------------------- ----------------------------- ----------------- ---------------------
> 8 .......................... < 12 ....................... Morning ........... Current hour
> 8 .......................... < 12 ....................... Evening ........... Current hour + 7
> 15 ......................... < 18 ....................... Evening ........... Current hour
> 15 ......................... < 18 ....................... Morning ........... 10:00 AM next day


Table 2: Appointment Time Fixing Rules (If No Time Preference Given)
--------------------------------------------------------------------
Patient Age ............ Time Preference .......... Appointment Time
----------------------- ------------------------ -------------------
< 50 ................... No Preference .......... 6:30 PM
> 50 ................... No Preference .......... 12 Noon


	            1. Verify if all inputs are available else do not fix the appointment and set appstatus(appointment status) as
	"Please enter all data to fix the appointment"
	            2. if all data is available then Verify the phone number (pmobile) for having 10 numerical digits else set appstatus as "please enter valid phone number to fix appointment"
	            3. If phone number is valid then get the current system hour 
	            4. Fix the appointment based on the following rules
	                    if the current hour of day is more than 8 Am and less than 12 Noon and if patient preferred time is
 Morning then the appointment time is current hour itself 
	                    set the patime(patient appointment time) to current hour. 
	                    ex: if the current hour is 9 AM then the patime = 9:00 AM
	                    Note: Use the getHours() javascript function to get the current hour
	                    
	                    follow the  table given in the description for the remaining rules.
                5. Once the appointment is fixed set the appstatus as below
                                pname+" your appointment is confirmed on "+padate+" for the speciality "+pspeciality+" at "+patime;
                6. Return the constructued appointment status (appstatus)
	    
	
 index.html
 ------------
 <html>
<head>

<style>
h1
{
color:#000000;
font-family:verdana;
text-align:center;
}

table{ 
border-collapse:separate; 
border-spacing: 0 15px; 
} 
th{ 
background-color: #E9EBE2; 
color: white; 
} 
th,td{ 
width: 150px; 
text-align: center; 
border: 1px solid black; 
padding: 5px;
}

tr:hover {background-color: #a1a1a1;}

body
{
   background-color:#E9EBE2;
}


</style>

<script src="script.js" lang="text/javascript"></script>

</head>

<body>

<center>
<h1>ONLINE DOCTOR APPOINTMENT</h1>

<table>
<tr>
<td>Patient Name
<td><input type="text" name="patientname" id="patientname" required  placeholder="Enter the patient name">
</tr>

<tr>
<td>Patient Age
<td><input type="text" name="patientage" id="patientage" required  placeholder="Enter the patient age">
</tr>

<tr>
   <td>Mobile Number</td>
   <td><input type="text" name="mobile" id="mobile" required  placeholder="Enter the patient mobile number"></td>
</tr>

<tr>
<td>Appointment Date
<td><input type="date" name="appointmentdate" id="appointmentdate" required>
</tr>

<tr>
<td>Speciality
<td>
<select name="speciality" id="speciality" required>
  <option value="General">General</option>
  <option value="Pediatrics">Pediatrics</option>
  <option value="ENT">ENT</option>
  <option value="Neurology">Neurology</option>
  <option value="Gastrology">Gastrology</option>
</select>C:\Users\2421065\OneDrive - Cognizant\Desktop\day5demos\demo2.html
</tr>

<tr>
<td>Preffered Time
<td>
<select name="prefferedtime" id="prefferedtime" required>
  <option value="Morning">Morning</option>
  <option value="Evening">Evening</option>
  <option value="No Preference">No Preference</option>
</select>
</tr>

<tr>
<td><input type="submit" name="submit" value="Submit" onclick="Booking()">
</tr>

</table>

<div id="result">
</div>

</center>
</body>
</html>

script.js
----------
function fixappointment(pname, page, pmobile, pdate, pspeciality, ptime)
{

    var appstatus;
    var patime;
    var date = new Date();
    var currHour = date.getHours();

    if (pname === "" || page === "" || pmobile === "" || pdate === "",
        pspeciality === "" || ptime === "")
    {
      appstatus="please enter all data to fix the appointment"
    }
  else  if (pmobile, length != 10 || isNaN(pmobile))
    {
        appstatus = "please enter valid phone no to fix appointment";
    }
    else
    {
        if (ptime === "No Preference" && page < 50)
        {
            patime="6:30 PM"
        }  
        else if (ptime === "No Preference" && page > 50)
        {
            patime="12 Noon"
        }
        else if (ptime == "Morning")
        {
            if (currHour > 8 && currHour < 12)
            {
                patime = currHour + ".00AM";
            }
            else if (currHour > 13 && currHour < 18)
            {
                patime="10.00 AM next day "
            }
        }
        else if (ptime = "Evening")
        {
            alert(currHour);
            if (currHour > 8 && currHour < 12) {
                patime = currHour + 7 + ".00AM";
            }
            else if (currHour > 13 && currHour < 18) {
                patime = currHour + ".00PM";
                }
        }

        appstatus = pname + "your appointment is confirmed on " + pdate + "for specility" + pspeciality
            + "at " + ptime;
    }

    return appstatus;
}

function Booking()
{
    pname = document.getElementById('patientname').value;
    page = document.getElementById('patientage').value;
    pmobile = document.getElementById('mobile').value;
    pdate = document.getElementById('appointmentdate').value;
    pspeciality = document.getElementById('speciality').value;
    ptime = document.getElementById("prefferedtime").value;

    appstatus = fixappointment(pname, page, pmobile, pdate, pspeciality, ptime);
    document.getElementById("result").innerHTML = appstatus;
    }
    
    
  Bootstrap
  ------------
  BOOTSTRAP 
 ------------
 Bootstrap is a free front-end framework for faster and easier web development 
Bootstrap includes HTML and CSS based design templates for typography, forms, buttons, tables, navigation, modals, image 
carousels and many other, as well as optional JavaScript plugins
Bootstrap also gives you the ability to easily create responsive designs

go to getbootstrap.com right side corner go for 5.0.3 version of bootstrap select and  left menu third one download is there 
and first downlod u click unzip the folder 
and finally open the folder in vscode 

add one file index.html in the downloaded fodler having css and js folder 
and code is below 


For designing first learn bootstrap from this link 

https://www.w3schools.com/bootstrap5/


and after learning go to this link 

https://getbootstrap.com/docs/5.0/getting-started/introduction/

and in this in search type what u want like forms ,dropdowns etc 

some code will come try to analize it and replace that code with  your desing code which u need it 


bootswatch.com is another site for taking code into desing 

index.html
-----------

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
</head>

<body>
    <div id="row1" class="row">
        <div class="col-lg-6 col-md-6 col-sm-6" style="background-color:yellow;">
            <span>row1 col1</span>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6" style="background-color:fuchsia ;">
            <span>row1 col2 </span>
        </div>
    </div>
    <div id="row2" class="row">
        <div class="col-lg-2 col-md-8 col-sm-6" style="background-color: aqua;">
            <span>row2col1</span>
        </div>
        <div class="col-lg-8 col-md-2 col-sm-1" style="background-color: rgb(0, 255, 13);">
            <span>row2col2</span>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-5" style="background-color: rgb(35, 171, 205);">
            <span>row2col3</span>
        </div>



    </div>

    <div class="row" id="row3">

         <div class="col-lg-6 col-md-6 col-sm-6"
          style="background-color: orange;">
         <span>row3 col1</span>
         </div>

        <div class="col-lg-6 col-md-6 col-sm-6"
         style="background-color: darkcyan;">
        <span>row3 col2</span>
          <div id="row4" class="row">
            <div class="col-lg-8 col-md-8 col-sm-8" style="background-color:chocolate;">
                 <span>row4col1</span>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-4" style="background-color:green;">
                <span>row4col2</span>
                <div id="row5" class="row">
                    <div class="col-lg-10" style="background-color: cornflowerblue;">
                        <span>row5col1</span>
                    </div>
                </div>
            </div>
          </div>
        </div>




    </div>
    <div id="row6" class="row">
        <div class="col-sm-6 col-md-4 col-lg-2" style="background-color:rgba(183, 215, 25, 0.505);">
            <span>Row-6 Col 1</span>
        </div>
        <div class="col-sm-6 col-md-4 offset-md-4 col-lg-2 offset-lg-8" style="background-color:rgb(26, 169, 26);">
            <span>Row-6 Col 2</span>
        </div>
    </div>
    
    
</body>


</html>


when  u waant to use CDN 
--------------------------
index2.html
--------------
	<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM"
        crossorigin="anonymous"></script>
</head>
<body>

    <button type="button" class="btn btn-primary">Primary</button>
    <button type="button" class="btn btn-secondary">Secondary</button>
    <button type="button" class="btn btn-success">Success</button>
    <button type="button" class="btn btn-danger">Danger</button>
    <button type="button" class="btn btn-warning">Warning</button>
    <button type="button" class="btn btn-info">Info</button>
    <button type="button" class="btn btn-light">Light</button>
    <button type="button" class="btn btn-dark">Dark</button>

    <hr/>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Navbar</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                    <li class="nav-item">
                        <a class="nav-link active" aria-current="page" href="#">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">Link</a>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button"
                            data-bs-toggle="dropdown" aria-expanded="false">
                            Dropdown
                        </a>
                        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <li><a class="dropdown-item" href="#">Action</a></li>
                            <li><a class="dropdown-item" href="#">Another action</a></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li><a class="dropdown-item" href="#">Something else here</a></li>
                        </ul>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link disabled" href="#" tabindex="-1" aria-disabled="true">Disabled</a>
                    </li>
                </ul>
                <form class="d-flex">
                    <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                    <button class="btn btn-outline-success" type="submit">Search</button>
                </form>
            </div>
        </div></nav>
    
    <button type="button" class="btn btn-link">Link</button>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"
        integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js"
        integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF"
        crossorigin="anonymous"></script>
</body>
</html>


when u want to se bootswatch.com

index3.html
------------
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link href="../css/quartz.css" rel="stylesheet" />
</head>
<body>
    <div class="row">
        <div class="col-lg-3">
            <div class="card-header">Header</div>
            <div class="card-body">
                <h4 class="card-title">Primary card title</h4>
                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.
                </p>
            </div>    </div>
       
        <div class="col-lg-6">
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th scope="col">Type</th>
                        <th scope="col">Column heading</th>
                        <th scope="col">Column heading</th>
                        <th scope="col">Column heading</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="table-active">
                        <th scope="row">Active</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr>
                        <th scope="row">Default</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-primary">
                        <th scope="row">Primary</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-secondary">
                        <th scope="row">Secondary</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-success">
                        <th scope="row">Success</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-danger">
                        <th scope="row">Danger</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-warning">
                        <th scope="row">Warning</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-info">
                        <th scope="row">Info</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-light">
                        <th scope="row">Light</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                    <tr class="table-dark">
                        <th scope="row">Dark</th>
                        <td>Column content</td>
                        <td>Column content</td>
                        <td>Column content</td>
                    </tr>
                </tbody>      </table>
        </div>
        <div class="col-lg-3">
            <div class="row">
                <label for="staticEmail" class="col-sm-2 col-form-label">Email</label>
                <div class="col-sm-10">
                    <input type="text" readonly="" class="form-control-plaintext" id="staticEmail" value="email@example.com">
                </div>
            </div>
            <div>
                <label for="exampleInputEmail1" class="form-label mt-4">Email address</label>
                <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp"
                    placeholder="Enter email">
                <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
            </div>
            <div>
                <label for="exampleInputPassword1" class="form-label mt-4">Password</label>
                <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password" autocomplete="off">    </div>
        </div>
    </div>
</div>
</body>
</html>

i want to generate the template which i have developed in html css using bootstrap 
including register.html page so for this 
I am giving both the codes to chatgpt and i am getting the code what happens 
is that all css cannot be replaced some css it will keep and manily layouts are altered 
by using bootstrap so you have to anilize which Which style is taken by css and 
which is taken by bootsrap for that u have to go through the documentation of 
bootstrap or if u dont have time ask chat gpt only explain the code which it 
has genrated to explain line by line 
index4.html 
-------------
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Template created by Raghu</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        header {
            background-color: #983030;
            color: white;
            padding: 40px 0;
        }

        nav ul {
            background: orange;
            padding-left: 0;
            margin-bottom: 0;
        }

        nav li {
            list-style: none;
            padding: 10px;
            text-align: center;
            cursor: pointer;
        }

        nav li:hover {
            background: lightpink;
        }

        article {
            margin-bottom: 20px;
            border-bottom: 1px solid black;
        }

        article h1 {
            font-size: 25px;
            background: #e0e0e0;
            padding: 10px;
        }

        #meta {
            color: gray;
            margin: 10px 0;
            font-size: 15px;
        }

        .latest-posts {
            height: 30px;
            background: #983030;
            margin: 10px 0;
            border: 2px solid black;
            text-align: center;
            color: white;
            line-height: 30px;
        }

        footer {
            background: #983030;
            color: white;
            padding: 10px;
            text-align: center;
            margin-top: 20px;
        }

        #side-bottom input[type='email'],
        #side-bottom input[type='submit'] {
            width: 100%;
            height: 30px;
        }

        #side-bottom input[type='submit'] {
            background: #ff9900;
            border: 2px solid black;
            margin-top: 10px;
        }
    </style>
</head>

<body>
    <div class="container mt-3">
        <header class="text-center">
            <h1>The Main Heading</h1>
        </header>

        <nav class="mt-2">
            <ul class="nav justify-content-center">
                <li class="nav-item"><a class="nav-link" href="#">Home</a></li>
                <li class="nav-item"><a class="nav-link" href="#">About Us</a></li>
                <li class="nav-item"><a class="nav-link" href="#">Music</a></li>
                <li class="nav-item"><a class="nav-link" href="#">Contact Us</a></li>
                <li class="nav-item"><a class="nav-link" href="#">Blogs</a></li>
                <li class="nav-item"><a class="nav-link" href="register.html">Register</a></li>
            </ul>
        </nav>

        <div class="row mt-3">
            <!-- Left Section -->
            <div class="col-lg-8">
                <article>
                    <h1>First Post Title</h1>
                    <div id="meta"><b>20 Feb 2016 | Raghavendra Ponde</b></div>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit...</p>
                </article>
                <article>
                    <h1>First Post Title</h1>
                    <div id="meta"><b>20 Feb 2016 | Raghavendra Ponde</b></div>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit...</p>
                </article>
            </div>

            <!-- Right Sidebar -->
            <div class="col-lg-4">
                <div id="side-top">
                    <h2 class="text-center text-decoration-underline">Latest Posts</h2>
                    <div id="latest-posts-lists" class="px-3">
                        <div class="latest-posts">First Post Title</div>
                        <div class="latest-posts">First Post Title</div>
                        <div class="latest-posts">First Post Title</div>
                        <div class="latest-posts">First Post Title</div>
                    </div>
                </div>

                <div id="side-bottom" class="mt-4">
                    <h2 class="text-center text-decoration-underline">News Letter</h2>
                    <form class="px-3">
                        <input type="email" placeholder="Enter your email" class="form-control" />
                        <input type="submit" value="Subscribe" class="btn btn-warning mt-2 w-100" />
                    </form>
                </div>
            </div>
        </div>

        <footer class="mt-4">&copy; RaghuTechnologies</footer>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>

</html>	
register.html
----------------
