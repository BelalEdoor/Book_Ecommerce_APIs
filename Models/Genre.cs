using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOOKSTORE
{
    [Table("Genre")]
    public class Genre
    {   
            public int Id { get; set; }
            [Required]
            [MaxLength(40)]
            public string? GenreName { get; set; }
            public required List<Book> Books { get; set; }

    }
}
