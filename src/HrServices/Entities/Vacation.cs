using HrServices.Enums;

namespace HrServices.Entities
{
    public class Vacation : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float AbsenceHours { get; set; }
        public Guid VacationTypeId { get; set; }
        public VacationType VacationType { get; set; }
        // todo: make configurable from db, where it saves the highlight colour for the calendar, and it can be edited from the admin UI
        public VacationPlanningState VacationPlanningState { get; set; }
    }
}
