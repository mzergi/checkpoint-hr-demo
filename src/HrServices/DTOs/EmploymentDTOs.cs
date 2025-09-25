using HrServices.Enums;

namespace HrServices.DTOs.Employments
{
    public class EmploymentCreateDTO
    {
        public Guid EmployeeId { get; set; }
        public float WeeklyWorkingHours { get; set; }
        public EmploymentTypes EmploymentType { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
        public string Currency { get; set; }
        public float Salary { get; set; }
        public float HourlyRate { get; set; }
        public string CostCenter { get; set; }
        public int PaidVacationsPerYear { get; set; }
        public Guid HomeDepartmentId { get; set; }
    }

    public class EmploymentUpdateDTO
    {
        public float? WeeklyWorkingHours { get; set; }
        public EmploymentTypes? EmploymentType { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
        public string? Currency { get; set; }
        public float? Salary { get; set; }
        public float? HourlyRate { get; set; }
        public string? CostCenter { get; set; }
        public int? PaidVacationsPerYear { get; set; }
        public Guid? HomeDepartmentId { get; set; }
    }
}
