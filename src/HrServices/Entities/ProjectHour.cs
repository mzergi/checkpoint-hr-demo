namespace HrServices.Entities
{
    // TODO: move this to project service or keep it here
    public class ProjectHour : Booking
    {
        /// <summary>
        /// Project foreign key
        /// </summary>
        public Guid ProjectId {  get; set; }
        /// <summary>
        /// Project position foreign key
        /// </summary>
        public Guid ProjectPositionId {  get; set; }
    }
}
