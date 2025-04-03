using System; 
using System.Collections.Generic; 
using System.Linq; 
using eShopLegacyMVC.Models; 
using eShopLegacyMVC.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
namespace eShopLegacyMVC.Services 
{ 
    public class CatalogService : ICatalogService, IDisposable 
    { 
        private readonly CatalogDBContext _context; 
        //private readonly CatalogItemHiLoGenerator _indexGenerator; 
        private readonly ILogger<CatalogService> _logger; 
        public CatalogService(CatalogDBContext context
        //, CatalogItemHiLoGenerator indexGenerator
        , ILogger<CatalogService> logger) 
        { 
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
            //_indexGenerator = indexGenerator ?? throw new ArgumentNullException(nameof(indexGenerator)); 
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
                return new PaginatedItemsViewModel<CatalogItem>( 
                    pageIndex, pageSize, totalItems, itemsOnPage); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching paginated catalog items."); 
                throw; 
            } 
        } 
        public CatalogItem FindCatalogItem(int id) 
        { 
            try 
            { 
                _logger.LogInformation("Finding catalog item with ID: {Id}", id); 
                return _context.CatalogItems.Include(c => c.CatalogBrand).Include(c => c.CatalogType).FirstOrDefault(ci => ci.Id == id); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while finding catalog item with ID: {Id}", id); 
                throw; 
            } 
        } 
        public IEnumerable<CatalogType> GetCatalogTypes() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all catalog types."); 
                return _context.CatalogTypes; 
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
                return _context.CatalogBrands; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching catalog brands."); 
                throw; 
            } 
        } 
        public void CreateCatalogItem(CatalogItem catalogItem) 
        { 
            try 
            { 
                _logger.LogInformation("Creating a new catalog item."); 
                //catalogItem.Id = _indexGenerator.GetNextSequenceValue(_context); 
                _context.CatalogItems.Add(catalogItem); 
                _context.SaveChanges(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a new catalog item."); 
                throw; 
            } 
        } 
        public void UpdateCatalogItem(CatalogItem catalogItem) 
        { 
            try 
            { 
                _logger.LogInformation("Updating catalog item with ID: {Id}", catalogItem.Id); 
                _context.Entry(catalogItem).State = EntityState.Modified; 
                _context.SaveChanges(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while updating catalog item with ID: {Id}", catalogItem.Id); 
                throw; 
            } 
        } 
        public void RemoveCatalogItem(CatalogItem catalogItem) 
        { 
            try 
            { 
                _logger.LogInformation("Removing catalog item with ID: {Id}", catalogItem.Id); 
                _context.CatalogItems.Remove(catalogItem); 
                _context.SaveChanges(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while removing catalog item with ID: {Id}", catalogItem.Id); 
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