using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectAgreementManagement.Models;
using System.Collections.Generic;

namespace ProjectAgreementManagement.Repositories
{
    public interface IProductGroupRepository 
    {
        IList<ProductGroup> GetAllProductGroup();

        List<SelectListItem> ProductGroupList(int selectedId = 0);
    }
}
