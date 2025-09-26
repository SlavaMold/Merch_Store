using Microsoft.AspNetCore.Mvc;

public class LanguageController : Controller
{
    public IActionResult Set(string lang, string returnUrl = "/")
    {
        // Сохраняем язык в куку на 30 дней
        Response.Cookies.Append("lang", lang, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            IsEssential = true
        });

        return LocalRedirect(returnUrl);
    }
}
