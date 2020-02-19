using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Pdf
{
    public class IndexPdfModel : PageModel
    {
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IMemoryCache _cache;

        private const string PREVIOUS_SEARCH_TEXT = "PreviousSearchText";

        public IndexPdfModel(ICatalogViewModelService catalogViewModelService, IMemoryCache cache)
        {
            _catalogViewModelService = catalogViewModelService;
            _cache = cache;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();

        public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId) {
            CatalogModel = await _catalogViewModelService.GetCatalogItems(
                 pageId ?? 0, 1000,
                 catalogModel.SearchFilter, 
                 catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied, 
                 convertPrice: true,
                 HttpContext.RequestAborted);
            CatalogModel.ResultView = catalogModel.ResultView; // HACK
            CatalogModel.ResultViews = Enum<ResultView>.GetAll()
                .Select(resultView => new SelectListItem { Value = resultView.ToString(), Text = resultView.ToString() });
        }
    }
}