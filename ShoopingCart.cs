1. Product Table

CREATE TABLE Product (
    productId INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    description TEXT,
    price DECIMAL(10, 2),
    categoryId INT,
    stockQuantity INT,
    FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
);

2. Category Table

CREATE TABLE Category (
    categoryId INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100)
);

3. Cart Table

CREATE TABLE Cart (
    cartId INT PRIMARY KEY AUTO_INCREMENT,
    userId INT,
    productId INT,
    quantity INT,
    FOREIGN KEY (userId) REFERENCES User(userId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);

4. Order Table

CREATE TABLE `Order` (
    orderId INT PRIMARY KEY AUTO_INCREMENT,
    userId INT,
    totalAmount DECIMAL(10, 2),
    orderDate DATE,
    status ENUM('PENDING', 'SHIPPED', 'DELIVERED', 'CANCELLED'),
    FOREIGN KEY (userId) REFERENCES User(userId)
);

5. Payment Table

CREATE TABLE Payment (
    paymentId INT PRIMARY KEY AUTO_INCREMENT,
    orderId INT,
    amount DECIMAL(10, 2),
    paymentStatus ENUM('PENDING', 'COMPLETED', 'FAILED'),
    paymentDate DATE,
    FOREIGN KEY (orderId) REFERENCES `Order`(orderId)
);

6. User Table

CREATE TABLE User (
    userId INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) UNIQUE,
    password VARCHAR(255),
    role ENUM('CUSTOMER', 'ADMIN'),
    email VARCHAR(100)
);

for the above tables i want classes with annotations so that i can use it in code first entity framework to generate tables  



✅ Product.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int StockQuantity { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}
✅ Category.cs

using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}
✅ Cart.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cart
{
    [Key]
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}
✅ Order.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmount { get; set; }

    public DateTime OrderDate { get; set; }

    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus Status { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    public ICollection<Payment> Payments { get; set; }
}

public enum OrderStatus
{
    PENDING,
    SHIPPED,
    DELIVERED,
    CANCELLED
}
✅ Payment.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [EnumDataType(typeof(PaymentStatus))]
    public PaymentStatus PaymentStatus { get; set; }

    public DateTime PaymentDate { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}

public enum PaymentStatus
{
    PENDING,
    COMPLETED,
    FAILED
}
✅ User.cs

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }

    [MaxLength(50)]
    public string Username { get; set; }

    [MaxLength(255)]
    public string Password { get; set; }

    [EnumDataType(typeof(UserRole))]
    public UserRole Role { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }
}

public enum UserRole
{
    CUSTOMER,
    ADMIN
}


I want context class as CartContext and give me the code also use enums in the table and use fluent api if needed 

✅ CartContext.cs

using Microsoft.EntityFrameworkCore;

public class CartContext : DbContext
{
    public CartContext(DbContextOptions<CartContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Decimal precision
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(10,2)");

        // Enums
        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Payment>()
            .Property(p => p.PaymentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        // Optional: Relationships (already handled via navigation properties)
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Carts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Payments)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Carts)
            .WithOne(c => c.Product)
            .HasForeignKey(c => c.ProductId);
    }
}


now for the above things how the tables will be generated in database using code first apprach i need the complete structure of tables now 

 1. Product Table

CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10,2) NOT NULL,
    CategoryId INT NOT NULL,
    StockQuantity INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
✅ 2. Category Table
sql
Copy
Edit
CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
✅ 3. Cart Table
sql
Copy
Edit
CREATE TABLE Cart (
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);
✅ 4. Order Table
sql
Copy
Edit
CREATE TABLE [Order] (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    OrderDate DATETIME2 NOT NULL,
    Status NVARCHAR(24) NOT NULL,  -- Enum stored as string: 'PENDING', 'SHIPPED', etc.
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
✅ 5. Payment Table
sql
Copy
Edit
CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentStatus NVARCHAR(24) NOT NULL,  -- Enum stored as string
    PaymentDate DATETIME2 NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES [Order](OrderId)
);
✅ 6. User Table
sql
Copy
Edit
CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(24) NOT NULL,  -- Enum stored as string: 'CUSTOMER', 'ADMIN'
    Email NVARCHAR(100) NOT NULL
);
🔁 Summary of Table Relationships
Product ➝ belongs to Category

Cart ➝ has ProductId and UserId (M:N simulation via quantity)

Order ➝ belongs to User

Payment ➝ belongs to Order

User ➝ has many Carts and Orders


3.1 Product Management Module

Purpose: Manages product listings, categories, and inventory.

• Controller:
    - ProductController
        • addProduct(productData)
        • updateProduct(productId, productData)
        • getProductDetails(productId)
        • deleteProduct(productId)
        • getAllProducts()

• Service:
    - ProductService
        • Handles CRUD operations for products.

• Model:
    - Entity: Product
        • Attributes:
            ◦ productId (PK)
            ◦ name (VARCHAR)
            ◦ description (TEXT)
            ◦ price (DECIMAL)
            ◦ categoryId (FK)
            ◦ stockQuantity (INT)

for this i want web api controller code and also need service also which will be used in controller of web api
  I want it in the form of interface where All methods will go and service class should implement that
  interface  and in ProductController i shoudl use it

✅ 1. IProductService.cs (Interface)

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductDetailsAsync(int productId);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(int productId, Product product);
    Task<bool> DeleteProductAsync(int productId);
}
✅ 2. ProductService.cs (Service Implementation)

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly CartContext _context;

    public ProductService(CartContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product> GetProductDetailsAsync(int productId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(int productId, Product updatedProduct)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return null;

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.CategoryId = updatedProduct.CategoryId;
        product.StockQuantity = updatedProduct.StockQuantity;

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
✅ 3. ProductController.cs (Web API Controller)

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<Product>> GetProductDetails(int productId)
    {
        var product = await _productService.GetProductDetailsAsync(productId);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        var newProduct = await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductDetails), new { productId = newProduct.ProductId }, newProduct);
    }

    [HttpPut("{productId}")]
    public async Task<ActionResult<Product>> UpdateProduct(int productId, Product product)
    {
        var updated = await _productService.UpdateProductAsync(productId, product);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _productService.DeleteProductAsync(productId);
        if (!result) return NotFound();
        return NoContent();
    }
}

