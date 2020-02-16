using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        private IServiceProvider _serviceProvider;
        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IEmailSender emailSender){
           
            _itemRepository = itemRepository;
            _emailSender = emailSender;
            _signInUser = signInUser;
            _serviceProvider = serviceProvider;

              var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            TemplateSubject = configuration.GetValue<string>("SendGrid:templateSubject");
            TemplateBody = configuration.GetValue<string>("SendGrid:TemplateMessage");
        }
        private string TemplateSubject;
        private string TemplateMessage;

//Catalog Item notification

        public async Task CatalogItemsNotificationAsync(int itemId, decimal priceNew){
            var catalogItem = await _itemRepository.GetByIdAsync(itemId);
            var users = _signInUser.UserUser.Users.ToList();

        }
       // / Catalog Item notification client

        public async Task CatalogItemNotification(string email, CatalogItem catalogItem, decimal priceNew){
            var anyChanges = false;
            if(priceNew != catalogItem.Price){ anyChanges = true;}

            if(anyChanges){
                // greeting
                int hour = DateTimeOffset.Now.Hour;
                var greeting = "Good morning";
                if(19 < hour){ greeting = "Good evening";}
                else if(12 < hour){ greeting = "Good afternoon";}

                MemberRenamerDelegate memberRenamer = member => member.Name;

                //email subject
                var templateContentSubject = await File.ReadAllTextAsync(TemplateSubject);
                // Parse a scriban template
                var templateSubject = Template.Parse(templateContentSubject);
                var subject = templateSubject.Render(new { CatalogItem = catalogItem }, memberRenamer);

                //email message
                var templateContentBody = await File.ReadAllTextAsync(TemplateMessage);
                // Parse a scriban template
                var templateMessage = Template.Parse(templateContentMessage);
                var message = templateMessage.Render(
                        new { Greeting =  greeting, PriceChanged = priceNew != catalogItem.Price}
                    );

            await _emailSender.SendEmailAsync(email, subject, message);
        }

    }

    
}