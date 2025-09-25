using System.ComponentModel.DataAnnotations;
using HrServices.Enums;

namespace HrServices.DTOs.Candidates
{
    public class CandidateCreateDTO
    {
        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
        public string? Comment { get; set; }
        public float? ExpectedSalary { get; set; }
        public ApplicationStates ApplicationState { get; set; } = ApplicationStates.Applied;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress] public string Email { get; set; }
        [Phone] public string PhoneNumber { get; set; }
        public string Sex { get; set; }
    }

    public class CandidateUpdateDTO
    {
        public string? Comment { get; set; }
        public float? ExpectedSalary { get; set; }
        public ApplicationStates? ApplicationState { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MothersName { get; set; }
        public DateTime? BirthDate { get; set; }
        [EmailAddress] public string? Email { get; set; }
        [Phone] public string? PhoneNumber { get; set; }
        public string? Sex { get; set; }
    }
}
