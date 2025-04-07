using System.ComponentModel.DataAnnotations;

namespace HrServices.DTOs.Filters;

public class PageFilters
{
    public int CurrentPage { get; set; }
    [Range(1, 100)]
    public int PageSize { get; set; }
}