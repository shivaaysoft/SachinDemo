using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectAgreementManagement.Data;
using ProjectAgreementManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgrManagement.Services
{
    public class ProductRepository : IProductRepository
    {
        #region Fields
        private readonly ApplicationDbContext _applicationDbContext;
        #endregion

        #region Ctor
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        #endregion

        #region Methods
        public Product GetProductById(int id)
        {
            if (id == 0)
                return null;

            var entity = (from p in _applicationDbContext.Product
                          where p.Id == id
                          select p).FirstOrDefault();
            return entity;
        }
        public IList<Product> GetAllProducts(int productGroupId)
        {
            var products = (from p in _applicationDbContext.Product
                            where p.ProductGroupId == productGroupId && p.Active == true
                            select p).ToList();
            return products;
        }

        public List<SelectListItem> ProductList(int productGroupId, int selectedId = 0)
        {
            var products = GetAllProducts(productGroupId);
            var productList = new List<SelectListItem>();
            productList.Add(new SelectListItem()
            {
                Text = "-- Select Product --",
                Value = ""
            });
            foreach (var product in products)
            {
                productList.Add(new SelectListItem()
                {
                    Text = product.ProductNumber.ToString(),
                    Value = product.Id.ToString(),
                    Selected = product.Id == selectedId
                });
            }
            return productList;
        }
        #endregion
    }
}
