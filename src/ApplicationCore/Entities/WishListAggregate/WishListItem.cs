namespace Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate
{
    public class WishListItem : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
        public int WishListId { get; private set; }
    }
}