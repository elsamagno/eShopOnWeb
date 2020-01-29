using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public CatalogController(ICatalogViewModelService catalogViewModelService) => _catalogViewModelService = catalogViewModelService;

        [HttpGet]
        public async Task<ActionResult<CatalogIndexViewModel>> List(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;           
            var catalogModel = await _catalogViewModelService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied, HttpContext.RequestAborted);
            return Ok(catalogModel);
        }
          [HttpGet("{id}")]
         public async Task<IActionResult> GetById(int itemId){
             return Ok();
         }
           
    }
     [HttpGet("{id}")]
        public async Task<ActionResult<CatalogIndexViewModel>> GetById(int id){
           var catalogItem = await _catalogViewModelService.GetItemById(id);
           return Ok(catalogItem);

        }
}
}
