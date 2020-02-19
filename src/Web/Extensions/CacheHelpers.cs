using System;
using System.Globalization;

namespace Microsoft.eShopWeb.Web.Extensions
{
  public class InvalidPageIndexException: Exception {

    }

    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(30);
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}-{4}-{5}";
//"items-0-10---"
//0,10,null,null,null
       public static string GenerateCatalogItemCacheKey(int pageIndex, int itemsPage, string searchText, int? brandId, int? typeId, string cultureName)
        {
            if (pageIndex < 0) {
                throw new InvalidPageIndexException();
            }
             string _searchText = string.IsNullOrEmpty(searchText)?"":searchText.Replace(" ", ""); // TODO: Handle invalid special chars in cache keys?
            return string.Format(_itemsKeyTemplate, pageIndex, itemsPage, _searchText, brandId, typeId, cultureName);
        }
            );
        }

        public static object GenerateCatalogItemCacheKey(int pageIndex, int iTEMS_PER_PAGE, object searchText, int? brandId, int? typeId)
        {
            throw new NotImplementedException();
        }

        public static string GenerateBrandsCacheKey()
        {
            return "brands";
        }

        public static string GenerateTypesCacheKey()
        {
            return "types";
        }

       public static string  GenerateCatalogItemIdKey(int id)
        {
            return $"catalog_item_{id}";
        }
    }
}
