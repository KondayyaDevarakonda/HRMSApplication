using ApplicantApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantApi.Repository
{
    public interface IApplicantRepository
    {
        IEnumerable<Applicant> GetApplicants();
        Applicant GetApplicantById(string companyId);
        void CreateApplicant(Applicant company);
        void UpdateApplicant(Applicant company);
        void DeleteApplicant(string companyCode);
        void Save();
        bool ApplicantExists(string id);
    }
}
