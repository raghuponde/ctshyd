First create two tables in the database WiproOnline 
use WiproOnline

drop table student;
create table student(studentid int primary key,studentname varchar(30));

create table course(courseid int primary key ,coursename varchar(30),duration  int,
studentid int foreign key references student(studentid));

select * from student;
select * from course;

insert code :
-------------

using System.Data.SqlClient;
namespace Ado.NetDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection(@"data source=LAPTOP-4G8BHPK9\SQLEXPRESS;initial catalog=WiproOnline;"+
"Integrated Security=true;");
        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();
            // SqlCommand cmd = new SqlCommand("insert into student values("+Convert.ToInt32(textBox1.Text)+",'"+textBox2.Text+"')", cnn);
            SqlCommand cmd = new SqlCommand("insert into student values(@studentid1,@studentname1)", cnn);
            cmd.Parameters.AddWithValue("@studentid1",Convert.ToInt32(textBox1.Text));
            cmd.Parameters.AddWithValue("@studentname1", textBox2.Text);
            int rowsAffected=cmd.ExecuteNonQuery();
            if (rowsAffected > 0) 
            {
                MessageBox.Show("Student added ");
            
            }
            else
            {
                MessageBox.Show("Student not added ");
            }
            cnn.Close();
        }
    }
}


update code 
------------
 private void button2_Click(object sender, EventArgs e)
 {
     cnn.Open();
     SqlCommand cmd = new SqlCommand("update student set studentname=@studentname1 where studentid=@studentid1", cnn);
     cmd.Parameters.AddWithValue("@studentname1", textBox2.Text);
     cmd.Parameters.AddWithValue("@studentid1",Convert.ToInt16(textBox1.Text));
     int rowsAffected=cmd.ExecuteNonQuery();
     if (rowsAffected > 0) 
     {
         MessageBox.Show("student updated ");
     
     }
     else
     {
         MessageBox.Show("student not updated");
     }
     cnn.Close();
 }
 
 
 delete operation 
 ----------------
 private void button3_Click(object sender, EventArgs e)
{
    cnn.Open();
    SqlCommand cmd = new SqlCommand("delete from student where studentid=@studentid1", cnn);
    cmd.Parameters.AddWithValue("@studentid1",Convert.ToInt32(textBox1.Text));
    int rowsAffeceted=cmd.ExecuteNonQuery();
    if (rowsAffeceted > 0)
    {
        MessageBox.Show("student deleted  ");

    }
    else
    {
        MessageBox.Show("student not deleted");
    }
    cnn.Close();
}

display and next buttons code 
------------------------------
  SqlDataReader dr;
  private void button4_Click(object sender, EventArgs e)
  {
      cnn.Open();
      SqlCommand cmd = new SqlCommand("select * from student", cnn);
       dr = cmd.ExecuteReader();
      dr.Read();
      textBox1.Text = dr[0].ToString();
      textBox2.Text = dr[1].ToString();

  }

  private void button5_Click(object sender, EventArgs e)
  {
      dr.Read();
      textBox1.Text = dr[0].ToString();
      textBox2.Text = dr[1].ToString();
  }
  
  I had not closed the connection becasue i want to get it in the next button click event 
  when  u click dislay at a time only fist row u can see here ..
  
  we are working connected architecture based on requiremnt we have to sometimes close the connection 
  and sometimes open th connection .
  
  display all button code
  ------------------------
  private void button6_Click(object sender, EventArgs e)
{
    cnn.Open();
    SqlCommand cmd = new SqlCommand("select * from student", cnn);
    dr = cmd.ExecuteReader();
    while (dr.Read())
    {
        MessageBox.Show("Studentid:" + dr[0].ToString() + "\n studentname:" + dr[1].ToString());
    }
}

double click on the form 
--------------------------
and in form load event write liek this code 
-----------------------------------------------
 private void Form1_Load(object sender, EventArgs e)
 {
     cnn.Open();
     SqlCommand cmd = new SqlCommand("select studentid from student", cnn);
      dr=cmd.ExecuteReader();
     comboBox1.Refresh();
     while (dr.Read())
     {
         this.comboBox1.Items.Add(dr[0].ToString());
     }
     cnn.Close();

 }
 
 Now double click the comobox means drop down then selected chnage event will come 
 
 write that code 
 ------------------
 
 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("select * from student where studentid="
                + Convert.ToInt16(comboBox1.Text), cnn);
            dr = cmd.ExecuteReader();
            dr.Read();
            textBox1.Text = dr[0].ToString();
            textBox2.Text=dr[1].ToString();
            cnn.Close();
        }



Now i have to jump from student form to course form means what ever the primary key of student should be there as foriegn key in course form 

so what i will do i will create a get property in student form  and i will create set proeprty in course form form 

add one button in student form which is course register 

now go to code.cs file of student means right click on the form and say view code 

 public string getstudentid
 {
     get
     {
         return textBox1.Text;
     }
 }
 
 above ia a get proeprty 
 
 and now goto form2 whihc is course there also right clik on the form and say view code and write like this anywhere on top of any button clcik event 
 
  public string setstudentid
 {
     set
     {
         textBox4.Text = value;
     }
 }



Now in button7 of studetn form write liek this code
 private void button7_Click(object sender, EventArgs e)
 {
     Form2 f2=new Form2();
     f2.setstudentid = getstudentid;
     f2.Show();
 }



Now add stored procedures to course table 



create proc insertcourse (@cid int ,@cname varchar(40),@duration int,@sid1 int)
as
begin
insert into course values(@cid,@cname,@duration,@sid1)
end

create proc updatecourse (@cid int ,@cname varchar(40),@duration int,@sid1 int)
as
begin
update  course set coursename=@cname,duration=@duration ,studentid=@sid1 where courseid=@cid;
end

create proc deletecourse(@sid1 int)
as
begin
delete from course where studentid=@sid1;
delete from student where studentid=@sid1
end

select * from course;


Take the sps and run it one by one in sql server okay 

Now come to form 2 and double clike the  insert button over there 

and write the below code 

insert button of course 
------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Ado.NetDemo1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string setstudentid
        {
            set
            {
                textBox4.Text = value;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        SqlConnection cnn = new SqlConnection(@"data source=LAPTOP-4G8BHPK9\SQLEXPRESS;initial catalog=WiproOnline;" +
"Integrated Security=true;");
        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();
            int cid = Convert.ToInt32(textBox1.Text);
            string cname = textBox2.Text;
            int duration= Convert.ToInt32(textBox3.Text);
            int studid= Convert.ToInt32(textBox4.Text);
            SqlCommand cmd = new SqlCommand("insertcourse", cnn);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cid", cid);
            cmd.Parameters.AddWithValue("@cname", cname);
            cmd.Parameters.AddWithValue("@duration",duration);
            cmd.Parameters.AddWithValue("@sid1", studid);
         int rowsAffcted=   cmd.ExecuteNonQuery();
            if(rowsAffcted > 0)
            {
                MessageBox.Show("course added ");
            }
            else
            {
                MessageBox.Show("course not added ");
            }
            cnn.Close(); 
        }
    }
}


Update button code of course 
----------------------------
   private void button2_Click(object sender, EventArgs e)
   {
       cnn.Open();
       int cid = Convert.ToInt32(textBox1.Text);
       string cname = textBox2.Text;
       int duration = Convert.ToInt32(textBox3.Text);
       int studid = Convert.ToInt32(textBox4.Text);
       SqlCommand cmd = new SqlCommand("updatecourse", cnn);
       cmd.CommandType = CommandType.StoredProcedure;
       cmd.Parameters.AddWithValue("@cid", cid);
       cmd.Parameters.AddWithValue("@cname", cname);
       cmd.Parameters.AddWithValue("@duration", duration);
       cmd.Parameters.AddWithValue("@sid1", studid);
       int rowsAffcted = cmd.ExecuteNonQuery();
       if (rowsAffcted > 0)
       {
           MessageBox.Show("course updated ");
       }
       else
       {
           MessageBox.Show("course not updated ");
       }
       cnn.Close();
   }
   
   what u do the student who has registered the course for that student only jump to course formm and u know course id so on text box1 keep the course id and modify other column details and click 
   the update button to see the chnages refer db tabels of course

delete button code of course 
----------------------------------
  private void button3_Click(object sender, EventArgs e)
  {
      cnn.Open();
     
      int studid = Convert.ToInt32(textBox4.Text);
      SqlCommand cmd = new SqlCommand("deletecourse", cnn);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@sid1", studid);
      int rowsAffcted = cmd.ExecuteNonQuery();
      if (rowsAffcted > 0)
      {
          MessageBox.Show("course deleted and cascadingly student also got deleted ");
      }
      else
      {
          MessageBox.Show("Nothing has been deleted  ");
      }
      cnn.Close();
  }
  
  here in sp both values of student and course will be deleetd and u have to just select the student id who has taken courses 
  
  
  
  if u want to delete  only course not the student also modify the delete proc like this but while selecting courseid text shoudl be given dont consider student id eventhough it is present
  
  
alter proc deletecourse(@cid1 int)
as
begin
delete from course where courseid=@cid1;
end
  
To configure an ADO.NET SQL Server connection in the App.config file of a Windows Forms or Console Application, follow these steps:

✅ Step 1: Add Connection String in App.config

Create or open your App.config file and add the following <connectionStrings> section inside the <configuration> root element:

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="MyDbConnection"
         connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>


✅ Step 2: Read the Connection String in C# Code
Now access the connection string in your C# code using ConfigurationManager.

🔷 Add reference to System.Configuration.dll if not already added.

Using statement:


using System.Configuration;
using System.Data.SqlClient;

string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    // Execute commands here
    Console.WriteLine("Connection successful!");
}

✅ Step 3: Ensure the System.Configuration Assembly is Added
If you're using .NET Framework:

Right-click on your project → Add Reference → Assemblies → Add System.Configuration.

🔍 Example App.config (Complete)

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="MyDbConnection"
         connectionString="Data Source=localhost;Initial Catalog=TestDB;Integrated Security=True"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>

There are some drawabacks in connected architecture 
-----------------------------------------------------
--->I need to be always connected to the database server I need to open and close the connection based on the requirement 

---->I can work with onl one table at  a time means i cannot work with multiple tables

---->I can move forwared direction only in data reader class or in connected architecute i cannot navifate fully ...


Dataset--->DataTables-->DataTable-->DataRows,DataColumns

DataRows-->DataRow
DataColumns-->DataColumn,DataRelation

1)DataTable=ds.Tables["emp"]

2)DataRow=dt.Rows[0];  or DataRow=ds.tables["emp"].rows[0];

3)DataColumn=ds.tables["emp"].columns["eno"]; or DataColumn=dt.Columns["empno"];


Now let us go with demo 

I will be using northwind db for this 

Now create a new project with winforms .net framework 

put one data grid by going to all winform --->choose items--->.net framework components -->data grid of winforms 


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DataSetDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"data source=LAPTOP-4G8BHPK9\SQLEXPRESS;initial catalog=NORTHWND;Integrated Security=true;");
      
        DataSet ds=new DataSet();
        SqlDataAdapter ad1;
        SqlDataAdapter ad2;

        private void Form1_Load(object sender, EventArgs e)
        {
            ad1 = new SqlDataAdapter("select * from dbo.Customers", cnn);
            ad2 = new SqlDataAdapter("select * from dbo.Products", cnn);
            ad1.Fill(ds, "cc");
            ad2.Fill(ds, "pp");
            dataGrid1.DataSource = ds;

        }
    }
}



Now i want to traverse the table forard direction and back ward direaction 

open new application of winforms of .net framework 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace navigatingTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(@"data source=LAPTOP-4G8BHPK9\SQLEXPRESS;initial catalog=WiproOnline;"+
               "Integrated Security=true;");
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;
        SqlDataAdapter ad1;
        int rn;
        private void button1_Click(object sender, EventArgs e)
        {
            rn = 0;
            dr = dt.Rows[rn];
            textBox1.Text = dr[0].ToString();
            textBox2.Text = dr[1].ToString();   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ad1 = new SqlDataAdapter("select * from student", cnn);
            ad1.Fill(ds, "ss");
           dt= ds.Tables["ss"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rn = rn + 1;
            if (rn <= dt.Rows.Count - 1)
            {
                dr=dt.Rows[rn];
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();

            }
            else
            {
                rn = rn - 1;
                MessageBox.Show("end of the record ..");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rn = rn - 1;
            if(rn>=0)
            {
                dr=dt.Rows[rn];
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
            }
            else
            {
                rn = rn + 1;
                MessageBox.Show("Begning  of the record ..");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rn=dt.Rows.Count - 1;
            dr=dt.Rows[rn];
            textBox1.Text=dr[0].ToString(); 
            textBox2.Text=dr[1].ToString();
        }
    }
}
