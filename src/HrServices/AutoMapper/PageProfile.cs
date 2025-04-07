using AutoMapper;
using HrServices.DTOs;
using HrServices.DTOs.Filters;
using HrServices.Entities;

namespace HrServices.AutoMapper;

public class PageProfile : Profile
{
    public PageProfile()
    {
        CreateMap(typeof(PageFilters), typeof(Page<>)).ReverseMap();
    }
}