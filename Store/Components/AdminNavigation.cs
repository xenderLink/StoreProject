using Microsoft.AspNetCore.Mvc;
public class AdminNavigation : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        ViewBag.SelectedController = RouteData?.Values["controller"];

        string[] controllers = {"Product", "CrudOrder"}; 

        return View(controllers);
    }
}