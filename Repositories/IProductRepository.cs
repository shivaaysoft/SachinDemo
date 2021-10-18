using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectAgreementManagement.Models;
using System.Collections.Generic;

namespace AgrManagement.Services
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        IList<Product> GetAllProducts(int productGroupId);
        List<SelectListItem> ProductList(int productGroupId, int selectedId = 0);
    }
}
