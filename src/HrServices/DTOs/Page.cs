namespace HrServices.DTOs;

public class Page<TEntity>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public ICollection<TEntity> Entries { get; set; }
}