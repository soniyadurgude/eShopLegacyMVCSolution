using Microsoft.AspNetCore.Mvc; 
using eShopLegacyMVC.Services; 
using System.Collections.Generic; 
using System.Linq; 
using Microsoft.Extensions.Logging; 
namespace eShopLegacyMVC.Controllers.WebApi 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class BrandsController : Controller 
    { 
        private readonly ICatalogService _service; 
        private readonly ILogger<BrandsController> _logger; 
        public BrandsController(ICatalogService service, ILogger<BrandsController> logger) 
        { 
            _service = service; 
            _logger = logger; 
        } 
        // GET: api/brands 
        [HttpGet] 
        public IEnumerable<Models.CatalogBrand> Get() 
        { 
            _logger.LogInformation("Fetching all catalog brands."); 
            var brands = _service.GetCatalogBrands(); 
            return brands; 
        } 
        // GET: api/brands/5 
        [HttpGet("{id}")] 
        public IActionResult Get(int id) 
        { 
            _logger.LogInformation("Fetching catalog brand with ID: {Id}", id); 
            var brands = _service.GetCatalogBrands(); 
            var brand = brands.FirstOrDefault(x => x.Id == id); 
            if (brand == null) 
            { 
                _logger.LogWarning("Catalog brand with ID: {Id} not found.", id); 
                return NotFound(); 
            } 
            return Ok(brand); 
        } 
        // DELETE: api/brands/5 
        [HttpDelete("{id}")] 
        public IActionResult Delete(int id) 
        { 
            _logger.LogInformation("Attempting to delete catalog brand with ID: {Id}", id); 
            var brandToDelete = _service.GetCatalogBrands().FirstOrDefault(x => x.Id == id); 
            if (brandToDelete == null) 
            { 
                _logger.LogWarning("Catalog brand with ID: {Id} not found for deletion.", id); 
                return NotFound(); 
            } 
            // demo only - don't actually delete 
            _logger.LogInformation("Catalog brand with ID: {Id} would be deleted (demo only).", id); 
            return Ok(); 
        } 
    } 
}