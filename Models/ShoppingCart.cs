using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOOKSTORE
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;  // لتجنب null

        public bool IsDeleted { get; set; } = false;

        // تهيئة الـ collection لتجنب مشاكل null
        public ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();
    }
}
