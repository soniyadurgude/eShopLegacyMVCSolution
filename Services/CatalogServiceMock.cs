/* using System; 
using System.Collections.Generic; 
using System.Linq; 
using eShopLegacyMVC.Models; 
using eShopLegacyMVC.Models.Infrastructure; 
using eShopLegacyMVC.ViewModel; 
namespace eShopLegacyMVC.Services 
{ 
    public class CatalogServiceMock : ICatalogService 
    { 
        private List<CatalogItem> catalogItems; 
        public CatalogServiceMock() 
        { 
            catalogItems = new List<CatalogItem>(PreconfiguredData.GetPreconfiguredCatalogItems()); 
        } 
        public IEnumerable<CatalogItem> GetCatalogItems() 
        { 
            Console.WriteLine("Fetching all catalog items from mock service."); 
            return catalogItems; 
        } 
        public CatalogItem GetCatalogItemById(int id) 
        { 
            Console.WriteLine($"Fetching catalog item with ID: {id} from mock service."); 
            return catalogItems.FirstOrDefault(x => x.Id == id); 
        } 
        public PaginatedItemsViewModel<CatalogItem> GetCatalogItemsPaginated(int pageSize = 10, int pageIndex = 0) 
        { 
            Console.WriteLine($"Fetching paginated catalog items from mock service. PageSize: {pageSize}, PageIndex: {pageIndex}"); 
            var items = ComposeCatalogItems(catalogItems); 
            var itemsOnPage = items 
                .OrderBy(c => c.Id) 
                .Skip(pageSize * pageIndex) 
                .Take(pageSize) 
                .ToList(); 
            return new PaginatedItemsViewModel<CatalogItem>( 
                pageIndex, pageSize, items.Count, itemsOnPage); 
        } 
        public CatalogItem FindCatalogItem(int id) 
        { 
            Console.WriteLine($"Finding catalog item with ID: {id} in mock service."); 
            return catalogItems.FirstOrDefault(x => x.Id == id); 
        } 
        public IEnumerable<CatalogType> GetCatalogTypes() 
        { 
            Console.WriteLine("Fetching all catalog types from mock service."); 
            return PreconfiguredData.GetPreconfiguredCatalogTypes(); 
        } 
        public IEnumerable<CatalogBrand> GetCatalogBrands() 
        { 
            Console.WriteLine("Fetching all catalog brands from mock service."); 
            return PreconfiguredData.GetPreconfiguredCatalogBrands(); 
        } 
        public void CreateCatalogItem(CatalogItem catalogItem) 
        { 
            try 
            { 
                Console.WriteLine("Creating a new catalog item in mock service."); 
                var maxId = catalogItems.Max(i => i.Id); 
                catalogItem.Id = ++maxId; 
                catalogItems.Add(catalogItem); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error creating catalog item: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
            } 
        } 
        public void UpdateCatalogItem(CatalogItem modifiedItem) 
        { 
            try 
            { 
                Console.WriteLine($"Updating catalog item with ID: {modifiedItem.Id} in mock service."); 
                var originalItem = FindCatalogItem(modifiedItem.Id); 
                if (originalItem != null) 
                { 
                    catalogItems[catalogItems.IndexOf(originalItem)] = modifiedItem; 
                } 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error updating catalog item: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
            } 
        } 
        public void RemoveCatalogItem(CatalogItem catalogItem) 
        { 
            try 
            { 
                Console.WriteLine($"Removing catalog item with ID: {catalogItem.Id} from mock service."); 
                catalogItems.Remove(catalogItem); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error removing catalog item: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
            } 
        } 
        public void Dispose() 
        { 
            Console.WriteLine("Disposing resources in mock service."); 
        } 
        private List<CatalogItem> ComposeCatalogItems(List<CatalogItem> items) 
        { 
            Console.WriteLine("Composing catalog items with types and brands in mock service."); 
            var catalogTypes = PreconfiguredData.GetPreconfiguredCatalogTypes(); 
            var catalogBrands = PreconfiguredData.GetPreconfiguredCatalogBrands(); 
            items.ForEach(i => i.CatalogBrand = catalogBrands.First(b => b.Id == i.CatalogBrandId)); 
            items.ForEach(i => i.CatalogType = catalogTypes.First(b => b.Id == i.CatalogTypeId)); 
            return items; 
        } 
    } 
} */