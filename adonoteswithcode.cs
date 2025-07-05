In organiztions,bussiness applications need to manage voluminious data .Data is stored in a related database in the form of related tables.retriving and manipulating data directly from database requires knowledge of database commands so overcome this drawback and we need to provide an friendly interface where user can just click the button and everything gets done automatically so we require a technology which is called ado.net means activex data objects .net so it means it is a part of .NET framework architecture .It is a model used by .net applications to communicate with the database for retriving ,accessing and updating data.so ado.net is an interface between client applications and datasources.

A client application can be windows application ,web application or other client applications such as office 
and data sources can be database ,text files ,xml files and can be webserver also holding some other database or other related information.This model is designed in such a way that a developer can access and write to wide variety of data sources such as Microsoft sql server and xml.By using ADO.NET data can be retrived from one data source and and saved in another.For example data can be retrived from microsoft excel and then saved in xml document.


The data residing in a database is retrived through data provider.The data provider is set of components including
the connection,coomand,datareader and dataadapter objects.It provides data to application and updates database
with changes made in application.

An application can access data either through dataset or through datareader object.
The two key componenets of ado.net object model is 

•	Data Provider
•	Dataset


Providers are communicators between front end and back end.providers are implemented using C++ functions.

we have managed providers and unmanaged providers.Managed providers improves application performance.

How many providers are there we can know from UDL: file.

open one notepad and save it as anyname.udl file and then again open that file and check providers over there 
by selecting the appropriate provider u click to the next file for example i have selected native client provider 
and told next and then data source name as RAGHU-PC  which my sql server 2005 server name when i log in sql server 2005 software through sql server management studio and as sql server authentication is there for my sql server username is sa and password is sql2005 and intial catalog indicates database name and just fulfill these details and say test connection and then u open the file u can see the corresponding providers and these information will help u to establish connection with the database through providers 
and information will look like this something when u open it okay ...

[oledb]
; Everything after this line is an OLE DB initstring
Provider=SQLNCLI.1;Persist Security Info=False;User ID=sa;Initial Catalog=AdventureWorks;Data Source=RAGHU-PC

so like this we can check the providers present in our systems .


ADO.NET having four namespaces...

1.System.data.Oledb: Using this namespace we can integrate with various types of databases using oledb providers.

2. System.Data.odbc:Using this namespace we can integrate with various types of database using odbc drivers.

3.System.data.Sqlclient:Using this namespace we can integrate with only sql server database.without referrring provider name.

4.System.data.Oracleclient: Using this namespace we can integrate with oracle database 


ado.net classes :
-----------------
1)sqlconnection :This class is used to establish connection between front end and back end it has got two methods 
one is open() and other is close() as we are working in connection oreiented architecture of the database we need these methods using open() method we can open already established connection and using close() method we can anyhow close the connection.
so this is for the namespace using System.Data.sqlclient ;
and if u are using System.Data.Oledb then the class will be oledbconnection class only prefix changes as we are going to do maximum programs in sql server i am using in built provider of sql server 2005 so i am using sql connection class only ...and all terms will come like this only 


2)sqlcommand class : This class is used to send sql statements to back end using this class we can execute all sql statements 
it has got 3 methods 

1)executereader () :This method is used to execute select statement this method gives collection of rows and columns and this method returns sqldatareader class so we have to catch the value in sqldatareader object 

2)executescalar( ): This method executes select statement and returns only one row from  the backend .

3)executenonquery () : This method is used to execute dml statements like insert,update and delete and it has used to execute  procedures,functions etc also .

so maximum we will be using executenonquery() method only here 

3)sqldatareader: Using this class we can store single table it works as forward record set only and i datareader by default the cursor is located before first record and column index starts at 0 and ends at fieldcount- 1

it has got two methods :
read() : this method is used to change the cursor postion to next record
close() : using this method we can reset datareader values after getting values from database.

so let us write our first program where i can insert,update ,delete and then select and say next also to move forward records and then i will include one combobox also for selecting values from it to display according to the ids and using this id i will jump to another form of related table and fill or do insert update and delete on child table as well

First go the database and create the following related tables like this 

use WiproOnline

drop table student;
create table student(studentid int primary key,studentname varchar(30));

create table course(courseid int primary key ,coursename varchar(30),duration  int,
studentid int foreign key references student(studentid));

select * from student;
select * from course;

insert into student values(101,'ravi')

open one windows application and design the interface like this 

----------------------------------------------combobox -----------

student id --------------------

student name :---------------------

insert | update | delete | display |next| displayall

------------------------------------------------------------------

insert button code 
-------------------

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


update button code:
------------------
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

display and next button codes 
---------------------------------
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


Update button code 
-----------------------
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

delete button code 
--------------------
delete button code 
----------------
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
  
if u want to delete  only course not the student also modify the delete proc like this but while selecting courseid text should be given dont consider student id eventhough it is present
  
  
alter proc deletecourse(@cid1 int)
as
begin
delete from course where courseid=@cid1;
end
  
  
DATASET TECHNOLOGY :
--------------------
All features in backend are present in dataset ,so if we want all the  ado.net features we can get it  from dataset.The Dataset is having collection of datatables,so dataset is connectionless database management system,Dataset is having collection of data relations,in dataset we can create dataviews, using dataset we can integrate with xml,so dataset improves the application performance ,In dataset we can generate sql statements automatically.

when using connectionless we use xml file ,so we store dataset values into xml .Many subclasses are provided in dataset.

Dataset--->DataTables-->DataTable-->DataRows,DataColumns

DataRows-->DataRow
DataColumns-->DataColumn,DataRelation

so dataset is collection of tables so which is nothing but DataTables class and in which each table is represented by DataTable class and each DataTable class is having set of rows and columns which are represnted by DataRows for set of rows to retrive and DataColumns  which will have collection of columns. 

under set of rows each row is represnted by a DataRow class and under set of columns each column is represnted by DataColumn class and DataRelation class as we cannot show relationship in rows but when column comes datarelation class comes into picture like this we are having these many classes in dataset and each one has its own use and properties defined for it .


so this is brief history of dataset architecture .

we should remeber the following commands which we will be using while connecting and retriving values from dataset technology .

1)DataTable=ds.Tables["emp"]

if u have given a dataset if u want to retrive one table into DataTable class use above formula and generally what ever u want to retrive create their classes with objects in global section class and use it which we will be seeing in the programming part.above here emp is the employee table name okay 

so in the DataTable class i can store one table from the dataset 

2)DataRow=dt.Rows[0];  or DataRow=ds.tables["emp"].rows[0];
            
so In DataRow class we can store one row from the datatable as well as from the DataSet;
as mentioned above 

3)DataColumn=ds.tables["emp"].columns["eno"]; or DataColumn=dt.Columns["empno"];

There is one class which is called DataAdapter class and it is communicator between DataSet and Back End using single dataadapter class we can store one table in dataset so using multiple dataadapter classes we can store multiple tables in dataset.

DataGrid :It is a  databound control , we can display datasource form dataset and from datatable in this datagrid control. 

   
There ar some drawabacks in connected architecture 

--->I need to be always connected to the database server I need to open and close the connection based on the requirement 

---->I can work with only one table at  a time means i cannot work with multiple tables

---->I can move forwared direction only in data reader class or in connected architecute i cannot navifate fully ...


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


