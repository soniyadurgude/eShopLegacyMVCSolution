using Microsoft.AspNetCore.Mvc; 
//using eShopLegacy.Utilities; 
using eShopLegacyMVC.Services; 
using System; 
using System.Linq; 
using Microsoft.Extensions.Logging; 
namespace eShopLegacyMVC.Controllers.WebApi 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class FilesController : ControllerBase 
    { 
        private readonly ICatalogService _service; 
        private readonly ILogger<FilesController> _logger; 
        public FilesController(ICatalogService service, ILogger<FilesController> logger) 
        { 
            _service = service; 
            _logger = logger; 
        } 
        // GET api/files 
        [HttpGet] 
        public IActionResult Get() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching catalog brands."); 
                var brands = _service.GetCatalogBrands() 
                    .Select(b => new BrandDTO 
                    { 
                        Id = b.Id, 
                        Brand = b.Brand 
                    }).ToList(); 
               /*  var serializer = new Serializing(); 
                var stream = serializer.SerializeBinary(brands); 
                _logger.LogInformation("Successfully fetched and serialized catalog brands."); 
                return File(stream, "application/octet-stream");  */
                return null;
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching catalog brands."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [Serializable] 
        public class BrandDTO 
        { 
            public int Id { get; set; } 
            public string Brand { get; set; } 
        } 
    } 
}