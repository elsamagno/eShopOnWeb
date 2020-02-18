using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class BaseApiController : ControllerBase
    { }
}
