﻿using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;
namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public static Expression<Func<CatalogItem, bool>> BuildCatalogFilterExpression(
            string searchText, int? brandId, int? typeId) {
                return catalogItem =>
                    (!brandId.HasValue || catalogItem.CatalogBrandId == brandId) &&
                    (!typeId.HasValue || catalogItem.CatalogTypeId == typeId)  &&
                    (string.IsNullOrEmpty(searchText) || catalogItem.Name.Contains(searchText, System.StringComparison.OrdinalIgnoreCase));
            }
        public CatalogFilterSpecification(string searchText, int? brandId, int? typeId)
            : base(BuildCatalogFilterExpression(searchText, brandId, typeId))
        {
        }
    }
}