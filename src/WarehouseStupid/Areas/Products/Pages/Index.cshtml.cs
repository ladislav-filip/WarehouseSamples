using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace WarehouseStupid.Areas.Products.Pages;

public class ProductsIndexModel : PageModel
{
    private readonly ILogger<ProductsIndexModel> _logger;
    private readonly SqliteConnection _connection;

    public record ProductRec(long ProductId, string Code, string Name, long Weight);

    public IEnumerable<ProductRec> Products;

    public ProductsIndexModel(ILogger<ProductsIndexModel> logger, SqliteConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }
    
    public void OnGet()
    {
        Products = _connection.Query<ProductRec>("SELECT * FROM products ORDER BY Name");
    }
}