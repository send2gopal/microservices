using microkart.catalog.Database;

namespace microkart.catalog.Models
{
    public record PagedProductView(
    int PageIndex,
    int PageSize,
    long Count,
    IEnumerable<Product> Items);
}
