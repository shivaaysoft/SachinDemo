using ProjectAgreementManagement.Models;
using System.Collections.Generic;

namespace ProjectAgreementManagement.Repositories
{
    public interface IAgreementRepository
    {
        AgreementModel GetAgreement(int Id);
        Agreement GetAgreementById(int Id);
        IEnumerable<AgreementModel> GetAllAgreements(string searchValue, string sortColumn, string sortColumnDir, int skip, int pageSize, out int recordsTotal, string userid);
        Agreement Add(Agreement agreement);
        Agreement Update(Agreement agreementChanges);
        void Delete(int Id);
    }
}
