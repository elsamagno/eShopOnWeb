﻿using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class CatalogItem : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        [DefaultValue(true)]dotnet
        public bool ShowPrice { get; set; }
        public int StockLoja { get; set; }
        
       List<StockPerStore> StockPerStore {get; set; }
       
        #region "Navigation properties"
        public CatalogType CatalogType { get; set; }
        public CatalogBrand CatalogBrand { get; set; }
        #endregion

    }
}