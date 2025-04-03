using System;
using Microsoft.AspNetCore.Mvc; 
namespace eShopLegacyMVC.Controllers.Api 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class CatalogController : ControllerBase 
    { 
        [HttpGet] 
        public IActionResult Get() 
        { 
            try 
            { 
                // Logic here 
                return Ok(new { Message = "Hello World!" }); 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.WriteLine($"Error: {ex.Message}, StackTrace: {ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
}