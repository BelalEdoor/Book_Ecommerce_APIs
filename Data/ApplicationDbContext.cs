using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<CartDetails> CartDetails { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderDetails> OrderDetails { get; set; } = default!;
        public DbSet<OrderStatus> OrderStatus { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = default!;
        public DbSet<Stock> Stocks { get; set; } = default!;
    }
}
