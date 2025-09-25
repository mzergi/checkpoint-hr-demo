using AutoMapper;
using HrServices.DTOs.DocumentTypes;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentTypeCreateDTO, DocumentType>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
