using System.ComponentModel.DataAnnotations;

namespace HrServices.DTOs.Employees;

public class EmployeeCreateDTO
{
    // person data
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
}