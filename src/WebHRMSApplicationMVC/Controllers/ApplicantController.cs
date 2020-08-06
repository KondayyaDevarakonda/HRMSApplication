using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebHRMSApplicationMVC.Model;
using WebHRMSApplicationMVC.Repository;

namespace WebHRMSApplicationMVC.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                serviceObj.ClientServiceUrl("ApplicantServiceUrl");
                HttpResponseMessage response = serviceObj.GetResponse("api/Applicants");
                response.EnsureSuccessStatusCode();
                List<Applicant> applicants = response.Content.ReadAsAsync<List<Applicant>>().Result;
                ViewBag.Title = "All Applicants";
                return View(applicants);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}