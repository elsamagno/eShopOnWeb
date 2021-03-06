﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class IndexModel : PageModel
    {
       
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IMemoryCache _cache;

         public IndexModel(ICatalogViewModelService catalogViewModelService, IMemoryCache cache)
        {
            _catalogViewModelService = catalogViewModelService;
            _cache = cache;
        }

        public CatalogIndexViewModel CatalogModel { get; set; } = new CatalogIndexViewModel();
        [Authorize(Roles="Administrators")]

         public async Task OnGet(CatalogIndexViewModel catalogModel, int? pageId, bool convertPrice = true)
        {
            var cacheKey = CacheHelpers.GenerateCatalogItemCacheKey(pageId.GetValueOrDefault(), Constants.ITEMS_PER_PAGE, catalogModel.SearchText, catalogModel.BrandFilterApplied, catalogModel.TypesFilterApplied, CultureInfo.CurrentCulture.Name);

            _cache.Remove(cacheKey);


            CatalogModel = await _catalogViewModelService.GetCatalogItems(
                pageId.GetValueOrDefault(),
                 Constants.ITEMS_PER_PAGE, 
                 catalogModel.SearchText, 
                 catalogModel.BrandFilterApplied, 
                 catalogModel.TypesFilterApplied,
                 convertPrice: false,
                 HttpContext.RequestAborted);
                 CatalogModel.ResultViews = Enum<ResultView>.GetAll().Select(resultView => new SelectListItem { Value = resultView.ToString(), Text = resultView.ToString() });
        }
    }
}
