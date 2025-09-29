using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace merch_store.DB_Layer.DBTables
{
    public class ProductDescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }  // Внешний ключ к Product

        [Required, StringLength(3)]
        public string Language { get; set; } = "eng"; // "eng", "ru", "ro"

        [Required]
        public string Description { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
