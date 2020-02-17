using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities;
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
        private readonly SignInManager<ApplicationUser> _signInUser;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CatalogNotifications> _logger;

        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IEmailSender emailSender, SignInManager<ApplicationUser> signInUser, IConfiguration configuration, ILoggerFactory loggerFactory){
           
            _itemRepository = itemRepository;
            _emailSender = emailSender;
            _signInUser = signInUser;
            _configuration = configuration;
            _logger = loggerFactory.CreateLogger<CatalogNotifications>();
       
        new Action(async () => {await loadTemplatesDataAsync();}).Invoke();
        }
        private Template templateSubject;
        private Template templateMessage;
        private string greeting = "Hello welcome" ;


        private async Task loadTemplatesDataAsync(){

            // for email subject
            var pathTemplateSubject    = _configuration.GetValue<string>("SendGrid:templateSubject");
            var templateContentSubject = await File.ReadAllTextAsync(pathTemplateSubject); 
            templateSubject = Template.Parse(templateContentSubject);

            // for email message
            var pathTemplateMessage   = _configuration.GetValue<string>("SendGrid:TemplateMessage");
            var templateContentMessage = await File.ReadAllTextAsync(pathTemplateMessage); 
            templateMessage = Template.Parse(templateContentMessage);

            // greeting
            int hour = DateTimeOffset.Now.Hour;
            { greeting = "Hello welcome";}
            
        }



//Catalog Item notification

        public async Task CatalogItemsNotificationAsync(int itemId, decimal priceNew){
             _logger.LogInformation("Inicialize catalog items notification...");

            var catalogItem = await _itemRepository.GetByIdAsync(itemId);
            var users = _signInUser.UserManager.Users.ToList();

        }
       // / Catalog Item notification client

        public async Task CatalogItemNotification(string email, CatalogItem catalogItem, decimal priceNew){
            var anyChanges = false;
            if(priceNew != catalogItem.Price){ anyChanges = true;}

            if(anyChanges){
              
                MemberRenamerDelegate memberRenamer = member => member.Name;

                var subject = templateSubject.Render(new { CatalogItem = catalogItem }, memberRenamer);
        
                var message = templateMessage.Render(
                        new { Greeting =  greeting, PriceChanged = priceNew != catalogItem.Price}
                    );

            await _emailSender.SendEmailAsync(email, subject, message);
            }
        }

    }

}