using BOOKSTORE;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
[Table("Genre")]
public class Genre
{
    [Key]
    [Column("GenreId")]   // ✨ هنا التعديل
    public int GenreId { get; set; }

    [Required, MaxLength(40)]
    public string GenreName { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new();
}
