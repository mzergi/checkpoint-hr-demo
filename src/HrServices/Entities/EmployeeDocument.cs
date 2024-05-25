namespace HrServices.Entities
{
    public class EmployeeDocument : BaseEntity
    {
        public Guid DocumentTypeId {  get; set; }
        public DocumentType DocumentType {  get; set; }
        public Guid EmployeeId {  get; set; }
        public Employee Employee {  get; set; }
        public DateTime ValidFrom {  get; set; }
        public DateTime? ValidTill { get; set; }
    }
}
