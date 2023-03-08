using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Category
{
    [Key]
    [Column("id")]
    public int categoryId {get;set;} 

    [Required]
    [Column("category", TypeName = "varchar(30)")]
    public string? name {get;set;} 

    [Column("icon")]
    public string? Icon {get;set;} 

    public virtual ICollection<ChildCategory>? childs {get;set;} //связь один ко многим (одна категория к множеству подгатегорий)
}

public class ChildCategory
{
    [Key]
    [Column("id")]
    public int typeId {get;set;} 

    [Column("category_id")]
    public int categoryId {get; set;}

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string? name {get;set;}

    [Column("image")]
    public string? Image {get;set;}

    public Category? category {get;set;} //для связи многие ко одному (множество подкатегорий к одной категории)

    public virtual ICollection<Product>? products {get;set;} //для связи один ко многим (одна подкатегория к множеству продуктов)
}