using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class WishListService : IWishListService
    {
        private readonly IAsyncRepository<WishList> _wishlistRepository;
        private readonly IAppLogger<WishListService> _logger;

        public WishListService(IAsyncRepository<WishList> wishlistRepository,
            IAppLogger<WishListService> logger)
        {
            _wishlistRepository = wishlistRepository;
            _logger = logger;
        }

        public async Task AddItemToWishList(int wishlistId, int catalogItemId)
        {
            _logger.LogInformation("AddItemTiWishList called");
            var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);

            wishlist.AddItem(catalogItemId);

            await _wishlistRepository.UpdateAsync(wishlist);
        }

        public async Task DeleteItemFromWishList(int wishlistId, int catalogItemId)
        {
            _logger.LogInformation("DeleteItemTiWishList called");
            var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
            wishlist.DeleteItem(catalogItemId);
            await _wishlistRepository.UpdateAsync(wishlist);
        }

    }
}