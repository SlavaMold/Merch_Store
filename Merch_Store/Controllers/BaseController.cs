using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace merch_store.Controllers { 
public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var lang = Request.Cookies["lang"] ?? "eng"; // по умолчанию eng
        ViewData["Lang"] = lang;
        base.OnActionExecuting(context);
    }
}
}