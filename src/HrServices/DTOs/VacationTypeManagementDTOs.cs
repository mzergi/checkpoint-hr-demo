namespace HrServices.DTOs.VacationTypes
{
    public class VacationTypeCreateDTO
    {
        public string Name { get; set; }
        public bool SubtractFromPaidVacation { get; set; }
        public bool SubtractFromWorkingHours { get; set; }
        public float SalaryMultiplier { get; set; }
        public bool ApplyForWholeDay {  get; set; }
    }

    public class VacationTypeUpdateDTO
    {
        public string? Name { get; set; }
        public bool? SubtractFromPaidVacation { get; set; }
        public bool? SubtractFromWorkingHours { get; set; }
        public float? SalaryMultiplier { get; set; }
        public bool? ApplyForWholeDay {  get; set; }
    }
}
