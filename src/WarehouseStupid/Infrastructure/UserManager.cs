using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WarehouseStupid.Infrastructure;

public class UserManager
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserManager(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task SignIn(string userName, string password)
    {
        var identity = new ClaimsIdentity(GetUserClaims(userName), CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        if (_contextAccessor.HttpContext != null)
        {
            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }

    public async Task SignOut()
    {
        if (_contextAccessor.HttpContext != null)
        {
            await _contextAccessor.HttpContext.SignOutAsync();
        }
    }

    private IEnumerable<Claim>? GetUserClaims(string userName)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "ID-123456"),
            new(ClaimTypes.Name, "fake uzivatele")
        };
        return result;
    }
}