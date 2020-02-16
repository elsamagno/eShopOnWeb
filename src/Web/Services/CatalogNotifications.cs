using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    
    public class CatalogNotifications
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IEmailSender _emailSender;
        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IEmailSender emailSender){
            _itemRepository = itemRepository;
            _emailSender = emailSender;
        }

       
        public async Task CatalogItemsNotifyAsync(int itemId, decimal priceNew){
            var catalogItem = await _itemRepository.GetByIdAsync(itemId);

         
        }
                
      
        public async Task CatalogItemNotify(string email, CatalogItem catalogItem, decimal priceNew){
            var subject = $"EShopOnWeb - {catalogItem.Name}";
            var message = "";

            await _emailSender.SendEmailAsync(email, subject, message);
        }

    }

    
}