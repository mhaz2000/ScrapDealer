﻿using ScrapDealer.Shared.ModuleExtensions;
using System.IdentityModel.Tokens.Jwt;

namespace ScrapDealer.Shared.Helpers
{
    public static class ClaimHelper
    {
        public static T GetClaim<T>(string accessToken, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(accessToken) as JwtSecurityToken;

            var claim = token!.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            var result = claim!.ToType<T>();
            return result;
        }
    }
}
