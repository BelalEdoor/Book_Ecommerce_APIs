using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOOKSTORE
{
    [Table("book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string BookName { get; set; } = string.Empty;

        [Required]
        [MaxLength(40)]
        public string AuthorName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? Image { get; set; }

        [ForeignKey(nameof(Genre))]   // ✨ هنا التعديل
        [Column("GenreId")]           // ✨ وهنا كمان
        public int GenreId { get; set; }

        public Genre? Genre { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new();
        public List<CartDetails> CartDetails { get; set; } = new();

        public Stock? Stock { get; set; }

        [NotMapped]
        public string? GenreName { get; set; }

        public int Quantity { get; set; }
    }
}
