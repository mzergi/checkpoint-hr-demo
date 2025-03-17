using System.ComponentModel.DataAnnotations;
using HrServices.Entities;

namespace HrServices.Models.Persons
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Sex { get; set; }
    }
}