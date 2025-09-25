using AutoMapper;
using HrServices.DTOs.EmployeeDocuments;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class EmployeeDocumentProfile : Profile
    {
        public EmployeeDocumentProfile()
        {
            CreateMap<EmployeeDocumentCreateDTO, EmployeeDocument>().ReverseMap();
            CreateMap<EmployeeDocument, EmployeeDocumentUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
