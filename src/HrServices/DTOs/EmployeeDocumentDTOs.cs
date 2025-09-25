namespace HrServices.DTOs.EmployeeDocuments
{
    public class EmployeeDocumentCreateDTO
    {
        public Guid DocumentTypeId {  get; set; }
        public Guid EmployeeId {  get; set; }
        public DateTime ValidFrom {  get; set; }
        public DateTime? ValidTill { get; set; }
    }

    public class EmployeeDocumentUpdateDTO
    {
        public Guid? DocumentTypeId {  get; set; }
        public DateTime? ValidFrom {  get; set; }
        public DateTime? ValidTill { get; set; }
    }
}
