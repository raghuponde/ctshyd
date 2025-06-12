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

