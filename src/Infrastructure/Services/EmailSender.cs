using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SendGrid;
using System.Net;

using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSenderSendGrid> _logger;
        private IServiceProvider _serviceProvider;
         public EmailSenderSendGrid(ILoggerFactory loggerFactory, IServiceProvider serviceProvider){
            _logger = loggerFactory.CreateLogger<EmailSenderSendGrid>();
            _serviceProvider = serviceProvider;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {   
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            var apiKeyString = configuration.GetValue<string>("SendGrid:apiKey");

            if(string.IsNullOrEmpty(apiKeyString)){
                throw new Exception("SendGrid apiKey is null or empty");
            }

            var from = configuration.GetValue<string>("SendGrid:from");
            var fromName = configuration.GetValue<string>("SendGrid:fromName");

            if(string.IsNullOrEmpty(from)){
                throw new Exception("Email From is null or empty");
            }

            var emailAddressFrom = new EmailAddress(from, fromName);
            var emailAddressTo   = new EmailAddress(email);
            string plainTextContent = "";
            SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(emailAddressFrom, emailAddressTo, subject, plainTextContent, message);

            var client = new SendGridClient(apiKeyString);
            var response = await client.SendEmailAsync(sendGridMessage);
            if(response.StatusCode == HttpStatusCode.Confirmation){
                  _logger.LogInformation($"Send e-mail to {email} is confirmed.");
            }

              else{
                _logger.LogError($"Send e-mail to {email} is not confirm. {response.ToString()}");
                throw new Exception(response.ToString());
            // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
        }
    }
}