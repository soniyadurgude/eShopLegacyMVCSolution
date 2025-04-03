using System.ComponentModel.DataAnnotations; 
namespace eShopLegacyMVC.Models 
{ 
    public class CatalogBrand 
    { 
        [Key] 
        public int Id { get; set; } 
        [Required] 
        [StringLength(100)] 
        public string Brand { get; set; } 
    } 
}