using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Layer.DBTables
{
    public class Product    
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        // цена – decimal, укажем точность в OnModelCreating
        public decimal Price { get; set; }
        public string? ImageFront { get; set; }
        public string? ImageBack { get; set; }
        public string? Category { get; set; }
        public string? Type { get; set; }
    }
}
