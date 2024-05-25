using HrServices.Models.Persons;

namespace HrServices.Models.Employees
{
    public class CreateEmployee : EmployeeBase
    {
        public Guid CreatedById { get; set; }
        public Guid TenantId { get; set; }
    }
}
