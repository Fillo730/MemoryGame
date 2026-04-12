using MemoryGame_API.Utils;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MemoryGame_API.Controllers;

public class BaseController : ControllerBase
{
    protected string GetLanguage(string? language)
    {
        
        if(language is null)
        {
            return AppConstants.DEFAULT_LANGUAGE;
        }
        else
        {
            return language;
        }
    }

    protected int GetUserIdFromToken()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(claim, out int userId))
        {
            return userId;
        }

        return 0; 
    }
}
