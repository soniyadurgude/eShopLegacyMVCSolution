using System.ComponentModel.DataAnnotations; 
namespace eShopLegacyMVC.Models 
{ 
    public class CatalogType 
    { 
        [Key] 
        public int Id { get; set; } 
        [Required] 
        [StringLength(100)] 
        public string Type { get; set; } 
    } 
}