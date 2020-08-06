using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantApi.DBContexts;
using ApplicantApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApplicantApi.Repository
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly HRMSDBContext _dbContext;

        public ApplicantRepository(HRMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Applicant> GetApplicants()
        {
            return _dbContext.Applicant.ToList();
        }

        public Applicant GetApplicantById(string applicantId)
        {
            return _dbContext.Applicant.Find(applicantId);//   .ToList().Find(x => x.CompanyId == companyId);
        }

        public void CreateApplicant(Applicant applicant)
        {
            _dbContext.Add(applicant);
            Save();
        }

        public void UpdateApplicant(Applicant applicant)
        {
            _dbContext.Update(applicant).Property(x => x.ApplicantId).IsModified = false;
            Save();
        }

        public void DeleteApplicant(string applicantCode)
        {
            var applicant = _dbContext.Applicant.ToList().Find(x => x.ApplicantCode == applicantCode);
            _dbContext.Applicant.Remove(applicant);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChangesAsync(); 
        }

        public bool ApplicantExists(string id)
        {
            return _dbContext.Applicant.Any(e => e.ApplicantCode == id);
        }

    }
}
