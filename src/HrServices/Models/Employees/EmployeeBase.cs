using HrServices.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrServices.Models.Employees
{
    public class EmployeeBase : Person
    {
        public string BirthPlace { get; set; }
        public string TaxNumber { get; set; }
        public string PersonalIdentifierCardNumber { get; set; }
        public string PassportNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Residence { get; set; }
        public string BankAccountNumber { get; set; }
        public int PaidVacationsPerYear { get; set; }
    }
}
