using System;
using System.Collections.Generic;

namespace ApplicantApi.Model
{
    public partial class Applicant
    {
        public int ApplicantId { get; set; }
        public string ApplicantCode { get; set; }
        public string Initial { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string ApplicantStatus { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
