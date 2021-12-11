namespace PruebaENG.Application.Common.Interfaces;

public interface IPaginacion
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}