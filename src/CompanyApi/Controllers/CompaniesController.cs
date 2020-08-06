using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApi.DBContexts;
using CompanyApi.Model;
using CompanyApi.Repository;
using System.Transactions;

namespace CompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _context;

        public CompaniesController(ICompanyRepository context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public IActionResult GetCompany()
        {
            var companies = _context.GetCompanies().OrderBy(company => company.CompanyId);
            if (companies == null)
            {
                return new OkObjectResult("Company Information not found");
            }
            return new OkObjectResult(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public IActionResult GetCompanyByID([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = _context.GetCompanyById(id);

            if (company == null)
            {
                return new OkObjectResult("Company Information not found for id : " + id);
            }
            return new OkObjectResult(company);
        }

        // POST: api/Companies
        [HttpPost]
        public IActionResult Post([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CompanyExists(company.ComanyCode))
            {
                return BadRequest();
            }

            try
            {
                using (var scope = new TransactionScope())
                {
                    _context.CreateCompany(company);
                    scope.Complete();
                }
            }
            catch (DbUpdateException)
            {
                if (CompanyExists(company.ComanyCode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompany", new { id = company.ComanyCode }, company);
        }

        [HttpPut]
        public IActionResult PutCompany([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CompanyExists(company.ComanyCode))
            {
                return BadRequest();
            }

            if (company != null)
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        _context.UpdateCompany(company);
                        scope.Complete();
                        return new OkResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.ComanyCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CompanyExists(id))
            {
                return NotFound();
            }

            _context.DeleteCompany(id);

            return new OkResult();
        }

        private bool CompanyExists(string id)
        {
            return _context.CompanyExists(id);
        }
    }
}