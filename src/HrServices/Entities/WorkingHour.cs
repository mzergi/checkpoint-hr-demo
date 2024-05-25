namespace HrServices.Entities
{
    /// <summary>
    /// Represents a daily working hour booking. Working Day represents the day the booking was made for, Booked day represents the day the booking was made on.
    /// Differentiating between the two dates is important because the cost itself is made for the day the booking was made on.
    /// (If I book my hours for april in may, then the company will have to bear the costs in may for the days I booked for april)
    /// </summary>
    public class WorkingHour : Booking
    {
        public DateTime From {  get; set; }
        public DateTime To { get; set; }
    }
}
