using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Store.Infrasctructure;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers;

[Route("/admin"), Route("/admin/products")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private IProductsRepository repository;
    private  Dictionary<int, string> categoriesForIndex = new();
    private List<SelectListItem> categoriesForDropDown = new();
    public int PageSize = 5;

    public ProductController(IProductsRepository rep, ICategoriesRepository catRep)
    {

        repository = rep;

        var asSpan = CollectionsMarshal.AsSpan(catRep.Childs.ToList());

        foreach (var item in asSpan)
        {
            categoriesForIndex.Add(item.typeId, item?.name);                                                   // id категории и её 
            categoriesForDropDown.Add( new SelectListItem{Value = item.typeId.ToString(), Text = item.name}); // имя для отображения на страничках
        }

    }
    public async Task <IActionResult> Index (int productPage = 1)
    {   

        ProductsViewModel VM = new ProductsViewModel()
        {
             Products = await repository.Products
                        .Skip( (productPage-1) * PageSize)
                        .Take(PageSize)
                        .OrderBy(i=>i.productId).ToListAsync(),
            
            Categories = categoriesForIndex,

             PagingInfo = new PagingInfo
             {
                TotalProducts = await repository.Products.CountAsync(),
                CurrentPage =  productPage,
                ProductsPerPage = PageSize
             }
        };

        return View("~/Views/Crud/Products/Index.cshtml", VM);
    }

    [HttpGet("/admin/products/add/")]
    public ActionResult Add()
    {        
        ViewBag.Categories = categoriesForDropDown;
        return View("~/Views/Crud/Products/Add.cshtml");
    }

    [HttpPost("/admin/products/add/"), ActionName("Add")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> AddPOST(Product product)
    {
        ViewBag.Categories = categoriesForDropDown;   

        if (product.productDescription == null)
        product.productDescription = null;
        
        else
        {
            if (product.productDescription.isJson() != true)
            ModelState.AddModelError("productDescription", "Неверный формат JSON");
        }

        if (ModelState.IsValid)
        {
            await repository.CreateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        return View ("~/Views/Crud/Products/Add.cshtml", product);
    }

    [HttpGet("/admin/products/edit/{id}")]
    public async Task <IActionResult> Edit(long? id)
    {
        if ( id==null || id ==0)
        return NotFound();

        var product  = await repository.Products.FirstOrDefaultAsync(p=>p.productId==id);

        if ( product == null)
        return NotFound();

        ViewBag.Categories = categoriesForDropDown;   

        return View ("~/Views/Crud/Products/Edit.cshtml", product);

    }

    [HttpPost("/admin/products/edit/{id}"), ActionName("Edit")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> EditPOST(long id, Product productData)
    {
    
        var product  = await repository.Products.FirstOrDefaultAsync(p=>p.productId==id);

        if (product == null)
        return NotFound();

        ViewBag.Categories = categoriesForDropDown;   

        if (productData.productDescription == null)
        product.productDescription = null;
        
        else
        {
            if (productData.productDescription.isJson() != true)
            ModelState.AddModelError("productDescription", "Неверный формат JSON");
        }

        if(ModelState.IsValid)
        {
            product.name = productData.name;
            product.price = productData.price;
            product.typeId = productData.typeId;
            product.sku = productData.sku;
            product.quantity = productData.quantity;

            await repository.UpdateProductAsync(product);
            return Redirect (Request.Headers["Referer"].ToString());
        }

        return View("~/Views/Crud/Products/Edit.cshtml", product); 
    }

    [HttpGet("/admin/products/delete/{id}")]
    public IActionResult Delete(long? id)
    {
        if( id==null || id==0)
        return NotFound();

        var product = repository.Products.FirstOrDefault(p=>p.productId==id);

        if( product == null)
        return NotFound();

        return View("~/Views/Crud/Products/Delete.cshtml", product);
    }

    [HttpPost("/admin/products/delete/{id}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> DeletePOST(long id)
    {
        var product  = await repository.Products.FirstOrDefaultAsync(p=>p.productId==id);

        if( product == null)
        return NotFound();

        await repository.DeleteProductAsync(product);

        return RedirectToAction(nameof(Index));
    }
}

