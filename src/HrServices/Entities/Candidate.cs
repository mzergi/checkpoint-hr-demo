using HrServices.Enums;
using HrServices.Models.Persons;
using System.ComponentModel.DataAnnotations;

namespace HrServices.Entities
{
    public class Candidate : BaseEntity, IPerson
    {
        public DateTime AppliedOn { get; set; }
        public DateTime LastStateChange { get; set; }
        public string Comment { get; set; }
        public float? ExpectedSalary { get; set; }
        public ApplicationStates ApplicationState { get; set; }
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
