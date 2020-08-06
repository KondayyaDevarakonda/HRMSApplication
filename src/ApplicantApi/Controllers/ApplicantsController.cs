using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicantApi.DBContexts;
using ApplicantApi.Model;
using ApplicantApi.Repository;
using System.Transactions;

namespace ApplicantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantRepository _context;

        public ApplicantsController(IApplicantRepository context)
        {
            _context = context;
        }

        // GET: api/Applicants
        [HttpGet]
        public IActionResult GetApplicant()
        {
            var applicants = _context.GetApplicants().OrderBy(x => x.ApplicantId);
            if (applicants == null)
            {
                return new OkObjectResult("Applicant Information not found");
            }
            return new OkObjectResult(applicants);
        }

        // GET: api/Applicants/5
        [HttpGet("{id}")]
        public IActionResult GetApplicantById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicant = _context.GetApplicantById(id);

            if (applicant == null)
            {
                return new OkObjectResult("Applicant Information not found for id : " + id);
            }
            return new OkObjectResult(applicant);
        }

        // POST: api/Applicants
        [HttpPost]
        public IActionResult PostApplicant([FromBody] Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ApplicantExists(applicant.ApplicantCode))
            {
                return BadRequest();
            }

            try
            {
                using (var scope = new TransactionScope())
                {
                    _context.CreateApplicant(applicant);
                    scope.Complete();
                }
            }
            catch (DbUpdateException)
            {
                if (ApplicantExists(applicant.ApplicantCode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplicant", new { id = applicant.ApplicantCode }, applicant);
        }

        // PUT: api/Applicants/5
        [HttpPut]
        public IActionResult PutApplicant([FromBody] Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ApplicantExists(applicant.ApplicantCode))
            {
                return BadRequest();
            }

            if (applicant != null)
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        _context.UpdateApplicant(applicant);
                        scope.Complete();
                        return new OkResult();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.ApplicantCode))
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

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public IActionResult DeleteApplicant([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ApplicantExists(id))
            {
                return NotFound();
            }

            _context.DeleteApplicant(id);

            return new OkResult();
        }

        private bool ApplicantExists(string id)
        {
            return _context.ApplicantExists(id);
        }
    }
}