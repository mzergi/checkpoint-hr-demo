namespace HrServices.Entities
{
    public class DocumentType : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<EmployeeDocument> Documents { get; set; }
    }
}
