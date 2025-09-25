namespace HrServices.DTOs.EmployeeDeadlines
{
    public class EmployeeDeadlineCreateDTO
    {
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public Guid EmployeeId { get; set; }
    }

    public class EmployeeDeadlineUpdateDTO
    {
        public string? Topic { get; set; }
        public string? Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}
