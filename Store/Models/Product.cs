using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Product
{
    [Column("id")]
    [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
    public long productId {get;set;}

    [Required(ErrorMessage = "Выбирете категорию")]    
    [Column("type_id")]
    [Display(Name = "Категория")]
    public int typeId {get;set;}

    [Required(ErrorMessage = "Введите название")]
    [Column(TypeName = "varchar(150)")]
    [Display(Name = "Название")]
    public string name {get;set;} 
    
    [Required( ErrorMessage = "Введите цену")]
    [Range(1, double.MaxValue, ErrorMessage = "Введите положительное число")]

    [Display(Name = "Цена")]
    public decimal? price {get;set;}

    [Display(Name = "Скидка")]
    public decimal? discount {get;set;}

    [Display(Name = "Количество на складе")]
    public short? quantity {get;set;}

    [Column("SKU", TypeName = "varchar(100)")]
    [Display(Name = "Артикул")]
    public string? sku {get;set;}
    
    [Column("description", TypeName = "json")]
    [Display(Name = "Описание")]
    public string? productDescription {get;set;}

    public virtual ICollection <productImage>? ProductImages {get;set;}

    public virtual ICollection <CartLine>? Lines {get;set;}
    public virtual ChildCategory? type {get;set;} 
    
}