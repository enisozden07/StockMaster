using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<StockLevel> StockLevels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<StockLevel>()
                .HasOne(sl => sl.Warehouse)
                .WithMany(w => w.StockLevels)
                .HasForeignKey(sl => sl.WarehouseId);

            modelBuilder.Entity<StockLevel>()
                .HasOne(sl => sl.Product)
                .WithMany(p => p.StockLevels)
                .HasForeignKey(sl => sl.ProductId);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderId);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Supplier)
                .WithMany(su => su.Shipments)
                .HasForeignKey(s => s.SupplierId);

            // Seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic items" },
                new Category { Id = 2, Name = "Books", Description = "Books and literature" },
                new Category { Id = 3, Name = "Clothing", Description = "Clothing items" },
                new Category { Id = 4, Name = "Home & Kitchen", Description = "Home and kitchen items" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 999.99m, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 799.99m, CategoryId = 1 },
                new Product { Id = 3, Name = "Novel", Description = "Bestselling novel", Price = 19.99m, CategoryId = 2 },
                new Product { Id = 4, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 9.99m, CategoryId = 3 },
                new Product { Id = 5, Name = "Jeans", Description = "Denim jeans", Price = 39.99m, CategoryId = 3 },
                new Product { Id = 6, Name = "Blender", Description = "High-speed blender", Price = 49.99m, CategoryId = 4 },
                new Product { Id = 7, Name = "Microwave", Description = "Compact microwave oven", Price = 89.99m, CategoryId = 4 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "123-456-7890", Address = "123 Elm Street" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Phone = "987-654-3210", Address = "456 Oak Avenue" },
                new Customer { Id = 3, Name = "Alice Johnson", Email = "alice.johnson@example.com", Phone = "555-555-5555", Address = "789 Maple Drive" },
                new Customer { Id = 4, Name = "Bob Brown", Email = "bob.brown@example.com", Phone = "111-222-3333", Address = "101 Pine Lane" }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Tech Supplies Inc.", ContactInfo = "tech.supplies@example.com" },
                new Supplier { Id = 2, Name = "Book Distributors Ltd.", ContactInfo = "book.distributors@example.com" },
                new Supplier { Id = 3, Name = "Fashion House Co.", ContactInfo = "fashion.house@example.com" },
                new Supplier { Id = 4, Name = "Home Goods Supplier", ContactInfo = "home.goods@example.com" }
            );

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { Id = 1, Location = "New York", Manager = "John Manager" },
                new Warehouse { Id = 2, Location = "Los Angeles", Manager = "Jane Manager" },
                new Warehouse { Id = 3, Location = "Chicago", Manager = "Alice Manager" },
                new Warehouse { Id = 4, Location = "Houston", Manager = "Bob Manager" }
            );

            modelBuilder.Entity<StockLevel>().HasData(
                new StockLevel { Id = 1, ProductId = 1, WarehouseId = 1, Quantity = 50 },
                new StockLevel { Id = 2, ProductId = 2, WarehouseId = 1, Quantity = 30 },
                new StockLevel { Id = 3, ProductId = 3, WarehouseId = 2, Quantity = 100 },
                new StockLevel { Id = 4, ProductId = 4, WarehouseId = 3, Quantity = 200 },
                new StockLevel { Id = 5, ProductId = 5, WarehouseId = 3, Quantity = 150 },
                new StockLevel { Id = 6, ProductId = 6, WarehouseId = 4, Quantity = 75 },
                new StockLevel { Id = 7, ProductId = 7, WarehouseId = 4, Quantity = 60 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = DateTime.Now, ShippedDate = null, CustomerId = 1 },
                new Order { Id = 2, OrderDate = DateTime.Now, ShippedDate = null, CustomerId = 2 },
                new Order { Id = 3, OrderDate = DateTime.Now, ShippedDate = null, CustomerId = 3 },
                new Order { Id = 4, OrderDate = DateTime.Now, ShippedDate = null, CustomerId = 4 }
            );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 999.99m },
                new OrderDetail { Id = 2, OrderId = 2, ProductId = 3, Quantity = 2, UnitPrice = 19.99m },
                new OrderDetail { Id = 3, OrderId = 3, ProductId = 4, Quantity = 3, UnitPrice = 9.99m },
                new OrderDetail { Id = 4, OrderId = 4, ProductId = 7, Quantity = 1, UnitPrice = 89.99m }
            );

            modelBuilder.Entity<Shipment>().HasData(
                new Shipment { Id = 1, ShipmentDate = DateTime.Now, TrackingNumber = "TRACK123", OrderId = 1, SupplierId = 1 },
                new Shipment { Id = 2, ShipmentDate = DateTime.Now, TrackingNumber = "TRACK456", OrderId = 2, SupplierId = 2 },
                new Shipment { Id = 3, ShipmentDate = DateTime.Now, TrackingNumber = "TRACK789", OrderId = 3, SupplierId = 3 },
                new Shipment { Id = 4, ShipmentDate = DateTime.Now, TrackingNumber = "TRACK012", OrderId = 4, SupplierId = 4 }
            );
        }
    }
}
