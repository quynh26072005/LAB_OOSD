using System.Collections.Generic;
using eShoppingPrototype.Models;
using eShoppingPrototype.Data;

namespace eShoppingPrototype.ServiceAdapter
{
    /// <summary>
    /// Adapter to external Product Management System
    /// In real system, this would call external APIs
    /// For prototype, connects to local database
    /// </summary>
    public class ProductServiceAdapter
    {
        private readonly SanPhamDAO _productDAO;

        public ProductServiceAdapter()
        {
            _productDAO = new SanPhamDAO();
        }

        public List<NhomSanPham> GetAllCategories()
        {
            // In real system: Call external Product Management API
            // For prototype: Use local database
            return _productDAO.GetAllCategories();
        }

        public List<SanPham> GetProductsByCategory(int categoryId)
        {
            return _productDAO.GetProductsByCategory(categoryId);
        }

        public SanPham GetProductDetail(int productId)
        {
            return _productDAO.GetProductById(productId);
        }

        public List<SanPham> SearchProducts(string keyword)
        {
            return _productDAO.SearchProducts(keyword);
        }
    }
}
