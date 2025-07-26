
SearchCustomer 
---------------

In db :
-----------
select CustomerID,ContactTitle,CompanyName from [dbo].[Customers] where 
ContactName='Yang Wang'


in front end 
---------------

public IActionResult SearchCustomer(string contactname)
{
    var searchcustomers = from cust in cnt.Customers
                          where
                        cust.ContactName == contactname
                          select new Customer
                          {
                              CustomerId = cust.CustomerId,
                              ContactTitle = cust.ContactTitle,
                              CompanyName = cust.CompanyName
                          };

    var query1 = searchcustomers.Single();

    return View(query1);
}

view 
--------
@model EntityFrameWorkDemo1.Models.Customer

@{
    ViewData["Title"] = "SearchCustomer";
}

<h1>SearchCustomer</h1>

<div>
    <h4>Customer</h4>
    <hr />
    <dl class="row">
        <dt class="col-sm-2">
            @Html.DisplayNameFor(model => model.CustomerId)
        </dt>
        <dd class="col-sm-10">
            @Html.DisplayFor(model => model.CustomerId)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.CompanyName)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.CompanyName)
        </dd>
        
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.ContactTitle)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.ContactTitle)
        </dd>
        
    </dl>
</div>
<div>
    <a asp-action="Edit" asp-route-id="@Model?.CustomerId">Edit</a> |
    <a asp-action="Index">Back to List</a>
</div>



In db :
----------
select * from categories 
select * from products

select p1.ProductName ,c1.CategoryName from Products p1 join 
Categories c1 on p1.CategoryID=c1.CategoryID where c1.CategoryName='Beverages'


In front end using ling 

------------------------
add one class Prodcat in Models folder it should like this which will take one column from prodcut one column from categoroy 
namespace EntityFrameWorkDemo1.Models
{
    public class ProdCat
    {
        public string prodname { set; get; }
        public string catname { set; get; }
    }
}

then write this action method as belwo 

earlie theere was one model for a view now multiple models are inolved for a sinle view and i am doing join operation also 

go to Product Table in models and add this navugation proeprty to it if it is not theer 

 public virtual Category? Category { get; set; }
 
 

 public IActionResult ProductsInCategory(string categoryname)
 {
     var productsincategory = from prod in cnt.Products
                              where
                            prod.Category.CategoryName == categoryname
                              select new ProdCat
                              {
                                  prodname= prod.ProductName,
                                  catname = prod.Category.CategoryName
                              };

     return View(productsincategory);
 }


view 
------
@model IEnumerable<EntityFrameWorkDemo1.Models.ProdCat>

@{

    ViewData["Title"] = "ProductsInCategory";
}

<h1>ProductsInCategory</h1>


    <div class="row">
        <div class="col-lg-9">
            <h3>Products In Category:</h3>
        </div>
        <form method="get" action="/NorthWind/ProductsInCategory">
            <!--not jumping to post in this page only display -->
            <div class="col-lg-2">
                <input type="text" id="Category" name="categoryname" class="form-control " />
            </div>
            <div class="col-lg-1">
                <input type="submit" id="searchproducts" class="btn btn-primary" value="search" />
            </div>
        </form>
    </div>

@if (Model.Count() == 0)
{
    <table>
        <tr>
            <td></td>
        </tr>
    </table>

}
else
{
    <div class="row">


        <div class="col-lg-12">

            <table id="prodlist" width="80%" cellpadding="7" cellspacing="7" class="table-striped table-hover">

                <thead>
                    <tr>
                        <th style="padding:15px">ProdName</th>
                        <th style="padding:15px">Category</th>

                    </tr>

                </thead>
                <tbody>
                    @foreach (var prod in Model)
                    {


                        <tr>
                            <td style="padding:15px">@prod.prodname</td>
                            <td style="padding:15px">@prod.catname</td>


                        </tr>


                    }
                </tbody>
            </table>

        </div>
    </div>
}

-- give me all the customers who have orders count more than 10 

select CustomerID, count(OrderID)  from Orders group by CustomerID having count(OrderID) > 10


first add one class in Models folder 

namespace EntityFrameWorkDemo1.Models
{
    public class CustomerRange
    {
        public string CustomerId { get; set; }
        public string ContactName { get; set; } = string.Empty;

        public int? orderscount { get; set; }
    }
}

check in product class of model do u have collection navigation proeprty for orders if not add it or leave it if it is there 

 public virtual ICollection<Order> Orders { get; set; }
 

and action method is like this using that above model 

 public IActionResult OrderRange(string range)
 {
     int range1 = Convert.ToInt32(range);
     var custorderscount = from cust in cnt.Customers
                           where cust.Orders.Count > range1
                           select new CustomerRange
                           {
                               CustomerId = cust.CustomerId,
                               ContactName = cust.ContactName,
                               orderscount=cust.Orders.Count,
                               
                           };

     return View(custorderscount);
 }
 
 view 
 ----
 @model IEnumerable<EntityFrameWorkDemo1.Models.CustomerRange>

@{
    ViewData["Title"] = "OrderRange";
}
<div class="row">
    <div class="col-lg-9">
        <h3>Find Customers more than :</h3>
    </div>
    <form method="get" action="/NorthWind/OrderRange">
        <div class="col-lg-2">
            <select name="range" id="orderrange">
                <option selected value="">Select Range</option>
                <option value="5">Five</option>
                <option value="10">Ten</option>
                <option value="15">Fifteen</option>
                <option value="20">Twenty</option>
            </select>
        </div>
        <div class="col-lg-1">
            <br />
            <input type="submit" id="findcustomers"
                   value="searchcustomerorders" class="btn btn-primary" />
        </div>
    </form>
</div>
@if (Model.Count() == 0)
{
    <table>
        <tr>
            <td></td>
        </tr>
    </table>
}
else
{
    <div class="row">
        <div class="col-lg-12">
            <table id="orderlist" width="80%" cellpadding="7" cellspacing="7"
                   class="table-striped table-hover">
                <thead>
                    <tr>
                        <th>Customer Id </th>
                        <th>Contact Name </th>
                        <th>No of orders </th>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var cust in Model)
                    {
                        <tr>
                            <td style="padding:15px">@cust.CustomerId </td>
                            <td style="padding:15px">@cust.ContactName</td>
                            <td style="padding:15px">@cust.orderscount</td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
    </div>

}
