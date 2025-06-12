put this Code into your file

<div id='feedback' onClick='goodbye()'>Users without Javascript see  this.</div>

updated coode 
--------------
  <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <div id='feedback' style="background-color:yellow;color:black; width:100%;height:200px;"
    onClick='goodbye()'>Users without Javascript see this.

    </div>
<script>
    document.getElementById('feedback').innerHTML="hello world";
    function goodbye()
    {
        document.getElementById('feedback').innerHTML="good bye all";
    }
</script>
</body>
</html>



put this Code into your file of body 

<p>Say what? <input id="sayThis" size=40>
<P>How many times? <select id='howMany'>
<option value=1>1</option>
<option value=5 selected>5</option>
<option value=10>10</option>
<option value=20>20</option>
</select>
  </p>
<p><button onClick='doLoop()'>Do It!</button></p>
<p><div id="results"></div></p>

updated code 
-------------
  <!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>

<body>
    <p>Say what? <input id="sayThis" size=40>
    <P>How many times? <select id='howMany'>
            <option value=1>one</option>
            <option value=5 selected>five</option>
            <option value=10>ten</option>
            <option value=20>twenty</option>
        </select>
    </p>
    <p><button onClick='doLoop()'>Do It!</button></p>
    <p>
    <div id="results" style="background-color:yellow;color:red;height:200px;">
        </div>
    </p>
    <script type="text/javascript">
        function doLoop()
        {
        var saywhat = document.getElementById('sayThis').value;
        var maxloop = document.getElementById('howMany').value;
        var str = "";// to store the output temperorily
        for (var i = 1; (i <= maxloop); i++) {

            str = str + i + ':' + saywhat + '<br/>';

        }
        document.getElementById('results').innerHTML = str;
    }
    </script>
</body>
</html>

JavaScript uses dialog boxes to interact with the user. The dialog boxes are created with three methods:

alert()
prompt()
confirm()
alert:
------
We saw in the last chapter that the write() and writeln() were JavaScript methods used to send output to the Web page. Another way to send output to the browser is with the alert() method. The alert() method creates a little independent box—called a dialog box—which contains a small triangle with an exclamation point. 

syntax:

alert("String of plain text");
alert(expression);

eg:
alert("Phone number is incorrect");
alert(a + b);
prompt :
-------
Since JavaScript does not provide a simple method for accepting user input , the prompt dialog box and HTML forms are used  The prompt dialog box pops up with a simple textfield box.

syntax:
prompt(message);
prompt(message, defaultText);

eg:
prompt("What is your name? ", "");
prompt("Where is your name? ", name);

The Confirm Box:
-----------------
The confirm dialog box is used to confirm a user's answer to a question. A question mark will appear in the box with an OK button and a Cancel button. If the user presses the OK button, true is returned; if he presses the Cancel button, false is returned. This method takes only one argument, the question you will ask the user.

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <script>
        var name=prompt("what is your name?","enter name");
        var age=prompt("what is your age ","age please");
        document.write("This employee with  "+name+" is "+age+"old");
        alert("This employee with  " + name + " is " + age + "old");
    </script>
    <script>
        var one=prompt("enter first number","enter 1st number");
        var two=prompt("enter second number","enter 2 number");
        var total=parseFloat(one)+parseFloat(two);
      if(confirm("do u want me to display total?")==true)
      {
        alert(total);

      }
    </script>
</body>
</html>

Basic validation to a form 
--------------------------
take this desing into the body of html

<form action="/cgi-bin/test.cgi" name="myForm"  
          onsubmit="return(validate());">
 <table cellspacing="2" cellpadding="2" border="1">
 <tr>
   <td align="right">Name</td>
   <td><input type="text" name="Name" /></td>
 </tr>
 <tr>
   <td align="right">EMail</td>
   <td><input type="text" name="EMail" /></td>
 </tr>
 <tr>
   <td align="right">Zip Code</td>
   <td><input type="text" name="Zip" /></td>
 </tr>
 <tr>
 <td align="right">Country</td>
 <td>
 <select name="Country">
   <option value="-1" selected>[choose yours]</option>
   <option value="1">USA</option>
   <option value="2">UK</option>
   <option value="3">INDIA</option>
 </select>
 </td>
 </tr>
 <tr>
   <td align="right"></td>
   <td><input type="submit" value="Submit" /></td>
 </tr>
 </table> </form>
  
udpated code using trim and ===
------------------------------------
 <!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <script type="text/javascript">
        function validate() {
            if (document.myForm.Name.value.trim() === "") {
                alert("please provide name");
                document.myForm.Name.focus();
                return false;
            }
            if (document.myForm.EMail.value.trim() === "") {
                alert("please provide email");
                document.myForm.EMail.focus();
                return false;
            }
            if (document.myForm.Zip.value.trim() === "" || isNaN(document.myForm.Zip.value.trim()) || document.myForm.Zip.value.length !=5) {
                alert("please provide proper zip code #####");
                document.myForm.Zip.focus();
                return false;
            }
            if (document.myForm.Country.value == "-1") {
                alert("please provide country");
                return false;
            }
            return (true);
        }
    </script>
</head>

<body>
    <form action="/cgi-bin/test.cgi" name="myForm" onsubmit="return(validate());">
        <table cellspacing="2" cellpadding="2" border="1">
            <tr>
                <td align="right">Name</td>
                <td><input type="text" name="Name" /></td>
            </tr>
            <tr>
                <td align="right">EMail</td>
                <td><input type="email" name="EMail" required /></td>
            </tr>
            <tr>
                <td align="right">Zip Code</td>
                <td><input type="text" name="Zip" /></td>
            </tr>
            <tr>
                <td align="right">Country</td>
                <td>
                    <select name="Country">
                        <option value="-1" selected>[choose yours]</option>
                        <option value="1">USA</option>
                        <option value="2">UK</option>
                        <option value="3">INDIA</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td align="right"></td>
                <td><input type="submit" value="Submit" /></td>
            </tr>
        </table>
    </form>
</body>

</html>

Regular Expression in Javascript :
---------------------------------------------
Till now i have done validations to a form or a form fields using normal 
  procedure like which is nothing but basic validations where we will see 
    that mandatory fields are filled but as per the requirement i want to validate the input which i pass in 
the textbox then i will go regular expressions 


regular expressions are there in .net also and also in javascript 


pan card : five chaarcters ,4 numbers and one again alphabet 

some sites to refer :

https://regexhero.net/  

to test without program 

https://regexr.com/  go through this website throghly instructions

and in google type 

regex in javascript and check some tutorials as well and 
in youtube also u can check it by putting the same 


https://www.w3schools.com/jsref/jsref_obj_regexp.asp

https://www.programiz.com/javascript/regex

in youtube :


https://www.youtube.com/watch?v=nlGF-zh0fsg

email validation code put in body of html
-------------------------
    <form>
        <table>
            <tr>
                <td>
                    <input type="text" placeholder="email" id="text" />
                </td>
                <td>
                    <div id="result">
    
                    </div>
                </td>
            </tr>
        </table>
        <br /><br />
        <button onclick="validate()" type="button">Submit</button>
    </form>


