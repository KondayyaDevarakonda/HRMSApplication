using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebHRMSApplicationMVC.Repository
{
    public class ServiceRepository
    {
        public ServiceRepository()
        {
            /*Client = new HttpClient();
            var config = ConfigurationManager.AppSettings["CompanyServiceUrl"];
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CompanyServiceUrl"].ToString());*/
        }

        public void ClientServiceUrl(String serviceUrl)
        {
            Client = new HttpClient();
            var serviceUrlFromConfiguration = ConfigurationManager.AppSettings[serviceUrl].ToString();
            Client.BaseAddress = new Uri(serviceUrlFromConfiguration);
        }

        public HttpClient Client { get; set; }

        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}
