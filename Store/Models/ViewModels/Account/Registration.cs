using System.ComponentModel.DataAnnotations;

namespace Store.Models.ViewModels;

public class RegistrationViewModel
{
    [Display(Name = "Имя пользователя")]
    [Required(ErrorMessage = "Введите имя")]
    [StringLength(30, MinimumLength = 5, 
    ErrorMessage = "Имя пользователя должно содержать от 5 до 30 символов")]
    public string? Name {get;set;}

    [Display(Name = "Электронная почта")]
    [Required(ErrorMessage = "Введите почту")]
    [RegularExpression(@"([a-zA-Z0-9\\_\\-\\.]+)@([a-zA-Z]+).([^\d]+)", 
    ErrorMessage = "Нверный формат почты. Пример: example@mail.ru")]
    public string? Email {get;set;}

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(20, MinimumLength = 6,
     ErrorMessage = "Пароль должен состоять из 6 или более символов")]
    [DataType(DataType.Password)]
    public string? Password {get;set;}

    [Display(Name = "Проверка пароля")]
    [DataType(DataType.Password)]
    public string? passwordConfirm {get;set;}


}