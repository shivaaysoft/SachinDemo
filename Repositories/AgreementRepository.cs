using Microsoft.AspNetCore.Identity;
using ProjectAgreementManagement.Data;
using ProjectAgreementManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace ProjectAgreementManagement.Repositories
{
    public class AgreementRepository : IAgreementRepository
    {
        #region Fields
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Ctor
        public AgreementRepository(ApplicationDbContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }
        #endregion

        #region Methods
        public Agreement GetAgreementById(int Id)
        {
            return _dbcontext.Agreement.FirstOrDefault(x => x.Id == Id);
        }
        public AgreementModel GetAgreement(int Id)
        {
            var model = (from a in _dbcontext.Agreement
                         where a.Id == Id
                         select new AgreementModel()
                         {
                             Id = a.Id,
                             UserId = a.UserId,
                             ProductGroupId = a.ProductGroupId,
                             ProductId = a.ProductId,
                             ProductPrice = a.ProductPrice,
                             NewPrice = a.NewPrice,
                             EffectiveDate = a.EffectiveDate,
                             ExpirationDate = a.ExpirationDate,
                             Active = a.Active
                         }).FirstOrDefault();
            if (model == null)
                return new AgreementModel();
            else
                return model;
        }
        public IEnumerable<AgreementModel> GetAllAgreements(string searchValue, string sortColumn, string sortColumnDir, int skip, int pageSize, out int recordsTotal, string userid)
        {
            var username = _userManager.FindByIdAsync(userid).Result;


            if (!string.IsNullOrEmpty(searchValue))
            {
                var agreements = from agr in _dbcontext.Agreement
                                 join pg in _dbcontext.ProductGroup on agr.ProductGroupId equals pg.Id
                                 join p in _dbcontext.Product on agr.ProductId equals p.Id
                                 where agr.UserId == username.Id && (pg.GroupCode.Contains(searchValue) || p.ProductNumber.Contains(searchValue))
                                 select new AgreementModel()
                                 {
                                     Id = agr.Id,
                                     ProductGroupId = agr.ProductGroupId,
                                     ProductGroupCode = pg.GroupCode,
                                     GroupDescription = pg.GroupDescription,
                                     ProductId = agr.ProductId,
                                     ProductNumber = p.ProductNumber.ToString(),
                                     ProductDescription = p.ProductDescription,
                                     UserId = agr.UserId,
                                     UserName = username.UserName,
                                     NewPrice = agr.NewPrice,
                                     EffectiveDate = agr.EffectiveDate,
                                     ExpirationDate = agr.ExpirationDate,
                                     ProductPrice = agr.ProductPrice
                                 };

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    agreements = agreements.OrderBy(sortColumn + " " + sortColumnDir);

                recordsTotal = agreements.Count();

                return agreements.Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                var agreements = from agr in _dbcontext.Agreement
                                 where agr.UserId == username.Id
                                 join pg in _dbcontext.ProductGroup on agr.ProductGroupId equals pg.Id
                                 join p in _dbcontext.Product on agr.ProductId equals p.Id
                                 select new AgreementModel()
                                 {
                                     Id = agr.Id,
                                     ProductGroupId = agr.ProductGroupId,
                                     ProductGroupCode = pg.GroupCode,
                                     GroupDescription = pg.GroupDescription,
                                     ProductId = agr.ProductId,
                                     ProductNumber = p.ProductNumber.ToString(),
                                     ProductDescription = p.ProductDescription,
                                     UserId = agr.UserId,
                                     UserName = username.UserName,
                                     NewPrice = agr.NewPrice,
                                     EffectiveDate = agr.EffectiveDate,
                                     ExpirationDate = agr.ExpirationDate,
                                     ProductPrice = agr.ProductPrice
                                 };

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    agreements = agreements.OrderBy(sortColumn + " " + sortColumnDir);

                recordsTotal = agreements.Count();

                return agreements.Skip(skip).Take(pageSize).ToList();
            }
        }

        public Agreement Add(Agreement agreement)
        {
            _dbcontext.Agreement.Add(agreement);
            _dbcontext.SaveChanges();
            return agreement;
        }
        public Agreement Update(Agreement agreementChanges)
        {
            var agreement = _dbcontext.Agreement.Attach(agreementChanges);
            agreement.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbcontext.SaveChanges();
            return agreementChanges;
        }
        public void Delete(int Id)
        {
            Agreement agreement = _dbcontext.Agreement.FirstOrDefault(e => e.Id == Id);
            if (agreement != null)
            {
                _dbcontext.Agreement.Remove(agreement);
                _dbcontext.SaveChanges();
            }
        }

        #endregion
    }
}
