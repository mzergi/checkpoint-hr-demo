namespace HrServices.DTOs.WorkingHours
{
    public class WorkingHourCreateDTO
    {
        public Guid EmployeeId { get; set; }
        public float Hours { get; set; }
        public DateTime WorkingDay { get; set; }
        public DateTime? BookedOnDay { get; set; }
        public DateTime From {  get; set; }
        public DateTime To { get; set; }
    }

    public class WorkingHourUpdateDTO
    {
        public float? Hours { get; set; }
        public DateTime? WorkingDay { get; set; }
        public DateTime? BookedOnDay { get; set; }
        public DateTime? From {  get; set; }
        public DateTime? To { get; set; }
    }
}
