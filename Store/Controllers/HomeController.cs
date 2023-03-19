using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.RegularExpressions;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers;

public class HomeController : Controller
{
    private IProductsRepository repository;
    private IImageRepository imageRepository;
    public int PageSize = 5;
    public HomeController(IProductsRepository rep, IImageRepository img)
    {
        repository = rep;
        imageRepository = img;

    }

    [Route("/"), Route("/Home")]
    public async Task <IActionResult> Index ()
    {
        var products = await repository.Products
              .OrderBy(p=>Guid.NewGuid())                                     
              .Take(PageSize).ToListAsync ();

        List<ProductListItem> productListItems = new();
        string description;

        foreach (var product in products)
        {
            var query = imageRepository.ProductImages
                        .Where(x=>x.productId==product.productId);

            description="";

            if(product.productDescription != null)
            description = Regex.Replace ( JsonSerializer
                                          .Deserialize<dynamic>(product.productDescription)
                                          .ToString(),  @"[^\w \- :,]", "" );

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
                           });
            }
        }

        return View
         ( new ProductListViewModel 
               {
                 Products = productListItems
               });
    }

    [Route("/Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode)
    {
        if (statusCode==404)
            return View("~/Views/Home/NotFound.cshtml");
        
        return RedirectToAction(nameof(Index));
    }
}

