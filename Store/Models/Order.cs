using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Order
{
    [Column("id")]
    [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
    public long OrderId {get;set;}
    
    [BindNever]
    public ICollection<CartLine>? Cart {get;set;}

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Введите имя")]
    public string? name {get;set;}

    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Введите фамилию")]
    public string? surname {get;set;}

    [Display(Name = "Номер")]
    [Required(ErrorMessage = "Введите номер")]
    [StringLength(12, MinimumLength = 11, ErrorMessage = "Номер должен быть из 11 или 12 символов")]
    public string? number {get;set;}

    [Display(Name = "Город")]
    [Required(ErrorMessage = "Введите город"), MaxLength]
    public string? city {get;set;}

    [Required(ErrorMessage= "Введите адрес")]
    [Display(Name = "Адрес")]
    public string? address {get;set;}

    [BindNever]
    [Column("shipped")]
    public bool Shipped { get; set; } = false;
}