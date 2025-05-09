using HrServices.Models.Persons;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HrServices.Entities
{
    public class Employee : Person
    {
        // Employee specific data
        public string BirthPlace { get; set; }
        public string TaxNumber { get; set; }
        public string PersonalIdentifierCardNumber { get; set; }
        public string PassportNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Residence { get; set; }
        public string BankAccountNumber { get; set; }
        public int PaidVacationsPerYear { get; set; }
        public Employment? CurrentEmployment 
        {
            get => Employments?.FirstOrDefault(e => e.ContractStart < DateTime.Now && (!e.ContractEnd.HasValue || e.ContractEnd > DateTime.Now));
        }
    
        public ICollection<Employment> Employments { get; set; }
        public ICollection<EmployeeDeadline> Deadlines { get; set; }
        [JsonIgnore]
        public ICollection<Skill> Skills { get; set; }
    }
}
