using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Dapper;

namespace WarehouseStupid.Areas.Warehouses.Pages;

public class Warehouse : PageModel
{
    private readonly SqliteConnection _connection;

    public class WarehouseModel
    {
        public long? Id { get; set; }
        public string? Name { get; set; }    
    }
    
    [BindProperty] public WarehouseModel? Data { get; set; }

    public Warehouse(SqliteConnection connection)
    {
        _connection = connection;
    }
    
    public void OnGet(int? id)
    {
        if (!id.HasValue) return;
        
        var prm = new { id = id };
        var data = _connection.QueryFirst<WarehouseModel>("SELECT WarehouseId as Id, Name FROM warehouses WHERE WarehouseId = @id",
            prm);

        Data = data;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Data.Id.HasValue)
        {
            await _connection.ExecuteAsync("UPDATE warehouses SET Name = @Name WHERE WarehouseId = @Id", Data);
        }
        else
        {
            await _connection.ExecuteAsync("INSERT INTO warehouses(Name) VALUES (@Name)", Data);
        }

        return RedirectToPage("./Index");
    }
    
}