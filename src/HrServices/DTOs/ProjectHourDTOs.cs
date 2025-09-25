namespace HrServices.DTOs.ProjectHours
{
    public class ProjectHourCreateDTO
    {
        public Guid EmployeeId { get; set; }
        public float Hours { get; set; }
        public DateTime WorkingDay { get; set; }
        public DateTime? BookedOnDay { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ProjectPositionId { get; set; }
    }

    public class ProjectHourUpdateDTO
    {
        public float? Hours { get; set; }
        public DateTime? WorkingDay { get; set; }
        public DateTime? BookedOnDay { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ProjectPositionId { get; set; }
    }
}
