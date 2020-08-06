using System;
using System.Collections.Generic;

namespace WebHRMSApplicationMVC.Model
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string ComanyCode { get; set; }
        public string ComanyName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
