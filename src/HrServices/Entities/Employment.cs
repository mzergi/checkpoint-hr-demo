using System.Text.Json.Serialization;
using HrServices.Enums;

namespace HrServices.Entities
{
    public class Employment : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        /// <summary>
        /// default = 40 hour workweek
        /// HC = WeeklyWorkingHours / 40
        /// TODO: make the base for 100% configurable?
        /// </summary>
        public float WeeklyWorkingHours { get; set; }
        public EmploymentTypes EmploymentType { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime? ContractEnd { get; set; }
        public string Currency { get; set; }
        public float Salary { get; set; }
        public float HourlyRate { get; set; }
        public string CostCenter { get; set; }
        public int PaidVacationsPerYear { get; set; }
        /// <summary>
        /// Foreign key to organization service
        /// There is no other place like home
        /// Literally
        /// There can only be one department for the guy
        /// </summary>
        public Guid HomeDepartmentId { get; set; }
    }
}
