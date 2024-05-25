using HrServices.Models.Persons;
using System.ComponentModel.DataAnnotations;

namespace HrServices.Entities
{
    public class Employee : BaseEntity, IPerson
    {
        // Person interface implementation
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }

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
            get => Employments.FirstOrDefault(e => e.ContractStart < DateTime.Now && (!e.ContractEnd.HasValue || e.ContractEnd > DateTime.Now));
        }

        public ICollection<Employment> Employments { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<EmployeeDeadline> Deadlines { get; set; }
    }
}
