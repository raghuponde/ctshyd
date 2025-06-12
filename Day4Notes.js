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
