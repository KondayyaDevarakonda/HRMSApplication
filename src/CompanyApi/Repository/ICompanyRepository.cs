using CompanyApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi.Repository
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompanyById(string companyId);
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(string companyCode);
        void Save();
        bool CompanyExists(string id);
    }
}
