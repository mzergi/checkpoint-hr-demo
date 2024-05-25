namespace HrServices.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // User id
        public Guid? CreatedById { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid TenantId { get; set; } = new Guid();
    }
}
