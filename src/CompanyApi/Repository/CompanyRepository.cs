using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.DBContexts;
using CompanyApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly HRMSDBContext _dbContext;

        public CompanyRepository(HRMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _dbContext.Company.ToList();
        }

        public Company GetCompanyById(string companyId)
        {
            return _dbContext.Company.Find(companyId);//   .ToList().Find(x => x.CompanyId == companyId);
        }

        public void CreateCompany(Company company)
        {
            _dbContext.Add(company);
            Save();
        }

        public void UpdateCompany(Company company)
        {
            _dbContext.Update(company).Property(x => x.CompanyId).IsModified = false;
            Save();
        }

        public void DeleteCompany(string companyCode)
        {
            var company = _dbContext.Company.ToList().Find(x => x.ComanyCode == companyCode);
            _dbContext.Company.Remove(company);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChangesAsync(); 
        }

        public bool CompanyExists(string id)
        {
            return _dbContext.Company.Any(e => e.ComanyCode == id);
        }
    }
}
