using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOOKSTORE
{
    [Table("CartDetails")]
    public class CartDetails
    {
        public int Id { get; set; }
        
        [Required]
        public int shoppingcartid { get; set; }

        [Required]
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public Book Book { get; set; }
        public ShoppingCart Shoppingcart { get; set; }


    }
}
