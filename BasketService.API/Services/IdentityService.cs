using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BasketService.API.Services
{
    public class IdentityService:IIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        

        public string GetUserIdentity()
        {
            return httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}