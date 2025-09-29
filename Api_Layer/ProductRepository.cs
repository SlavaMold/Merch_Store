using merch_store.DB_Layer.Contexts;
using merch_store.DB_Layer.DBTables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace merch_store.API_Layer
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) { _context = context; }


        private void AttachDescriptions(List<Product> products, string lang)
        {
            foreach (var p in products)
            {
                var desc = p.Descriptions?
                    .FirstOrDefault(d => d.Language.ToLower() == lang.ToLower());

                p.Description = desc?.Description ?? "";
            }
        }

        private void AttachDescription(Product? product, string lang)
        {
            if (product == null) return;

            var desc = product.Descriptions?
                .FirstOrDefault(d => d.Language.ToLower() == lang.ToLower());

            product.Description = desc?.Description ?? "";
        }


        public List<Product> GetAllProducts(string lang)
        {
            var products = _context.Products
                .Include(p => p.Descriptions)
                .AsNoTracking()
                .ToList();

            AttachDescriptions(products, lang);
            return products;
        }

        public Product? GetProductById(int id, string lang)
        {
            var product = _context.Products
                .Include(p => p.Descriptions)
                .FirstOrDefault(p => p.Id == id);

            AttachDescription(product, lang);
            return product;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public List<Product> GetProductsByCategory(string category, string lang)
        {
            if (string.IsNullOrWhiteSpace(category))
                return GetAllProducts(lang);

            var products = _context.Products
                .Include(p => p.Descriptions)
                .AsNoTracking()
                .Where(p => p.Category.ToLower() == category.ToLower())
                .ToList();

            AttachDescriptions(products, lang);
            return products;
        }


        public List<Product> GetProductsByType(string type, string lang)
        {
            if (string.IsNullOrWhiteSpace(type))
                return GetAllProducts(lang);

            var products = _context.Products
                .Include(p => p.Descriptions)
                .AsNoTracking()
                .Where(p => p.Type.ToLower() == type.ToLower())
                .ToList();

            AttachDescriptions(products, lang);
            return products;
        }

        public List<Product> GetProductsByTypes(IEnumerable<string> types, string lang)
        {
            var lowered = types.Select(t => t.ToLower()).ToList();

            var products = _context.Products
                .Include(p => p.Descriptions)
                .AsNoTracking()
                .Where(p => lowered.Contains(p.Type.ToLower()))
                .ToList();

            AttachDescriptions(products, lang);
            return products;
        }

        // Универсальный метод — применим фильтры
        public List<Product> GetFilteredProducts(string? categoryCode, string? typeCode, string? bandName, string lang)
        {
            var q = _context.Products
        .Include(p => p.Descriptions) // сразу подгружаем переводы
        .AsQueryable();

            // Фильтр по ByBands
            q = q.Where(p => p.ByBands || !p.ByBands); // для отладки можно убрать, потом фильтровать ниже

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                var cat = categoryCode.Trim().ToLower();

                if (cat == "bybands")
                {
                    // Только товары, где ByBands = true
                    q = q.Where(p => p.ByBands);

                    if (!string.IsNullOrWhiteSpace(bandName) && bandName.ToLower() != "all")
                    {
                        var bn = bandName.Trim().ToLower();
                        q = q.Where(p => p.BandName != null && p.BandName.Replace(" ", "-").ToLower() == bn);
                    }
                    // else — все товары ByBands
                }
                else
                {
                    // Clothes / Accessories — только ByBands = false
                    q = q.Where(p => !p.ByBands);
                    q = q.Where(p => p.Category.ToLower() == cat);

                    if (!string.IsNullOrWhiteSpace(typeCode) && typeCode.ToLower() != "all")
                    {
                        var t = typeCode.Trim().ToLower();
                        q = q.Where(p => p.Type.ToLower() == t);
                    }
                    // else — все товары выбранной категории
                }
            }
            else
            {
                // Нет категории — возвращаем всё
            }

            var products = q.AsNoTracking().ToList();

            // выбираем описание по языку
            foreach (var p in products)
            {
                var desc = p.Descriptions
                    .FirstOrDefault(d => d.Language.ToLower() == lang.ToLower());

                // Добавляем временное поле, чтобы не ломать Product в БД
                p.Description = desc?.Description ?? "";
            }

            return products;
        }

        public List<Product> GetAllWithDescriptions(string lang)
        {
            var products = _context.Products
                .Include(p => p.Descriptions)
                .AsNoTracking()
                .ToList();

            // Проставляем Description из Descriptions по выбранному языку
            foreach (var p in products)
            {
                var desc = p.Descriptions.FirstOrDefault(d => d.Language.ToLower() == lang.ToLower());
                if (desc != null)
                {
                    // создаем поле Description на лету для ViewModel или можно добавить временное свойство
                    p.Description = desc.Description;
                }
                else
                {
                    p.Description = ""; // fallback, если перевода нет
                }
            }

            return products;
        }
    }
}
