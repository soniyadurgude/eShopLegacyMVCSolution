using System; 
using System.Collections.Generic; 
using System.Linq; 
using eShopLegacyCore.Models; 
using eShopLegacyCore.Models.Infrastructure; 
using eShopLegacyCore.ViewModel; 
namespace eShopLegacyCore.Services 
{ 
    public class CatalogServiceMock : ICatalogService 
    { 
        private List<CatalogItem> _catalogItems; 
        public CatalogServiceMock() 
        { 
            _catalogItems = new List<CatalogItem>(PreconfiguredData.GetPreconfiguredCatalogItems()); 
        } 
        public PaginatedItemsViewModel<CatalogItem> GetCatalogItemsPaginated(int pageSize = 10, int pageIndex = 0) 
        { 
            var items = ComposeCatalogItems(_catalogItems); 
            var itemsOnPage = items 
                .OrderBy(c => c.Id) 
                .Skip(pageSize * pageIndex) 
                .Take(pageSize) 
                .ToList(); 
            return new PaginatedItemsViewModel<CatalogItem>( 
                pageIndex, pageSize, items.Count, itemsOnPage); 
        } 
        public CatalogItem GetCatalogItemById(int id) 
        { 
            return _catalogItems.FirstOrDefault(x => x.Id == id); 
        } 
        public IEnumerable<CatalogType> GetCatalogTypes() 
        { 
            return PreconfiguredData.GetPreconfiguredCatalogTypes(); 
        } 
        public IEnumerable<CatalogBrand> GetCatalogBrands() 
        { 
            return PreconfiguredData.GetPreconfiguredCatalogBrands(); 
        } 
        public void CreateCatalogItem(CatalogItem catalogItem) 
        { 
            var maxId = _catalogItems.Max(i => i.Id); 
            catalogItem.Id = ++maxId; 
            _catalogItems.Add(catalogItem); 
        } 
        public void UpdateCatalogItem(CatalogItem modifiedItem) 
        { 
            var originalItem = GetCatalogItemById(modifiedItem.Id); 
            if (originalItem != null) 
            { 
                _catalogItems[_catalogItems.IndexOf(originalItem)] = modifiedItem; 
            } 
        } 
        public void DeleteCatalogItem(int id) 
        { 
            var item = _catalogItems.Find(i => i.Id == id); 
            if (item != null) 
            { 
                _catalogItems.Remove(item); 
            } 
        } 
        public void Dispose() 
        { 
            // No resources to dispose 
        } 
        private List<CatalogItem> ComposeCatalogItems(List<CatalogItem> items) 
        { 
            var catalogTypes = PreconfiguredData.GetPreconfiguredCatalogTypes(); 
            var catalogBrands = PreconfiguredData.GetPreconfiguredCatalogBrands(); 
            items.ForEach(i => i.CatalogBrand = catalogBrands.First(b => b.Id == i.CatalogBrandId)); 
            items.ForEach(i => i.CatalogType = catalogTypes.First(b => b.Id == i.CatalogTypeId)); 
            return items; 
        } 
    } 
}