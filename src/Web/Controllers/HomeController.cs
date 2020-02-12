using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Web.Pages;
using Microsoft.Extensions.Localization;

namespace Web.Controllers

{
    
    [Route("[controller]/[action]")]
    public class DefaultController : Controller
    {
         [HttpGet]
        public IActionResult Index()
        {
        return this.View();
        }
    }
} 