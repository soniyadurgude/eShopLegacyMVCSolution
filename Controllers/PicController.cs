using eShopLegacyMVC.Services; 
using log4net; 
using Microsoft.AspNetCore.Mvc; 
using System; 
using System.IO; 
using System.Net; 
namespace eShopLegacyMVC.Controllers 
{ 
    public class PicController : Controller 
    { 
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); 
        public const string GetPicRouteName = "GetPicRouteTemplate"; 
        private ICatalogService service; 
        public PicController(ICatalogService service) 
        { 
            this.service = service; 
        } 
        // GET: Pic/5.png 
        [HttpGet] 
        [Route("items/{catalogItemId:int}/pic", Name = GetPicRouteName)] 
        public IActionResult Index(int catalogItemId) 
        { 
            _log.Info($"Now loading... /items/Index?{catalogItemId}/pic"); 
            if (catalogItemId <= 0) 
            { 
                _log.Warn("Invalid catalog item ID provided."); 
                return new StatusCodeResult((int)HttpStatusCode.BadRequest); 
            } 
            try 
            { 
                var item = service.FindCatalogItem(catalogItemId); 
                if (item != null) 
                { 
                    var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pics"); 
                    var path = Path.Combine(webRoot, item.PictureFileName); 
                    if (!System.IO.File.Exists(path)) 
                    { 
                        _log.Warn($"File not found: {path}"); 
                        return NotFound(); 
                    } 
                    string imageFileExtension = Path.GetExtension(item.PictureFileName); 
                    string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension); 
                    var buffer = System.IO.File.ReadAllBytes(path); 
                    return File(buffer, mimetype); 
                } 
                _log.Warn("Catalog item not found."); 
                return NotFound(); 
            } 
            catch (Exception ex) 
            { 
                _log.Error("An error occurred while processing the request.", ex); 
                return StatusCode((int)HttpStatusCode.InternalServerError); 
            } 
        } 
        private string GetImageMimeTypeFromImageFileExtension(string extension) 
        { 
            string mimetype; 
            switch (extension) 
            { 
                case ".png": 
                    mimetype = "image/png"; 
                    break; 
                case ".gif": 
                    mimetype = "image/gif"; 
                    break; 
                case ".jpg": 
                case ".jpeg": 
                    mimetype = "image/jpeg"; 
                    break; 
                case ".bmp": 
                    mimetype = "image/bmp"; 
                    break; 
                case ".tiff": 
                    mimetype = "image/tiff"; 
                    break; 
                case ".wmf": 
                    mimetype = "image/wmf"; 
                    break; 
                case ".jp2": 
                    mimetype = "image/jp2"; 
                    break; 
                case ".svg": 
                    mimetype = "image/svg+xml"; 
                    break; 
                default: 
                    mimetype = "application/octet-stream"; 
                    break; 
            } 
            return mimetype; 
        } 
    } 
}