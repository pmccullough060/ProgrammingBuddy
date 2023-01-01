using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using prog_buddy_api.Models.DTO;
using System.Collections.Generic;

namespace prog_buddy_api.Services
{
    public class AuthenticationService
    {
        public static string IssueJwtToken(UserDTO user)
        {
            Dictionary<string, object> claims = new Dictionary<string, object>
            {
                {
                    "username",
                    user.UserName
                },
                {
                    "role",
                    user.Role
                }
            };

            var encoder = new JwtEncoder(new HMACSHA256Algorithm(), new JsonNetSerializer(), new JwtBase64UrlEncoder());

            return encoder.Encode(claims, "12345");
        }

        public static bool ValidateJwtToken(HttpRequest request)
        {
            // Check if we have a header.
            if (!request.Headers.ContainsKey("Authorization"))
            {
                return false;
            }

            string authorizationHeader = request.Headers["Authorization"];
            
            // Check if the value is empty.
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return false;
            }

            // Check if we can decode the header.
            IDictionary<string, object> claims = null;

            try
            {
                if (authorizationHeader.StartsWith("Bearer"))
                {
                    authorizationHeader = authorizationHeader.Substring(7);
                }
                // Validate the token and decode the claims.
                claims = new JwtBuilder().WithAlgorithm(new HMACSHA256Algorithm()).WithSecret("12345").MustVerifySignature().Decode<IDictionary<string, object>>(authorizationHeader);
            }
            catch
            {
                return false;
            }

            // Check if we have user claim.
            if (!claims.ContainsKey("username"))
            {
                return false;
            }

            return true;


        }
    }
}
