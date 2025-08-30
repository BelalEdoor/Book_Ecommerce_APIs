using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BOOKSTORE.Controllers
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<CartDetails> CartDetailss { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderDetails> OrderDetailss { get; set; } = default!;
        public DbSet<OrderStatus> OrderStatuss { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = default!;
        public DbSet<Stock> Stocks { get; set; } = default!;
    }
}