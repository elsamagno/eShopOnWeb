﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

using System.Threading.Tasks;


namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class EditCatalogItemModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;

          public EditCatalogItemModel(ICatalogItemViewModelService catalogItemViewModelService, CatalogNotifications catalogNotifications)
       
        {
            _catalogItemViewModelService = catalogItemViewModelService;
            _catalogNotifications = catalogNotifications;
        }

        [BindProperty]
        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public Task OnGet(CatalogItemViewModel catalogModel)
        {
            CatalogModel = catalogModel;
            return Task.CompletedTask;
        }

          [HttpPost]
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            if (submitButton == "Save")
            {
                return await Edit();
            }
            else if (submitButton == "Delete")
            {
                return await Delete();
            }
            return RedirectToPage("/Admin/Index");
        }

        public async Task<IActionResult> Edit()
        {
            if (ModelState.IsValid)
            {
                await _catalogItemViewModelService.UpdateCatalogItem(CatalogModel);
                await _catalogNotifications.CatalogItemsNotifyAsync(CatalogModel.Id, CatalogModel.Price);
            }

            return RedirectToPage("/Admin/Index");
        
          }
        public async Task<IActionResult> Delete()
        {
            if (ModelState.IsValid)
            {
                await _catalogItemViewModelService.DeleteCatalogItem(CatalogModel);
            }

            return RedirectToPage("/Admin/Index");
    }
}
