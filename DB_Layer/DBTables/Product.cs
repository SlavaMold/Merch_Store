using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace merch_store.DB_Layer.DBTables
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)] // ограничим длину
        public string Name { get; set; } = null!;

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

        [MaxLength(255)]
        public string? BandName { get; set; }  // только если Category = bybands

        [Required(ErrorMessage = "Type is required")]
        public bool ByBands { get; set; }

        public ICollection<ProductDescription> Descriptions { get; set; } = new List<ProductDescription>();

        public string Description { get; set; } = null!;
    }
}
