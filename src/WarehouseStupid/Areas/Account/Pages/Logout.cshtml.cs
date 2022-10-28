using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseStupid.Infrastructure;

namespace WarehouseStupid.Areas.Account.Pages;

public class LogoutModel : PageModel
{
    private readonly UserManager _userManager;

    public LogoutModel(UserManager userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        await _userManager.SignOut();
        return RedirectToPage("Index");
    }
}