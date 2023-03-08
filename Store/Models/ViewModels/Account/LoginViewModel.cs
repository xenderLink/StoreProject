using System.ComponentModel.DataAnnotations;

namespace Store.Models.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Введите логин")]
    public string? Login {get; set;}

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    public string? Password {get; set;}
    public bool checkAuthentication {get;set;} = true;
    public string ReturnUrl { get; set; } = "/";
}