using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace merch_store.DB_Layer.DBTables
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)] // ограничим длину
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")] // точность цены
        [Range(0, 999999.99, ErrorMessage = "Price must be >= 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ImageFront is required")]
        public string ImageFront { get; set; } = null!;

        [Required(ErrorMessage = "ImageBack is required")]
        public string ImageBack { get; set; } = null!;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = null!;

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = null!;
    }
}
