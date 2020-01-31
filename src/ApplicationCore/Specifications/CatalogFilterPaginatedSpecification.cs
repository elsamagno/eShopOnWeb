using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterPaginatedSpecification(
            int skip, int take, 
            string searchText, int? brandId, int? typeId)
            : base(CatalogFilterSpecification.BuildCatalogFilterExpression(searchText, brandId, typeId))
        {
            ApplyPaging(skip, take);
        }
    }
}
