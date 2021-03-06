﻿
using System.Security.Claims;
using DiversifyCL.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace DiversifyCL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /**
         * Get Current Logged In User
         */
        public string GetCurrentLoggedInUser()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
