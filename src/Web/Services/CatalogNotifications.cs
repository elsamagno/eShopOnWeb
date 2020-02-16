using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    
    public class CatalogNotifications
    {
        private readonly SignInUser<ApplicationUser> _signInUser;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<CatalogNotifications> _logger;
        private IServiceProvider _serviceProvider;
        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IEmailSender emailSender){
           
            _itemRepository = itemRepository;
            _emailSender = emailSender;
            _signInUser = signInUser;
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger<CatalogNotifications>();

            //   var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            // TemplateSubject = configuration.GetValue<string>("SendGrid:templateSubject");
            // TemplateMessage = configuration.GetValue<string>("SendGrid:TemplateMessage");
       
        new Action(async () => {await loadTemplatesDataAsync();}).Invoke();
        }
        private Template templateSubject;
        private Template templateMessage;
        private string greeting = "Hello welcome" ;


        private async Task loadTemplatesDataAsync(){
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();

            // for email subject
            var pathTemplateSubject    = configuration.GetValue<string>("SendGrid:templateSubject");
            var templateContentSubject = await File.ReadAllTextAsync(pathTemplateSubject); 
            templateSubject            = Template.Parse(templateContentSubject);

            // for email message
            var pathTemplateMessage   = configuration.GetValue<string>("SendGrid:TemplateMessage");
            var templateContentMessage = await File.ReadAllTextAsync(pathTemplateMessage); 
            templateMessage           = Template.Parse(templateContentMessage;

            // greeting
            int hour = DateTimeOffset.Now.Hour;
            { greeting = "Hello welcome";}
            
        }



//Catalog Item notification

        public async Task CatalogItemsNotificationAsync(int itemId, decimal priceNew){
             _logger.LogInformation("Inicialize catalog items notification...");

            var catalogItem = await _itemRepository.GetByIdAsync(itemId);
            var users = _signInUser.UserUser.Users.ToList();

        }
       // / Catalog Item notification client

        public async Task CatalogItemNotification(string email, CatalogItem catalogItem, decimal priceNew){
            var anyChanges = false;
            if(priceNew != catalogItem.Price){ anyChanges = true;}

            if(anyChanges){
              
                MemberRenamerDelegate memberRenamer = member => member.Name;

        
                var message = templateMessage.Render(
                        new { Greeting =  greeting, PriceChanged = priceNew != catalogItem.Price}
                    );

            await _emailSender.SendEmailAsync(email, subject, message);
        }

    }

    
}