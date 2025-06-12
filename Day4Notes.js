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
