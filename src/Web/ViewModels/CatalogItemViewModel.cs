﻿
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
        public bool ShowPrice { get; set; }
        public Currency PriceUnit {get; set;}
        public List<StockPerStoreViewModel> StockPerStore { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        public IEnumerable<SelectListItem> Brands {get; set; }
        public IEnumerable<SelectListItem> Types {get; set; }

    }
}
