namespace HrServices.Entities
{
    public class EmployeeDeadline : BaseEntity
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}
