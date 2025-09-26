using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var lang = Request.Cookies["lang"] ?? "eng"; // по умолчанию eng
        ViewData["Lang"] = lang;
        base.OnActionExecuting(context);
    }
}
