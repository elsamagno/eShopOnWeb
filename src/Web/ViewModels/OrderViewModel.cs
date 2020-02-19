﻿using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class OrderViewModel
    {
        private const string DEFAULT_STATUS = "Pending";

        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Total { get; set; }
         public OrderStatus Status {get; set; }
        public string Notes {get; set; }
        public Address ShippingAddress { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
