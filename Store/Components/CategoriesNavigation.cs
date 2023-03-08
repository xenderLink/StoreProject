using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Components;

public class CategoriesNavigation : ViewComponent
{
    private ICategoriesRepository repository;

    public CategoriesNavigation (ICategoriesRepository rep) =>
    repository = rep;

    public IViewComponentResult Invoke()
    {
        ViewBag.category = RouteData?.Values["category"];
        ViewBag.SelectedController = RouteData?.Values["controller"];
        ViewBag.Error = RouteData?.Values["action"];

        return View ( repository.Categories
                      .Select(c => c.name)
                      .Distinct()
                      .OrderBy(c=>c)
                    );
    }
}