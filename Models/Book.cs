using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOOKSTORE
{ 
[Table("book")]
public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? BookName { get; set; }

    [Required]
    [MaxLength(40)]
    public string? AuthorName { get; set; }   // ✅ تعديل الاسم

    [Required]
    public double? Price { get; set; }
    public string Image { get; set; }

    [ForeignKey("Genre")]
    [Column("GenreId")]
    public int GenreId { get; set; }

    public Genre Genre { get; set; }


    public List<OrderDetails> OrderDetails { get; set; }
    public List<CartDetails> CartDetails { get; set; }
    public Stock Stock { get; set; }

    [NotMapped]
    public string GenreName { get; set; }
    public int Quantity { get; internal set; }
}
}