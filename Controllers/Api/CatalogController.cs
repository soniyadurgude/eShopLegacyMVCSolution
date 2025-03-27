using Microsoft.AspNetCore.Mvc; 
using System; 
namespace eShopLegacyMVC.Controllers.Api 
{ 
    [Route("api")] 
    public class CatalogController : Controller 
    { 
        [HttpGet] 
        public ActionResult Index() 
        { 
            try 
            { 
                // Log the start of the action 
                Console.WriteLine("CatalogController.Index action started."); 
                var result = Json(new { Message = "Hello World!" }); 
                // Log the successful completion of the action 
                Console.WriteLine("CatalogController.Index action completed successfully."); 
                return result; 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.WriteLine($"Error in CatalogController.Index: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
                // Return an error response 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
}