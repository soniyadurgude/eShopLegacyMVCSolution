using eShopLegacy.Utilities; 
using eShopLegacyMVC.Services; 
using Microsoft.AspNetCore.Mvc; 
using System; 
using System.Linq; 
using System.Net; 
namespace eShopLegacyMVC.Controllers.WebApi 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class FilesController : Controller 
    { 
        private readonly ICatalogService _service; 
        public FilesController(ICatalogService service) 
        { 
            _service = service ?? throw new ArgumentNullException(nameof(service)); 
        } 
        // GET: api/files 
        [HttpGet] 
        public IActionResult Get() 
        { 
            try 
            { 
                var brands = _service.GetCatalogBrands() 
                    .Select(b => new BrandDTO 
                    { 
                        Id = b.Id, 
                        Brand = b.Brand 
                    }).ToList(); 
                var serializer = new Serializing(); 
                var serializedContent = serializer.SerializeBinary(brands); 
                return File(serializedContent, "application/octet-stream"); 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.Error.WriteLine($"Error occurred: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request."); 
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