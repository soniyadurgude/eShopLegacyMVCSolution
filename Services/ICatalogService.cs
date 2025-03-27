using System; 
using System.Collections.Generic; 
using eShopLegacyCore.Models; 
using eShopLegacyMVC.ViewModel; 
public interface ICatalogService : IDisposable 
{ 
    IEnumerable<CatalogItem> GetCatalogItems(); 
    CatalogItem GetCatalogItemById(int id); 
    void CreateCatalogItem(CatalogItem item); 
    void UpdateCatalogItem(CatalogItem item); 
    void DeleteCatalogItem(int id); 
    IEnumerable<CatalogType> GetCatalogTypes(); 
    IEnumerable<CatalogBrand> GetCatalogBrands(); 
    PaginatedItemsViewModel<CatalogItem> GetCatalogItemsPaginated(int pageSize, int pageIndex); 
}