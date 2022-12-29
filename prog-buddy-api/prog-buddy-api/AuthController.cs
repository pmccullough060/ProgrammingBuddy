using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using prog_buddy_api.Models.Request;
using prog_buddy_api.Models.Response;
using prog_buddy_api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prog_buddy_api.Models.DTO;
using prog_buddy_api.Enums;
using Data;
using User = prog_buddy_api.Models.DTO.User;

namespace prog_buddy_api
{
    public class AuthController
    {
        private readonly Context _context;

        public AuthController(Context context)
        {
            _context = context;
        }

        [FunctionName("Login")]
        public async Task<LoginResponseModel> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = "Login")] HttpRequest req,
        ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var compileRequestModel = JsonConvert.DeserializeObject<LoginRequestModel>(requestBody);

            // DB call to validate the user etc...
            var user = new User
            {
                UserName = compileRequestModel.Email,
                Role = UserRoles.User,
            };

            var token = AuthenticationService.IssueJwtToken(user);

            return new LoginResponseModel
            {
                Token = token,
                Expiry = DateTime.Now,
            };
        }
    }
}
