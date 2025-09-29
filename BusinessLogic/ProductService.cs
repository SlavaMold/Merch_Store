using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using merch_store.API_Layer;
using merch_store.DB_Layer.DBTables;

namespace merch_store.BusinessLogic.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository repo) => _productRepository = repo;

        // Словарь: категория -> допустимые типы (если нужно)
        private static readonly Dictionary<string, string[]> CategoryToTypes = new()
        {
            { "clothes", new[] { "tshirt", "hoodie" } },
            { "accessories", new[] { "signs", "banners" } }
            // "bybands" специально не ограничиваем (фильтрация по Type — это имя группы)
        };

        public List<Product> GetFilteredProducts(string? categoryCode, string? typeCode, string? bandName, string lang)
        {
            if (string.IsNullOrWhiteSpace(categoryCode))
            {
                // нет фильтра — все товары
                return _productRepository.GetAllProducts(lang);
            }

            var cat = categoryCode.ToLower();

            if (cat == "bybands")
            {
                return _productRepository.GetFilteredProducts(categoryCode, typeCode, bandName, lang);
            }

            // Для Clothes и Accessories:
            if (CategoryToTypes.TryGetValue(cat, out var allowedTypes))
            {
                // если клики по заголовку — показываем всё в категории
                if (string.IsNullOrWhiteSpace(typeCode))
                {
                    return _productRepository.GetProductsByCategory(cat, lang);
                }

                // если клик по подтипу (typeCode) — показать только этот type
                return _productRepository.GetProductsByType(typeCode, lang);
            }

            // fallback: общий фильтр по Category или по Type
            return _productRepository.GetFilteredProducts(categoryCode, typeCode, null, lang);
        }

        public void CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
        }

        public List<Product> GetAllProducts(string lang)
        {
            return _productRepository.GetAllWithDescriptions(lang);
        }
    }
}
