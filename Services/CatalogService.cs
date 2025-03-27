using System; 
using System.Collections.Generic; 
using System.Linq; 
using eShopLegacyCore.Models; 
using eShopLegacyCore.ViewModel; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Logging; 
namespace eShopLegacyCore.Services 
{ 
    public class CatalogService : ICatalogService 
    { 
        private readonly CatalogDBContext _context; 
        private readonly ILogger<CatalogService> _logger; 
        public CatalogService(CatalogDBContext context, ILogger<CatalogService> logger) 
        { 
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        } 
        public PaginatedItemsViewModel<CatalogItem> GetCatalogItemsPaginated(int pageSize, int pageIndex) 
        { 
            try 
            { 
                _logger.LogInformation("Fetching paginated catalog items. PageSize: {PageSize}, PageIndex: {PageIndex}", pageSize, pageIndex); 
                var totalItems = _context.CatalogItems.LongCount(); 
                var itemsOnPage = _context.CatalogItems 
                    .Include(c => c.CatalogBrand) 
                    .Include(c => c.CatalogType) 
                    .OrderBy(c => c.Id) 
                    .Skip(pageSize * pageIndex) 
                    .Take(pageSize) 
                    .ToList(); 
                _logger.LogInformation("Fetched {Count} items for page {PageIndex}.", itemsOnPage.Count, pageIndex); 
                return new PaginatedItemsViewModel<CatalogItem>( 
                    pageIndex, pageSize, totalItems, itemsOnPage); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching paginated catalog items."); 
                throw; 
            } 
        } 
        public CatalogItem GetCatalogItemById(int id) 
        { 
            try 
            { 
                _logger.LogInformation("Fetching catalog item with ID: {Id}", id); 
                return _context.CatalogItems.Find(id); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching the catalog item with ID: {Id}", id); 
                throw; 
            } 
        } 
        public void CreateCatalogItem(CatalogItem item) 
        { 
            try 
            { 
                _logger.LogInformation("Creating a new catalog item."); 
                _context.CatalogItems.Add(item); 
                _context.SaveChanges(); 
                _logger.LogInformation("Catalog item created successfully."); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a new catalog item."); 
                throw; 
            } 
        } 
        public void UpdateCatalogItem(CatalogItem item) 
        { 
            try 
            { 
                _logger.LogInformation("Updating catalog item with ID: {Id}", item.Id); 
                _context.CatalogItems.Update(item); 
                _context.SaveChanges(); 
                _logger.LogInformation("Catalog item updated successfully."); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while updating the catalog item with ID: {Id}", item.Id); 
                throw; 
            } 
        } 
        public void DeleteCatalogItem(int id) 
        { 
            try 
            { 
                _logger.LogInformation("Deleting catalog item with ID: {Id}", id); 
                var item = _context.CatalogItems.Find(id); 
                if (item != null) 
                { 
                    _context.CatalogItems.Remove(item); 
                    _context.SaveChanges(); 
                    _logger.LogInformation("Catalog item deleted successfully."); 
                } 
                else 
                { 
                    _logger.LogWarning("Catalog item with ID: {Id} not found.", id); 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while deleting the catalog item with ID: {Id}", id); 
                throw; 
            } 
        } 
        public IEnumerable<CatalogType> GetCatalogTypes() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all catalog types."); 
                return _context.CatalogTypes.ToList(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching catalog types."); 
                throw; 
            } 
        } 
        public IEnumerable<CatalogBrand> GetCatalogBrands() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all catalog brands."); 
                return _context.CatalogBrands.ToList(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching catalog brands."); 
                throw; 
            } 
        } 
        public void Dispose() 
        { 
            _logger.LogInformation("Disposing CatalogService."); 
            _context.Dispose(); 
        } 
    } 
}