using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers;

public class CatalogController : Controller
{
    private IProductsRepository productsRepository;
    private ICategoriesRepository categoriesRepository;
    private IImageRepository imageRepository;
    public int PageSize = 5;
    public CatalogController(IProductsRepository pR, ICategoriesRepository cR, IImageRepository iR)
    {
        productsRepository = pR;
        categoriesRepository = cR;
        imageRepository = iR;  
    }

    public async Task <IActionResult> Index()
    {

        return View (new CatalogViewModel()
        {
            Categories = categoriesRepository.Categories,
            subCategories = await categoriesRepository.SubCategories.ToListAsync()       
        });
    }
    
    [Route("[controller]/{category}")]
    public async Task <IActionResult> Categories (string? category)
    {
        if(categoriesRepository.Categories.FirstOrDefault(c=>c.name==category)==null)
        return Redirect("/Catalog");
        
        ViewBag.category = category;

        return View ( await categoriesRepository.SubCategories
                      .Join ( categoriesRepository
                      .Categories, childs=>childs.categoryId,
                       category=>category.categoryId,
                       (child, category) => new {child, category}
                            )
                      .Where(x=>x.category.name==category)
                      .Select(x=>x.child)
                      .OrderBy(x=>x.typeId).ToListAsync()
                    );
    }
    
    [Route("[controller]/{category}/{subCategory}"), ActionName("Products")]
    public async Task <IActionResult> ViewProductsByCategory
    (string? category, string? subCategory, int productPage=1)
    {

        var urlCategory = categoriesRepository.Categories
                          .FirstOrDefault(n=>n.name==category);
        
        if ( urlCategory == null)
        return Redirect("/Catalog");

        var urlSubCategory = await categoriesRepository
                                   .SubCategories
                                   .FirstOrDefaultAsync(n=>n.name==subCategory);

        if(urlSubCategory == null)
           return RedirectToAction(nameof(Categories), new {category = category});
        
        var categorizedProducts = await (from products in productsRepository.Products 
                                         join childs in categoriesRepository.SubCategories
                                         on products.typeId equals childs.typeId
                                         where (childs.name == subCategory)
                                         select products)  
                                        .ToListAsync();

        if(categorizedProducts.Count == 0)
           return View();
        
        List<ProductListItem> productListItems = new();
        string description;

        foreach(var product in categorizedProducts.Skip((productPage-1) * PageSize).Take(PageSize) )
        {
            var query = imageRepository.ProductImages
                        .Where(x=>x.productId==product.productId);
            
            description = "";

            if(product.productDescription!=null)
               description = Regex.Replace ( JsonSerializer
                                             .Deserialize<dynamic>(product.productDescription)
                                             .ToString(),  @"[^\w \- :,.]", "" ); //запрещены все символы, кроме двоеточия, запятой, точки и запятой.     

            if(query != null)
            {
                var p = new ProductListItem()
                        {
                          productId = product.productId,
                          Name = product.name,
                          Price = product.price,
                          Path = await imageRepository.Images
                          .Join (query, i=>i.imageId, pi=>pi.imageId, (i, pi) =>
                           new{ I = i, PI = pi} )
                          .Where(IPI => IPI.PI.productId == product.productId)
                          .Select(IPI=>IPI.I.Path)
                          .FirstOrDefaultAsync(),
                          Description = description
                        };

                productListItems.Add (p);
            }
            else
            {
                productListItems.Add ( new ProductListItem()
                                       {                      
                                         productId = product.productId,
                                         Name = product.name,
                                         Price = product.price,
                                         Description = description                                
                                       } );
            }
        }

        return View 
        ( new ProductListViewModel
           {
             Products = productListItems,
             PagingInfo = new PagingInfo
             {
                CurrentPage = productPage,
                ProductsPerPage = PageSize,
                TotalProducts = categorizedProducts.Count
             }
           });
    }
}