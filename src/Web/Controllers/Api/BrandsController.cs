using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class BrandsController : BaseApiController
    {
        private readonly IBrandViewModelService _brandViewModelService;

        public BrandsController(IBrandViewModelService brandViewModelService) => _brandViewModelService = brandViewModelService;

        [HttpGet]
        public async Task<IActionResult> List(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;           
            var catalogModel = await _brandViewModelService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied, HttpContext.RequestAborted);
            return Ok(catalogModel);
        }
    }
}