using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseStupid.Infrastructure;
using static System.String;

namespace WarehouseStupid.Areas.Account.Pages;

public class LoginModel : PageModel
{
    private readonly UserManager _userManager;
    private readonly ILogger<LoginModel> _logger;

    public class LoginRec
    {
        public LoginRec()
        {
            
        }
        public LoginRec(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        [Required]
        public string UserName { get; init; }
        
        [Required, MinLength(3)]
        public string Password { get; init; }

        public void Deconstruct(out string UserName, out string Password)
        {
            UserName = this.UserName;
            Password = this.Password;
        }
    }

    [BindProperty] public LoginRec? Data { get; private set; }

    public LoginModel(UserManager userManager, ILogger<LoginModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public void OnGet()
    {
        Data = new LoginRec(Empty, Empty);
    }

    public async Task<IActionResult> OnPost(LoginRec data)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Err"] = "chyba";
            return Page();
        }
        else
        {
            await _userManager.SignIn(data.UserName, data.Password);
            return RedirectToPage("Index");
        }
    }
}