using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectAgreementManagement.Data;
using ProjectAgreementManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectAgreementManagement.Repositories
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        #region Fields
        private readonly ApplicationDbContext _applicationDbContext;
        #endregion

        #region ctor
        public ProductGroupRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        #endregion

        #region Methods
        public IList<ProductGroup> GetAllProductGroup()
        {
            var model = (from pg in _applicationDbContext.ProductGroup
                         where pg.Active == true
                         select pg).ToList();
            return model;
        }

        public List<SelectListItem> ProductGroupList(int selectedId = 0)
        {
            var pGroups = GetAllProductGroup();

            var productGroupList = new List<SelectListItem>();
            productGroupList.Add(new SelectListItem()
            {
                Text = "-- Select Product Group --",
                Value = ""
            });
            foreach (var pGroup in pGroups)
            {
                productGroupList.Add(new SelectListItem()
                {
                    Text = pGroup.GroupCode,
                    Value = pGroup.Id.ToString(),
                    Selected = pGroup.Id == selectedId
                });
            }
            return productGroupList;
        }
        #endregion
    }
}
