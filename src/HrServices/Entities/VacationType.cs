namespace HrServices.Entities
{
    public class VacationType : BaseEntity
    {
        public string Name { get; set; }
        public bool SubtractFromPaidVacation { get; set; }
        public bool SubtractFromWorkingHours { get; set; }
        public float SalaryMultiplier { get; set; }
        public bool ApplyForWholeDay {  get; set; }
    }
}
