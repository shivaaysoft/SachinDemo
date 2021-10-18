using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectAgreementManagement.Models
{
    public class AgreementModel
    {
        public AgreementModel()
        {
            ProductList = new List<SelectListItem>();
            ProductGroupList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int ProductGroupId { get; set; }
        public string ProductGroupCode { get; set; }
        public string GroupDescription { get; set; }
        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string ProductDescription { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }
        public decimal ProductPrice { get; set; }
        [Required]
        [Display(Name = "New Price")]
        public decimal NewPrice { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
        public IList<SelectListItem> ProductGroupList { get; set; }
        public IList<SelectListItem> ProductList { get; set; }
    }
}
