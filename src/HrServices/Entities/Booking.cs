namespace HrServices.Entities
{
    public class Booking : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public float Hours { get; set; }
        /// <summary>
        /// The day the booking was made for (e.g. I book my hours for 04.16 on 04.20, Working day in 04.16)
        /// </summary>
        public DateTime WorkingDay { get; set; }
        /// <summary>
        /// The day the booking was made for (e.g. I book my hours for 04.16 on 04.20, Booked day is 04.20)
        /// </summary>
        public DateTime BookedOnDay { get; set; }


        // TODO: add supervisor approval handling?
        // public BookingState BookingState { get; set; }
        // public Employee ApprovedBy { get; set; }
        // public DateTime ApprovedOn { get; set }
    }
}
