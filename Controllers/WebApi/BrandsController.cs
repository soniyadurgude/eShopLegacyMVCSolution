using Microsoft.AspNetCore.Mvc; 
using eShopLegacyMVC.Services; 
using System.Collections.Generic; 
using System.Linq; 
namespace eShopLegacyMVC.Controllers.WebApi 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class BrandsController : ControllerBase 
    { 
        private readonly ICatalogService _service; 
        public BrandsController(ICatalogService service) 
        { 
            _service = service; 
        } 
        // GET: api/Brands 
        [HttpGet] 
        public IActionResult GetBrands() 
        { 
            try 
            { 
                var brands = _service.GetCatalogBrands(); 
                return Ok(brands); 
            } 
            catch (System.Exception ex) 
            { 
                // Log the error details 
                System.Diagnostics.Debug.WriteLine($"Error in GetBrands: {ex.Message}, StackTrace: {ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        // GET: api/Brands/5 
        [HttpGet("{id}")] 
        public IActionResult GetBrand(int id) 
        { 
            try 
            { 
                var brands = _service.GetCatalogBrands(); 
                var brand = brands.FirstOrDefault(x => x.Id == id); 
                if (brand == null) 
                { 
                    return NotFound(); 
                } 
                return Ok(brand); 
            } 
            catch (System.Exception ex) 
            { 
                // Log the error details 
                System.Diagnostics.Debug.WriteLine($"Error in GetBrand: {ex.Message}, StackTrace: {ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        // DELETE: api/Brands/5 
        [HttpDelete("{id}")] 
        public IActionResult DeleteBrand(int id) 
        { 
            try 
            { 
                var brandToDelete = _service.GetCatalogBrands().FirstOrDefault(x => x.Id == id); 
                if (brandToDelete == null) 
                { 
                    return NotFound(); 
                } 
                // demo only - don't actually delete 
                return Ok(); 
            } 
            catch (System.Exception ex) 
            { 
                // Log the error details 
                System.Diagnostics.Debug.WriteLine($"Error in DeleteBrand: {ex.Message}, StackTrace: {ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
}