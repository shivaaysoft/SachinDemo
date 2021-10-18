using AgrManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectAgreementManagement.Models;
using ProjectAgreementManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectAgreementManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgreementRepository _agreementRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly UserManager<IdentityUser> _userManager;


        public HomeController(IAgreementRepository agreementRepository,
                            IProductRepository productRepository,
                            IProductGroupRepository productGroupRepository,
                            UserManager<IdentityUser> userManager)
        {
            _agreementRepository = agreementRepository;
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetData()
        {
            var user = _userManager.GetUserAsync(User).Result;

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var model = _agreementRepository.GetAllAgreements(searchValue, sortColumn, sortColumnDirection, skip, pageSize, out recordsTotal, user.Id);

            return Json(new { draw = draw, data = model, recordsFiltered = recordsTotal, recordsTotal = recordsTotal });
        }

        public JsonResult DeleteAgreement(int id)
        {
            bool result = false;
            if (id > 0)
            {
                _agreementRepository.Delete(id);
                result = true;
            }
            return Json(new { response = result });
        }

        [HttpGet]
        public IActionResult AddEditAgreement(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            AgreementModel model = new AgreementModel();
            if (id > 0)
            {
                model = _agreementRepository.GetAgreement(id);
                model.UserId = user.Id;
                model.ProductGroupList = _productGroupRepository.ProductGroupList(model.ProductGroupId);
                model.ProductList = _productRepository.ProductList(model.ProductGroupId, model.ProductId);
            }
            else
            {
                model.UserId = user.Id;
                model.EffectiveDate = DateTime.Now;
                model.ExpirationDate = DateTime.Now;
                model.ProductGroupList = _productGroupRepository.ProductGroupList();
                model.ProductList = _productRepository.ProductList(0);
            }

            return PartialView("_AddOrEdit", model);
        }

        [HttpPost]
        public IActionResult AddEditAgreement(AgreementModel model)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (ModelState.IsValid)
            {
                var product = _productRepository.GetProductById(model.ProductId);
                if (model.Id > 0)
                {
                    var agreement = _agreementRepository.GetAgreementById(model.Id);
                    agreement.UserId = user.Id;
                    agreement.ProductGroupId = model.ProductGroupId;
                    agreement.ProductId = model.ProductId;
                    agreement.EffectiveDate = model.EffectiveDate;
                    agreement.ExpirationDate = model.ExpirationDate;
                    agreement.ProductPrice = product != null ? product.Price : 0;
                    agreement.NewPrice = model.NewPrice;
                    agreement.Active = model.Active;
                    _agreementRepository.Update(agreement);
                }
                else
                {
                    var agreement = new Agreement()
                    {
                        UserId = user.Id,
                        ProductGroupId = model.ProductGroupId,
                        ProductId = model.ProductId,
                        EffectiveDate = model.EffectiveDate,
                        ExpirationDate = model.ExpirationDate,
                        ProductPrice = product != null ? product.Price : 0,
                        NewPrice = model.NewPrice,
                        Active = model.Active
                    };
                    _agreementRepository.Add(agreement);
                }
            }

            return RedirectToAction("Index");
        }

        public JsonResult GetProductsByProductGroupId(int productGroupId)
        {
            var products = _productRepository.GetAllProducts(productGroupId);
            var productlist = new List<SelectListItem>();

            foreach (var product in products)
            {
                productlist.Add(new SelectListItem()
                {
                    Text = product.ProductNumber,
                    Value = product.Id.ToString(),
                });
            }

            return Json(new { productlist = productlist });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
