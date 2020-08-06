using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebHRMSApplicationMVC.Model;
using WebHRMSApplicationMVC.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebHRMSApplicationMVC.Controllers
{
    public class CompanyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                serviceObj.ClientServiceUrl("CompanyServiceUrl");
                HttpResponseMessage response = serviceObj.GetResponse("api/Companies");
                response.EnsureSuccessStatusCode();
                List<Company> products = response.Content.ReadAsAsync<List<Company>>().Result;
                ViewBag.Title = "All Products";
                return View(products);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
