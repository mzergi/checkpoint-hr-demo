namespace HrServices.DTOs.Vacations
{
    public class VacationCreateDTO
    {
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float AbsenceHours { get; set; }
        public Guid VacationTypeId { get; set; }
        public int VacationPlanningState { get; set; }
    }

    public class VacationUpdateDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? AbsenceHours { get; set; }
        public Guid? VacationTypeId { get; set; }
        public int? VacationPlanningState { get; set; }
    }
}
