using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers;

public class AccountController : Controller
{
    private UserManager<StoreUser> userManager;
    private SignInManager<StoreUser> signInManager;
    private RoleManager<IdentityRole> roleManager;

    public AccountController( UserManager<StoreUser> userMgr,
                              SignInManager<StoreUser> signInMgr,
                              RoleManager<IdentityRole> roleMgr
                            )
    {
        userManager = userMgr;
        signInManager = signInMgr;
        roleManager = roleMgr;
    }
    
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        if(Request.GetDisplayUrl().ToString().Contains('?') )
           return NotFound();

        return View ("~/Views/Account/Login.cshtml", new LoginViewModel());
    }

    [HttpPost("/Login")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> Login(LoginViewModel loginVM)
    {
        if (loginVM?.Login!=null && loginVM?.Password!=null)
        {
            StoreUser? user =  await userManager.FindByNameAsync(loginVM?.Login);
            
            if(user == null) 
            user =  await userManager.FindByEmailAsync(loginVM?.Login);

            if(user != null)
            {                
                await signInManager.SignOutAsync();
                
                var result = await signInManager.PasswordSignInAsync(user, loginVM?.Password, false, false);
                
                if(result.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    
                    if(roles.Contains( Role.Admin.ToString()) )
                       return Redirect("/admin");
                    
                    else if(roles.Contains( Role.Moderator.ToString()) )
                       return Redirect("/admin/orders");
                    
                    else
                       return Redirect("/Home"); 
                }
                else 
                   ModelState.AddModelError("Password", "Неверные Логин/Пароль");
            }
            else 
               ModelState.AddModelError("Password", "Неверные Логин/Пароль");
        }

        return View("~/Views/Account/Login.cshtml", loginVM);
    }

    [Route("/Logout")]
    public async Task <IActionResult> Logout ()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }

    [HttpGet("/Registrastion"),]
    public ViewResult UserRegistration()
    {
        return View("~/Views/Account/Registr.cshtml", new RegistrationViewModel());
    }

    [HttpPost("/Registrastion"), ActionName("Registr")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult> UserRegistration(RegistrationViewModel regVM)
    {
        if(regVM.Name !=null && regVM.Password !=null)
        {
            StoreUser? user = await userManager.FindByNameAsync(regVM?.Name);

            if(user != null)
               ModelState.AddModelError("Name", "Пользователем с таким именем занят");
            
            user = await userManager.FindByEmailAsync(regVM?.Email);
            
            if(user != null)
               ModelState.AddModelError("Email", "Данная электронная почта занята");

            if(regVM.Password != regVM.passwordConfirm )
               ModelState.AddModelError("passwordConfirm", "Пароли не совпадают");

            if(ModelState.IsValid)
            {
                user = new StoreUser(regVM.Name);
                user.Email = regVM.Email;

                var resultCreate = await userManager.CreateAsync(user, regVM.Password);

                if(resultCreate.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Basic.ToString());

                    var resultSignIn =  await signInManager.PasswordSignInAsync(user, regVM.Password, false, false);

                    if(resultSignIn.Succeeded)
                       return Redirect ("/Home");
                }
                else
                   ModelState.AddModelError
                   ("Password", "Пароль должен содержать заглваные буквы и специальные символы (#, *, ~ и т.д.)");
            }
        }
        
        return View("~/Views/Account/Registr.cshtml", regVM);
    }
}