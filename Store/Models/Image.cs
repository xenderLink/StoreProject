using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Store.Models;

[Table("images")]

public class Image
{
    [Column("id")]
    [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
    public long imageId {get;set;}

    [Column("path")]
    public string? Path {get;set;}
    public virtual ICollection <productImage>? ProductImages {get;set;}

}

[Table("product_images")]

public class productImage
{
    [Column("product_id")]
    public long productId {get;set;}
    public virtual Product? Product {get;set;}

    [Column("image_id")]
    public long imageId {get;set;}
    public virtual Image? Image {get;set;}
}