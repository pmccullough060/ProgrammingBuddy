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
using Microsoft.AspNetCore.Mvc;

namespace prog_buddy_api.Controllers
{
    public class ProfileController
    {
        [FunctionName("GetUserProjects")]
        public static async Task<IActionResult> GetUserProjects(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetUserProjects")] HttpRequest req,
            ILogger log)
        {
            // Check the JWT token:
            var authenticated = AuthenticationService.ValidateJwtToken(req);

            if (!authenticated)
            {
                // do something etc..
            }

            // Get the email from the json

            // retrieve the porject.

            return new OkObjectResult("TODO: return models of the users projects");
        }
    }
}
